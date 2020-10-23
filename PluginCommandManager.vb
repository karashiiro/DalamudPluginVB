Imports System.Reflection
Imports Dalamud.Game.Command
Imports Dalamud.Plugin
Imports DalamudPluginProjectTemplateVB.Attributes

Public Class PluginCommandManager(Of THost)
    Implements IDisposable

    Private ReadOnly pluginInterface As DalamudPluginInterface
    Private ReadOnly pluginCommands As (String, CommandInfo)()
    Private ReadOnly host As THost

    Sub New(host As THost, pluginInterface As DalamudPluginInterface)
        Me.host = host
        Me.pluginInterface = pluginInterface

        Dim methods As IEnumerable(Of MethodInfo) = host.GetType().GetMethods(BindingFlags.Public Or BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.Instance)
        Me.pluginCommands = methods.Where(Function(method)
                                              Return Not method.GetCustomAttribute(Of CommandAttribute)() Is Nothing
                                          End Function).SelectMany(Function(method)
                                                                       Return GetCommandInfoTuple(method)
                                                                   End Function).ToArray()

        AddCommandHandlers()
    End Sub

    Private Sub AddCommandHandlers()
        For i = 0 To Me.pluginCommands.Length - 1
            Dim cTuple = Me.pluginCommands(i)
            Me.pluginInterface.CommandManager.AddHandler(cTuple.Item1, cTuple.Item2)
        Next
    End Sub

    Private Sub RemoveCommandHandlers()
        For i = 0 To Me.pluginCommands.Length - 1
            Dim cTuple = Me.pluginCommands(i)
            Me.pluginInterface.CommandManager.RemoveHandler(cTuple.Item1)
        Next
    End Sub

    Private Function GetCommandInfoTuple(method As MethodInfo) As IEnumerable(Of (String, CommandInfo))
        Dim handlerDelegate = [Delegate].CreateDelegate(GetType(CommandInfo.HandlerDelegate), Me.host, method)

        Dim command = handlerDelegate.Method.GetCustomAttribute(Of CommandAttribute)()
        Dim aliases = handlerDelegate.Method.GetCustomAttribute(Of AliasesAttribute)()
        Dim helpMessage = handlerDelegate.Method.GetCustomAttribute(Of HelpMessageAttribute)()
        Dim doNotShowInHelp = handlerDelegate.Method.GetCustomAttribute(Of DoNotShowInHelpAttribute)()

        Dim cInfo = New CommandInfo(handlerDelegate) With {
            .HelpMessage = helpMessage?.HelpMessage,
            .ShowInHelp = Not doNotShowInHelp Is Nothing
        }
        If cInfo.HelpMessage Is Nothing Then cInfo.HelpMessage = ""

        ' Create list of tuples that will be filled with one tuple per alias, in addition to the base command tuple.
        Dim commandInfoTuples = New List(Of (String, CommandInfo)) From {(command.Command, cInfo)}
        If Not aliases Is Nothing Then
            For i = 0 To aliases.Aliases.Length
                commandInfoTuples.Add((aliases.Aliases(i), cInfo))
            Next
        End If

        Return commandInfoTuples
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        RemoveCommandHandlers()
    End Sub
End Class