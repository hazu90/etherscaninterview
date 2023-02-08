Imports System.Configuration
Imports EtherscanInterview.Helpers
Imports EtherscanInterview.Models
Imports Newtonsoft.Json

Namespace ExternalApi
    Public Class ApiGetBlockByNumber
        Implements IApiGetBlockByNumber

        Public _endpointFormat As String = ConfigurationManager.AppSettings("ApiGetBlockByNumberFormat").ToString()

        Public Sub New()
        End Sub

        Public Async Function GetAsync(ByVal hexValue As String) As Task(Of GetBlockInformationResponseModel) Implements IApiGetBlockByNumber.GetAsync
            Dim tokenApi = ConfigurationManager.AppSettings("ApiGetTokenKey").ToString()
            Dim apiGetBlockEndpoint = String.Format(_endpointFormat, hexValue, tokenApi)
            Dim dataGetBlockResponse = Await ApiUtils.MakeGetRequestAsync(apiGetBlockEndpoint)

            If String.IsNullOrEmpty(dataGetBlockResponse) Then
                Return Nothing
            End If

            Try
                Return JsonConvert.DeserializeObject(Of GetBlockInformationResponseModel)(dataGetBlockResponse)
            Catch
                Return Nothing
            End Try
        End Function
    End Class
End Namespace
