using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets;
using OX.Bapps;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.IO;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using OX.Wallets.Base;
using OX.SmartContract;
using Markdig;
using System.Diagnostics.Metrics;
using OX.Cryptography.ECC;

namespace OX.Wallets.Letters
{
    public partial class LetterControl : UserControl, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        protected INotecase Operater;
        WalletAccount LocalAccount;
        ECPoint Remote;
        SecretLetterKey LetterKey;
        SecretLetterState Letter;

        public LetterControl(Module module, INotecase notecase, WalletAccount localAccount, ECPoint remote, SecretLetterKey letterKey, SecretLetterState letter)
        {
            this.Module = module;
            this.Operater = notecase;
            this.LocalAccount = localAccount;
            this.Remote = remote;
            this.LetterKey = letterKey;
            this.Letter = letter;
            InitializeComponent();
            this.MouseDown += LetterControl_MouseDown;
            var time = letter.Timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
            var from = Contract.CreateSignatureRedeemScript(letter.SecretLetterTransaction.From).ToScriptHash().ToAddress();
            this.dt_title.Text = $"{time}   >  {from}";
           
            if (Letter.SecretLetterTransaction.TryDecrypt(localAccount.GetKey(), this.Remote, out SecretLetterBody body, out byte[] plainText))
            {
                var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                renderer.DocumentText = Markdown.ToHtml(System.Text.Encoding.UTF8.GetString(plainText), pipeline);
                //this.Height = renderer.Document.Window.Size.Height + 50;
            }
        }

        private void LetterControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DarkContextMenu menu = new DarkContextMenu();

                if (menu.Items.Count > 0)
                    menu.Show(this, e.Location);
            }
        }




        #region IBlockChainTrigger
        public void OnBappEvent(BappEvent be)
        {

        }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
        }
        public void HeartBeat(HeartBeatContext context)
        {

        }
        public void OnFlashState(FlashState flashstate)
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
        public void OnRebuild() { }
        #endregion


    }
}
