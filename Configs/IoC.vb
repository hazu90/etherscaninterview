Imports Autofac
Imports EtherscanInterview.ExternalApi
Imports EtherscanInterview.Helpers.Logging
Imports EtherscanInterview.Repositories
Imports EtherscanInterview.UnitOfWorks

Namespace Configs
    Public Class IoC
        Public Shared Function Config() As IContainer
            Dim builder As New ContainerBuilder()
            builder.RegisterType(Of EtherScanDBContext)().SingleInstance()
            builder.RegisterType(Of UnitOfWork)().As(Of IUnitOfWork)().SingleInstance()
            builder.RegisterType(Of Processer)()
            builder.RegisterType(Of TransactionRepository)().As(Of ITransactionRepository)().InstancePerLifetimeScope()
            builder.RegisterType(Of BlockRepository)().As(Of IBlockRepository)().InstancePerLifetimeScope()
            builder.RegisterType(Of FileLogger)().As(Of BaseLogger)().InstancePerLifetimeScope()
            builder.RegisterType(Of ApiGetBlockByNumber)().As(Of IApiGetBlockByNumber)().InstancePerLifetimeScope()
            builder.RegisterType(Of ApiGetBlockTransCountByNumber)().As(Of IApiGetBlockTransCountByNumber)().InstancePerLifetimeScope()
            builder.RegisterType(Of ApiGetTransByBlockNumAndIndex)().As(Of IApiGetTransByBlockNumAndIndex)().InstancePerLifetimeScope()
            Return builder.Build()
        End Function
    End Class
End Namespace
