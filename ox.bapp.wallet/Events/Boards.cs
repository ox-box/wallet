using OX.Wallets.UI.Config;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;
using OX.Wallets.UI.Forms;
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
using OX.Bapps;
using OX.Wallets.Base.Events;
using OX.Wallets.Base.Wallets;

namespace OX.Wallets
{
    public partial class Boards : DarkDocument, INotecaseTrigger, IModuleComponent
    {
        //static Dictionary<uint, RoomLine> Lines = new Dictionary<uint, RoomLine>();
        public Module Module { get; set; }
        protected INotecase Operater;
        protected uint CurrentIndex;
        #region Constructor Region

        public Boards()
        {
            InitializeComponent();
            this.bt_pre.Text = UIHelper.LocalString("< 上 1 段", "< Previous");
            this.bt_pre10.Text = UIHelper.LocalString("< 上 10 段", "< Previous 10");
            this.bt_next.Text = UIHelper.LocalString("下 1 段 >", "Next >");
            this.bt_next10.Text = UIHelper.LocalString("下 10 段 >", "Next 10 >");
            this.cb_auto.Text = UIHelper.LocalString("自动定位到最近", "Auto Focus Current");
            this.DockText = UIHelper.LocalString("事件板", "Event Boards");
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
        }

        #endregion

        #region Event Handler Region

        public override void Close()
        {
            base.Close();
        }

        #endregion

        public void ResetIndex()
        {
            var index = Blockchain.Singleton.Height;
            var c = index % 100000;
            if (c > 0)
                index = index - c + 100000;
            if (this.CurrentIndex != index)
            {
                this.CurrentIndex = index;
                this.lb_index.Text = this.CurrentIndex.ToString();
                ShowIndex();
            }
        }
        public void ShowIndex()
        {
            this.RoundPanel.Controls.Clear();
            var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
            if (bizPlugin != default)
            {
                var boards = bizPlugin.GetRangeBoards(this.CurrentIndex);
                foreach (var r in boards.OrderBy(m => m.Key.BoardTxIndex))
                {
                    var tx = Blockchain.Singleton.GetTransaction(r.Value);
                    if (tx.IsNull()) return;
                    if (tx is EventTransaction et && et.EventType == EventType.Board)
                    {
                        var board = et.Data.AsSerializable<Board>();
                        if (board.IsNull()) return;
                        BoardButton rhb = new BoardButton(this.Operater, r.Key, board);
                        this.RoundPanel.Controls.Add(rhb);
                    }
                }
            }
            this.RoundPanel_SizeChanged(this.RoundPanel, System.EventArgs.Empty);
        }

        #region IBlockChainTrigger
        public void OnBappEvent(BappEvent bet)
        {
            if (bet.ContainEventType(WalletBappEventType.EventTransactionEvent, out BappEventItem[] eventItems))
            {
                this.DoInvoke(() => { ShowIndex(); });
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
            this.DoInvoke(() =>
            {
                if (this.cb_auto.Checked)
                {
                    ResetIndex();
                }
            });
        }
        public void ChangeWallet(INotecase operater)
        {
            bool needResetIndex = false;
            if (this.Operater.IsNull())
            {
                needResetIndex = true;
            }
            this.Operater = operater;
            if (needResetIndex)
                this.ResetIndex();
        }
        public void OnRebuild()
        {
        }
        #endregion

        private void cb_auto_CheckedChanged(object sender, System.EventArgs e)
        {
            if (sender is DarkCheckBox cb)
            {
                if (cb.Checked)
                {
                    ResetIndex();
                }
            }
        }

        private void bt_pre_Click(object sender, System.EventArgs e)
        {
            this.cb_auto.Checked = false;
            if (this.CurrentIndex > 100000)
                this.CurrentIndex -= 100000;
            else
                this.CurrentIndex = 0;
            this.lb_index.Text = this.CurrentIndex.ToString();
            this.ShowIndex();
        }

        private void bt_next_Click(object sender, System.EventArgs e)
        {
            this.cb_auto.Checked = false;
            this.CurrentIndex += 100000;
            this.lb_index.Text = this.CurrentIndex.ToString();
            this.ShowIndex();
        }



        private void bt_pre10_Click(object sender, EventArgs e)
        {
            this.cb_auto.Checked = false;
            if (this.CurrentIndex > 100000 * 10)
                this.CurrentIndex -= 100000 * 10;
            this.CurrentIndex = 0;
            this.lb_index.Text = this.CurrentIndex.ToString();
            this.ShowIndex();
        }



        private void bt_next10_Click(object sender, EventArgs e)
        {
            this.cb_auto.Checked = false;
            this.CurrentIndex += 100000 * 10;
            this.lb_index.Text = this.CurrentIndex.ToString();
            this.ShowIndex();
        }


    }
}
