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
using OX.Wallets.Base.Wallets;

namespace OX.Wallets.Base
{
    public partial class DialogSingleTokenPayTo : DarkDialog
    {
        INotecase Operater;
        public DialogSingleTokenPayTo()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("私营资产转帐", "Token Transfer");
            this.lb_assetName.Text = UIHelper.LocalString("资产名称:", "Asset Name:");
            this.lb_assetId.Text = UIHelper.LocalString("资产Id:", "Asset Id:");
            this.darkLabel2.Text = UIHelper.LocalString("余额:", "Balance:");
            this.darkLabel3.Text = UIHelper.LocalString("收款地址:", "To:");
            this.darkLabel4.Text = UIHelper.LocalString("金额:", "Amount:");
            btnOk.Text = UIHelper.LocalString("确定", "OK");
            btnOk.Enabled = false;
        }

        public UInt160 From;
        public UInt256 AssetId;
        public string AssetName = string.Empty;
        public DialogSingleTokenPayTo(INotecase operater, UInt256 assetId, UInt160 from = null, UInt160 to = null) : this()
        {
            this.Operater = operater;
            this.From = from;
            this.AssetId = assetId;
            if (to.IsNotNull())
            {
                textBox1.Text = to.ToAddress();
                textBox1.ReadOnly = true;
            }
        }
        public DialogSingleTokenPayTo(INotecase operater, TransferRequest transferRequest) : this()
        {
            this.Operater = operater;
            this.From = transferRequest.From;
            if (transferRequest.To.IsNotNull())
            {
                textBox1.Text = transferRequest.To.ToAddress();
                textBox1.ReadOnly = true;
            }
            this.AssetId = transferRequest.Asset;
            if (transferRequest.Amount != default)
            {
                textBox2.Text = transferRequest.Amount.ToString();
            }
        }
        public TxOutListBoxItem GetOutput()
        {
            return new TxOutListBoxItem
            {
                AssetName = AssetName,
                AssetId = this.AssetId,
                Value = new BigDecimal(Fixed8.Parse(textBox2.Text).GetData(), 8),
                ScriptHash = OX.Wallets.Wallet.ToScriptHash(textBox1.Text)
            };
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0 || textBox2.TextLength == 0)
            {
                btnOk.Enabled = false;
                return;
            }
            try
            {
                OX.Wallets.Wallet.ToScriptHash(textBox1.Text);
            }
            catch (FormatException)
            {
                btnOk.Enabled = false;
                return;
            }
            if (!Fixed8.TryParse(textBox2.Text, out Fixed8 amount))
            {
                btnOk.Enabled = false;
                return;
            }
            if (amount == Fixed8.Zero)
            {
                btnOk.Enabled = false;
                return;
            }
            btnOk.Enabled = true;
        }

        private void rbGTC_CheckedChanged(object sender, EventArgs e)
        {
            RefreshBalance();
        }
        private void RefreshBalance()
        {
            textBox3.Text = this.From == null ? this.Operater.Wallet.GetAvailable(this.AssetId).ToString() : this.Operater.Wallet.GeAccountAvailable(this.From, this.AssetId).ToString();
            textBox_TextChanged(this, EventArgs.Empty);
        }


        private void PayToDialog_Load(object sender, EventArgs e)
        {
            var assetState = Blockchain.Singleton.GetSnapshot().Assets.TryGet(this.AssetId);
            if (assetState.IsNotNull())
            {
                AssetName = assetState.GetName();
                this.lb_assetName_v.Text = AssetName;
                this.darkTextBox1.Text = this.AssetId.ToString();
                RefreshBalance();
            }
        }
    }
}
