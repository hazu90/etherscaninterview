Imports EtherscanInterview.Models

Namespace ExternalApi
    Public Interface IApiGetTransByBlockNumAndIndex
        Function GetAsync(ByVal hexValue As String, ByVal transactionIndex As String) As Task(Of GetTransactionInformationResponseModel)
    End Interface
End Namespace


