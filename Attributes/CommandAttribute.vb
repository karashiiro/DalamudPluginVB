Namespace Attributes
    <AttributeUsage(AttributeTargets.Method)>
    Public Class CommandAttribute
        Inherits Attribute

        Private _command As String
        Public Property Command As String
            Get
                Return _command
            End Get
            Set(value As String)
                _command = value
            End Set
        End Property

        Sub New(_command As String)
            Command = _command
        End Sub
    End Class
End Namespace