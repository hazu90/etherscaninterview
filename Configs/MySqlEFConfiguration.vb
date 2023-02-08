Imports MySql.Data.EntityFramework
Imports MySql.Data.MySqlClient
Imports System.Data.Common
Imports System.Data.Entity

Namespace Configs
    Public Class MySqlEFConfiguration
        Inherits DbConfiguration

        Public Sub New()
            SetProviderServices(MySqlProviderInvariantName.ProviderName, New MySqlProviderServices())
        End Sub

        Public Shared Function GetMySqlConnection(ByVal connectionString As String) As DbConnection
            Dim connectionFactory = New MySqlConnectionFactory()
            Return connectionFactory.CreateConnection(connectionString)
        End Function
    End Class
End Namespace
