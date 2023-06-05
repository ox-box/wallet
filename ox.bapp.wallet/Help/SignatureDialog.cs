using Akka.Actor;
using OX.IO.Actors;
using OX.Ledger;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using OX.Wallets.UI.Forms;
using OX.Wallets.UI;
using OX.SmartContract;
using OX.IO;
using System.Xml;
using OX.Bapps;
using OX.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text;

namespace OX.Wallets.Base
{
    public partial class SignatureDialog : DarkDialog, INotecaseTrigger, IModuleComponent
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
        public SignatureDialog(INotecase operater)
        {
            InitializeComponent();
            this.Operater = operater;
            this.DialogButtons = DarkDialogButton.OkCancel;
        }


        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("数据签名", "Data Signature");
            this.lb_input.Text = UIHelper.LocalString("输入:", "Input:");
            this.lb_type.Text = UIHelper.LocalString("输入类型:", "Input Type:");
            this.lb_from.Text = UIHelper.LocalString("签名账户:", "Sign Account:");
            this.lb_output.Text = UIHelper.LocalString("输出:", "Output:");
            this.btnOk.Text = UIHelper.LocalString("复制", "Copy");
            this.btnCancel.Text = UIHelper.LocalString("关闭", "Close");
            this.bt_signature.Text = UIHelper.LocalString("签名", "Signature");
            this.rb_text.Text = "Text";
            this.rb_hex.Text = "Hex";
            initAccounts();
        }

        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        public void OnBappEvent(BappEvent be) { }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
        }
        public void HeartBeat(HeartBeatContext context)
        {

        }
        public void BeforeOnBlock(Block block)
        {
        }
        public void OnBlock(Block block)
        {
        }
        public void AfterOnBlock(Block block)
        {
        }
        public void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            initAccounts();
        }
        public void OnRebuild()
        {

        }
        void initAccounts()
        {
            if (this.Operater.IsNotNull())
            {
                this.DoInvoke(() =>
                {
                    this.cbAccounts.Items.Clear();
                    foreach (var act in this.Operater.Wallet.GetHeldAccounts())
                    {
                        this.cbAccounts.Items.Add(new AccountDescriptor { Account = act });
                    }
                    this.cbAccounts.SelectedIndex = 0;
                });
            }
        }

        private void bt_signature_Click(object sender, EventArgs e)
        {
            var intput = this.tb_input.Text;
            if (intput.IsNullOrEmpty()) return;

            byte[] raw, signedData = null;
            try
            {
                if (rb_hex.Checked)
                {
                    raw = intput.HexToBytes();
                }
                else
                {
                    raw = Encoding.UTF8.GetBytes(intput);
                }
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            var account = (AccountDescriptor)cbAccounts.SelectedItem;
            var keys = account.Account.GetKey();

            try
            {
                signedData = Crypto.Default.Sign(raw, keys.PrivateKey, keys.PublicKey.EncodePoint(false).Skip(1).ToArray());
            }
            catch (Exception err)
            {
                MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            this.tb_output.Text = signedData?.ToHexString();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var str = this.tb_output.Text;
            if (str.IsNotNullAndEmpty())
            {
                Clipboard.SetText(str);
                string msg = str + UIHelper.LocalString("  已复制", "  copied");
                DarkMessageBox.ShowInformation(msg, "");
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
