Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports EtherscanInterview.Entities

Namespace Repositories
    Public Interface IBlockRepository
        Function Add(ByVal entity As Blocks) As Blocks
    End Interface
End Namespace
