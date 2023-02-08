Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace Models
    Public Class BlockInformationModel
        Public Property Hash As String
        Public Property ParentHash As String
        Public Property Miner As String
        Public Property BlockReward As String
        Public Property GasLimit As String
        Public Property GasUsed As String
        Public Property Transactions As IList(Of TransactionInformationModel)
    End Class
End Namespace
