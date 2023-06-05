using Akka.Actor;
using OX.IO.Actors;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.Wallets;
using OX.Bapps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OX.Wallets.UI.Forms;
using OX.Wallets.UI;

namespace OX.Wallets.Base
{
    public partial class DetainDialog : DarkDialog, INotecaseTrigger, IModuleComponent
    {
        INotecase Operater;
        WalletAccount Account;
        public Module Module { get; set; }
        public DetainDialog(WalletAccount account)
        {
            InitializeComponent();
            this.Account = account;
        }


        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("商业频道租赁", "Business Channel Lease");
            this.lb_address.Text = UIHelper.LocalString("地址:", "Address:");
            this.lb_state.Text = UIHelper.LocalString("状态:", "State:");
            this.lb_expireIndex.Text = UIHelper.LocalString("到期高度:", "Expire Index:");
            this.lb_DurationIndex.Text = UIHelper.LocalString("租赁周期:", "Lease Range:");
            this.lb_oxsBalance.Text = UIHelper.LocalString("OXS扣押余额:", "Detain OXS:");
            this.lb_detainState.Text = UIHelper.LocalString("扣押状态:", "Detain Status:");
            this.lb_fee.Text = UIHelper.LocalString("手续费:", "Fee:");
            this.lb_AskFee.Text = UIHelper.LocalString("调用费:", "Call Fee:");
            this.darkTitle1.Text = UIHelper.LocalString("设置", "setting");
            this.lb_address_v.Text = this.Account.Address;
            this.cb_detainState.Items.Add(UIHelper.LocalString("冻结", "Freeze"));
            this.cb_detainState.Items.Add(UIHelper.LocalString("解冻", "UnFreeze"));
            this.cb_detainState.SelectedIndex = 0;
            bool isFrozen = Blockchain.Singleton.IsFrozen(this.Account.ScriptHash, out uint expireIndex);
            this.lb_state_v.Text = isFrozen ? UIHelper.LocalString("冻结", "Freeze") : UIHelper.LocalString("解冻", "UnFreeze");
            if (isFrozen)
            {
                this.lb_expireIndex_v.Text = expireIndex.ToString();
                bool isBiz = Blockchain.Singleton.VerifyBizValidator(this.Account.ScriptHash, out Fixed8 balance, out Fixed8 askFee);
                this.lb_oxsBalance_v.Text = $"{balance} OXS";
                this.tb_askFee_V.Text = (askFee.GetInternalValue() / Fixed8.OXU.GetInternalValue()).ToString();
            }

        }
        public DetainTransaction GetTransaction()
        {
            DetainTransaction tx;
            BuildTransaction(out tx);
            return tx;
        }
        bool BuildTransaction(out DetainTransaction tx)
        {
            try
            {


                var selectIndex = cb_detainState.SelectedIndex;
                DetainStatus state = DetainStatus.Freeze;
                uint d = 0, f = 0;
                if (selectIndex != 0)
                {
                    state = DetainStatus.UnFreeze;
                    this.lb_AskFee.Visible = false;
                    this.tb_askFee_V.Visible = false;
                    this.darkLabel1.Visible = false;
                    this.lb_DurationIndex.Visible = false;
                    this.tb_DurationIndex.Visible = false;
                }
                else
                {
                    this.lb_AskFee.Visible = true;
                    this.tb_askFee_V.Visible = true;
                    this.darkLabel1.Visible = true;
                    this.lb_DurationIndex.Visible = true;
                    this.tb_DurationIndex.Visible = true;
                    d = uint.Parse(this.tb_DurationIndex.Text);
                    if (d < 100)
                    {
                        tx = null;
                        this.btnOk.Enabled = false;
                        return false;
                    }
                    f = uint.Parse(this.tb_askFee_V.Text);
                    if (f > 1000)
                    {
                        tx = null;
                        this.btnOk.Enabled = false;
                        return false;
                    }
                }
                tx = new DetainTransaction(Account.ScriptHash)
                {
                    DetainDuration = d,
                    DetainState = state,
                    AskFee = Fixed8.OXU * f
                };
                this.lb_fee_v.Text = $"{tx.SystemFee} OXC";
                this.btnOk.Enabled = true;
                return true;
            }
            catch  
            {
                tx = null;
                this.btnOk.Enabled = false;
                return false;
            }
        }
        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }


        private void combo_address_TextChanged(object sender, EventArgs e)
        {
            BuildTransaction(out DetainTransaction tx);
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

        }
        public void OnRebuild()
        {
        }
        private void tb_DurationIndex_TextChanged(object sender, EventArgs e)
        {
            var s = tb_DurationIndex.Text;
            if (!uint.TryParse(s, out uint v))
            {
                if (s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    this.tb_DurationIndex.Clear();
                    this.tb_DurationIndex.AppendText(s);
                }
            }
            else
            {
                BuildTransaction(out DetainTransaction tx);
            }
        }

        private void tb_askFee_V_TextChanged(object sender, EventArgs e)
        {
            var s = tb_askFee_V.Text;
            if (!uint.TryParse(s, out uint v))
            {
                if (s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    this.tb_askFee_V.Clear();
                    this.tb_askFee_V.AppendText(s);
                }
            }
            else
            {
                BuildTransaction(out DetainTransaction tx);
            }
        }
    }
}
