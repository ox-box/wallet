using OX.Network.P2P.Payloads;
using OX.SmartContract;
using System;
using System.Linq;
using System.Collections.Generic;

namespace OX.Wallets
{
    public static class TXHelper
    {
        public static bool IsSingleInputTx(this Transaction tx)
        {
            if (tx.References.IsNullOrEmpty()) return false;
            return tx.References.Values.Select(m => m.ScriptHash).Distinct().Count() == 1;
        }
    }
}
