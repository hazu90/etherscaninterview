Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Entities
    <Table("blocks")>
    Public Class Blocks
        <Key>
        Public Property BlockId As Integer
        Public Property BlockNumber As Integer
        Public Property Hash As String
        Public Property ParentHash As String
        Public Property Miner As String
        Public Property BlockReward As Decimal
        Public Property GasLimit As Decimal
        Public Property GasUsed As Decimal
        Private Property Transactions As ICollection(Of Transactions)
    End Class
End Namespace


