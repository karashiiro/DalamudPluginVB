Namespace Attributes
    <AttributeUsage(AttributeTargets.Method)>
    Public Class AliasesAttribute
        Inherits Attribute

        Private _aliases As String()
        Public Property Aliases As String()
            Get
                Return _aliases
            End Get
            Set(value As String())
                _aliases = value
            End Set
        End Property

        Sub New(ParamArray _aliases As String())
            Aliases = _aliases
        End Sub
    End Class
End Namespace