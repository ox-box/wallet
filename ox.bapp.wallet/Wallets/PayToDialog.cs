using OX.Wallets;
using OX.Wallets.Base.Wallets;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace OX.Wallets.Base
{
    internal partial class PayToDialog : OX.Wallets.UI.Forms.DarkForm
    {
        INotecase Operater;
        public PayToDialog(INotecase operater, AssetDescriptor asset = null, UInt160 scriptHash = null)
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("支付", "Payment");
            this.label1.Text = UIHelper.LocalString("对方账户", "Pay to");
            this.label1.ForeColor = Color.White;
            this.label2.Text = UIHelper.LocalString("金额", "Amount");
            this.label2.ForeColor = Color.White;
            this.label3.Text = UIHelper.LocalString("资产", "Asset");
            this.label3.ForeColor = Color.White;
            this.label4.Text = UIHelper.LocalString("余额", "Balance");
            this.label4.ForeColor = Color.White;
            this.lb_assetName.Text = UIHelper.LocalString("资产Id", "Asset Id");
            this.lb_assetName.ForeColor = Color.White;
            this.button1.Text = UIHelper.LocalString("确定", "OK");
            this.Operater = operater;
            if (asset == null)
            {
                foreach (UInt256 asset_id in operater.Wallet.FindUnspentCoins().Select(p => p.Output.AssetId).Distinct())
                {
                    comboBox1.Items.Add(new AssetDescriptor(asset_id));
                }
            }
            else
            {
                comboBox1.Items.Add(asset);
                comboBox1.SelectedIndex = 0;
                comboBox1.Enabled = false;
            }
            if (scriptHash != null)
            {
                textBox1.Text = scriptHash.ToAddress();
                textBox1.ReadOnly = true;
            }
        }

        public TxOutListBoxItem GetOutput()
        {
            AssetDescriptor asset = (AssetDescriptor)comboBox1.SelectedItem;
            return new TxOutListBoxItem
            {
                AssetName = asset.AssetName,
                AssetId = asset.AssetId,
                Value = BigDecimal.Parse(textBox2.Text, asset.Decimals),
                ScriptHash = textBox1.Text.ToScriptHash()
            };
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is AssetDescriptor asset)
            {
                textBox3.Text = this.Operater.Wallet.GetAvailable(asset.AssetId).ToString();
                this.tb_assetName.Text = asset.AssetId.ToString();
            }
            else
            {
                textBox3.Text = String.Empty;
                this.tb_assetName.Text = String.Empty;
            }
            textBox_TextChanged(this, EventArgs.Empty);
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex < 0 || textBox1.TextLength == 0 || textBox2.TextLength == 0)
            {
                button1.Enabled = false;
                return;
            }
            try
            {
                textBox1.Text.ToScriptHash();
            }
            catch (FormatException)
            {
                button1.Enabled = false;
                return;
            }
            AssetDescriptor asset = (AssetDescriptor)comboBox1.SelectedItem;
            if (!BigDecimal.TryParse(textBox2.Text, asset.Decimals, out BigDecimal amount))
            {
                button1.Enabled = false;
                return;
            }
            if (amount.Sign <= 0)
            {
                button1.Enabled = false;
                return;
            }
            button1.Enabled = true;
        }
    }
}
