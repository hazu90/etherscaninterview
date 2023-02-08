Imports EtherscanInterview.Models

Namespace ExternalApi
    Public Interface IApiGetBlockTransCountByNumber
        Function GetAsync(ByVal hexValue As String) As Task(Of GetBlockTransactionCountResponseModel)
    End Interface
End Namespace


