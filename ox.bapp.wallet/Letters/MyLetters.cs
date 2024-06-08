using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections;
using System.Drawing;
using System.Collections.Generic;
using OX.Wallets.UI.Docking;
using OX.Wallets.UI.Controls;
using OX.Wallets;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets.NEP6;
using System.Drawing.Imaging;
using OX;
using OX.Wallets.UI;
using OX.Wallets.UI.Forms;
using OX.Persistence;
using OX.IO;
using OX.Network.P2P;
using OX.Bapps;
using OX.SmartContract;
using OX.Wallets.Base.Letters;
using OX.Wallets.Base.Wallets;
using OX.Cryptography.ECC;
using OX.Wallets.Base;

namespace OX.Wallets.Letters
{
    public partial class MyLetters : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        private INotecase Operater;
        bool NeedReload;
        #region Constructor Region

        public MyLetters()
        {
            InitializeComponent();
            this.DockArea = DarkDockArea.Left;
            this.treeRooms.MouseDown += TreeAsset_MouseDown;

        }

        private void TreeAsset_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DarkContextMenu menu = new DarkContextMenu();
                ToolStripMenuItem sm;
                DarkTreeNode[] nodes = treeRooms.SelectedNodes.ToArray();
                if (nodes != null && nodes.Length == 1)
                {
                    var node = nodes.FirstOrDefault();
                    if ((int)node.NodeType == 2)
                    {
                        sm = new ToolStripMenuItem(UIHelper.LocalString("查看链邮", "View blockchain mail"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click;
                        menu.Items.Add(sm);
                    }
                }
                if (menu.Items.Count > 0)
                    menu.Show(this.treeRooms, e.Location);
            }
        }

        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<UInt256, LetterPair> t = ToolStripMenuItem.Tag as Tuple<UInt256, LetterPair>;
            if (this.Module is LetterModule md)
            {
                md.OpenLetterLine(t.Item1, t.Item2);
            }
        }

        public void Clear()
        {
            this.treeRooms.Nodes.Clear();
        }

        #endregion
        #region IBlockChainTrigger
        public void OnBappEvent(BappEvent be)
        {

        }


        public void OnMesssage(int msgType, string msg, byte[] data)
        {
            if (msgType == 19214)
            {
                ChangeWallet(this.Operater);
            }
        }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
        }
        public void HeartBeat(HeartBeatContext context)
        {

        }
        public void BeforeOnBlock(Block block)
        {
            if (NeedReload)
            {
                NeedReload = false;
                reload();
            }
        }
        public void OnBlock(Block block)
        {
        }
        public void AfterOnBlock(Block block)
        {
            foreach (var tx in block.Transactions)
            {
                if (tx is SecretLetterTransaction slt)
                {
                    if (this.Operater.IsNotNull() && this.Operater.Wallet.IsNotNull() && this.Operater.Wallet.GetHeldAccounts().Select(m => m.ScriptHash.Hash).Contains(slt.ToHash))
                    {
                        NeedReload = true;
                    }
                }
            }
        }
        public void ChangeWallet(INotecase operater)
        {
            if (operater.IsNull()) return;
            this.Operater = operater;
            reload();
        }
        public void OnRebuild()
        {
        }
        public void OnFlashMessage(FlashMessage flashMessage)
        {

        }
        void reload()
        {

            var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
            if (bizPlugin != default)
            {
                this.DoInvoke(() =>
                {
                    this.treeRooms.Nodes.Clear();
                    foreach (var glocal in bizPlugin.GetLetterLines()?.GroupBy(m => m.Value.Local))
                    {
                        var node = new DarkTreeNode(glocal.Key.ToAddress());
                        node.Tag = glocal;
                        node.NodeType = 1;
                        foreach (var r in glocal)
                        {
                            var remoteSH = Contract.CreateSignatureRedeemScript(r.Value.Remote).ToScriptHash();
                            var subnode = new DarkTreeNode(remoteSH.ToAddress());
                            subnode.Tag = new Tuple<UInt256, LetterPair>(r.Key, r.Value);
                            subnode.NodeType = 2;
                            node.Nodes.Add(subnode);
                        }
                        this.treeRooms.Nodes.Add(node);
                    }
                });
            }
        }


        #endregion
    }
}
