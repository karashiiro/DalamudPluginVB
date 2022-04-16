Imports Dalamud.Game.ClientState
Imports Dalamud.Game.Command
Imports Dalamud.Game.Gui
Imports Dalamud.Interface.Windowing
Imports Dalamud.Logging
Imports Dalamud.Plugin
Imports DalamudPluginProjectTemplateVB.Attributes

Public Class Plugin
    Implements IDalamudPlugin

    Private ReadOnly pluginInterface As DalamudPluginInterface
    Private ReadOnly chat As ChatGui
    Private ReadOnly clientState As ClientState

    Private ReadOnly commandManager As PluginCommandManager(Of Plugin)
    Private ReadOnly config As Configuration
    Private ReadOnly windowSystem As WindowSystem

    Public ReadOnly Property Name As String Implements IDalamudPlugin.Name
        Get
            Return "Your Plugin's Display Name"
        End Get
    End Property

    Public Sub New(pi As DalamudPluginInterface, commands As CommandManager, chat As ChatGui, clientState As ClientState)
        Me.pluginInterface = pi
        Me.chat = chat
        Me.clientState = clientState

        Me.config = Me.pluginInterface.GetPluginConfig()
        If Me.config Is Nothing Then
            Me.config = Me.pluginInterface.Create(Of Configuration)()
        End If

        Me.windowSystem = New WindowSystem(GetType(Plugin).AssemblyQualifiedName)

        Dim window = Me.pluginInterface.Create(Of PluginWindow)()
        If window IsNot Nothing Then
            Me.windowSystem.AddWindow(window)
        End If

        AddHandler pluginInterface.UiBuilder.Draw, AddressOf Me.windowSystem.Draw

        Me.commandManager = New PluginCommandManager(Of Plugin)(Me, commands)
    End Sub

    <Command("/example1")>
    <HelpMessage("Example help message.")>
    Public Sub ExampleCommand1(command As String, args As String)
        Dim world = ClientState.LocalPlayer.CurrentWorld.GameData
        chat.Print($"Hello, {world.Name}!")
        PluginLog.Log("Message sent successfully.")
    End Sub

#Region "IDisposable Support"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposing Then Return

        Me.commandManager.Dispose()

        Me.pluginInterface.SavePluginConfig(config)

        Me.windowSystem.RemoveAllWindows()
        RemoveHandler pluginInterface.UiBuilder.Draw, AddressOf Me.windowSystem.Draw
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
