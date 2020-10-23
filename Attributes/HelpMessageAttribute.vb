Namespace Attributes
    <AttributeUsage(AttributeTargets.Method)>
    Public Class HelpMessageAttribute
        Inherits Attribute

        Private _helpMessage As String
        Public Property HelpMessage As String
            Get
                Return _helpMessage
            End Get
            Set(value As String)
                _helpMessage = value
            End Set
        End Property

        Sub New(_helpMessage As String)
            HelpMessage = _helpMessage
        End Sub
    End Class
End Namespace