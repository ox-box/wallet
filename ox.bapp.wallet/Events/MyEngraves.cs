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
    public partial class MyEngraves : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        private INotecase Operater;
        #region Constructor Region

        public MyEngraves()
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
                    sm = new ToolStripMenuItem(UIHelper.LocalString("打开事件", "Open Event"));
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
            EngraveTx engraveTx = (EngraveTx)ToolStripMenuItem.Tag;
            if (this.Module is EventModule md)
            {
                md.OpenEvent(engraveTx);
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

        public void OnRebuild()
        {
        }
        public void ChangeWallet(INotecase operater)
        {
            if (operater.IsNull()) return;
            this.Operater = operater;
            this.DoInvoke(() =>
            {
                this.treeRooms.Nodes.Clear();
            });
            var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
            if (bizPlugin != default)
            {
                if (this.Operater.Wallet.IsNotNull() && this.Operater.Wallet is NEP6Wallet nep6wallet)
                {
                    List<EngraveTx> list = new List<EngraveTx>();
                    foreach (var act in this.Operater.Wallet.GetHeldAccounts())
                    {
                        foreach (var p in bizPlugin.GetEngravesByHolder(act.ScriptHash))
                        {
                            var tx = Blockchain.Singleton.GetTransaction(p.Key.EngraveHash);
                            if (tx.IsNotNull() && tx is EventTransaction et)
                            {
                                EngraveTx engraveTx = new EngraveTx() { ET = et, EG = p.Value };
                                list.Add(engraveTx);
                            }
                        }
                    }
                    foreach (var l in list.OrderByDescending(m => m.EG.Timestamp))
                    {
                        AppendEngrave(l);
                    }
                }
            }
        }
        void AppendEngrave(EngraveTx engraveTx)
        {
            this.DoInvoke(() =>
            {
                string time = engraveTx.EG.Timestamp.ToDateTime().ToString("yyyy-MM-dd");
                var node = new DarkTreeNode($"[{time}]{engraveTx.EG.Title}");
                node.Tag = engraveTx;
                this.treeRooms.Nodes.Add(node);
            });
        }

        #endregion
    }
}
