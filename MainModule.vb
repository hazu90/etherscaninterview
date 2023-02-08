Imports Autofac
Imports EtherscanInterview.Configs

Module MainModule

    Sub Main()
        Execute().GetAwaiter().GetResult()
    End Sub

    Async Function Execute() As Task
        Dim builder As IContainer = IoC.Config()
        Await (builder.Resolve(Of Processer)()).Run(builder)
    End Function

End Module
