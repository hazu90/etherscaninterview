Imports EtherscanInterview.Entities

Namespace Repositories
    Public Class BlockRepository
        Implements IBlockRepository

        Private _dbContext As EtherScanDBContext

        Public Sub New(ByVal dbContext As EtherScanDBContext)
            _dbContext = dbContext
        End Sub

        Public Function Add(ByVal entity As Blocks) As Blocks Implements IBlockRepository.Add
            Return _dbContext.Blocks.Add(entity)
        End Function
    End Class
End Namespace
