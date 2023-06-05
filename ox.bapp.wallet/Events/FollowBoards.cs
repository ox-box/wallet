using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections;
using System.Drawing;
using System.Collections.Generic;
using OX.Wallets.UI.Docking;
using OX.Wallets.UI.Controls;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets.NEP6;
using System.Drawing.Imaging;
using OX.Wallets.UI;
using OX.Wallets.UI.Forms;
using OX.Persistence;
using OX.IO;
using OX.Network.P2P;
using OX.Bapps;
using OX.Wallets.Base.Events;
using OX.Wallets.Base.Wallets;

namespace OX.Wallets.Base
{
    public partial class FollowBoards : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        private INotecase Operater;
        #region Constructor Region

        public FollowBoards()
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
                    sm = new ToolStripMenuItem(UIHelper.LocalString("打开事件板", "Open Event Board"));
                    sm.Tag = node.Tag;
                    sm.Click += Sm_Click;
                    menu.Items.Add(sm);

                    sm = new ToolStripMenuItem(UIHelper.LocalString("发布事件", "Publish Event"));
                    sm.Tag = node.Tag;
                    sm.Click += SmPublish_Click;
                    menu.Items.Add(sm);

                    sm = new ToolStripMenuItem(UIHelper.LocalString("移除事件板", "Remove Event Board"));
                    sm.Tag = node.Tag;
                    sm.Click += SmRemove_Click;
                    menu.Items.Add(sm);
                }
                if (menu.Items.Count > 0)
                    menu.Show(this.treeRooms, e.Location);
            }
        }
        private void SmPublish_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            string boardKey = (string)ToolStripMenuItem.Tag;
            if (this.Module is EventModule md)
            {
                md.PublishEvent(boardKey);
            }
        }
        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            string boardKey = (string)ToolStripMenuItem.Tag;
            if (this.Module is EventModule md)
            {
                md.OpenEventBoard(boardKey);
            }
        }
        private void SmRemove_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            string boardKey = (string)ToolStripMenuItem.Tag;
            if (this.Operater.Wallet.IsNotNull() && this.Operater.Wallet is NEP6Wallet nep6wallet)
            {
                if (nep6wallet.DeleteStone("collectionboard", boardKey))
                {
                    ChangeWallet(this.Operater);
                }
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
            if (be.ContainEventType(WalletBappEventType.CollectionBoardEvent, out BappEventItem[] eventItems))
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
        }



        public void ChangeWallet(INotecase operater)
        {
            if (operater.IsNull()) return;
            this.Operater = operater;
            this.treeRooms.Nodes.Clear();
            var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
            if (bizPlugin != default)
            {
                if (this.Operater.Wallet.IsNotNull() && this.Operater.Wallet is NEP6Wallet nep6wallet)
                {
                    foreach (var stone in nep6wallet.GetStones("collectionboard"))
                    {
                        if (BoardKey.TryParser(stone.Key, out BoardKey key))
                        {
                            var sh = bizPlugin.GetBoard(key);
                            if (sh.IsNotNull())
                                AppendBoard(stone.Key, stone.Value);
                            else
                                nep6wallet.DeleteStone(stone.Type, stone.Key);
                        }
                    }
                }
            }
        }
        public void OnRebuild()
        {
        }
        void AppendBoard(string boardKey, string boardName)
        {
            foreach (var n in this.treeRooms.Nodes)
            {
                if (n.Tag is string rr)
                {
                    if (rr == boardKey)
                        return;
                }
            }
            var node = new DarkTreeNode($"{boardKey}:{boardName}");
            node.Tag = boardKey;
            this.treeRooms.Nodes.Add(node);
        }

        #endregion
    }
}
