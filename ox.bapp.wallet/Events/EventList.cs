using OX.Wallets.UI.Config;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;
using OX.Wallets.UI.Forms;
using OX.Bapps;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets.NEP6;
using System.Drawing.Imaging;
using OX.Wallets.UI;
using OX.Persistence;
using OX.IO;
using System.Collections;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System;
using OX.Wallets.Base.Events;
using OX.Wallets.Base.Wallets;

namespace OX.Wallets.Base
{
    public partial class EventList : DarkDocument, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        protected INotecase Operater;
        BoardKey Key;
        Board Board;
        protected uint CurrentPageIndex;
        #region Constructor Region

        public EventList()
        {
            InitializeComponent();
            this.bt_pre.Text = UIHelper.LocalString("< 上 1 页", "< Previous");
            this.bt_pre10.Text = UIHelper.LocalString("< 上 10 页", "< Previous 10");
            this.bt_pre100.Text = UIHelper.LocalString("< 上 100 页", "< Previous 100");
            this.bt_next.Text = UIHelper.LocalString("下 1 页 >", "Next >");
            this.bt_next10.Text = UIHelper.LocalString("下 10 页 >", "Next 10 >");
            this.bt_next100.Text = UIHelper.LocalString("下 100 页 >", "Next 100 >");

        }

        public EventList(BoardKey key, Board board)
            : this()
        {
            this.Key = key;
            this.Board = board;
            this.DockText = this.Board.Name;
            this.lb_boardName.Text = $"{ this.Key.ToKey()}:{ this.Board.Name}";
            this.tb_remark.Text = this.Board.Remark;
            this.RoundPanel.SizeChanged += RoundPanel_SizeChanged;
            this.SizeChanged += GameRoom_SizeChanged;
        }

        private void GameRoom_SizeChanged(object sender, EventArgs e)
        {
        }

        protected virtual void RoundPanel_SizeChanged(object sender, System.EventArgs e)
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
                //if (ctrl is DarkButton db)
                //{
                //    db.Width = w;
                //    db.Margin = new Padding(5, 5, 0, 0);
                //}
            }
        }

        #endregion

        #region Event Handler Region

        public override void Close()
        {
            var result = DarkMessageBox.ShowWarning(UIHelper.LocalString($"确定要退出事件板{this.Board.Name}吗?", $"Are you sure you want to exit the event board{this.Board.Name}?"), UIHelper.LocalString("退出事件板", "exit event board"), DarkDialogButton.YesNo);
            if (result == DialogResult.No)
                return;
            base.Close();
        }

        #endregion

        public void ResetPageIndex()
        {
            var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
            if (bizPlugin != default)
            {
                var pageState = bizPlugin.GetEngravePageState(this.Key);
                if (pageState.IsNull()) pageState = new EngravePageState();
                this.CurrentPageIndex = pageState.LastPageIndex;
                this.lb_pageIndex.Text = this.CurrentPageIndex.ToString();
                ShowPageIndex();
            }


        }
        public void ShowPageIndex()
        {
            var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
            if (bizPlugin != default)
            {
                var hashPage = bizPlugin.GetEngravePageHash(this.Key, this.CurrentPageIndex);
                if (hashPage.IsNotNull())
                {
                    this.DoInvoke(() =>
                    {
                        this.RoundPanel.Controls.Clear();
                        List<EngraveTx> list = new List<EngraveTx>();
                        foreach (var sh in hashPage.Hashes)
                        {
                            var tx = Blockchain.Singleton.GetTransaction(sh);
                            if (tx is EventTransaction et)
                            {
                                if (et.EventType == EventType.Engrave)
                                {
                                    var engrave = et.Data.AsSerializable<Engrave>();
                                    if (engrave.IsNotNull())
                                    {
                                        list.Add(new EngraveTx() { EG = engrave, ET = et });
                                    }
                                }
                            }
                        }
                        foreach (var egnraveTx in list.OrderByDescending(m => m.EG.Timestamp))
                        {
                            appendEngrave(egnraveTx);
                        }
                        this.RoundPanel_SizeChanged(this.RoundPanel, System.EventArgs.Empty);
                    });
                }
            }
        }
        void appendEngrave(EngraveTx engraveTx)
        {
            DarkLabel label = new DarkLabel();
            var time = engraveTx.EG.Timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm");
            label.Text = $"[{time}]{engraveTx.EG.Title}";
            label.Tag = engraveTx;
            label.Cursor = Cursors.Hand;
            label.Margin = new Padding() { Bottom = 3, Top = 12 };
            label.Click += Label_Click;
            this.RoundPanel.Controls.Add(label);
        }

        private void Label_Click(object sender, EventArgs e)
        {
            DarkLabel lb = sender as DarkLabel;
            EngraveTx engraveTx = lb.Tag as EngraveTx;
            if (this.Module is EventModule em)
            {
                em.OpenEvent(engraveTx);
            }
        }
        #region IBlockChainTrigger

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
        public virtual void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            this.ResetPageIndex();
        }
        public void OnRebuild()
        {
        }
        #endregion



        private void bt_pre_Click(object sender, System.EventArgs e)
        {
            if (this.CurrentPageIndex > 0)
                this.CurrentPageIndex -= 1;
            else this.CurrentPageIndex = 0;
            this.lb_pageIndex.Text = this.CurrentPageIndex.ToString();
            this.ShowPageIndex();
        }

        private void bt_next_Click(object sender, System.EventArgs e)
        {
            this.CurrentPageIndex += 1;
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

        private void bt_pre100_Click(object sender, EventArgs e)
        {
            if (this.CurrentPageIndex > 100)
                this.CurrentPageIndex -= 100;
            else this.CurrentPageIndex = 0;
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

        private void tb_remark_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
