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

namespace OX.Wallets.Letters
{
    public partial class NewLetter : DarkForm
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
        ECPoint Remote;
        public NewLetter(INotecase operater, WalletAccount local = default, ECPoint remote = default)
        {
            InitializeComponent();
            this.Operater = operater;
            Local = local;
            Remote = remote;
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            renderer.DocumentText = Markdown.ToHtml(editor.Text, pipeline);
        }

        private void tb_b_Click(object sender, EventArgs e)
        {
            string insertText = "**";
            int selectionStart = editor.SelectionStart;
            int selectionStop = editor.SelectionStart + editor.SelectionLength + insertText.Length;

            editor.Text = editor.Text.Insert(selectionStart, insertText);
            editor.Text = editor.Text.Insert(selectionStop, insertText);
            editor.SelectionStart = selectionStop + insertText.Length;
        }

        private void tb_i_Click(object sender, EventArgs e)
        {
            string insertText = "*";
            int selectionStart = editor.SelectionStart;
            int selectionStop = editor.SelectionStart + editor.SelectionLength + insertText.Length;

            editor.Text = editor.Text.Insert(selectionStart, insertText);
            editor.Text = editor.Text.Insert(selectionStop, insertText);
            editor.SelectionStart = selectionStop + insertText.Length;
        }

        private void tb_l1_Click(object sender, EventArgs e)
        {
            string insertText = "- ";
            int selectionStart = editor.GetFirstCharIndexOfCurrentLine();

            editor.Text = editor.Text.Insert(selectionStart, insertText);
            editor.SelectionStart = selectionStart + insertText.Length;
        }

        private void tb_l2_Click(object sender, EventArgs e)
        {
            string insertText = "1. ";
            int selectionStart = editor.GetFirstCharIndexOfCurrentLine();

            editor.Text = editor.Text.Insert(selectionStart, insertText);
            editor.SelectionStart = selectionStart + insertText.Length;
        }
        private void insertHeading(int level)
        {
            string insertText = "";
            for (int i = 0; i < level; i++)
            {
                insertText += '#';
            }
            insertText += " ";
            int selectionStart = editor.GetFirstCharIndexOfCurrentLine();

            editor.Text = editor.Text.Insert(selectionStart, insertText);
            editor.SelectionStart = selectionStart + level + 1;

        }

        private void tb_h1_Click(object sender, EventArgs e)
        {
            insertHeading(1);
        }

        private void tb_h2_Click(object sender, EventArgs e)
        {
            insertHeading(2);
        }

        private void tb_h3_Click(object sender, EventArgs e)
        {
            insertHeading(3);
        }

        private void tb_h4_Click(object sender, EventArgs e)
        {
            insertHeading(4);
        }

        private void tb_h5_Click(object sender, EventArgs e)
        {
            insertHeading(5);
        }

        private void tb_h6_Click(object sender, EventArgs e)
        {
            insertHeading(6);
        }

        private void NewLetter_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("写邮件", "New Mail");
            this.lb_to.Text = UIHelper.LocalString("收件公钥:", "Recipient Public Key:");
            this.lb_from.Text = UIHelper.LocalString("发件账户:", "Sender Account:");
            this.bt_ok.Text = UIHelper.LocalString("发送", "Send");
            this.bt_cancel.Text = UIHelper.LocalString("关闭", "Close");
            initAccounts();
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

                    if (Remote.IsNotNull())
                    {
                        this.tb_to.ReadOnly = true;
                        this.tb_to.Text = Remote.ToString();
                    }
                });
            }
        }
        private void bt_cancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {
            try
            {
                var ad = this.cbAccounts.SelectedItem as AccountDescriptor;
                if (ad.IsNotNull() && this.editor.Text.IsNotNullAndEmpty())
                {
                    Random random = new Random();
                    var nonce = new byte[32];
                    random.NextBytes(nonce);
                    SecretLetterTransaction tx = new SecretLetterTransaction(Local.GetKey(), Remote, nonce, this.editor.Text);
                    if (tx.IsNotNull() && this.Operater.Wallet.IsNotNull())
                    {
                        this.Operater.Wallet.MixBuildAndRelaySingleOutputTransaction(tx, ad.Account.ScriptHash, tx2 =>
                        {
                            string msg = $"{UIHelper.LocalString("链邮交易已广播", "Relay blockchain mail transaction completed")}   {tx2.Hash}";
                            DarkMessageBox.ShowInformation(msg, "");
                        });
                    }
                }

            }
            catch
            {

            }
        }

        private void tb_to_TextChanged(object sender, EventArgs e)
        {
            this.Remote = default;
            ECPoint.TryParse(this.tb_to.Text, ECCurve.Secp256r1, out this.Remote);
        }
    }
}
