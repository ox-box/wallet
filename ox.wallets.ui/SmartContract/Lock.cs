//using OX.SmartContract.Framework.Services;
//using OX.SmartContract.Framework;
//using System;
//using System.Numerics;
//using OX.Ledger;

//namespace OX.SmartContract
//{
//    /// <summary>
//    /// Contract Script Hash:0x41a48aa8f3982151136eeeabbfa97ec9b3f56b5a
//    /// </summary>
//    public class Lock : OX.SmartContract.Framework.SmartContract
//    {
//        public static bool Main(bool isTimeLock, uint lockExpiration, int Flag, byte[] pubkey, byte[] signature)
//        {
//            if (isTimeLock)
//            {
//                Header header = Blockchain.GetHeader(Blockchain.GetHeight());
//                if (lockExpiration > header.Timestamp) return false;
//            }
//            else
//            {
//                if (lockExpiration > Blockchain.GetHeight()) return false;
//            }
//            return VerifySignature(signature, pubkey) || Runtime.CheckWitness(pubkey);
//        }
//    }
//}
