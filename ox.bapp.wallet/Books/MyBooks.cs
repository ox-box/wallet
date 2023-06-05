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
using OX.Wallets.Base.Books;
using OX.Wallets.Base.Wallets;

namespace OX.Wallets.Base
{
    public partial class MyBooks : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        private INotecase Operater;
        #region Constructor Region

        public MyBooks()
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
                    sm = new ToolStripMenuItem(UIHelper.LocalString("打开书籍", "Open book"));
                    sm.Tag = node.Tag;
                    sm.Click += Sm_Click;
                    menu.Items.Add(sm);

                    //sm = new ToolStripMenuItem(UIHelper.LocalString("发布事件", "Publish Event"));
                    //sm.Tag = node.Tag;
                    //sm.Click += SmPublish_Click;
                    //menu.Items.Add(sm);
                }
                if (menu.Items.Count > 0)
                    menu.Show(this.treeRooms, e.Location);
            }
        }
        private void SmPublish_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            //string boardKey = (string)ToolStripMenuItem.Tag;
            //if (this.Module is EventModule md)
            //{
            //    md.PublishEvent(boardKey);
            //}
        }
        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            string boardKey = (string)ToolStripMenuItem.Tag;
            if (this.Module is BookModule md)
            {
                //md.OpenEventBoard(boardKey);
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
        }
        public void OnBlock(Block block)
        {
        }
        public void AfterOnBlock(Block block)
        {
            foreach (var tx in block.Transactions)
            {
                if (tx is BookTransaction bt)
                {
                    if (this.Operater.IsNotNull() && this.Operater.Wallet.IsNotNull() && this.Operater.Wallet.ContainsAndHeld(Contract.CreateSignatureRedeemScript(bt.Author).ToScriptHash()))
                    {
                        reload();
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
            this.DoInvoke(() =>
            {
                this.treeRooms.Nodes.Clear();
            });
            var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
            if (bizPlugin != default)
            {
                foreach (var b in bizPlugin.GetMyBooks())
                {
                    AppendBook(b.Key, b.Value);
                }
            }
        }
        void AppendBook(MyBookKey bk, BookTransaction bt)
        {
            this.DoInvoke(() =>
            {
                foreach (var n in this.treeRooms.Nodes)
                {
                    if (n.Tag is MyBookKey key)
                    {
                        if (key.Equals(bk))
                            return;
                    }
                }
                var node = new DarkTreeNode(System.Text.Encoding.UTF8.GetString(bt.Data));
                node.Tag = bk;

                var subnode = new DarkTreeNode(UIHelper.LocalString($"作者:  {bk.Author.ToAddress()}", $"Author:  {bk.Author.ToAddress()}"));
                subnode.Tag = bk;
                node.Nodes.Add(subnode);
                subnode = new DarkTreeNode(UIHelper.LocalString($"书籍Id:   {bk.Index}-{bk.N}", $"Book Id:   {bk.Index}-{bk.N}"));
                subnode.Tag = bk;
                node.Nodes.Add(subnode);

                this.treeRooms.Nodes.Add(node);
            });
        }

        #endregion
    }
}
