Imports Autofac
Imports EtherscanInterview.Entities
Imports EtherscanInterview.ExternalApi
Imports EtherscanInterview.Helpers
Imports EtherscanInterview.Helpers.Logging
Imports EtherscanInterview.Models
Imports EtherscanInterview.UnitOfWorks
Imports System
Imports System.Collections.Generic
Imports System.Configuration
Imports System.Diagnostics
Imports System.Numerics
Imports System.Threading.Tasks

Public Class Processer
    Private _apiGetBlockByNumber As IApiGetBlockByNumber
    Private _apiGetBlockTransCount As IApiGetBlockTransCountByNumber
    Private _apiGetTransByBlockNumAndIndex As IApiGetTransByBlockNumAndIndex
    Private _unitOfWork As IUnitOfWork
    Private _logFile As BaseLogger

    Public Sub New(ByVal unitOfWork As IUnitOfWork, ByVal logFile As BaseLogger, ByVal apiGetBlockByNumber As IApiGetBlockByNumber, ByVal apiGetBlockTransCount As IApiGetBlockTransCountByNumber, ByVal apiGetTransByBlockNumAndIndex As IApiGetTransByBlockNumAndIndex)
        _logFile = logFile
        _apiGetBlockByNumber = apiGetBlockByNumber
        _apiGetBlockTransCount = apiGetBlockTransCount
        _apiGetTransByBlockNumAndIndex = apiGetTransByBlockNumAndIndex
        _unitOfWork = unitOfWork
    End Sub

    Public Async Function Run(ByVal container As IContainer) As Task
        Dim beginBlock As Integer = Int32.Parse(ConfigurationManager.AppSettings("BlockBegin").ToString())
        Dim endBlock As Integer = Int32.Parse(ConfigurationManager.AppSettings("BlockEnd").ToString())
        Dim listTask As New List(Of Task)()
        Dim stopWatch As New Stopwatch()
        stopWatch.Start()
        Dim startTime As TimeSpan = stopWatch.Elapsed
        Dim endTime As TimeSpan
        Dim messageGet = $"Progam start in {startTime.ToString("mm\:ss\.ff")}"
        Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") & "   " & messageGet)
        Try

            For blockNumber As Integer = beginBlock To endBlock
                Dim blockNumberRunInTask = blockNumber
                Dim newTask = Task.Run(Function() FetchAndSaveDataAsync(blockNumberRunInTask))
                listTask.Add(newTask)

            Next

            Await Task.WhenAll(listTask)
            Dim startSaveDbTime As TimeSpan = stopWatch.Elapsed
            messageGet = $"Start save all data into database  Time: {startSaveDbTime.ToString("mm\:ss\.ff")}"
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") & "   " & messageGet)
            _logFile.Log(messageGet)
            Await _unitOfWork.SaveChangesAsync()
            Dim endSaveDbTime As TimeSpan = stopWatch.Elapsed
            messageGet = $"End save all data into database  Time: {endSaveDbTime.ToString("mm\:ss\.ff")} Total execution: {(startSaveDbTime - endSaveDbTime).TotalMilliseconds} ms"
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") & "   " & messageGet)
            _logFile.Log(messageGet)
            stopWatch.[Stop]()
            endTime = stopWatch.Elapsed
            messageGet = $"Progam end in {endTime.ToString("mm\:ss\.ff")} Total execution: {(endTime - startTime).TotalMilliseconds} ms"
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") & "   " & messageGet)
            _logFile.Log(messageGet)

        Catch ex As Exception
            Console.WriteLine(ex.Message)
        End Try
    End Function

    Public Async Function FetchAndSaveDataAsync(ByVal blockNumber As Integer) As Task
        Try
            Dim stopWatch As New Stopwatch()
            Dim startApi As TimeSpan = TimeSpan.Zero
            Dim endApi As TimeSpan = TimeSpan.Zero
            stopWatch.Start()
            Dim hexValue = ConvertNumberToHex.Convert(blockNumber)
            startApi = stopWatch.Elapsed
            Dim messageGet = $"eth_getBlockByNumber Start Block:{blockNumber} Time: {startApi.ToString("mm\:ss\.ff")}"
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") & "   " & messageGet)
            _logFile.Log(messageGet)
            Dim blockInformation = Await _apiGetBlockByNumber.GetAsync(hexValue)
            endApi = stopWatch.Elapsed
            messageGet = $"eth_getBlockByNumber End Block:{blockNumber} Time: {endApi.ToString("mm\:ss\.ff")} Total execution: {(endApi - startApi).TotalMilliseconds} ms"
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") & "   " & messageGet)
            _logFile.Log(messageGet)

            If blockInformation?.Result Is Nothing Then
                stopWatch.[Stop]()
                Return
            End If

            startApi = stopWatch.Elapsed
            messageGet = $"eth_getBlockTransactionCountByNumber Start Block {blockNumber} Time: {startApi.ToString("mm\:ss\.ff")}"
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") & "   " & messageGet)
            _logFile.Log(messageGet)
            Dim getBlockTransResponse = Await _apiGetBlockTransCount.GetAsync(hexValue)
            endApi = stopWatch.Elapsed
            messageGet = $"eth_getBlockTransactionCountByNumber End Block {blockNumber} Time: {endApi.ToString("mm\:ss\.ff")} Total execution: {(endApi - startApi).TotalMilliseconds} ms"
            Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") & "   " & messageGet)
            _logFile.Log(messageGet)

            If getBlockTransResponse?.Result Is Nothing Then
                stopWatch.[Stop]()
                Return
            End If

            Dim hexBlockTransCount = getBlockTransResponse.Result
            Dim num = Convert.ToInt32(hexBlockTransCount, 16)
            Dim newBlock = New Blocks() With {
                .BlockNumber = blockNumber,
                .GasLimit = Convert.ToInt32(blockInformation.Result.GasLimit, 16),
                .GasUsed = Convert.ToInt32(blockInformation.Result.GasUsed, 16),
                .BlockReward = Convert.ToInt32(blockInformation.Result.BlockReward, 16),
                .Hash = blockInformation.Result.Hash,
                .Miner = blockInformation.Result.Miner,
                .ParentHash = blockInformation.Result.ParentHash
            }
            Dim blockInfo = _unitOfWork.BlockRepository.Add(newBlock)
            Dim listTransaction = New List(Of Transactions)()

            For Each transItem As TransactionInformationModel In blockInformation.Result.Transactions
                startApi = stopWatch.Elapsed
                messageGet = $"eth_getTransactionByBlockNumberAndIndex Start Block: {blockNumber} TransactionIndex {transItem.TransactionIndex} Time: {stopWatch.Elapsed.ToString("mm\:ss\.ff")}"
                Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") & "   " & messageGet)
                _logFile.Log(messageGet)
                Dim getTransactionResponse = Await _apiGetTransByBlockNumAndIndex.GetAsync(hexValue, transItem.TransactionIndex)
                endApi = stopWatch.Elapsed
                messageGet = $"eth_getTransactionByBlockNumberAndIndex End Block: {blockNumber} TransactionIndex {transItem.TransactionIndex} Time: {stopWatch.Elapsed.ToString("mm\:ss\.ff")} Total execution {(endApi - startApi).TotalMilliseconds} ms"
                Console.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff") & "   " & messageGet)
                _logFile.Log(messageGet)

                If getTransactionResponse?.Result IsNot Nothing Then
                    Dim newTransactionItem = New Transactions() With {
                        .Blocks = blockInfo,
                        .From = getTransactionResponse.Result.From,
                        .[To] = getTransactionResponse.Result.[To],
                        .Gas = CDec(BigInteger.Parse(getTransactionResponse.Result.Gas.Replace("0x", ""), System.Globalization.NumberStyles.AllowHexSpecifier)),
                        .GasPrice = CDec(BigInteger.Parse(getTransactionResponse.Result.GasPrice.Replace("0x", ""), System.Globalization.NumberStyles.AllowHexSpecifier)),
                        .Hash = getTransactionResponse.Result.Hash,
                        .TransactionIndex = Convert.ToInt32(getTransactionResponse.Result.TransactionIndex, 16),
                        .Value = CDec(BigInteger.Parse(getTransactionResponse.Result.Value.Replace("0x", ""), System.Globalization.NumberStyles.AllowHexSpecifier))
                    }
                    listTransaction.Add(newTransactionItem)
                End If
            Next

            _unitOfWork.TransactionRepository.BulkInsert(listTransaction)
            stopWatch.[Stop]()
        Catch ex As Exception
            Console.WriteLine($"{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss.ff")}    Error bock {blockNumber} : {ex.Message}")
            _logFile.Log($"Error bock {blockNumber} : {ex.Message}")
        End Try
    End Function
End Class
