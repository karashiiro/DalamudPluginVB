Imports System.Numerics
Imports Dalamud.Interface.Windowing
Imports ImGuiNET

Public Class PluginWindow
    Inherits Window

    Public Sub New()
        MyBase.New("TemplateWindow")

        IsOpen = True
        Size = New Vector2(810, 520)
        SizeCondition = ImGuiCond.FirstUseEver
    End Sub

    Public Overrides Sub Draw()
        ImGui.Text("Hello, world!")
    End Sub
End Class