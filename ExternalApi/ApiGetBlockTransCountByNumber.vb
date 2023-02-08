
Imports System.Configuration
Imports EtherscanInterview.Helpers
Imports EtherscanInterview.Models
Imports Newtonsoft.Json

Namespace ExternalApi
    Friend Class ApiGetBlockTransCountByNumber
        Implements IApiGetBlockTransCountByNumber

        Public _endpointFormat As String = ConfigurationManager.AppSettings("ApiGetBlockTransCountByNumFormat").ToString()

        Public Async Function GetAsync(ByVal hexValue As String) As Task(Of GetBlockTransactionCountResponseModel) Implements IApiGetBlockTransCountByNumber.GetAsync
            Dim tokenApi = ConfigurationManager.AppSettings("ApiGetTokenKey").ToString()
            Dim apiGetBlockEndpoint = String.Format(_endpointFormat, hexValue, tokenApi)
            Dim dataGetBlockTransResponse = Await ApiUtils.MakeGetRequestAsync(apiGetBlockEndpoint)

            If String.IsNullOrEmpty(dataGetBlockTransResponse) Then
                Return Nothing
            End If

            Try
                Return JsonConvert.DeserializeObject(Of GetBlockTransactionCountResponseModel)(dataGetBlockTransResponse)
            Catch
                Return Nothing
            End Try
        End Function
    End Class
End Namespace


