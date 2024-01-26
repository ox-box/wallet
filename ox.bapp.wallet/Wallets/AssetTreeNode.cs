using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor.Dsl;
using NBitcoin.OpenAsset;
using Nethereum.Model;
using OX.Ledger;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;



namespace OX.Wallets.Base.Wallets
{
    public abstract class BaseTreeNode : DarkTreeNode
    {
        public abstract WalletAccount Account { get; protected set; }
        public abstract void Refresh();
    }
    public class CommonAccountTreeNode : BaseTreeNode
    {
        public override WalletAccount Account { get; protected set; }
        public CommonAccountTreeNode(WalletAccount account)
        {
            Account = account;
        }

        public override void Refresh() { }
    }
    public class LockRootTreeNode : BaseTreeNode
    {
        public override WalletAccount Account { get; protected set; }
        public UInt256 AssetID { get; protected set; }
        public LockRootTreeNode(WalletAccount account, UInt256 assetId)
        {
            Account = account;
            AssetID = assetId;
        }

        public override void Refresh() { }
    }
    public class LockAccountTreeNode : BaseTreeNode
    {
        public override WalletAccount Account { get; protected set; }
        public OutputKey OutputKey { get; protected set; }
        public LockAssetMerge LockAssetMerge { get; protected set; }
        public LockAccountTreeNode(WalletAccount account, OutputKey outputkey, LockAssetMerge lam)
        {
            Account = account;
            OutputKey = outputkey;
            LockAssetMerge = lam;
        }

