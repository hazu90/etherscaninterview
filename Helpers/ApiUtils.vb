Imports System.IO
Imports System.Net
Imports System.Threading.Tasks

Namespace Helpers
    Public Class ApiUtils
        Public Shared Async Function MakeGetRequestAsync(ByVal url As String) As Task(Of String)
            Dim request As HttpWebRequest = CType(WebRequest.Create(url), HttpWebRequest)
            request.Method = "GET"
            request.ContentType = "application/json"
            Dim webResponse = Await request.GetResponseAsync()

            Using webStream As Stream = webResponse.GetResponseStream()
                Using responseReader As StreamReader = New StreamReader(webStream)
                    Return Await responseReader.ReadToEndAsync()
                End Using
            End Using
        End Function
    End Class
End Namespace
