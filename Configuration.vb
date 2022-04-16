Imports Dalamud.Configuration
Imports Dalamud.Plugin
Imports Newtonsoft.Json

Public Class Configuration
    Implements IPluginConfiguration

    Public Property Version As Integer Implements IPluginConfiguration.Version

    <JsonIgnore> Private ReadOnly pluginInterface As DalamudPluginInterface

    Public Sub New(pi As DalamudPluginInterface)
        pluginInterface = pi
    End Sub

    Public Sub Save()
        pluginInterface.SavePluginConfig(Me)
    End Sub
End Class