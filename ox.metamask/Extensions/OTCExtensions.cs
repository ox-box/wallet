using Nethereum.ABI;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.ABI.Model;
using Nethereum.JsonRpc.Client;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.RPC.Eth.Transactions;
using System;
using System.Numerics;
using System.Threading.Tasks;
using OX;
using OX.Wallets;
using System.Reflection.Metadata.Ecma335;
using Nethereum.Contracts.QueryHandlers.MultiCall;
using Nethereum.Web3;
using System.Collections.Generic;

namespace OX.MetaMask
{
    public class EthDepositTempResult
    {
        public bool OK { get; set; } = false;
        public string? EthTxId { get; set; }
        public string? stringToSign { get; set; }
        public string? signatureData { get; set; }
    }
    public static class OTCExtensions
    {
        //private async void Send()
        //{
        //    string infuraUrl = "https://ropsten.infura.io/v3/5c190aed92b74116a1ae2336fee217ae";
        //    var web3 = new Web3(infuraUrl);
        //    var contractAddress = "0xc59aF6Db2883674D0CBA40De88494E83b2b4DCD0";
        //    var funcName = "hello";
        //    var receipt = await _metaMask.SendTransactionAndWaitForReceipt(web3.Client, funcName, contractAddress, 0, new List<Parameter>().ToArray());
        //    _result = "Block number: " + receipt.BlockNumber.ToString();
        //    _showResultModal = true;
        //    StateHasChanged();
        //}
        public static async Task<string> SendUSDT(this IMetaMaskService metaMask, string toEthAddress, decimal amount)
        {
            var data = GetEncodedFunction(toEthAddress, amount);
            return await metaMask.SendTransaction("0xdac17f958d2ee523a2206206994597c13d831ec7", 0, data);
        }
        static string GetEncodedFunction(string toEthAddress, decimal amount)
        {
            FunctionABI function = new FunctionABI("transfer", false);
            var inputsParameters = new[] {
                    new Parameter("address", "to"),
                    new Parameter("uint256", "value")
                };
            function.InputParameters = inputsParameters;

            var functionCallEncoder = new FunctionCallEncoder();
            BigInteger weiValue = (BigInteger)(1000000 * amount);
            var data = functionCallEncoder.EncodeRequest(function.Sha3Signature, inputsParameters, new object[] {
               toEthAddress,
                weiValue
            }
            );
            return data;
        }
        public static async Task<string> SendUSDTFrom(this IMetaMaskService metaMask, string fromEthAddress, string toEthAddress, decimal amount)
        {
            var data = GetEncodedFunctionFrom(fromEthAddress, toEthAddress, amount);
            return await metaMask.SendTransaction("0xdac17f958d2ee523a2206206994597c13d831ec7", 0, data);
        }
        static string GetEncodedFunctionFrom(string fromEthAddress, string toEthAddress, decimal amount)
        {
            FunctionABI function = new FunctionABI("transferFrom", false);
            var inputsParameters = new[] {
                 new Parameter("address", "from"),
                    new Parameter("address", "to"),
                    new Parameter("uint256", "value")
                };
            function.InputParameters = inputsParameters;

            var functionCallEncoder = new FunctionCallEncoder();
            BigInteger weiValue = (BigInteger)(1000000 * amount);
            var data = functionCallEncoder.EncodeRequest(function.Sha3Signature, inputsParameters, new object[] {
               fromEthAddress,
                toEthAddress,
                weiValue
            }
            );
            return data;
        }
        public static async Task<EthDepositTempResult> TryDeposit(this IMetaMaskService metaMask, string fromEthAddress, string toEthPoolAddress, UInt160 oxAddress, decimal amount)
        {

            var ts = DateTime.Now.ToTimestamp();
            string stringToSign = $"{oxAddress.ToAddress()}--{ts}";
            var signatureData = await metaMask.PersonalSign(stringToSign);
            var signer = new Nethereum.Signer.EthereumMessageSigner();
            var ethaddress = signer.EncodeUTF8AndEcRecover(stringToSign, signatureData);
            if (ethaddress.ToLower() == fromEthAddress.ToLower())
            {
                var data = System.Text.Encoding.UTF8.GetBytes(stringToSign).ToHexString();
                BigInteger weiValue = (BigInteger)(1000000000000000000 * amount);
                var ethTxId = await metaMask.SendTransaction(toEthPoolAddress, weiValue, data);
                EthDepositTempResult result = new EthDepositTempResult
                {
                    OK = true,
                    EthTxId = ethTxId,
                    stringToSign = stringToSign,
                    signatureData = signatureData
                };
                return result;
            }
            return new EthDepositTempResult();
        }
        public static async Task<string> TrySimpleDeposit(this IMetaMaskService metaMask, string toEthPoolAddress, UInt160 oxAddress, decimal amount)
        {
            var ts = DateTime.Now.ToTimestamp();
            string stringToSign = $"{oxAddress.ToAddress()}--{ts}";
            var data = System.Text.Encoding.UTF8.GetBytes(stringToSign).ToHexString();
            BigInteger weiValue = (BigInteger)(1000000000000000000 * amount);
            return await metaMask.SendTransaction(toEthPoolAddress, weiValue, data);
        }
    }
}