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

namespace OX.Wallets.Base
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
                    sm = new ToolStripMenuItem(UIHelper.LocalString("查看私信", "View Letter"));
                    sm.Tag = node.Tag;
                    sm.Click += Sm_Click;
                    menu.Items.Add(sm);
                }
                if (menu.Items.Count > 0)
                    menu.Show(this.treeRooms, e.Location);
            }
        }

        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            SecretLetterKey key = ToolStripMenuItem.Tag as SecretLetterKey;
            if (this.Module is LetterModule md)
            {
                new ViewLetterDialog(this.Operater, key).ShowDialog();
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
        void reload()
        {

            var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
            if (bizPlugin != default)
            {
                this.DoInvoke(() =>
                {
                    this.treeRooms.Nodes.Clear();
                    foreach (var b in bizPlugin.GetMyLetters().OrderByDescending(m => m.Key.LetterIndex))
                    {
                        var from = Contract.CreateSignatureRedeemScript(b.Key.From).ToScriptHash();
                        var node = new DarkTreeNode(from.ToAddress());
                        node.Tag = b.Key;
                        this.treeRooms.Nodes.Add(node);
                    }
                });
            }
        }


        #endregion
    }
}
