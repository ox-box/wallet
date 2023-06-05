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

namespace OX.Wallets.Base
{
    public partial class BTCAsset : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        private INotecase Operater;
        #region Constructor Region

        public BTCAsset()
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
                    DarkTreeNode node = nodes[0];
                    //查看私钥
                    sm = new ToolStripMenuItem(UIHelper.LocalString("查看私钥", "Show Private Key"));
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
            OpenAccount openaccount = ToolStripMenuItem.Tag as OpenAccount;
            if (this.Operater.Wallet is OpenWallet openwallet)
            {
                using (VerifyPwdForMnemonic VerifyPwdForMnemonic = new VerifyPwdForMnemonic())
                {
                    if (VerifyPwdForMnemonic.ShowDialog() != DialogResult.OK || openwallet.WalletPassword != VerifyPwdForMnemonic.GetPassword()) return;
                    new DialogShowOpenAccountKey(openaccount, openwallet.WalletPassword).ShowDialog();
                }                
            }
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
            this.Clear();
            if (operater.Wallet is OpenWallet openwallet)
            {
                foreach (var act in openwallet.BTCAccounts)
                {
                    DarkTreeNode node = new DarkTreeNode() { Text = act.Address };
                    node.Tag = act;
                    this.treeAsset.Nodes.Add(node);
                }
            }
        }
        public void OnRebuild()
        {
        }
        #endregion
    }
}
