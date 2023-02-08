Namespace Helpers.Logging
    Public MustInherit Class BaseLogger
        Protected ReadOnly lockObj As Object = New Object()
        Public MustOverride Sub Log(ByVal message As String)
    End Class
End Namespace
