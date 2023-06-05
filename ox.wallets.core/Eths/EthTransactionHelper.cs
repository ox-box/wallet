using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using OX.IO;
using OX.Network.P2P.Payloads;
using System.Runtime.CompilerServices;
using OX.Ledger;

namespace OX.Wallets.Eths
{

    public static class EthTransactionHelper
    {
        static Dictionary<string, uint> LastTransaction = new Dictionary<string, uint>();
        public static bool AllowTransaction(this string ethAddress)
        {
            if (!LastTransaction.TryGetValue(ethAddress, out uint value))
            {
                value = 0;
                LastTransaction[ethAddress] = value;
            }
            return value + 10 < Blockchain.Singleton.Height;
        }
        public static void SetLastTransactionIndex(this string ethAddress, uint index)
        {
            LastTransaction[ethAddress] = index;
        }
        public static bool AllowTransaction(this EthID ethid)
        {
            if (!LastTransaction.TryGetValue(ethid.EthAddress, out uint value))
            {
                value = 0;
                LastTransaction[ethid.EthAddress] = value;
            }
            return value + 10 < Blockchain.Singleton.Height;
        }
        public static void SetLastTransactionIndex(this EthID ethid, uint index)
        {
            LastTransaction[ethid.EthAddress] = index;
        }
    }
}
