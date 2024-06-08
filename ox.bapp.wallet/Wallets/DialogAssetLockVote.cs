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
using OX.Wallets.UI.Config;
using OX.Cryptography.ECC;
using OX.SmartContract;
using OX.Network.P2P.Payloads;
using Nethereum.Util;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using OX.Bapps;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Org.BouncyCastle.Cms;
using OX.IO;

namespace OX.Wallets.Base
{
    public partial class DialogAssetLockVote : DarkDialog
    {

        INotecase Operater;
        uint H;
        public AssetState Asset;
        public string AssetName;

        public DialogAssetLockVote()
        {
            InitializeComponent();

            btnOk.Text = UIHelper.LocalString("确定", "OK");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            this.lb_asset.Text = UIHelper.LocalString("资产:", "Asset:");
            this.lb_amount.Text = UIHelper.LocalString("金额:", "Amount:");
            this.lb_balance.Text = UIHelper.LocalString("可用余额:", "Available Balance:");
            this.darkLabel1.Text = UIHelper.LocalString("锁仓高度:", "Lock Block:");
            btnOk.Enabled = false;
        }

        public DialogAssetLockVote(INotecase operater, AssetState asset) : this()
        {
            this.Operater = operater;
            this.Asset = asset;
        }


        public Transaction BuildTransaction(out WalletAccount account)
        {
            var h = uint.Parse(this.tb_expire.Text);
            AccountListItem ali = this.cb_accounts.SelectedItem as AccountListItem;
            account = ali.Account;
            LockAssetTransaction lat = new LockAssetTransaction
            {
                LockContract = Blockchain.LockAssetContractScriptHash,
                IsTimeLock = false,
                LockExpiration = h,
                Recipient = ali.Account.GetKey().PublicKey,
                Purpose = LockAssetPurpose.DaoVote
            };
            lat.Attach = new DaoVote { Index = h, AssetId = this.Asset.AssetId }.ToArray();
            Fixed8.TryParse(tb_amount.Text, out Fixed8 amount);
            lat.Outputs = new TransactionOutput[] { new TransactionOutput { AssetId = this.Asset.AssetId, ScriptHash = lat.GetContract().ScriptHash, Value = amount } };
            return lat;
        }


        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (!Fixed8.TryParse(tb_amount.Text, out Fixed8 amount))
            {
                btnOk.Enabled = false;
                return;
            }
            if (!Fixed8.TryParse(tb_balance.Text, out Fixed8 balance))
            {
                btnOk.Enabled = false;
                return;
            }
            if (!uint.TryParse(tb_expire.Text, out uint expire) || expire % 10000 > 0 || expire == 0)
            {
                btnOk.Enabled = false;
                return;
            }
            if (amount > balance)
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
            AccountListItem ali = this.cb_accounts.SelectedItem as AccountListItem;
            if (ali.IsNotNull())
            {
                if (this.Operater.Wallet.TryGetWalletAccountBalance(ali.Account.ScriptHash, out Dictionary<UInt256, WalletAccountBalance> balances) && balances.TryGetValue(this.Asset.AssetId, out WalletAccountBalance balance))
                {
                    tb_balance.Text = balance.AvailableBalance.ToString();
                    textBox_TextChanged(this, EventArgs.Empty);
                }
            }
        }


        private void PayToDialog_Load(object sender, EventArgs e)
        {
            this.AssetName = Asset.GetName();
            this.Text = UIHelper.LocalString($"{this.AssetName}  资产锁仓投票", $"{this.AssetName}   Asset Lock Vote");
            this.tb_expire.Text = this.H.ToString();
            var daoVoteList = Blockchain.Singleton.CurrentSnapshot.DaoVoteList.TryGet(this.Asset.AssetId);
            if (daoVoteList.IsNotNull() && daoVoteList.Votes.IsNotNullAndEmpty())
            {
                foreach (var vote in daoVoteList.Votes.OrderByDescending(m => m.Key))
                {
                    var itm = new DarkListItem($"{vote.Key}         {vote.Value} {this.AssetName}");
                    itm.Tag = vote;
                    this.lv_votes.Items.Add(itm);
                }
            }
            foreach (var act in this.Operater.Wallet.GetHeldAccounts())
            {
                this.cb_accounts.Items.Add(new AccountListItem(act));
            }
            this.cb_accounts.SelectedIndex = 0;
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

        private void lv_votes_SelectedIndicesChanged(object sender, EventArgs e)
        {
            var itemid = this.lv_votes.SelectedIndices?.FirstOrDefault();
            if (itemid.HasValue && this.lv_votes.Items.IsNotNullAndEmpty())
            {
                var item = this.lv_votes.Items[itemid.Value];
                var vote = (KeyValuePair<uint, Fixed8>)item.Tag;
                this.tb_expire.Text = vote.Key.ToString();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (uint.TryParse(this.tb_expire.Text, out uint index))
            {
                if (index % 10000 == 0 && index > Blockchain.Singleton.HeaderHeight && this.Asset.AssetId.IsNotNull())
                {
                    var tx = this.BuildTransaction(out WalletAccount account);
                    if (tx.IsNotNull())
                    {
                        this.Operater.Wallet.MixBuildAndRelaySingleOutputTransaction(tx, account.ScriptHash, tx2 =>
                        {
                            string msg = $"{UIHelper.LocalString("锁仓投票交易已广播", "Relay lock vote transaction completed")}   {tx2.Hash}";
                            DarkMessageBox.ShowInformation(msg, "");
                            this.Close();
                        });
                    }
                }
            }
        }
    }
}
