Imports ImGuiNET

Public Class PluginUI
    Private _isVisible As Boolean
    Public Property IsVisible As Boolean
        Get
            Return _isVisible
        End Get
        Set(value As Boolean)
            _isVisible = value
        End Set
    End Property

    Sub Draw()
        If Not IsVisible Then Return
    End Sub
End Class