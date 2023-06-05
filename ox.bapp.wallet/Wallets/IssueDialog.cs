using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets.UI.Forms;
using OX.Wallets.UI.Controls;
using System;
using System.Linq;
using System.Windows.Forms;

namespace OX.Wallets.Base
{
    internal partial class IssueDialog : DarkDialog
    {
        INotecase operater;
        public IssueDialog(INotecase notecase, AssetState asset = null)
        {
            this.operater = notecase;
            this.DialogButtons = DarkDialogButton.OkCancel;
            this.btnCancel.Click += BtnCancel_Click;
            InitializeComponent();
            this.txOutListBox1.Operater = notecase;
            this.Text = UIHelper.LocalString("资产分发", "Asset Distribution");
            this.btnOk.Text = UIHelper.LocalString("确定", "OK");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            this.groupBox1.Text = UIHelper.LocalString("资产 Id", "Asset Id");
            this.groupBox2.Text = UIHelper.LocalString("资产详情", "Asset Details");
            this.groupBox3.Text = UIHelper.LocalString("分发", "Distribution");
            this.label2.Text = UIHelper.LocalString("发行者:", "Owner:");
            this.label3.Text = UIHelper.LocalString("管理者:", "Admin:");
            this.label4.Text = UIHelper.LocalString("总量:", "Cap:");
            this.label5.Text = UIHelper.LocalString("已发行:", "Issued:");
            this.lb_assetName.Text = UIHelper.LocalString("资产名称", "Asset Name:");
            if (asset != null)
            {
                textBox5.Text = asset.AssetId.ToString();
                textBox5.Enabled = false;
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public IssueTransaction GetTransaction()
        {
            if (txOutListBox1.Asset == null) return null;
            return this.operater.Wallet.MakeTransaction(new IssueTransaction
            {
                Version = 1,
                Outputs = txOutListBox1.Items.GroupBy(p => p.ScriptHash).Select(g => new TransactionOutput
                {
                    AssetId = (UInt256)txOutListBox1.Asset.AssetId,
                    Value = g.Sum(p => new Fixed8((long)p.Value.Value)),
                    ScriptHash = g.Key
                }).ToArray()
            }, fee: Fixed8.One);
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            AssetState state;
            if (UInt256.TryParse(textBox5.Text, out UInt256 asset_id))
            {
                state = Blockchain.Singleton.Store.GetAssets().TryGet(asset_id);
                txOutListBox1.Asset = new AssetDescriptor(asset_id);
            }
            else
            {
                state = null;
                txOutListBox1.Asset = null;
            }
            if (state == null)
            {
                this.tb_assetName.Text = string.Empty;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                groupBox3.Enabled = false;
            }
            else
            {
                this.tb_assetName.Text = state.GetName();
                textBox1.Text = state.Owner.ToString();
                textBox2.Text = state.Admin.ToAddress();
                textBox3.Text = state.Amount == -Fixed8.Satoshi ? "+\u221e" : state.Amount.ToString();
                textBox4.Text = state.Available.ToString();
                groupBox3.Enabled = true;
            }
            txOutListBox1.Clear();
        }

        private void txOutListBox1_ItemsChanged(object sender, EventArgs e)
        {
            this.btnOk.Enabled = txOutListBox1.ItemCount > 0;
        }
    }
}
