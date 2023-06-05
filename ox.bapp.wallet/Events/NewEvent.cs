using Akka.Actor;
using OX.IO.Actors;
using OX.Ledger;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using OX.Wallets.UI.Forms;
using OX.Wallets.UI;
using OX.SmartContract;
using OX.IO;
using System.Xml;
using OX.Bapps;
using OX.Wallets.Base.Events;

namespace OX.Wallets.Base
{
    public partial class NewEvent : DarkDialog, INotecaseTrigger, IModuleComponent
    {
        INotecase Operater;
        Board Board;
        BoardKey Key;
        UInt160 ScriptHash;
        public Module Module { get; set; }
        public NewEvent(INotecase operater, BoardKey key, Board board, UInt160 sh = null)
        {
            InitializeComponent();
            this.Operater = operater;
            this.Board = board;
            this.Key = key;
            this.ScriptHash = sh;
        }


        private void NewEvent_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString($"在 {this.Key.ToKey()}:{this.Board.Name} 创建事件", $"Createn event from {this.Key.ToKey()}:{this.Board.Name}");
            this.lb_name.Text = UIHelper.LocalString("事件主题:", "Event Title:");
            this.lb_remark.Text = UIHelper.LocalString("事件正文:", "Event Body:");
            this.lb_from.Text = UIHelper.LocalString("作者:", "Author:");
            this.lb_Private.Text = UIHelper.LocalString("权限:", "Permission:");
            this.cb_Private.Text = UIHelper.LocalString("禁止评论", "No Comment");
            this.btnOk.Text = UIHelper.LocalString("发表", "Publish");
            initAccounts();
        }
        public EventTransaction GetTransaction(out UInt160 from)
        {
            from = default;
            if (this.tb_name.Text.IsNullOrEmpty() || this.tb_name.Text.Trim().IsNullOrEmpty()) return default;
            var body = this.tb_remark.Text;
            if (body.IsNotNullAndEmpty()) body = body.Trim();


            Engrave egrave = new Engrave()
            {
                BoardTxIndex = this.Key.BoardTxIndex,
                BoardTxPosition = this.Key.BoardTxPosition,
                Title = this.tb_name.Text.Trim(),
                Message = body,
                Timestamp = DateTime.Now.ToTimestamp(),
            };
            if (this.cb_Private.Checked)
            {
                egrave.Data = new byte[] { 0x01 };
            }
            from = this.cbAccounts.Text.ToScriptHash();
            EventTransaction tx = new EventTransaction()
            {
                EventType = EventType.Engrave,
                ScriptHash = from,
                Data = egrave.ToArray()
            };
            return tx;
        }
        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

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
            initAccounts();
        }
        public void OnRebuild()
        {
        }
        void initAccounts()
        {
            if (this.Operater.IsNotNull())
            {
                this.DoInvoke(() =>
                {
                    this.cbAccounts.Items.Clear();
                    foreach (var act in this.Operater.Wallet.GetHeldAccounts())
                    {
                        if (this.ScriptHash.IsNull() || this.ScriptHash.Equals(act.ScriptHash))
                            this.cbAccounts.Items.Add(act.Address);
                    }
                    this.cbAccounts.SelectedIndex = 0;
                });
            }
        }



    }
}
