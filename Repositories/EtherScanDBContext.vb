Imports System.Configuration
Imports System.Data.Entity
Imports EtherscanInterview.Configs
Imports EtherscanInterview.Entities

Namespace Repositories
    <DbConfigurationType(GetType(MySqlEFConfiguration))>
    Public Class EtherScanDBContext
        Inherits DbContext

        Public Sub New()
            MyBase.New(MySqlEFConfiguration.GetMySqlConnection(ConfigurationManager.ConnectionStrings("EtherScanDBContext").ConnectionString), True)
        End Sub

        Public Property Blocks As DbSet(Of Blocks)
        Public Property Transactions As DbSet(Of Transactions)
    End Class
End Namespace
