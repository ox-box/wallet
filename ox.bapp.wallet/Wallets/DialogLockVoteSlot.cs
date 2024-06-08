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
    public partial class DialogLockVoteSlot : DarkDialog
    {

        INotecase Operater;
        uint H;
        public UInt160 Slot;

        public DialogLockVoteSlot()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("锁仓投票", "Lock Vote");
            btnOk.Text = UIHelper.LocalString("确定", "OK");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            this.lb_asset.Text = UIHelper.LocalString("资产:", "Asset:");
            this.lb_amount.Text = UIHelper.LocalString("金额:", "Amount:");
            this.lb_balance.Text = UIHelper.LocalString("可用余额:", "Available Balance:");
            this.darkLabel1.Text = UIHelper.LocalString("锁仓高度:", "Lock Block:");
            btnOk.Enabled = false;
        }

        public DialogLockVoteSlot(INotecase operater, UInt160 slot, uint height) : this()
        {
            this.Operater = operater;
            this.Slot = slot;
            this.H = height;
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
                Purpose = LockAssetPurpose.SlotOffVote
            };
            lat.Attach = new SlotOffVote { Index = h, Slot = this.Slot }.ToArray();
            Fixed8.TryParse(tb_amount.Text, out Fixed8 amount);
            lat.Outputs = new TransactionOutput[] { new TransactionOutput { AssetId = Blockchain.OXS, ScriptHash = lat.GetContract().ScriptHash, Value = amount } };
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
                if (this.Operater.Wallet.TryGetWalletAccountBalance(ali.Account.ScriptHash, out Dictionary<UInt256, WalletAccountBalance> balances) && balances.TryGetValue(Blockchain.OXS, out WalletAccountBalance balance))
                {
                    tb_balance.Text = balance.AvailableBalance.ToString();
                    textBox_TextChanged(this, EventArgs.Empty);
                }
            }
        }


        private void PayToDialog_Load(object sender, EventArgs e)
        {
            this.tb_expire.Text = this.H.ToString();
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





    }
}
