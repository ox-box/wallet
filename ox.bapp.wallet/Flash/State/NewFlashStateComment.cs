using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Markdig;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using OX.Cryptography;
using OX.Cryptography.ECC;
using OX.Network.P2P.Payloads;
using System.Diagnostics;
using OX.Ledger;
using OX.Persistence;
using OX.Wallets.Flash.Chat;
using System.IO;
using Nethereum.Util;
using Akka.Pattern;

namespace OX.Wallets.Flash.State
{
    public partial class NewFlashStateComment : DarkDialog
    {
        public class AccountDescriptor
        {
            public WalletAccount Account;
            public override string ToString()
            {
                return Account.Address;
            }
        }
        INotecase Operater;
        public Module Module { get; set; }
        WalletAccount Local;
        UInt256 FlashStateHash;
        UInt256 ParentCommentHash;
        public NewFlashStateComment(INotecase operater, UInt256 flashStateHash, UInt256 parentCommentHash = default)
        {
            InitializeComponent();
            this.Operater = operater;
            this.FlashStateHash = flashStateHash;
            this.ParentCommentHash = parentCommentHash;
        }


        private void NewLetter_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("闪态评论", "Fash State Comment");
            this.lb_from.Text = UIHelper.LocalString("账户:", "Account:");
            initAccounts();
            check();
        }
        void initAccounts()
        {
            if (this.Operater.IsNotNull())
            {
                this.DoInvoke(() =>
                {
                    this.cbAccounts.Items.Clear();
                    if (Local.IsNotNull())
                    {
                        this.cbAccounts.Items.Add(new AccountDescriptor { Account = Local });
                    }
                    else
                    {
                        foreach (var act in this.Operater.Wallet.GetHeldAccounts())
                        {
                            if (this.Local.IsNull())
                                this.Local = act;
                            this.cbAccounts.Items.Add(new AccountDescriptor { Account = act });
                        }
                    }
                    this.cbAccounts.SelectedIndex = 0;


                });
            }
        }






        void check()
        {
            var ad = this.cbAccounts.SelectedItem as AccountDescriptor;
            if (ad.IsNull())
            {
                this.btnOk.Enabled = false;
                return;
            }
            var accountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(ad.Account.ScriptHash);
            this.btnOk.Enabled = accountState.IsNotNull() && Blockchain.Singleton.AllowFlashMessage(accountState, out uint _);
        }

        private void cbAccounts_SelectedIndexChanged(object sender, EventArgs e)
        {
            check();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                var ad = this.cbAccounts.SelectedItem as AccountDescriptor;
                if (ad.IsNotNull())
                {
                    var accountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(ad.Account.ScriptHash);
                    if (accountState.IsNotNull() && Blockchain.Singleton.AllowFlashMessage(accountState, out uint _))
                    {
                        var text = this.tb_comment.Text;
                        if (text.IsNotAnEmptyAddress())
                        {
                            var textData = System.Text.Encoding.UTF8.GetBytes(text);
                            StateComment sc = new StateComment { StateHash = this.FlashStateHash, ParentCommentHash = this.ParentCommentHash ?? UInt256.Zero, Data = textData };
                            var fs = new FlashStateComment(ad.Account.ScriptHash, Blockchain.Singleton.HeaderHeight, new StateComment[] { sc });
                            this.Operater.SignAndSendFlashMessage(fs);
                            this.Close();
                        }
                    }
                }
            }
            catch
            {

            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
