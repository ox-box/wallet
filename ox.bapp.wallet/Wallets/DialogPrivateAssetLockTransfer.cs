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
    public partial class DialogPrivateAssetLockTransfer : DarkDialog
    {
        OpenWallet Wallet;
        WalletAccount Accout;
        UInt256 AssetId;
        AssetState AssetState;
        public DialogPrivateAssetLockTransfer()
        {
            InitializeComponent();
            btnOk.Text = UIHelper.LocalString("确定", "OK");
            btnOk.Enabled = false;
            this.lb_assetName.Text = UIHelper.LocalString("资产名称:", "Asset Name:");
            this.lb_assetId.Text = UIHelper.LocalString("资产Id:", "Asset Id:");
            this.lb_precision.Text = UIHelper.LocalString("精度", "Precision:");
            this.darkLabel2.Text = UIHelper.LocalString("余额:", "Balance:");
            this.darkLabel3.Text = UIHelper.LocalString("收款公钥:", "Public Key:");
            this.darkLabel5.Text = UIHelper.LocalString("锁定类型:", "Lock Type:");
            this.rbTime.Text = UIHelper.LocalString("时间锁定:", "Lock Time:");
            this.rbBlock.Text = UIHelper.LocalString("区块锁定:", "Lock Block:");
            this.cb_lockself.Text = UIHelper.LocalString("自主锁仓", "Lock Self");
        }

        public DialogPrivateAssetLockTransfer(OpenWallet wallet, WalletAccount account, UInt256 assetId) : this()
        {
            this.Wallet = wallet;
            this.Accout = account;
            this.AssetId = assetId;
            AssetState = Blockchain.Singleton.Store.GetAssets().TryGet(assetId);
            this.lb_assetName_v.Text = AssetState.GetName();
            this.lb_assetId_v.Text = assetId.ToString();
            this.lb_precision_v.Text = AssetState.Precision.ToString();
        }

        public TransactionOutput GetOutput(out ECPoint ecp, out bool isTime, out uint expiration)
        {
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
                AssetId = this.AssetId,
                Value = Fixed8.Parse(textBox2.Text),
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
            var p = Math.Pow(10, 8 - this.AssetState.Precision);
            var remind = amount.GetInternalValue() % p;
            if (remind > 0)
            {
                btnOk.Enabled = false;
                return;
            }
            btnOk.Enabled = true;
        }


        private void RefreshBalance()
        {
            var balance = this.Wallet.GeAccountAvailable(this.Accout.ScriptHash, this.AssetId).ToString();
            textBox3.Text = balance;
            textBox_TextChanged(this, EventArgs.Empty);
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

        private void cb_lockself_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_lockself.Checked)
            {
                this.textBox1.ReadOnly = true;
                this.textBox1.Text = this.Accout.GetKey().PublicKey.ToString();
            }
            else
            {
                this.textBox1.Text = string.Empty;
                this.darkLabel6.Text = string.Empty;
                this.textBox1.ReadOnly = false;
            }
        }
    }
}
