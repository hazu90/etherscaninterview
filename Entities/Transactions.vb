Imports System.ComponentModel.DataAnnotations
Imports System.ComponentModel.DataAnnotations.Schema

Namespace Entities
    <Table("transactions")>
    Public Class Transactions
        <Key>
        Public Property TransactionId As Integer
        Public Property BlockId As Integer
        <ForeignKey(NameOf(BlockId))>
        Public Property Blocks As Blocks
        Public Property Hash As String
        Public Property From As String
        Public Property [To] As String
        Public Property Value As Decimal
        Public Property Gas As Decimal
        Public Property GasPrice As Decimal
        Public Property TransactionIndex As Integer
    End Class

End Namespace

