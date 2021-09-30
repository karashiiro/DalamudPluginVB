Imports Dalamud.Configuration
Imports Dalamud.Plugin
Imports Newtonsoft.Json

Public Class Configuration
    Implements IPluginConfiguration

    Public Property Version As Integer Implements IPluginConfiguration.Version

    <JsonIgnore> Private pluginInterface As DalamudPluginInterface

    Sub Initialize(pi As DalamudPluginInterface)
        pluginInterface = pi
    End Sub

    Sub Save()
        pluginInterface.SavePluginConfig(Me)
    End Sub
End Class