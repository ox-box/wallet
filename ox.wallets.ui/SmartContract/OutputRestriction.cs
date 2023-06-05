//using OX.SmartContract.Framework.Services;
//using OX.SmartContract.Framework;
//using System;
//using System.Numerics;
//using System.Linq;
//using System.Runtime.Serialization;
//using System.Collections.Generic;
//using System.Runtime.Intrinsics.X86;
//using System.Collections.Concurrent;
//using OX.Ledger;

//namespace OX.SmartContract
//{
//    /// <summary>
//    /// Contract Script Hash:0xe64586c07a90ec1a1b0c8fc22868cf3eff94560b
//    /// </summary>
//    public class OutputRestriction : OX.SmartContract.Framework.SmartContract
//    {
//        public static bool Main(byte[] sideScopes, bool mustFlag, byte[] targets, byte[] ownerPubKey, byte[] pubkey, byte[] signature)
//        {
//            Transaction tx = OX.SmartContract.Framework.Services.System.ExecutionEngine.ScriptContainer as Transaction;
//            //try
//            //{
//            string sh = "";
//            foreach (var refer in tx.GetReferences())
//            {
//                var r = refer.ScriptHash.AsString();
//                if (sh == "")
//                {
//                    sh = r;
//                }
//                else if (sh != r)
//                {
//                    return false;
//                }
//            }

//            var c = targets.Length / 20;
//            var m = sideScopes.Length / 20;
//            foreach (var output in tx.GetOutputs())
//            {
//                bool ok = false;
//                var s = output.ScriptHash.AsString();
//                if (c > 0)
//                {
//                    for (int i = 0; i < c; i++)
//                    {
//                        if (s == targets.Range(i * 20, 20).AsString())
//                        {
//                            ok = true;
//                            break;
//                        }
//                    }
//                }

//                for (int p = 0; p < m; p++)
//                {
//                    if (s == sideScopes.Range(p * 20, 20).AsString())
//                    {
//                        ok = true;
//                        break;
//                    }
//                    else if (Blockchain.IsInSide(output.ScriptHash, sideScopes.Range(p * 20, 20), "0x1bb1483c8c1175b37062d7d586bd4b67abb255e2"))
//                    {
//                        ok = true;
//                        break;
//                    }
//                }
//                if (s == sh) ok = true;
//                if (!ok) return false;
//            }
//            //must flag ScriptHash or Public Key
//            if (mustFlag)
//            {
//                bool flaged = false;
//                foreach (var attr in tx.GetAttributes())
//                {
//                    if (attr.Usage == 0xff)
//                    {
//                        if (attr.Data.AsString() != ownerPubKey.AsString())
//                        {
//                            return false;
//                        }
//                        flaged = true;
//                    }
//                    if (attr.Usage == 0xfe)
//                    {
//                        if (attr.Data.AsString() != sh)
//                        {
//                            return false;
//                        }
//                        flaged = true;
//                    }
//                }
//                if (!flaged) return false;
//            }
//            return VerifySignature(signature, pubkey) || VerifySignature(signature, ownerPubKey) || Runtime.CheckWitness(pubkey) || Runtime.CheckWitness(ownerPubKey);
//            //}
//            //catch
//            //{
//            //    return false;
//            //}
//        }
//    }
//}
