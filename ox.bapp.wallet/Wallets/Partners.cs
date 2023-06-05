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
using OX.Wallets.UI;
using OX.Wallets.UI.Forms;
using OX.Persistence;
using OX.Bapps;

namespace OX.Wallets.Base
{
    public partial class Partners : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        private INotecase Operater;
        #region Constructor Region

        public Partners()
        {
            InitializeComponent();
            this.treePartners.MouseDown += TreeAsset_MouseDown;
        }

        private void TreeAsset_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DarkContextMenu menu = new DarkContextMenu();
                var sm = new ToolStripMenuItem(UIHelper.LocalString("新增转账伙伴", "New transfer partner"));
                sm.Click += Sm_Click;
                menu.Items.Add(sm);
                DarkTreeNode[] nodes = treePartners.SelectedNodes.ToArray();
                if (nodes != null && nodes.Length == 1)
                {
                    var node = nodes.FirstOrDefault();
                    sm = new ToolStripMenuItem(UIHelper.LocalString("复制账户地址到粘贴板", "Copy address to clipboard"));
                    sm.Tag = node.Tag;
                    sm.Click += Sm2_Click;
                    menu.Items.Add(sm);
                    sm = new ToolStripMenuItem(UIHelper.LocalString("复制电话号码到粘贴板", "Copy mobile number to clipboard"));
                    sm.Tag = node.Tag;
                    sm.Click += Sm4_Click;
                    menu.Items.Add(sm);
                    sm = new ToolStripMenuItem(UIHelper.LocalString("复制备注到粘贴板", "Copy remark to clipboard"));
                    sm.Tag = node.Tag;
                    sm.Click += Sm5_Click;
                    menu.Items.Add(sm);
                    sm = new ToolStripMenuItem(UIHelper.LocalString("删除转账伙伴", "Remove transfer partner"));
                    sm.Tag = node.Tag;
                    sm.Click += Sm3_Click; ;
                    menu.Items.Add(sm);

                }
                if (menu.Items.Count > 0)
                    menu.Show(this.treePartners, e.Location);
            }
        }
        private void Sm3_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            NEP6Partner hi = ToolStripMenuItem.Tag as NEP6Partner;
            if (this.Operater.Wallet is NEP6Wallet nep6Wallet)
            {
                nep6Wallet.DeletePartner(hi.Address);
                nep6Wallet.Save();
                var oldPartner = this.treePartners.Nodes.FirstOrDefault(m =>
                {
                    var p = m.Tag as NEP6Partner;
                    return p.Address == hi.Address;
                });
                if (oldPartner != default)
                {
                    this.treePartners.Nodes.Remove(oldPartner);
                }
            }
        }
        private void Sm2_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            NEP6Partner hi = ToolStripMenuItem.Tag as NEP6Partner;
            try
            {
                Clipboard.SetText(hi.Address);
                string msg = hi.Address + UIHelper.LocalString("  已复制", "  copied");
                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }
        private void Sm4_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            NEP6Partner hi = ToolStripMenuItem.Tag as NEP6Partner;
            try
            {
                Clipboard.SetText(hi.Mobile);
                string msg = hi.Mobile + UIHelper.LocalString("  已复制", "  copied");
                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }
        private void Sm5_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            NEP6Partner hi = ToolStripMenuItem.Tag as NEP6Partner;
            try
            {
                if (!string.IsNullOrEmpty(hi.Remark))
                {
                    Clipboard.SetText(hi.Remark);
                    string msg = hi.Remark + UIHelper.LocalString("  已复制", "  copied");
                    Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                    DarkMessageBox.ShowInformation(msg, "");
                }
            }
            catch (Exception) { }
        }
        private void Sm_Click(object sender, EventArgs e)
        {
            if (this.Operater.Wallet is NEP6Wallet nep6Wallet)
            {
                using (NewPartnerDialog dialog = new NewPartnerDialog())
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    try
                    {
                        var partner = dialog.GetPartner();
                        if (partner != default)
                        {
                            nep6Wallet.AddPartner(partner.Address, partner.Name, partner.Mobile, partner.Remark);
                            nep6Wallet.Save();
                            var oldPartner = this.treePartners.Nodes.FirstOrDefault(m =>
                            {
                                var p = m.Tag as NEP6Partner;
                                return p.Address == partner.Address;
                            });
                            if (oldPartner != default)
                            {
                                oldPartner.Tag = partner;
                                oldPartner.Text = partner.Name;
                            }
                            else
                            {
                                AddPartner(partner);
                            }
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        public void Clear()
        {
            this.treePartners.Nodes.Clear();
        }

        #endregion
        #region IBlockChainTrigger
        public void OnBappEvent(BappEvent be) { }

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
            this.Operater = operater;
            this.DoPartners();
        }
        public void OnRebuild()
        {
        }
        private void Wallet_WalletAccountEvent(object sender, WalletAccountChangeEventArgs e)
        {

        }
        void DoPartners()
        {
            this.DoInvoke(() =>
            {
                this.treePartners.Nodes.Clear();
                if (this.Operater.Wallet is NEP6Wallet nep6Wallet)
                {
                    foreach (var partner in nep6Wallet.GetPartners())
                    {
                        AddPartner(partner);
                    }
                }
            });
        }
        private void AddPartner(NEP6Partner partner)
        {
            DarkTreeNode node = new DarkTreeNode(partner.Name);
            node.NodeType = 1;
            node.Tag = partner;
            DarkTreeNode subNode = new DarkTreeNode($"{UIHelper.LocalString("地址", "Address")}  :  {partner.Address}");
            subNode.Tag = partner;
            subNode.NodeType = 2;
            node.Nodes.Add(subNode);
            subNode = new DarkTreeNode($"{UIHelper.LocalString("电话", "Mobile")}  :  {partner.Mobile}");
            subNode.Tag = partner;
            subNode.NodeType = 2;
            node.Nodes.Add(subNode);
            subNode = new DarkTreeNode($"{UIHelper.LocalString("备注", "Remark")}  :  {partner.Remark}");
            subNode.Tag = partner;
            subNode.NodeType = 2;
            node.Nodes.Add(subNode);
            this.treePartners.Nodes.Add(node);
        }
        #endregion
    }
}