        public override void Refresh() { }
    }
    public class AccountNode : BaseTreeNode
    {
        public Wallet Wallet { get; private set; }
        public override WalletAccount Account { get; protected set; }
        BalanceNode OXSBalanceNode;
        BalanceNode OXCBalanceNode;
        CommonAccountTreeNode PrivateNodeRoot;
        List<BalanceNode> PrivateAssetNodes = new List<BalanceNode>();
        public AccountNode(Wallet wallet, WalletAccount account, IEnumerable<KeyValuePair<OutputKey, LockAssetMerge>> lockAssetRecords)
        {
            this.Wallet = wallet;
            this.Account = account;
            this.Tag = account;
            this.PrivateNodeRoot = new CommonAccountTreeNode(account) { Tag = account, Text = UIHelper.LocalString("私营资产", "Private Assets") };
            var bizPlugin = WalletBappProvider.Instance;
            if (bizPlugin.IsNotNull() && wallet.IsNotNull())
            {

                List<UInt256> Assets = new List<UInt256>();

                var accountCoins = wallet.FindUnspentCoins(account.ScriptHash);
                Assets.AddRange(accountCoins.Select(p => p.Output.AssetId).Distinct());
                if (lockAssetRecords.IsNotNullAndEmpty())
                {
                    foreach (var ass in lockAssetRecords.Select(m => m.Value.Output.AssetId).Distinct())
                    {
                        if (!Assets.Contains(ass))
                            Assets.Add(ass);
                    }
                }

                var oxsBalance = accountCoins.Where(m => m.Output.AssetId.Equals(Blockchain.OXS)).Sum(m => m.Output.Value);
                var oxcBalance = accountCoins.Where(m => m.Output.AssetId.Equals(Blockchain.OXC)).Sum(m => m.Output.Value);
                var oxsalrs = lockAssetRecords?.Where(m => m.Value.Output.AssetId.Equals(Blockchain.OXS));
                var oxcalrs = lockAssetRecords?.Where(m => m.Value.Output.AssetId.Equals(Blockchain.OXC));
                CommonAccountTreeNode nativeNode = new CommonAccountTreeNode(account) { Tag = account, Text = UIHelper.LocalString("原生资产", "Native Assets") };
                OXSBalanceNode = new BalanceNode(account, Blockchain.OXS, oxsBalance, oxsalrs);
                OXCBalanceNode = new BalanceNode(account, Blockchain.OXC, oxcBalance, oxcalrs);
                nativeNode.Nodes.Add(OXSBalanceNode);
                nativeNode.Nodes.Add(OXCBalanceNode);
                this.Nodes.Add(nativeNode);

                foreach (var asset in Assets)
                {
                    if (!asset.Equals(Blockchain.OXS) && !asset.Equals(Blockchain.OXC))
                    {
                        var Balance = accountCoins.Where(m => m.Output.AssetId.Equals(asset)).Sum(m => m.Output.Value);
                        var alrs = lockAssetRecords?.Where(m => m.Value.Output.AssetId.Equals(asset));
                        BalanceNode bn = new BalanceNode(account, asset, Balance, alrs);
                        PrivateNodeRoot.Nodes.Add(bn);
                    }
                }
                this.Nodes.Add(PrivateNodeRoot);
            }

            this.Refresh();
        }
        public override void Refresh()
        {
            var s = Account.WatchOnly ? "   #   " : string.Empty;
            if (Account.Label.IsNotNullAndEmpty())
                s += $"[{Account.Label}] ";
            Text = s + Account.Address;
            this.OXSBalanceNode.Refresh();
            this.OXCBalanceNode.Refresh();
        }



    }
    public class BalanceNode : BaseTreeNode
    {
        public override WalletAccount Account { get; protected set; }
        public UInt256 AssetID { get; private set; }
        AssetState AssetState;
        string AssetName;
        Fixed8 Amount = Fixed8.Zero;
        Fixed8 totalLocked = Fixed8.Zero;
        CommonAccountTreeNode UnlockLockNode;
        LockRootTreeNode LockRoot;
        public BalanceNode(WalletAccount account, UInt256 assetId, Fixed8 amount = default, IEnumerable<KeyValuePair<OutputKey, LockAssetMerge>> alrs = default)
        {
            Account = account;
            this.AssetID = assetId;
            AssetState = Blockchain.Singleton.Store.GetAssets().TryGet(assetId);
            AssetName = AssetState.GetName();
            this.Amount = amount;
            this.Tag = AssetState;
            UnlockLockNode = new CommonAccountTreeNode(account) { Tag = AssetState, Text = UIHelper.LocalString($"未锁仓余额  :  {Amount}", $"Balance of unlocked  :  {Amount}") };
            this.Nodes.Add(UnlockLockNode);

            if (alrs.IsNotNullAndEmpty())
            {
                totalLocked = alrs.Sum(m => m.Value.Output.Value);
            }
            this.LockRoot = new LockRootTreeNode(account, assetId);
            Fixed8 availableTotalLock = Fixed8.Zero;
            if (alrs.IsNotNullAndEmpty())
            {
                foreach (var t in alrs.OrderBy(m => m.Value.Tx.LockExpiration))
                {
                    if ((t.Value.Tx.IsTimeLock && DateTime.Now.ToTimestamp() > t.Value.Tx.LockExpiration) || (!t.Value.Tx.IsTimeLock && Blockchain.Singleton.Height > t.Value.Tx.LockExpiration))
                        availableTotalLock += t.Value.Output.Value;
                    LockAccountTreeNode subnode = new LockAccountTreeNode(account, t.Key, t.Value) { Text = UIHelper.LocalString($"锁仓金额  :  {t.Value.Output.Value}", $"Lock Amount  :  {t.Value.Output.Value}") };
                    subnode.NodeType = 2;
                    subnode.Tag = t;
                    var subsubnode = new LockAccountTreeNode(account, t.Key, t.Value) { Text = UIHelper.LocalString($"锁仓地址  :  {t.Value.Output.ScriptHash.ToAddress()}", $"Lock Address  :  {t.Value.Output.ScriptHash.ToAddress()}") };
                    subsubnode.NodeType = 2;
                    subsubnode.Tag = t;
                    subnode.Nodes.Add(subsubnode);
                    var locktype = t.Value.Tx.IsTimeLock ? "锁定时间" : "锁定区块";
                    var locktypeen = t.Value.Tx.IsTimeLock ? "Lock Time" : "Lock Block";
                    subsubnode = new LockAccountTreeNode(account, t.Key, t.Value) { Text = UIHelper.LocalString($"锁定类型  :  {locktype}", $"Lock Type  :  {locktypeen}") };
                    subsubnode.NodeType = 2;
                    subsubnode.Tag = t;
                    subnode.Nodes.Add(subsubnode);
                    var expstr = t.Value.Tx.IsTimeLock ? t.Value.Tx.LockExpiration.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss") : t.Value.Tx.LockExpiration.ToString();
                    subsubnode = new LockAccountTreeNode(account, t.Key, t.Value) { Text = UIHelper.LocalString($"到期  :  {expstr}", $"Expire  :  {expstr}") };
                    subsubnode.NodeType = 2;
                    subsubnode.Tag = t;
                    subnode.Nodes.Add(subsubnode);
                    if (!AccountAsset.lockAssetKeys.Contains(t.Key)) AccountAsset.lockAssetKeys.Add(t.Key);
                    this.LockRoot.Nodes.Add(subnode);
                }
            }
            this.LockRoot.Text = UIHelper.LocalString($"锁仓总余额  :  {availableTotalLock}/{totalLocked}", $"Balance of locked  :  {availableTotalLock}/{totalLocked}");
            this.Nodes.Add(this.LockRoot);
            this.Refresh();
        }
        public void SetBalance(Fixed8 amount)
        {
            this.Amount = amount;
        }
        public override void Refresh()
        {
            this.Text = $"{AssetName}  :  {AssetID.ToString()}";
            this.UnlockLockNode.Text = UIHelper.LocalString($"未锁仓余额  :  {Amount}", $"Balance of unlocked  :  {Amount}");
        }
    }


}
