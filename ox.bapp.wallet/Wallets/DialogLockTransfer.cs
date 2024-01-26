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
        public class AssetDesc
        {
            public AssetState AssetState;
            public override string ToString()
            {
                return $"{AssetState.GetName()}   /   {AssetState.AssetId.ToString()}";
            }
        }
        INotecase Operater;
        public DialogLockTransfer()
        {
            InitializeComponent();
            btnOk.Text = UIHelper.LocalString("确定", "OK");
            btnOk.Enabled = false;
            this.lb_asset.Text = UIHelper.LocalString("资产:", "Asset:");
            this.darkLabel2.Text = UIHelper.LocalString("余额:", "Balance:");
            this.darkLabel3.Text = UIHelper.LocalString("收款公钥:", "Public Key:");
            this.darkLabel5.Text = UIHelper.LocalString("锁定类型:", "Lock Type:");
            this.rbTime.Text = UIHelper.LocalString("解锁时间:", "Unlock Time:");
            this.rbBlock.Text = UIHelper.LocalString("解锁区块:", "Unlock Block:");
            this.cb_lockself.Text = UIHelper.LocalString("自主锁仓", "Lock Self");
        }

        WalletAccount Account;
        public DialogLockTransfer(INotecase operater, WalletAccount account) : this()
        {
            this.Operater = operater;
            this.Account = account;
        }
        private UInt256 SelectedAssetID(out string AssetName)
        {
            AssetName = string.Empty;
            var item = this.cb_assets.SelectedItem;
            if (item.IsNull()) return default;
            if (item is AssetDesc assetDesc)
            {
                return assetDesc.AssetState.AssetId;
            }
            return default;
        }
        public TransactionOutput GetOutput(out ECPoint ecp, out bool isTime, out uint expiration)
        {
            UInt256 AssetID = SelectedAssetID(out string assetName);
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

        private void RefreshBalance()
        {
            UInt256 assetid = SelectedAssetID(out string assetName);
            if (assetid.IsNotNull())
            {
                textBox3.Text = this.Operater.Wallet.GeAccountAvailable(this.Account.ScriptHash, assetid).ToString();
                textBox_TextChanged(this, EventArgs.Empty);
            }
        }


        private void PayToDialog_Load(object sender, EventArgs e)
        {
            var accountState =Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(this.Account.ScriptHash);
            if (accountState.IsNotNull())
            {
                foreach (var asset in Blockchain.Singleton.Store.GetAssets().Find().Where(m => accountState.Balances.ContainsKey(m.Key)).OrderByDescending(m => m.Key == Blockchain.OXS).ThenByDescending(m => m.Key == Blockchain.OXC))
                {
                    this.cb_assets.Items.Add(new AssetDesc { AssetState = asset.Value });
                }
            }
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

        private void cb_assets_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshBalance();
        }

        private void cb_lockself_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cb_lockself.Checked)
            {
                this.textBox1.ReadOnly = true;
                this.textBox1.Text = this.Account.GetKey().PublicKey.ToString();
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
