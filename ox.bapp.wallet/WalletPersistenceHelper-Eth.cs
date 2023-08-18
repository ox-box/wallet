using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using OX.IO.Data.LevelDB;
using OX.Network.P2P.Payloads;
using OX.IO;
using OX.SmartContract;
using OX.Ledger;
using OX.Persistence;
using OX.Cryptography.AES;
using Akka.Util.Internal;
using OX.Wallets.Base.Events;
using OX.Wallets.Base.Wallets;

namespace OX.Wallets.Base
{
    public static partial class WalletPersistenceHelper
    {
        public static void Save_EthereumMapTransaction(this WriteBatch batch, WalletBappProvider provider, Block block, EthereumMapTransaction emt, ushort blockN)
        {
            if (emt.IsNotNull() && emt.EthMapContract.Equals(Blockchain.EthereumMapContractScriptHash)/* && provider.Wallet.IsNotNull()*/)
            {
                var sh = emt.GetContract().ScriptHash;
                EthereumMapTransactionMerge emtm = new EthereumMapTransactionMerge { EthereumMapTransaction = emt, LastIndex = block.Index };
                batch.Put(SliceBuilder.Begin(WalletBizPersistencePrefixes.ALL_Eth_Map).Add(sh), SliceBuilder.Begin().Add(emtm));
                provider.AllEthereumMaps[sh] = emtm;
            }
        }
    }
}
