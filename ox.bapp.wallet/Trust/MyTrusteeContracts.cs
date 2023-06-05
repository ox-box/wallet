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
using OX.IO.Data.LevelDB;
using OX.IO;

namespace OX.Wallets.Base
{
    public partial class MyTrusteeContracts : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        private INotecase Operater;

        #region Constructor Region

        public MyTrusteeContracts()
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
                if (nodes != null && nodes.Length == 1)
                {
                    var node = nodes.FirstOrDefault();
                    int nt = (int)node.NodeType;
                    if (nt == 3)
                    {
                        KeyValuePair<UInt160, AssetTrustContract> p = (KeyValuePair<UInt160, AssetTrustContract>)node.Tag;
                        sm = new ToolStripMenuItem(UIHelper.LocalString("复制信托地址", "Copy Trust Address"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click;
                        menu.Items.Add(sm);
                        sm = new ToolStripMenuItem(UIHelper.LocalString("复制委托人地址", "Copy Truster Address"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click1;
                        menu.Items.Add(sm);
                        sm = new ToolStripMenuItem(UIHelper.LocalString("查看信托余额", "View Trust Balance"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click3;
                        menu.Items.Add(sm);

                        if (Blockchain.Singleton.Height > p.Value.LastTransferIndex + 10)
                        {
                            sm = new ToolStripMenuItem(UIHelper.LocalString("信托转帐", "Trust Transfer"));
                            sm.Tag = node.Tag;
                            sm.Click += Sm_Click4;
                            menu.Items.Add(sm);
                        }
                    }
                    else if (nt == 4)
                    {
                        sm = new ToolStripMenuItem(UIHelper.LocalString("复制地址", "Copy  Address"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click2;
                        menu.Items.Add(sm);
                    }
                }
                if (menu.Items.Count > 0)
                    menu.Show(this.treeAsset, e.Location);
            }
        }
        private void Sm_Click4(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            KeyValuePair<UInt160, AssetTrustContract> p = (KeyValuePair<UInt160, AssetTrustContract>)ToolStripMenuItem.Tag;
            new TransferTrustAsset(this.Operater, p.Value.Trustee, p.Key, p.Value).ShowDialog();
        }
        private void Sm_Click3(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            KeyValuePair<UInt160, AssetTrustContract> p = (KeyValuePair<UInt160, AssetTrustContract>)ToolStripMenuItem.Tag;
            new DialogTrustAssetBalance(p.Key).ShowDialog();
        }

        private void Sm_Click2(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            UInt160 p = ToolStripMenuItem.Tag as UInt160;
            var addr = p.ToAddress();
            Clipboard.SetText(addr);
            string msg = addr + UIHelper.LocalString("  已复制", "  copied");
            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
            DarkMessageBox.ShowInformation(msg, "");
        }
        private void Sm_Click1(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            KeyValuePair<UInt160, AssetTrustContract> p = (KeyValuePair<UInt160, AssetTrustContract>)ToolStripMenuItem.Tag;
            var addr = Contract.CreateSignatureRedeemScript(p.Value.Truster).ToScriptHash().ToAddress();
            Clipboard.SetText(addr);
            string msg = addr + UIHelper.LocalString("  已复制", "  copied");
            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
            DarkMessageBox.ShowInformation(msg, "");
        }

        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            KeyValuePair<UInt160, AssetTrustContract> p = (KeyValuePair<UInt160, AssetTrustContract>)ToolStripMenuItem.Tag;
            var addr = p.Key.ToAddress();
            Clipboard.SetText(addr);
            string msg = addr + UIHelper.LocalString("  已复制", "  copied");
            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
            DarkMessageBox.ShowInformation(msg, "");
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
                if (tx is AssetTrustTransaction att)
                {
                    if (att.TrustContract.Equals(Blockchain.TrustAssetContractScriptHash))
                    {
                        var trustee = Contract.CreateSignatureRedeemScript(att.Trustee).ToScriptHash();
                        if (this.Operater.Wallet.ContainsAndHeld(trustee))
                        {
                            DoReload();
                        }

                    }
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


        public void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            DoReload();
        }
        public void OnRebuild() { }
        void DoReload()
        {
            this.DoInvoke(() =>
            {
                this.treeAsset.Nodes.Clear();
            });
            var bizPlugin = WalletBappProvider.Instance;
            if (bizPlugin != default)
            {
                this.DoInvoke(() =>
                {
                    foreach (var p in this.Operater.Wallet.GetHeldAccounts())
                    {
                        var node = new DarkTreeNode(p.Address);
                        node.NodeType = 1;
                        node.Tag = p;
                        foreach (var ct in bizPlugin.AssetTrustContacts.Where(m => m.Value.Trustee.Equals(p.GetKey().PublicKey)))
                        {
                            var subnode = new DarkTreeNode(UIHelper.LocalString($"信托地址: {ct.Key.ToAddress()}", $"Trust Address: {ct.Key.ToAddress()}"));
                            subnode.NodeType = 3;
                            subnode.Tag = ct;
                            var trusterAddr = Contract.CreateSignatureRedeemScript(ct.Value.Truster).ToScriptHash().ToAddress();
                            var n3 = new DarkTreeNode(UIHelper.LocalString($"委托人地址: {trusterAddr}", $"Truster Address: {trusterAddr}"));
                            n3.NodeType = 3;
                            n3.Tag = ct;
                            subnode.Nodes.Add(n3);
                            n3 = new DarkTreeNode(UIHelper.LocalString($"主信托范围", $"Main Trust Scope"));
                            n3.NodeType = 3;
                            n3.Tag = ct;
                            foreach (var sh in ct.Value.Targets)
                            {
                                var n4 = new DarkTreeNode(sh.ToAddress());
                                n4.NodeType = 4;
                                n4.Tag = sh;
                                n3.Nodes.Add(n4);
                            }
                            subnode.Nodes.Add(n3);
                            n3 = new DarkTreeNode(UIHelper.LocalString($"边际信托范围", $"Side Trust Scope"));
                            n3.NodeType = 3;
                            n3.Tag = ct;
                            foreach (var sh in ct.Value.SideScopes)
                            {
                                var n4 = new DarkTreeNode(sh.ToAddress());
                                n4.NodeType = 4;
                                n4.Tag = sh;
                                n3.Nodes.Add(n4);
                            }
                            subnode.Nodes.Add(n3);
                            node.Nodes.Add(subnode);
                        }
                        this.treeAsset.Nodes.Add(node);
                    }
                });
            }
        }
        #endregion
    }
}
