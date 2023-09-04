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
using OX.Cryptography.ECC;
using OX.Network.P2P.Payloads;
using OX.SmartContract;


namespace OX.Wallets.Base
{
    public partial class DialogLockTransfer : DarkDialog
    {
        INotecase Operater;
        public DialogLockTransfer()
        {
            InitializeComponent();
            btnOk.Text = UIHelper.LocalString("确定", "OK");
            btnOk.Enabled = false;
            this.darkLabel1.Text = UIHelper.LocalString("资产类型:", "Asset Type:");
            this.darkLabel2.Text = UIHelper.LocalString("余额:", "Balance:");
            this.darkLabel3.Text = UIHelper.LocalString("收款公钥:", "Public Key:");
            this.darkLabel5.Text = UIHelper.LocalString("锁定类型:", "Lock Type:");
            this.rbTime.Text = UIHelper.LocalString("解锁时间:", "Unlock Time:");
            this.rbBlock.Text = UIHelper.LocalString("解锁区块:", "Unlock Block:");
        }
        /// <summary>
        /// 1:GTC,2:GTS
        /// </summary>
        public int AssetType
        {
            get
            {
                return rbGTC.Checked ? 1 : 2;
            }

        }
        public UInt160 From;
        public DialogLockTransfer(INotecase operater, UInt160 from = null) : this()
        {
            this.Operater = operater;
            this.From = from;
        }

        public TransactionOutput GetOutput(out ECPoint ecp, out bool isTime, out uint expiration)
        {
            UInt256 AssetID = SelectedAssetID();
            ecp = ECPoint.Parse(textBox1.Text, ECCurve.Secp256r1);
            isTime = this.rbTime.Checked;
            if (isTime)
            {
                expiration = this.dtp_time.Value.ToTimestamp();
            }
            else
            {
                expiration = uint.Parse(this.tb_block.Text);
            }
            return new TransactionOutput
            {
                AssetId = AssetID,
                Value = new BigDecimal(Fixed8.Parse(textBox2.Text).GetData(), 8).ToFixed8(),
                ScriptHash = Contract.CreateSignatureRedeemScript(ecp).ToScriptHash()
            };
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.TextLength == 0)
            {
                btnOk.Enabled = false;
                return;
            }
            try
            {
                this.darkLabel6.Text = String.Empty;
                var ecp = ECPoint.Parse(textBox1.Text, ECCurve.Secp256r1);
                this.darkLabel6.Text = Contract.CreateSignatureRedeemScript(ecp).ToScriptHash().ToAddress();
            }
            catch (FormatException)
            {
                btnOk.Enabled = false;
                return;
            }
            if (textBox2.TextLength == 0)
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
            UInt256 assetid = SelectedAssetID();
            textBox3.Text = this.From == null ? this.Operater.Wallet.GetAvailable(assetid).ToString() : this.Operater.Wallet.GeAccountAvailable(this.From, assetid).ToString();
            textBox_TextChanged(this, EventArgs.Empty);
        }
        private UInt256 SelectedAssetID()
        {
            if (this.AssetType == 1)
            {
                return Blockchain.OXC;
            }
            else
            {
                return Blockchain.OXS;
            }
        }

        private void PayToDialog_Load(object sender, EventArgs e)
        {
            RefreshBalance();
        }

        private void rbTime_CheckedChanged(object sender, EventArgs e)
        {
            this.dtp_time.Visible = this.rbTime.Checked;
            this.tb_block.Visible = this.rbBlock.Checked;
        }

        private void tb_block_TextChanged(object sender, EventArgs e)
        {
            var tb = sender as UI.Controls.DarkTextBox;
            if (!uint.TryParse(tb.Text, out uint index))
            {
                var s = tb.Text;
                if (s.Length > 0)
                {
                    s = s.Substring(0, s.Length - 1);
                    tb.Clear();
                    tb.AppendText(s);
                }
            }
        }
    }
}
