Imports Dalamud.Game.ClientState
Imports Dalamud.Game.Command
Imports Dalamud.Game.Gui
Imports Dalamud.IoC
Imports Dalamud.Logging
Imports Dalamud.Plugin
Imports DalamudPluginProjectTemplateVB.Attributes

Public Class Plugin
    Implements IDalamudPlugin

    <PluginService>
    <RequiredVersion("1.0")>
    Private Property PluginInterface As DalamudPluginInterface

    <PluginService>
    <RequiredVersion("1.0")>
    Private Property Commands As CommandManager

    <PluginService>
    <RequiredVersion("1.0")>
    Private Property Chat As ChatGui

    <PluginService>
    <RequiredVersion("1.0")>
    Private Property ClientState As ClientState

    Private ReadOnly commandManager As PluginCommandManager(Of Plugin)
    Private ReadOnly config As Configuration
    Private ReadOnly ui As PluginUI

    Public ReadOnly Property Name As String Implements IDalamudPlugin.Name
        Get
            Return "Your Plugin's Display Name"
        End Get
    End Property

    Public Sub New()
        config = PluginInterface.GetPluginConfig()
        If config Is Nothing Then
            config = New Configuration()
        End If
        config.Initialize(PluginInterface)

        ui = New PluginUI()
        AddHandler PluginInterface.UiBuilder.Draw, AddressOf ui.Draw

        commandManager = New PluginCommandManager(Of Plugin)(Me, Commands)
    End Sub

    <Command("/example1")>
    <HelpMessage("Example help message.")>
    Public Sub ExampleCommand1(command As String, args As String)
        Dim world = ClientState.LocalPlayer.CurrentWorld.GameData
        Chat.Print($"Hello {world.Name}!")
        PluginLog.Log("Message sent successfully.")
    End Sub

#Region "IDisposable Support"
    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not disposing Then Return

        commandManager.Dispose()

        PluginInterface.SavePluginConfig(config)

        RemoveHandler PluginInterface.UiBuilder.Draw, AddressOf ui.Draw
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code. Put cleanup code in 'Dispose(disposing As Boolean)' method
        Dispose(disposing:=True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region
End Class
