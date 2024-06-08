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
using OX.VM;
using OX.Wallets.UI.Forms;
using OX.Persistence;
using OX.SmartContract;
using OX.Cryptography.ECC;
using OX.Wallets.Base.Wallets;
using Nethereum.Model;
using System.Security.Principal;
using Akka.Actor.Dsl;
using OX.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OX.Wallets.Base;
using OX.IO.Data.LevelDB;
using System.Runtime.Intrinsics.X86;

namespace OX.Wallets.Flash
{
    public partial class MyTalks : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        public static List<CoinReference> lockAssetKeys = new List<CoinReference>();
        public Module Module { get; set; }
        private INotecase Operater;
        uint stateChangedIndex = 0;
        #region Constructor Region

        public MyTalks()
        {
            InitializeComponent();
            this.DockArea = DarkDockArea.Left;
            this.treeLines.MouseDown += TreeAsset_MouseDown;
            this.DockText = UIHelper.LocalString("我的线路", "My Talk Lines");
            this.bt_Fresh.Text = UIHelper.LocalString("刷新线路", "Refresh talk lines");
        }
        #region context menus
        private void TreeAsset_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DarkContextMenu menu = new DarkContextMenu();
                ToolStripMenuItem sm;
                DarkTreeNode[] nodes = this.treeLines.SelectedNodes.ToArray();
                if (nodes != null && nodes.Length == 1)
                {
                    var node = nodes.FirstOrDefault();
                    if ((int)node.NodeType == 3)
                    {
                        sm = new ToolStripMenuItem(UIHelper.LocalString("打开线路", "Open Talk Line"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click;
                        menu.Items.Add(sm);
                        sm = new ToolStripMenuItem(UIHelper.LocalString("修改名称", "Edit Name"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click2;
                        menu.Items.Add(sm);
                        sm = new ToolStripMenuItem(UIHelper.LocalString("删除线路", "Remove Talk Line"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click8;
                        menu.Items.Add(sm);
                        sm = new ToolStripMenuItem(UIHelper.LocalString("删除聊天记录", "Clear Chat History"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click9;
                        menu.Items.Add(sm);
                    }
                    else if ((int)node.NodeType == 4)
                    {
                        sm = new ToolStripMenuItem(UIHelper.LocalString("打开线路", "Open Talk Line"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click1;
                        menu.Items.Add(sm);
                        sm = new ToolStripMenuItem(UIHelper.LocalString("复制群聊密钥", "Copy Multicast Key"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click4;
                        menu.Items.Add(sm);
                        sm = new ToolStripMenuItem(UIHelper.LocalString("邀请群聊", "Invite Multicast"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click5;
                        menu.Items.Add(sm);
                        sm = new ToolStripMenuItem(UIHelper.LocalString("修改名称", "Edit Name"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click3;
                        menu.Items.Add(sm);
                        sm = new ToolStripMenuItem(UIHelper.LocalString("删除线路", "Remove Talk Line"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click6;
                        menu.Items.Add(sm);
                        sm = new ToolStripMenuItem(UIHelper.LocalString("删除聊天记录", "Clear Chat History"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click7;
                        menu.Items.Add(sm);
                    }
                    if (menu.Items.Count > 0)
                        menu.Show(this.treeLines, e.Location);
                }

            }
        }

        private void Sm_Click9(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<UInt256, UnicastTalkLineValue> t = ToolStripMenuItem.Tag as Tuple<UInt256, UnicastTalkLineValue>;
            var rs = FlashMessageProvider.Instance.GetAll<TalkLineKey, FlashUnicastRecord>(FlashStatePersistencePrefixes.FlashUnicast_Record, t.Item1);
            if (rs.IsNotNull())
            {
                var result = DarkMessageBox.ShowWarning(UIHelper.LocalString($"确定要删除聊天记录吗?", $"Are you sure you want to remove chat history?"), UIHelper.LocalString("删除聊天记录", "remove chat history"), DarkDialogButton.YesNo);
                if (result == DialogResult.No)
                    return;
                FlashMessageProvider.Instance.Do(wb =>
                {
                    foreach (var r in rs)
                        wb.Delete(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashUnicast_Record).Add(r.Key));
                });
                if (this.Module is FlashMessageModule fmm)
                    fmm.ClearUnicastTalkLineRecords(t);
            }
        }

        private void Sm_Click8(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<UInt256, UnicastTalkLineValue> t = ToolStripMenuItem.Tag as Tuple<UInt256, UnicastTalkLineValue>;
            var result = DarkMessageBox.ShowWarning(UIHelper.LocalString($"确定要删除线路吗?", $"Are you sure you want to remove the talk line?"), UIHelper.LocalString("删除线路", "remove talk line"), DarkDialogButton.YesNo);
            if (result == DialogResult.No)
                return;
            if (FlashMessageProvider.Instance.UnicastTalkLines.Remove(t.Item1, out UnicastTalkLineValue _))
            {
                FlashMessageProvider.Instance.Do(wb =>
                {
                    wb.Delete(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashUnicast_TalkLine).Add(t.Item1));
                });
                if (this.Module is FlashMessageModule fmm)
                {
                    fmm.CloseUnicastTalkLine(t);
                }
                reloadLines();
            }
        }

        private void Sm_Click7(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<UInt256, MulticastTalkLineValue> t = ToolStripMenuItem.Tag as Tuple<UInt256, MulticastTalkLineValue>;
            var rs = FlashMessageProvider.Instance.GetAll<TalkLineKey, FlashMulticastRecord>(FlashStatePersistencePrefixes.FlashMulticast_Record, t.Item1);
            if (rs.IsNotNull())
            {
                var result = DarkMessageBox.ShowWarning(UIHelper.LocalString($"确定要删除聊天记录吗?", $"Are you sure you want to remove chat history?"), UIHelper.LocalString("删除聊天记录", "remove chat history"), DarkDialogButton.YesNo);
                if (result == DialogResult.No)
                    return;
                FlashMessageProvider.Instance.Do(wb =>
                {
                    foreach (var r in rs)
                        wb.Delete(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashMulticast_Record).Add(r.Key));
                });
                if (this.Module is FlashMessageModule fmm)
                    fmm.ClearMulticastTalkLineRecords(t);
            }
        }

        private void Sm_Click6(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<UInt256, MulticastTalkLineValue> t = ToolStripMenuItem.Tag as Tuple<UInt256, MulticastTalkLineValue>;
            var result = DarkMessageBox.ShowWarning(UIHelper.LocalString($"确定要删除线路吗?", $"Are you sure you want to remove the talk line?"), UIHelper.LocalString("删除线路", "remove talk line"), DarkDialogButton.YesNo);
            if (result == DialogResult.No)
                return;
            if (FlashMessageProvider.Instance.MulticastTalkLines.Remove(t.Item1, out MulticastTalkLineValue _))
            {
               
                FlashMessageProvider.Instance.Do(wb =>
                {
                    wb.Delete(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashMulticast_TalkLine).Add(t.Item1));
                });
                if (this.Module is FlashMessageModule fmm)
                {
                    fmm.CloseMulticastTalkLine(t);
                }
                reloadLines();
            }
        }

        private void Sm_Click5(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<UInt256, MulticastTalkLineValue> t = ToolStripMenuItem.Tag as Tuple<UInt256, MulticastTalkLineValue>;
            if (t.Item2.Label.IsNotNullAndEmpty())
            {
                var msg = System.Text.Encoding.UTF8.GetBytes(t.Item2.Label);

                using (var dialog = new InviteMulticast(t.Item2.Label))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        var walletAccount = this.Operater.Wallet.GetAccount(t.Item2.Local);
                        if (walletAccount.IsNotNull() && !walletAccount.WatchOnly)
                        {
                            var pubs = dialog.GetPublics();
                            if (pubs.IsNotNullAndEmpty())
                            {
                                FlashMulticastNotice fmn = new FlashMulticastNotice(walletAccount.GetKey(), Blockchain.Singleton.HeaderHeight, t.Item2.Key, pubs.ToArray(), msg);
                                fmn.ContentType = FlashMessageContentType.Text;
                                this.Operater.SignAndSendFlashMessage(fmn);
                            }
                        }
                    }
                }
            }
        }

        private void Sm_Click4(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<UInt256, MulticastTalkLineValue> t = ToolStripMenuItem.Tag as Tuple<UInt256, MulticastTalkLineValue>;
            var s = t.Item2.Key.ToHexString();
            Clipboard.SetText(s);
            string msg = UIHelper.LocalString($"群聊密钥 {s}  已复制", $"Multicast key {s}  copied");
            DarkMessageBox.ShowInformation(msg, "");
        }

        private void Sm_Click3(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<UInt256, MulticastTalkLineValue> t = ToolStripMenuItem.Tag as Tuple<UInt256, MulticastTalkLineValue>;
            if (FlashMessageProvider.Instance.MulticastTalkLines.TryGetValue(t.Item1, out MulticastTalkLineValue utlv))
            {
                using (var dialog = new EditTalkLineName(utlv.Label))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        utlv.Label = dialog.GetLabel();
                        FlashMessageProvider.Instance.Do(wb =>
                        {
                            wb.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashMulticast_TalkLine).Add(t.Item1), SliceBuilder.Begin().Add(utlv));
                        });
                        reloadLines();
                        if (this.Module is FlashMessageModule fmm)
                            fmm.ModifyMulticastTalkLineLabel(t);
                    }
                }
            }
        }

        private void Sm_Click2(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<UInt256, UnicastTalkLineValue> t = ToolStripMenuItem.Tag as Tuple<UInt256, UnicastTalkLineValue>;
            if (FlashMessageProvider.Instance.UnicastTalkLines.TryGetValue(t.Item1, out UnicastTalkLineValue utlv))
            {
                using (var dialog = new EditTalkLineName(utlv.Label))
                {
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        utlv.Label = dialog.GetLabel();
                        FlashMessageProvider.Instance.Do(wb =>
                        {
                            wb.Put(SliceBuilder.Begin(FlashStatePersistencePrefixes.FlashUnicast_TalkLine).Add(t.Item1), SliceBuilder.Begin().Add(utlv));
                        });
                        reloadLines();
                        if (this.Module is FlashMessageModule fmm)
                            fmm.ModifyUnicastTalkLineLabel(t);
                    }
                }
            }
        }

        private void Sm_Click1(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<UInt256, MulticastTalkLineValue> t = ToolStripMenuItem.Tag as Tuple<UInt256, MulticastTalkLineValue>;
            var m = this.Module as FlashMessageModule;
            m.OpenMulticastTalkLine(t);
        }

        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<UInt256, UnicastTalkLineValue> t = ToolStripMenuItem.Tag as Tuple<UInt256, UnicastTalkLineValue>;
            var m = this.Module as FlashMessageModule;
            m.OpenUnicastTalkLine(t);
        }
        #endregion

        public void Clear()
        {
            this.treeLines.Nodes.Clear();
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
        public void OnFlashMessage(FlashMessage flashMessage)
        {

        }
        public void HeartBeat(HeartBeatContext context)
        {

        }


        public void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            reloadLines();
        }
        public void OnRebuild()
        {

        }
        void reloadLines()
        {
            this.DoInvoke(() =>
            {
                this.Clear();
                DarkTreeNode node = new DarkTreeNode { Text = UIHelper.LocalString("私聊线路", "Unicast Line") };
                node.NodeType = 1;
                foreach (var ul in FlashMessageProvider.Instance.UnicastTalkLines.GroupBy(m => m.Value.Local))
                {
                    DarkTreeNode ownernode = new DarkTreeNode { Text = ul.Key.ToAddress() };
                    ownernode.NodeType = 2;
                    foreach (var sul in ul)
                    {
                        DarkTreeNode linenode = new DarkTreeNode { Text = $"{sul.Value.Label}-{Contract.CreateSignatureRedeemScript(sul.Value.Remote).ToScriptHash().ToAddress()}" };
                        linenode.NodeType = 3;
                        linenode.Tag = new Tuple<UInt256, UnicastTalkLineValue>(sul.Key, sul.Value);
                        ownernode.Nodes.Add(linenode);
                    }
                    node.Nodes.Add(ownernode);
                }
                this.treeLines.Nodes.Add(node);

                node = new DarkTreeNode { Text = UIHelper.LocalString("群聊线路", "Multicast Line") };
                node.NodeType = 1;
                foreach (var ul in FlashMessageProvider.Instance.MulticastTalkLines.GroupBy(m => m.Value.Local))
                {
                    DarkTreeNode ownernode = new DarkTreeNode { Text = ul.Key.ToAddress() };
                    ownernode.NodeType = 2;
                    foreach (var sul in ul)
                    {
                        DarkTreeNode linenode = new DarkTreeNode { Text = sul.Value.Label };
                        linenode.NodeType = 4;
                        linenode.Tag = new Tuple<UInt256, MulticastTalkLineValue>(sul.Key, sul.Value);
                        ownernode.Nodes.Add(linenode);
                    }
                    node.Nodes.Add(ownernode);
                }
                this.treeLines.Nodes.Add(node);

            });

        }
        #endregion

        private void bt_Fresh_Click(object sender, EventArgs e)
        {
            this.reloadLines();
        }
    }
}
