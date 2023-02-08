Namespace Helpers
    Public Class ConvertNumberToHex
        Public Shared Function Convert(ByVal number As Integer) As String
            Return number.ToString("X")
        End Function
    End Class
End Namespace
