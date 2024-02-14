using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor.Dsl;
using OX.Ledger;
using OX.Network.P2P.Payloads;
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
        public CoinReference OutputKey { get; protected set; }
        public MyLockAssetMerge LockAssetMerge { get; protected set; }
        public LockAccountTreeNode(WalletAccount account, CoinReference outputkey, MyLockAssetMerge lam)
        {
            Account = account;
            OutputKey = outputkey;
            LockAssetMerge = lam;
        }

        public override void Refresh() { }
    }
    public class AccountNode : BaseTreeNode
    {
        public OpenWallet Wallet { get; private set; }
        public override WalletAccount Account { get; protected set; }
        public AccountNode(OpenWallet wallet, WalletAccount account)
        {
            this.Wallet = wallet;
            this.Account = account;
            this.Tag = account;

            wallet.TryGetWalletAccountBalance(account.ScriptHash, out Dictionary<UInt256, WalletAccountBalance> balances);
            CommonAccountTreeNode nativeNode = new CommonAccountTreeNode(account) { Tag = account, Text = UIHelper.LocalString("原生资产", "Native Assets") };
            if (balances.TryGetValue(Blockchain.OXS, out WalletAccountBalance oxsBalance))
            {
                var OXSBalanceNode = new BalanceNode(account, oxsBalance);
                nativeNode.Nodes.Add(OXSBalanceNode);
            }
            if (balances.TryGetValue(Blockchain.OXC, out WalletAccountBalance oxcBalance))
            {
                var OXCBalanceNode = new BalanceNode(account, oxcBalance);
                nativeNode.Nodes.Add(OXCBalanceNode);
            }
            this.Nodes.Add(nativeNode);

            var PrivateNode = new CommonAccountTreeNode(account) { Tag = account, Text = UIHelper.LocalString("私营资产", "Private Assets") };
            foreach (var balance in balances.Where(m => !m.Value.AssetId.Equals(Blockchain.OXS) && !m.Value.AssetId.Equals(Blockchain.OXC)))
            {
                PrivateNode.Nodes.Add(new BalanceNode(account, balance.Value));
            }
            this.Nodes.Add(PrivateNode);

            this.Refresh();
        }
        public override void Refresh()
        {
            var s = Account.WatchOnly ? "   #   " : string.Empty;
            if (Account.Label.IsNotNullAndEmpty())
                s += $"[{Account.Label}] ";
            Text = s + Account.Address;
        }



    }
    public class BalanceNode : BaseTreeNode
    {
        public override WalletAccount Account { get; protected set; }
        public WalletAccountBalance Balance { get; private set; }
        AssetState AssetState;
        string AssetName;
        public BalanceNode(WalletAccount account, WalletAccountBalance balance)
        {
            this.Account = account;
            this.Balance = balance;
            AssetState = Blockchain.Singleton.Store.GetAssets().TryGet(balance.AssetId);
            AssetName = AssetState.GetName();
            this.Tag = AssetState;
            var TotalBalanceNode = new CommonAccountTreeNode(account) { Tag = AssetState, Text = UIHelper.LocalString($"总余额  :  {balance.TotalBalance}", $" Total Balance  :  {balance.TotalBalance}") };
            this.Nodes.Add(TotalBalanceNode);
            var MasterBalanceNode = new CommonAccountTreeNode(account) { Tag = AssetState, Text = UIHelper.LocalString($"主余额  :  {balance.MasterBalance}", $" Master Balance  :  {balance.MasterBalance}") };
            this.Nodes.Add(MasterBalanceNode);
            var AvailableBalanceNode = new CommonAccountTreeNode(account) { Tag = AssetState, Text = UIHelper.LocalString($"可用余额  :  {balance.AvailableBalance}", $"Available Balance  :  {balance.AvailableBalance}") };
            this.Nodes.Add(AvailableBalanceNode);
            var TotalLockBalanceNode = new CommonAccountTreeNode(account) { Tag = AssetState, Text = UIHelper.LocalString($"总锁仓余额  :  {balance.TotalLockBalance}", $"Total Lock Balance  :  {balance.TotalLockBalance}") };
            this.Nodes.Add(TotalLockBalanceNode);
            var TotalUnlockBalanceNode = new CommonAccountTreeNode(account) { Tag = AssetState, Text = UIHelper.LocalString($"已解锁余额  :  {balance.TotalUnlockBalance}", $"Unlocked Balance  :  {balance.TotalUnlockBalance}") };
            this.Nodes.Add(TotalUnlockBalanceNode);
            if (balance.LockAssets.IsNotNullAndEmpty())
            {
                var LockRoot = new LockRootTreeNode(account, balance.AssetId) { Tag = AssetState, Text = UIHelper.LocalString("锁仓记录", "Locked Records") };
                foreach (var t in balance.LockAssets.OrderBy(m => m.Value.Tx.LockExpiration))
                {
                    LockAccountTreeNode subnode = new LockAccountTreeNode(account, t.Key, t.Value) { Text = UIHelper.LocalString($"锁仓金额  :  {t.Value.Output.Value}", $"Lock Amount  :  {t.Value.Output.Value}") };
                    subnode.NodeType = 2;
                    subnode.Tag = t;
                    var subsubnode = new LockAccountTreeNode(account, t.Key, t.Value) { Text = UIHelper.LocalString($"锁仓地址  :  {t.Value.Output.ScriptHash.ToAddress()}", $"Lock Address  :  {t.Value.Output.ScriptHash.ToAddress()}") };
                    subsubnode.NodeType = 2;
                    subsubnode.Tag = t;
                    subnode.Nodes.Add(subsubnode);
                    var locktype = t.Value.Tx.IsTimeLock ? "时间锁" : "区块锁";
                    var locktypeen = t.Value.Tx.IsTimeLock ? "Lock Time" : "Lock Block";                   
                    var expstr = t.Value.Tx.IsTimeLock ? t.Value.Tx.LockExpiration.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss") : t.Value.Tx.LockExpiration.ToString();
                    subsubnode = new LockAccountTreeNode(account, t.Key, t.Value) { Text = UIHelper.LocalString($"{locktype}到期  :  {expstr}", $"{locktypeen} Expire  :  {expstr}") };
                    subsubnode.NodeType = 2;
                    subsubnode.Tag = t;
                    subnode.Nodes.Add(subsubnode);
                    if (!AccountAsset.lockAssetKeys.Contains(t.Key)) AccountAsset.lockAssetKeys.Add(t.Key);
                    LockRoot.Nodes.Add(subnode);
                }
                this.Nodes.Add(LockRoot);
            }
            this.Refresh();
        }

        public override void Refresh()
        {
            this.Text = $"{AssetName}  :  {this.Balance.AssetId.ToString()}";
        }
    }


}
