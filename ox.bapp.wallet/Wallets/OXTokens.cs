using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections;
using System.Drawing;
using System.Collections.Generic;
using OX.Wallets.UI.Docking;
using OX.Wallets.UI.Controls;
using OX.Wallets;
using OX.Bapps;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets.NEP6;
using System.Drawing.Imaging;
using OX.Wallets.UI;
using OX.Wallets.UI.Forms;
using OX.Persistence;
using OX.Cryptography.ECC;
using OX.SmartContract;
using NBitcoin.OpenAsset;

namespace OX.Wallets.Base
{
    public partial class OXTokens : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        private INotecase Operater;
        #region Constructor Region

        public OXTokens()
        {
            InitializeComponent();
            this.DockArea = DarkDockArea.Left;
            this.treeAsset.MouseDown += TreeAsset_MouseDown;
        }

        private void TreeAsset_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DarkContextMenu menu = new DarkContextMenu();
                ToolStripMenuItem sm;

                DarkTreeNode[] nodes = treeAsset.SelectedNodes.ToArray();
                if (nodes.IsNotNullAndEmpty() && nodes.Length == 1)
                {
                    DarkTreeNode node = nodes[0];
                    sm = new ToolStripMenuItem(UIHelper.LocalString("复制资产Id", "Copy Asset Id"));
                    sm.Tag = node.Tag;
                    sm.Click += Sm_Click;
                    menu.Items.Add(sm);
                }

