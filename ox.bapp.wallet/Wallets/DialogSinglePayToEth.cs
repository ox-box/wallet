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
using OX.Wallets.Eths;
using Nethereum.Util;

namespace OX.Wallets.Base
{
    public partial class DialogSinglePayToEth : DarkDialog
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
        public DialogSinglePayToEth()
        {
            InitializeComponent();
            btnOk.Text = UIHelper.LocalString("确定", "OK");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            this.lb_asset.Text = UIHelper.LocalString("资产:", "Asset:");
            this.lb_address.Text = UIHelper.LocalString("以太坊地址:", "Eth Address:");
            this.lb_amount.Text = UIHelper.LocalString("金额:", "Amount:");
            this.lb_balance.Text = UIHelper.LocalString("余额:", "Balance:");
            this.lb_lockindex.Text = UIHelper.LocalString("锁仓高度:", "Lock Height:");
            btnOk.Enabled = false;
            
        }

        public UInt160 From;
        public DialogSinglePayToEth(INotecase operater, UInt160 from = null) : this()
        {
            this.Operater = operater;
            this.From = from;
        }

        public TxOutListBoxItem GetOutput(out string ethAddress, out uint lockindex)
        {
            lockindex = 0;
            ethAddress = string.Empty;
            UInt256 AssetID = SelectedAssetID(out string AssetName);
            if (AssetID.IsNull()) return default;
            ethAddress = this.textBox1.Text;
            lockindex = uint.Parse(this.tb_lockIndex.Text);
            return new TxOutListBoxItem
            {
                AssetName = AssetName,
                AssetId = AssetID,
                Value = new BigDecimal(Fixed8.Parse(textBox2.Text).GetData(), 8),
                ScriptHash = ethAddress.BuildMapAddress(lockindex)
            };
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            this.lb_mapaddress.Text = string.Empty;
            if (textBox1.Text.IsNotNullAndEmpty() && textBox1.Text.IsValidEthereumAddressHexFormat())
            {
                var sh = this.textBox1.Text.BuildMapAddress();
                this.lb_mapaddress.Text = sh.ToAddress();
            }
            if (textBox1.TextLength == 0 || textBox2.TextLength == 0)
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
            UInt256 assetid = SelectedAssetID(out string AssetName);
            if (assetid.IsNotNull())
            {
                textBox3.Text = this.From == null ? this.Operater.Wallet.GetAvailable(assetid).ToString() : this.Operater.Wallet.GeAccountAvailable(this.From, assetid).ToString();
                textBox_TextChanged(this, EventArgs.Empty);
            }
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

        private void PayToDialog_Load(object sender, EventArgs e)
        {
            var accountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(this.From);
            if (accountState.IsNotNull())
            {
                foreach (var asset in Blockchain.Singleton.Store.GetAssets().Find().Where(m => accountState.Balances.ContainsKey(m.Key)).OrderByDescending(m => m.Key == Blockchain.OXS).ThenByDescending(m => m.Key == Blockchain.OXC))
                {
                    this.cb_assets.Items.Add(new AssetDesc { AssetState = asset.Value });
                }
            }
            RefreshBalance();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cb_assets_SelectedIndexChanged(object sender, EventArgs e)
        {
            RefreshBalance();
        }

        private void tb_lockIndex_TextChanged(object sender, EventArgs e)
        {
            if (!uint.TryParse(this.tb_lockIndex.Text, out var index))
            {
                this.tb_lockIndex.Text = "0";
            }
        }
    }
}
