# DalamudPluginVB
An opinionated Visual Studio project template for Dalamud plugins, with attributes for more maintainable command setup and teardown.

## Hint paths
The project file includes hint paths for the dependencies, which are set up to not include much information about the developer's filesystem.
You may need to adjust these paths yourself, if they don't represent your development environment.

## Attributes
This template includes an attribute framework reminiscent of [Discord.Net](https://github.com/discord-net/Discord.Net).

```vbnet
<Command("/example1")>
<Aliases("/ex1", "/e1")>
<HelpMessage("Example help message.")>
Public Sub ExampleCommand1(command As String, args As String)
    Dim chat = Me.pluginInterface.Framework.Gui.Chat
    Dim world = Me.pluginInterface.ClientState.LocalPlayer.CurrentWorld.GameData
    chat.Print($"Hello {world.Name}!")
    PluginLog.Log("Message sent successfully.")
End Sub

<Command("/example2")>
<DoNotShowInHelp>
Public Sub ExampleCommand2(command As String, args As String)
    ' do nothing
End Sub
```

This automatically registers and unregisters the methods that they're attached to on initialization and dispose.

## GitHub Actions
Running the shell script `DownloadGithubActions.sh` will download some useful GitHub actions for you. You can also delete this file if you have no need for it.

### Current Actions
  * dotnet build/test
