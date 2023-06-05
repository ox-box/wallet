using OX.Wallets;
using OX.Wallets.Base.Wallets;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OX.Wallets.Base
{
    internal partial class BulkPayDialog : OX.Wallets.UI.Forms.DarkForm
    {
        INotecase Operater;
        public BulkPayDialog(INotecase operater, AssetDescriptor asset = null)
        {
            InitializeComponent();
            this.Operater = operater;
            if (asset == null)
            {
                foreach (UInt256 asset_id in this.Operater.Wallet.FindUnspentCoins().Select(p => p.Output.AssetId).Distinct())
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
        }

        public TxOutListBoxItem[] GetOutputs()
        {
            AssetDescriptor asset = (AssetDescriptor)comboBox1.SelectedItem;
            return textBox1.Lines.Where(p => !string.IsNullOrWhiteSpace(p)).Select(p =>
            {
                string[] line = p.Split(new[] { ' ', '\t', ',' }, StringSplitOptions.RemoveEmptyEntries);
                return new TxOutListBoxItem
                {
                    AssetName = asset.AssetName,
                    AssetId = asset.AssetId,
                    Value = BigDecimal.Parse(line[1], asset.Decimals),
                    ScriptHash = line[0].ToScriptHash()
                };
            }).Where(p => p.Value.Value != 0).ToArray();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is AssetDescriptor asset)
            {
                textBox3.Text = this.Operater.Wallet.GetAvailable(asset.AssetId).ToString();
            }
            else
            {
                textBox3.Text = "";
            }
            textBox1_TextChanged(this, EventArgs.Empty);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            button1.Enabled = comboBox1.SelectedIndex >= 0 && textBox1.TextLength > 0;
        }
    }
}
