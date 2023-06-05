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
    }
}