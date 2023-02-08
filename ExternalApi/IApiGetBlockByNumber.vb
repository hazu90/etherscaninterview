Imports EtherscanInterview.Models
Namespace ExternalApi
    Public Interface IApiGetBlockByNumber
        Function GetAsync(ByVal hexValue As String) As Task(Of GetBlockInformationResponseModel)
    End Interface
End Namespace