                if (menu.Items.Count > 0)
                    menu.Show(this.treeAsset, e.Location);
            }
        }

        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            AssetState assetState = ToolStripMenuItem.Tag as AssetState;
            try
            {
                Clipboard.SetText(assetState.AssetId.ToString());
                string msg = assetState.AssetId.ToString() + UIHelper.LocalString("  已复制", "  copied");
                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }

        public void Clear()
        {
            this.treeAsset.Nodes.Clear();
        }

        #endregion
        #region IBlockChainTrigger
        public void OnBappEvent(BappEvent be) { }

        public void OnCrossBappMessage(CrossBappMessage message)
        {

        }

        public void AfterOnBlock(Block block)
        {
            foreach (var tx in block.Transactions)
            {
                if (tx is InvocationTransaction it)
                {
                    reload();
                }
            }
        }
        public void BeforeOnBlock(Block block)
        {
        }
        public void OnBlock(Block block)
        {
        }

        public void HeartBeat(HeartBeatContext context)
        {
        }
        void reload()
        {
            this.DoInvoke(() =>
            {
                this.Clear();
                foreach (var asset in Blockchain.Singleton.Store.GetAssets().Find().Where(m => !m.Key.Equals(Blockchain.OXS) && !m.Key.Equals(Blockchain.OXC)))
                {
                    DarkTreeNode node = new DarkTreeNode(asset.Value.GetName());
                    node.Tag = asset.Value;
                    DarkTreeNode subnode = new DarkTreeNode(UIHelper.LocalString($"资产Id:{asset.Key.ToString()}", $"Asset Id:{asset.Key.ToString()}"));
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    var owner = Contract.CreateSignatureRedeemScript(asset.Value.Owner).ToScriptHash().ToAddress();
                    subnode = new DarkTreeNode(UIHelper.LocalString($"资产所有者:{owner}", $"Asset Owner:{owner}"));
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    subnode = new DarkTreeNode(UIHelper.LocalString($"资产管理者:{asset.Value.Admin.ToAddress()}", $"Asset Admin:{asset.Value.Admin.ToAddress()}"));
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    subnode = new DarkTreeNode(UIHelper.LocalString($"资产分发者:{asset.Value.Issuer.ToAddress()}", $"Asset Issuer:{asset.Value.Issuer.ToAddress()}"));
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    subnode = new DarkTreeNode { Text = UIHelper.LocalString($"精度:{asset.Value.Precision}", $"Precision:{asset.Value.Precision}") };
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    var cap = asset.Value.Amount == -Fixed8.Satoshi ? UIHelper.LocalString("无限", "unlimited") : asset.Value.Amount.ToString();
                    subnode = new DarkTreeNode { Text = UIHelper.LocalString($"总量:{cap}", $"Cap:{cap}") };
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    var Available = asset.Value.Available.ToString();
                    subnode = new DarkTreeNode { Text = UIHelper.LocalString($"已发行:{Available}", $"Issued:{Available}") };
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    Fixed8 totalLock = Fixed8.Zero;
                    Fixed8 s1 = Fixed8.Zero, s2 = Fixed8.Zero, s3 = Fixed8.Zero, s4 = Fixed8.Zero, s5 = Fixed8.Zero, s6 = Fixed8.Zero;
                    if (WalletBappProvider.Instance.AllLockAssets.IsNotNullAndEmpty())
                    {
                        var alats = WalletBappProvider.Instance.AllLockAssets.Values.Where(m => m.Output.AssetId.Equals(asset.Key));
                        if (alats.IsNotNullAndEmpty())
                        {
                            totalLock = alats.Sum(m => m.Output.Value);
                            var tsoxsTime = alats.Where(m => m.Tx.IsTimeLock);
                            var tsoxsBlock = alats.Where(m => !m.Tx.IsTimeLock);
                            var t1 = DateTime.Now.AddDays(182).ToTimestamp();
                            var t2 = DateTime.Now.AddDays(365).ToTimestamp();
                            var t3 = DateTime.Now.AddDays(365 + 182).ToTimestamp();
                            var ts = DateTime.Now.ToTimestamp();
                            var t1p = tsoxsTime.Where(m => m.Tx.LockExpiration - ts > t1);
                            if (t1p.IsNotNullAndEmpty())
                            {
                                s1 = t1p.Sum(m => m.Output.Value);
                            }
                            var t2p = tsoxsTime.Where(m => m.Tx.LockExpiration - ts > t2);
                            if (t2p.IsNotNullAndEmpty())
                            {
                                s2 = t2p.Sum(m => m.Output.Value);
                            }
                            var t3p = tsoxsTime.Where(m => m.Tx.LockExpiration - ts > t3);
                            if (t3p.IsNotNullAndEmpty())
                            {
                                s3 = t3p.Sum(m => m.Output.Value);
                            }
                            var h = Blockchain.Singleton.HeaderHeight;
                            var b1p = tsoxsBlock.Where(m => m.Tx.LockExpiration - h > 1000000);
                            if (b1p.IsNotNullAndEmpty())
                            {
                                s4 = b1p.Sum(m => m.Output.Value);
                            }
                            var b2p = tsoxsBlock.Where(m => m.Tx.LockExpiration - h > 2000000);
                            if (b2p.IsNotNullAndEmpty())
                            {
                                s5 = b2p.Sum(m => m.Output.Value);
                            }
                            var b3p = tsoxsBlock.Where(m => m.Tx.LockExpiration - h > 3000000);
                            if (b3p.IsNotNullAndEmpty())
                            {
                                s6 = b3p.Sum(m => m.Output.Value);
                            }
                        }
                    }
                    subnode = new DarkTreeNode { Text = UIHelper.LocalString($"累计锁仓:{totalLock}", $"Total Lock:{totalLock}") };
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    subnode = new DarkTreeNode { Text = UIHelper.LocalString($"流通总量:{asset.Value.Available-totalLock}", $"Total Liquid:{asset.Value.Available-totalLock}") };
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);

                    subnode = new DarkTreeNode { Text = UIHelper.LocalString($"剩余锁仓时间超过半年： {s1}", $"Remaining total lock later 0.5 year: {s1}") };
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    subnode = new DarkTreeNode { Text = UIHelper.LocalString($"剩余锁仓时间超过1年： {s2}", $"Remaining total lock later 1 year: {s2}") };
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    subnode = new DarkTreeNode { Text = UIHelper.LocalString($"剩余锁仓时间超过1.5年： {s3}", $"Remaining total lock later 1.5 year: {s3}") };
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    subnode = new DarkTreeNode { Text = UIHelper.LocalString($"剩余锁仓区块>1000000： {s4}", $"Remaining total lock blocks than 1000000: {s4}") };
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    subnode = new DarkTreeNode { Text = UIHelper.LocalString($"剩余锁仓区块>2000000： {s5}", $"Remaining total lock blocks than 2000000: {s5}") };
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);
                    subnode = new DarkTreeNode { Text = UIHelper.LocalString($"剩余锁仓区块>3000000： {s6}", $"Remaining total lock blocks than 3000000: {s6}") };
                    subnode.Tag = asset.Value;
                    node.Nodes.Add(subnode);



                  
                    this.treeAsset.Nodes.Add(node);
                }
            });
        }

        public void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            this.Clear();
            reload();
        }
        public void OnRebuild() { }
        #endregion
    }
}
