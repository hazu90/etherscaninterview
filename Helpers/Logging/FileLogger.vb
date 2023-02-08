Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.IO

Namespace Helpers.Logging
    Public Class FileLogger
        Inherits BaseLogger

        Public filePath As String = ConfigurationManager.AppSettings("LogFilePath").ToString()

        Public Overrides Sub Log(ByVal message As String)
            SyncLock lockObj

                Using streamWriter As StreamWriter = New StreamWriter(filePath, True)
                    streamWriter.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") & "   " & message)
                    streamWriter.Close()
                End Using
            End SyncLock
        End Sub
    End Class
End Namespace
