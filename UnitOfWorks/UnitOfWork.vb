Imports EtherscanInterview.Repositories
Imports System
Imports System.Threading.Tasks

Namespace UnitOfWorks
    Public Class UnitOfWork
        Implements IUnitOfWork

        Private _dbContext As EtherScanDBContext
        Private Property _blockRepository As IBlockRepository

        Public ReadOnly Property BlockRepository As IBlockRepository Implements IUnitOfWork.BlockRepository
            Get
                If _blockRepository IsNot Nothing Then
                    Return _blockRepository
                Else
                    _blockRepository = New BlockRepository(_dbContext)

                    Return _blockRepository
                End If
            End Get
        End Property

        Private _transactionRepository As ITransactionRepository

        Public ReadOnly Property TransactionRepository As ITransactionRepository Implements IUnitOfWork.TransactionRepository
            Get
                If _transactionRepository IsNot Nothing Then
                    Return _transactionRepository
                Else
                    _transactionRepository = New TransactionRepository(_dbContext)
                    Return _transactionRepository
                End If
            End Get
        End Property

        Public Sub New(ByVal dbContext As EtherScanDBContext)
            _dbContext = dbContext
        End Sub

        Public Async Function SaveChangesAsync() As Task(Of Integer) Implements IUnitOfWork.SaveChangesAsync
            Return Await _dbContext.SaveChangesAsync()
        End Function

        Public Sub Dispose() Implements IUnitOfWork.Dispose

            _dbContext.Dispose()
            GC.SuppressFinalize(Me)
        End Sub

        Private Class CSharpImpl
            <Obsolete("Please refactor calling code to use normal Visual Basic assignment")>
            Shared Function __Assign(Of T)(ByRef target As T, value As T) As T
                target = value
                Return value
            End Function
        End Class
    End Class
End Namespace
