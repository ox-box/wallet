using Akka.Actor;
using OX.IO.Actors;
using OX.Ledger;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using OX.Wallets.UI;
using OX.SmartContract;
using OX.IO;
using System.Xml;
using OX.Bapps;
using OX.Wallets.UI.Config;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;
using OX.Wallets.UI.Forms;
using OX.Wallets.Base.Events;
using OX.Wallets.Base.Wallets;

namespace OX.Wallets.Base
{
    public partial class DiggList : DarkForm, INotecaseTrigger, IModuleComponent
    {
        INotecase Operater;
        EngraveTx EngraveTx;
        public Module Module { get; set; }
        uint CurrentPageIndex;
        public DiggList(Module module, INotecase operater, EngraveTx engraveTx)
        {
            InitializeComponent();
            this.Operater = operater;
            this.Module = module;
            this.EngraveTx = engraveTx;
            this.bt_pre.Text = UIHelper.LocalString("< 上 1 页", "< Previous");
            this.bt_pre10.Text = UIHelper.LocalString("< 上 10 页", "< Previous 10");
            this.bt_pre100.Text = UIHelper.LocalString("< 上 100 页", "< Previous 100");
            this.bt_next.Text = UIHelper.LocalString("下 1 页 >", "Next >");
            this.bt_next10.Text = UIHelper.LocalString("下 10 页 >", "Next 10 >");
            this.bt_next100.Text = UIHelper.LocalString("下 100 页 >", "Next 100 >");
            this.bt_newDigg.Text = UIHelper.LocalString("评价", "Digg");
            this.bt_close.Text = UIHelper.LocalString("关闭", "Close");
        }


        private void DiggList_Load(object sender, EventArgs e)
        {
            var time = this.EngraveTx.EG.Timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm");
            this.Text = $"[{time}]{this.EngraveTx.EG.Title}";
            this.tb_EngraveMessage.Text = this.EngraveTx.EG.Message;
            this.RoundPanel.SizeChanged += RoundPanel_SizeChanged;
            this.ResetPageIndex();
        }

        private void RoundPanel_SizeChanged(object sender, EventArgs e)
        {
            foreach (Control ctrl in this.RoundPanel.Controls)
            {
                if (ctrl is DarkTitle dt)
                    dt.Width = this.RoundPanel.Width - 10;
                if (ctrl is Panel pl)
                    pl.Width = this.RoundPanel.Width - 10;
                if (ctrl is DarkLabel lb)
                    lb.Width = this.RoundPanel.Width - 10;
            }
            int w = this.RoundPanel.Size.Width - 30;
            IEnumerator itr = this.RoundPanel.Controls.GetEnumerator();
            List<Control> cs = new List<Control>();
            while (itr.MoveNext())
            {
                cs.Add(itr.Current as Control);
            }
            this.RoundPanel.Controls.Clear();
            foreach (var c in cs)
            {
                this.RoundPanel.Controls.Add(c);
            }
            while (itr.MoveNext())
            {
                Control ctrl = itr.Current as Control;
                if (ctrl is DarkTitle dt)
                    dt.Width = this.RoundPanel.Width - 10;
                if (ctrl is Panel pl)
                    pl.Width = this.RoundPanel.Width - 10;
                if (ctrl is DarkLabel lb)
                    lb.Width = this.RoundPanel.Width - 10;

            }
        }


        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }


