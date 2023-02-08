Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace Models
    Public Class TransactionInformationModel
        Public Property TransactionId As Integer
        Public Property BlockId As Integer
        Public Property Hash As String
        Public Property From As String
        Public Property [To] As String
        Public Property Value As String
        Public Property Gas As String
        Public Property GasPrice As String
        Public Property TransactionIndex As String
    End Class
End Namespace
