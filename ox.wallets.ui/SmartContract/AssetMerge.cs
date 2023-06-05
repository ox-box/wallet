//using OX.SmartContract.Framework.Services;
//using OX.SmartContract.Framework;
//using System;
//namespace OX.SmartContract
//{
//    /// <summary>
//    /// Contract Script Hash:0x1bb1483c8c1175b37062d7d586bd4b67abb255e2
//    /// </summary>
//    public class AssetMerge : OX.SmartContract.Framework.SmartContract
//    {
//        public static bool Main(int SideType, byte[] Data, int Flag, byte[] pubkey, byte[] signature)
//        {
//            return VerifySignature(signature, pubkey) || Runtime.CheckWitness(pubkey);
//        }
//    }
//}
