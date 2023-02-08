Imports EtherscanInterview.Repositories
Imports System
Imports System.Threading.Tasks

Namespace UnitOfWorks
    Public Interface IUnitOfWork
        Inherits IDisposable

        ReadOnly Property BlockRepository As IBlockRepository
        ReadOnly Property TransactionRepository As ITransactionRepository
        Function SaveChangesAsync() As Task(Of Integer)
    End Interface
End Namespace
