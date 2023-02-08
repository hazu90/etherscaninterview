Imports Dapper
Imports EtherscanInterview.Entities
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks

Namespace Repositories
    Public Class TransactionRepository
        Implements ITransactionRepository

        Private _dbContext As EtherScanDBContext

        Public Sub New(ByVal dbContext As EtherScanDBContext)
            _dbContext = dbContext
        End Sub

        Public Function BulkInsert(ByVal listItem As List(Of Transactions)) As Boolean Implements ITransactionRepository.BulkInsert
            For Each item As Transactions In listItem
                _dbContext.Transactions.Add(item)
            Next

            Return True
        End Function
    End Class
End Namespace
