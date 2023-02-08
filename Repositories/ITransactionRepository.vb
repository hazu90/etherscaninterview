Imports System.Collections.Generic
Imports EtherscanInterview.Entities

Namespace Repositories
    Public Interface ITransactionRepository
        Function BulkInsert(ByVal listItem As List(Of Transactions)) As Boolean
    End Interface
End Namespace
