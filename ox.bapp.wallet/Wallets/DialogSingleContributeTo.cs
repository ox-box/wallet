using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using OX.Wallets;
using OX.Ledger;
using OX.Network.P2P.Payloads;



namespace OX.Wallets.Base
{
    public partial class DialogSingleContributeTo : DarkDialog
    {
        public class RewardAmountItem
        {
            public RewardAmount RA;
            public uint RAValue;
            public override string ToString()
            {
                return RAValue.ToString();
            }
        }
        INotecase Operater;
        public DialogSingleContributeTo()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("捐献", "Contribute");
            this.darkLabel2.Text = UIHelper.LocalString("OXC余额:", "OXC Balance:");
            this.darkLabel4.Text = UIHelper.LocalString("捐赠金额:", "Contribute Amount:");
            btnOk.Text = UIHelper.LocalString("确定", "OK");
            btnOk.Enabled = false;
            this.cbAmount.Items.Add(new RewardAmountItem { RA = RewardAmount._10000, RAValue = 10000 });
            this.cbAmount.Items.Add(new RewardAmountItem { RA = RewardAmount._100000, RAValue = 100000 });
            this.cbAmount.Items.Add(new RewardAmountItem { RA = RewardAmount._1000000, RAValue = 1000000 });
            this.cbAmount.Items.Add(new RewardAmountItem { RA = RewardAmount._10000000, RAValue = 10000000 });
        }

        public UInt160 From;
        public DialogSingleContributeTo(INotecase operater, UInt160 from = null) : this()
        {
            this.Operater = operater;
            this.From = from;

        }

        public RewardAmount GetAmountKind()
        {
            if (cbAmount.SelectedItem.IsNotNull())
            {
                var item = cbAmount.SelectedItem as RewardAmountItem;
                return item.RA;
            }
            return RewardAmount._10000;
        }




        private void RefreshBalance()
        {
            textBox3.Text = this.From == null ? this.Operater.Wallet.GetAvailable(Blockchain.OXC_Token.Hash).ToString() : this.Operater.Wallet.GeAccountAvailable(this.From, Blockchain.OXC_Token.Hash).ToString();
        }

        private void PayToDialog_Load(object sender, EventArgs e)
        {
            RefreshBalance();
        }

        private void cbAmount_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = false;
            RefreshBalance();
            if (Fixed8.TryParse(this.textBox3.Text, out Fixed8 balance))
            {
                var item = cbAmount.SelectedItem as RewardAmountItem;
                if (item.IsNotNull())
                {
                    if (balance >= Fixed8.One * item.RAValue)
                        btnOk.Enabled = true;
                }
            }
        }
    }
}
