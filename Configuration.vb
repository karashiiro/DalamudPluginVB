Imports Dalamud.Configuration
Imports Dalamud.Plugin
Imports Newtonsoft.Json

Public Class Configuration
    Implements IPluginConfiguration

    Private _version As Integer
    Public Property Version As Integer Implements IPluginConfiguration.Version
        Get
            Return _version
        End Get
        Set(value As Integer)
            _version = value
        End Set
    End Property

    <JsonIgnore> Private pluginInterface As DalamudPluginInterface

    Sub Initialize(pluginInterface As DalamudPluginInterface)
        Me.pluginInterface = pluginInterface
    End Sub

    Sub Save()
        Me.pluginInterface.SavePluginConfig(Me)
    End Sub
End Class