        public void OnBappEvent(BappEvent be)
        {
            if (be.ContainEventType(WalletBappEventType.EventTransactionEvent, out BappEventItem[] eventItems))
            {
                ShowPageIndex();
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
            this.Operater = operater;
        }
        public void OnRebuild()
        {

        }
        private void bt_pre100_Click(object sender, EventArgs e)
        {
            if (this.CurrentPageIndex > 100)
                this.CurrentPageIndex -= 100;
            else this.CurrentPageIndex = 0;
            this.lb_pageIndex.Text = this.CurrentPageIndex.ToString();
            this.ShowPageIndex();
        }

        private void bt_pre10_Click(object sender, EventArgs e)
        {
            if (this.CurrentPageIndex > 10)
                this.CurrentPageIndex -= 10;
            else this.CurrentPageIndex = 0;
            this.lb_pageIndex.Text = this.CurrentPageIndex.ToString();
            this.ShowPageIndex();
        }

        private void bt_pre_Click(object sender, EventArgs e)
        {
            if (this.CurrentPageIndex > 0)
                this.CurrentPageIndex -= 1;
            else this.CurrentPageIndex = 0;
            this.lb_pageIndex.Text = this.CurrentPageIndex.ToString();
            this.ShowPageIndex();
        }

        private void bt_next_Click(object sender, EventArgs e)
        {
            this.CurrentPageIndex += 1;
            this.lb_pageIndex.Text = this.CurrentPageIndex.ToString();
            this.ShowPageIndex();
        }

        private void bt_next10_Click(object sender, EventArgs e)
        {
            this.CurrentPageIndex += 10;
            this.lb_pageIndex.Text = this.CurrentPageIndex.ToString();
            this.ShowPageIndex();
        }

        private void bt_next100_Click(object sender, EventArgs e)
        {
            this.CurrentPageIndex += 100;
            this.lb_pageIndex.Text = this.CurrentPageIndex.ToString();
            this.ShowPageIndex();
        }
        void ShowPageIndex()
        {
            var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
            if (bizPlugin != default)
            {
                var hashPage = bizPlugin.GetDiggPageHash(this.EngraveTx.ET.Hash, this.CurrentPageIndex);
                if (hashPage.IsNotNull())
                {
                    this.DoInvoke(() =>
                    {
                        this.RoundPanel.Controls.Clear();
                        List<Digg> list = new List<Digg>();
                        foreach (var sh in hashPage.Hashes)
                        {
                            var tx = Blockchain.Singleton.GetTransaction(sh);
                            if (tx is EventTransaction et)
                            {
                                if (et.EventType == EventType.Digg)
                                {
                                    var digg = et.Data.AsSerializable<Digg>();
                                    if (digg.IsNotNull())
                                    {
                                        list.Add(digg);
                                    }
                                }
                            }
                        }
                        foreach (var digg in list.OrderByDescending(m => m.Timestamp))
                        {
                            appendDigg(digg);
                        }
                        this.RoundPanel_SizeChanged(this.RoundPanel, System.EventArgs.Empty);
                    });
                }
            }
        }
        void appendDigg(Digg digg)
        {
            DarkLabel label = new DarkLabel();
            var time = digg.Timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm");
            label.Text = $"[{time}]{digg.Message}";
            label.Tag = digg;
            label.Cursor = Cursors.Hand;
            label.Margin = new Padding() { Bottom = 3, Top = 12 };
            //label.Click += Label_Click;
            this.RoundPanel.Controls.Add(label);
        }
        void ResetPageIndex()
        {
            var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
            if (bizPlugin != default)
            {
                var pageState = bizPlugin.GetDiggPageState(this.EngraveTx.ET.Hash);
                if (pageState.IsNull()) pageState = new DiggPageState();
                this.CurrentPageIndex = pageState.LastPageIndex;
                this.lb_pageIndex.Text = this.CurrentPageIndex.ToString();
                ShowPageIndex();
            }
        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_newDigg_Click(object sender, EventArgs e)
        {
            if (!this.EngraveTx.EG.IsOpen) return;
            //Create Digg
            using (NewDigg dialog = new NewDigg(this.Operater, this.EngraveTx))
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var tx = dialog.GetTransaction(out UInt160 from);
                    if (tx.IsNotNull() && this.Operater.Wallet.IsNotNull())
                    {
                        this.Operater.Wallet.MixBuildAndRelaySingleOutputTransaction(tx, from, tx =>
                        {
                            string msg = $"{UIHelper.LocalString("创建评论交易已广播", "Relay event transaction completed")}   {tx.Hash}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                        });
                    }                     
                }
            }
        }
    }
}
