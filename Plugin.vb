Imports Dalamud.Plugin
Imports DalamudPluginProjectTemplateVB.Attributes

Public Class Plugin
    Implements IDalamudPlugin

    Private pluginInterface As DalamudPluginInterface
    Private commandManager As PluginCommandManager(Of Plugin)
    Private config As Configuration
    Private ui As PluginUI

    Public ReadOnly Property Name As String Implements IDalamudPlugin.Name
        Get
            Return "Your Plugin's Display Name"
        End Get
    End Property

    <Command("/example1")>
    <HelpMessage("Example help message.")>
    Public Sub ExampleCommand1(command As String, args As String)
        Dim chat = Me.pluginInterface.Framework.Gui.Chat
        Dim world = Me.pluginInterface.ClientState.LocalPlayer.CurrentWorld.GameData
        chat.Print($"Hello {world.Name}!")
        PluginLog.Log("Message sent successfully.")
    End Sub

    Public Sub Initialize(pluginInterface As DalamudPluginInterface) Implements IDalamudPlugin.Initialize
        Me.pluginInterface = pluginInterface

        Me.config = Me.pluginInterface.GetPluginConfig()
        If Me.config Is Nothing Then
            Me.config = New Configuration()
        End If
        Me.config.Initialize(Me.pluginInterface)

        Me.ui = New PluginUI()
        AddHandler Me.pluginInterface.UiBuilder.OnBuildUi, AddressOf Me.ui.Draw

        Me.commandManager = New PluginCommandManager(Of Plugin)(Me, Me.pluginInterface)
    End Sub

#Region "IDisposable Support"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposing Then Return

        Me.commandManager.Dispose()

        Me.pluginInterface.SavePluginConfig(Me.config)

        RemoveHandler Me.pluginInterface.UiBuilder.OnBuildUi, AddressOf Me.ui.Draw

        Me.pluginInterface.Dispose()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
