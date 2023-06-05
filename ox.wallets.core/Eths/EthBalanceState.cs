using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using OX.IO;
using OX.Network.P2P.Payloads;
using OX.Ledger;

namespace OX.Wallets.Eths
{
    public class EthAssetBalanceState
    {
        public UInt256 AssetId;
        public string AssetName;
        public Fixed8 MasterBalance = Fixed8.Zero;
        public Fixed8 AvailableBalance = Fixed8.Zero;
        public Fixed8 TotalBalance = Fixed8.Zero;
    }
    public class EthBalanceState : Dictionary<UInt256, EthAssetBalanceState>
    {
        
        public EthAssetBalanceState TryGetBalance(UInt256 asset)
        {
            if (!this.TryGetValue(asset, out var balance))
            {
                balance = new EthAssetBalanceState() { AssetId = asset };
                var assetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(asset);
                if (assetState.IsNotNull())
                {
                    balance.AssetName = assetState.GetName();
                }
            }
            return balance;
        }
        public EthAssetBalanceState[] ToArray()
        {
            if (this.IsNotNullAndEmpty())
                return this.Values.OrderByDescending(m => m.AssetId == Blockchain.OXS).ThenByDescending(m => m.AssetId == Blockchain.OXC).ToArray();
            else
                return new EthAssetBalanceState[0];
        }
    }
}
