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
using OX.Cryptography.ECC;
using OX.Cryptography.AES;

namespace OX.Wallets.Base
{
    public partial class NewLetterDialog : DarkDialog, INotecaseTrigger, IModuleComponent
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
        public NewLetterDialog(INotecase operater)
        {
            InitializeComponent();
            this.Operater = operater;
            this.DialogButtons = DarkDialogButton.OkCancel;
        }


        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("写信", "New Letter");
            this.lb_msg.Text = UIHelper.LocalString("内容:", "Content:");
            this.lb_to.Text = UIHelper.LocalString("收信公钥:", "Recipient Public Key:");
            this.lb_from.Text = UIHelper.LocalString("发信账户:", "Sender Account:");
            this.btnOk.Text = UIHelper.LocalString("发送", "Send");
            this.btnCancel.Text = UIHelper.LocalString("关闭", "Close");
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



        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                var ad = this.cbAccounts.SelectedItem as AccountDescriptor;
                if (ad.IsNotNull() && this.tb_msg.Text.IsNotNullAndEmpty())
                {
                    var pubkey = ECPoint.Parse(this.tb_to.Text, ECCurve.Secp256r1);
                    var sh = Contract.CreateSignatureRedeemScript(pubkey).ToScriptHash();
                    var data = System.Text.Encoding.UTF8.GetBytes(this.tb_msg.Text);
                    var key = ad.Account.GetKey();
                    var sharekey = key.DiffieHellman(pubkey);
                    var cryptoData = data.Encrypt(sharekey);

                    SecretLetterTransaction slt = new SecretLetterTransaction
                    {
                        Flag = 1,
                        From = ad.Account.GetKey().PublicKey,
                        ToHash = sh.Hash,
                        Data = cryptoData
                    };
                    slt = this.Operater.Wallet.MakeTransaction(slt, ad.Account.ScriptHash, ad.Account.ScriptHash);
                    if (slt.IsNotNull())
                    {
                        this.Operater.SignAndSendTx(slt);
                        string msg = $"{UIHelper.LocalString("私信交易已广播", "relay private letter transaction completed")}   {slt.Hash}";
                        DarkMessageBox.ShowInformation(msg, "");
                    }
                }
                //var str = this.tb_output.Text;
                //if (str.IsNotNullAndEmpty())
                //{
                //    Clipboard.SetText(str);
                //    string msg = str + UIHelper.LocalString("  已复制", "  copied");
                //    DarkMessageBox.ShowInformation(msg, "");
                //}
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
