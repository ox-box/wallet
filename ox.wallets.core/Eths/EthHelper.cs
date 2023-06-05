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

    public static class EthHelper
    {

        public static string Omit(this string address, int limitLength = 6)
        {
            var length = address.Length;
            if (length > 15)
            {
                return address.Substring(0, limitLength) + "..." + address.Substring(length - limitLength, limitLength);
            }
            return address;
        }

        public static EthBalanceState QueryBalanceState(this OpenWallet openWallet, EthID ethid)
        {
            return openWallet.QueryBalanceState(ethid.EthAddress);
        }
        public static EthBalanceState QueryBalanceState(this OpenWallet openWallet, string ethAddress)
        {
            var sh = ethAddress.BuildMapAddress();
            EthBalanceState state = new EthBalanceState();
            var masterState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(sh);
            if (masterState.IsNotNull())
            {
                foreach (var b in masterState.Balances)
                {
                    var balance = new EthAssetBalanceState { MasterBalance = b.Value };
                    var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(b.Key);
                    if (assetState.IsNotNull())
                    {
                        balance.AssetId = b.Key;
                        balance.AssetName = assetState.GetName();
                    }
                    state[b.Key] = balance;
                }
            }
            if (OXRunTime.RunMode == RunMode.Server)
            {
                var h = Blockchain.Singleton.Height;
                var utxos = openWallet.GetAllEthereumMapUTXOs();
                var us = utxos.Where(m => m.Value.EthAddress.ToLower() == ethAddress.ToLower());
                foreach (var g in us.GroupBy(m => m.Value.Output.AssetId))
                {
                    if (!state.TryGetValue(g.Key, out var b))
                    {
                        b = new EthAssetBalanceState() { AssetId = g.Key };
                        var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(g.Key);
                        if (assetState.IsNotNull())
                        {
                            b.AssetName = assetState.GetName();
                        }
                        state[g.Key] = b;
                    }

                    b.TotalBalance = g.Sum(m => m.Value.Output.Value);
                    var ls = g.Where(m => m.Value.LockExpirationIndex < h);
                    if (ls.IsNotNullAndEmpty())
                        b.AvailableBalance = ls.Sum(m => m.Value.Output.Value);
                }
            }
            return state;
        }
    }
}
