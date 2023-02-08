Imports System.Configuration
Imports EtherscanInterview.Helpers
Imports EtherscanInterview.Models
Imports Newtonsoft.Json

Namespace ExternalApi
    Public Class ApiGetTransByBlockNumAndIndex
        Implements IApiGetTransByBlockNumAndIndex

        Public _endpointFormat As String = ConfigurationManager.AppSettings("ApiGetTransByBlockNumAndIndexFormat").ToString()

        Public Async Function GetAsync(hexValue As String, transactionIndex As String) As Task(Of GetTransactionInformationResponseModel) Implements IApiGetTransByBlockNumAndIndex.GetAsync
            Dim tokenApi = ConfigurationManager.AppSettings("ApiGetTokenKey").ToString()
            Dim apiGetBlockEndpoint = String.Format(_endpointFormat, hexValue, transactionIndex, tokenApi)
            Dim dataGetBlockTransResponse = Await ApiUtils.MakeGetRequestAsync(apiGetBlockEndpoint)

            If String.IsNullOrEmpty(dataGetBlockTransResponse) Then
                Return Nothing
            End If

            Try
                Return JsonConvert.DeserializeObject(Of GetTransactionInformationResponseModel)(dataGetBlockTransResponse)
            Catch
                Return Nothing
            End Try
        End Function

    End Class

End Namespace

