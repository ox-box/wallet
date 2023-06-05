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
using OX.Network.P2P.Payloads;

namespace OX.Wallets.Base
{
    public partial class DialogEthMapPayTo : DarkDialog
    {
        INotecase Operater;
        public DialogEthMapPayTo()
        {
            InitializeComponent();
            btnOk.Text = UIHelper.LocalString("确定", "OK");
            btnOk.Enabled = false;
        }

        public UInt160 From;
        public UInt256 AssetId;
        public string AssetName;
        Fixed8 Balance;
        public DialogEthMapPayTo(INotecase operater, UInt256 assetId, string assetName, Fixed8 balance, UInt160 from = null) : this()
        {
            this.Operater = operater;
            this.From = from;
            this.AssetId = assetId;
            this.AssetName = assetName;
            this.Balance = balance;
        }

        public TransactionOutput BuildOutput()
        {
            return new TransactionOutput
            {
                AssetId = this.AssetId,
                Value = Fixed8.Parse(textBox2.Text),
                ScriptHash = textBox1.Text.ToScriptHash(),
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





        private void PayToDialog_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("以太坊映射转帐", "Ethereum Map Transfer");
            this.lb_assetName.Text = UIHelper.LocalString("资产名称:", "Asset Name:");
            this.lb_assetId.Text = UIHelper.LocalString("资产Id:", "Asset Id:");
            this.lb_balance.Text = UIHelper.LocalString("资产余额:", "Asset Balance:");
            this.lb_to.Text = UIHelper.LocalString("目标地址:", "To Address:");
            this.lb_amount.Text = UIHelper.LocalString("金额:", "Amount:");
            this.lb_assetName_v.Text = this.AssetName;
            this.lb_assetId_v.Text = this.AssetId.ToString();
            this.textBox3.Text = this.Balance.ToString();

        }
    }
}
