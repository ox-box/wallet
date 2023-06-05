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
    public partial class NewDigg : DarkDialog, INotecaseTrigger, IModuleComponent
    {
        INotecase Operater;
        EngraveTx EngraveTx;
        public Module Module { get; set; }
        public NewDigg(INotecase operater, EngraveTx engraveTx)
        {
            InitializeComponent();
            this.Operater = operater;
            this.EngraveTx = engraveTx;
        }


        private void NewDigg_Load(object sender, EventArgs e)
        {
            var time = this.EngraveTx.EG.Timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm");
            this.Text = $"[{time}]{this.EngraveTx.EG.Title}";
            this.lb_name.Text = UIHelper.LocalString("评价:", "Comment:");
            this.lb_from.Text = UIHelper.LocalString("作者:", "Author:");
            this.lb_reward.Text = UIHelper.LocalString("赏金 OXC:", "Reward OXC:");
            this.btnOk.Text = UIHelper.LocalString("确定", "OK");
            initAccounts();
        }
        public EventTransaction GetTransaction(out UInt160 from)
        {
            from = default;
            if (!Fixed8.TryParse(this.tb_reward.Text, out Fixed8 amt) || amt < Fixed8.One) return default;
            if (this.tb_name.Text.IsNullOrEmpty() || this.tb_name.Text.Trim().IsNullOrEmpty()) return default;
            Digg digg = new Digg()
            {
                EngraveId = this.EngraveTx.ET.Hash,
                AtDiggId = UInt256.Zero,
                DiggType = DiggType.Up,
                Timestamp = DateTime.Now.ToTimestamp(),
            };
            var body = this.tb_name.Text;
            if (body.IsNotNullAndEmpty()) digg.Message = body.Trim();
            from = this.cbAccounts.Text.ToScriptHash();
            EventTransaction tx = new EventTransaction()
            {
                EventType = EventType.Digg,
                ScriptHash = from,
                Data = digg.ToArray()
            };
            tx.Outputs = new TransactionOutput[] { new TransactionOutput() { AssetId = Blockchain.OXC, ScriptHash = this.EngraveTx.ET.ScriptHash, Value = amt } };
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
                        this.cbAccounts.Items.Add(act.Address);
                    }
                    this.cbAccounts.SelectedIndex = 0;
                });
            }
        }

        private void tb_reward_TextChanged(object sender, EventArgs e)
        {
            var s = this.tb_reward.Text;
            if (!Fixed8.TryParse(s, out Fixed8 amt) || amt < Fixed8.FromDecimal(0.09M))
            {
                if (s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    this.tb_reward.Clear();
                    this.tb_reward.AppendText(s);
                }
            }
        }
    }
}
