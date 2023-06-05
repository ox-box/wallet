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
    public partial class ViewLetterDialog : DarkDialog, INotecaseTrigger, IModuleComponent
    {

        INotecase Operater;
        public Module Module { get; set; }
        SecretLetterKey Letter;
        public ViewLetterDialog(INotecase operater, SecretLetterKey letter)
        {
            InitializeComponent();
            this.Operater = operater;
            this.Letter = letter;
            this.DialogButtons = DarkDialogButton.Ok;
        }


        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("查看私信", "View Letter");
            this.lb_msg.Text = UIHelper.LocalString("内容:", "Content:");
            this.lb_to.Text = UIHelper.LocalString("收信人:", "Recipient:");
            this.lb_from.Text = UIHelper.LocalString("发信人:", "Sender:");
            this.btnOk.Text = UIHelper.LocalString("关闭", "Close");
            var act = this.Operater.Wallet.GetAccount(this.Letter.Recipient);
            if (act.IsNotNull())
            {
                var sharekey = act.GetKey().DiffieHellman(this.Letter.From);
                var decryptedData = this.Letter.Msg.Decrypt(sharekey);
                this.tb_msg.Text = System.Text.Encoding.UTF8.GetString(decryptedData);
                this.tb_to.Text = this.Letter.Recipient.ToAddress();
                this.tb_fromPubkey.Text = this.Letter.From.ToString();
                this.tb_fromAddr.Text = Contract.CreateSignatureRedeemScript(this.Letter.From).ToScriptHash().ToAddress();
            }
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
        }
        public void OnRebuild()
        {

        }




        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
