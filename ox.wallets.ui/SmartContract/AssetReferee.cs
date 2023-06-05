//using OX.SmartContract.Framework.Services;
//using OX.SmartContract.Framework;
//using System;
//namespace OX.SmartContract
//{
//    public class AssetReferee : OX.SmartContract.Framework.SmartContract
//    {
//        public static bool Main(ushort txN, uint blockIndex, byte[] pubkey, byte[] signature)
//        {
//            if (txN < 1) return false;
//            if (blockIndex >= Blockchain.GetHeight()) return false;           
//            return VerifySignature(signature, pubkey);
//        }
//    }
//}
