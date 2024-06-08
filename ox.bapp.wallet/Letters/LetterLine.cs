using OX.Bapps;
using OX.IO;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text;
using OX.Wallets.Base;
using OX.Cryptography.ECC;
using System.IO;
using System.Drawing.Imaging;
using OX.Wallets.Base.Wallets;
using OX.SmartContract;
using Markdig;

namespace OX.Wallets.Letters
{

    public partial class LetterLine : DarkDocument, INotecaseTrigger, IModuleComponent
    {

        public Module Module { get; set; }
        protected INotecase Operater;
        UInt256 Line;
        LetterPair Pair;
        WalletAccount LocalAccount;
        string LocalAddress;
        string RemoteAddress;
        bool NeedReload;
        #region Constructor Region

        public LetterLine(Module module, INotecase notecase, UInt256 letterLine, LetterPair pair)
        {
            this.Module = module;
            this.Operater = notecase;
            this.Line = letterLine;
            this.Pair = pair;
            this.LocalAccount = Operater.Wallet.GetAccount(pair.Local);
            this.LocalAddress = pair.Local.ToAddress();
            this.RemoteAddress = Contract.CreateSignatureRedeemScript(pair.Remote).ToScriptHash().ToAddress();
            InitializeComponent();

            var lineId = BitConverter.ToUInt16(Line.ToArray());
            this.DockText = UIHelper.LocalString($"链邮-{lineId}", $"BMail-{lineId}");
            this.RoundPanel.SizeChanged += RoundPanel_SizeChanged;
            this.SizeChanged += GameRoom_SizeChanged;
            this.lb_key.Text = $"{LocalAddress}   <=>    {RemoteAddress}";
            this.bt_newLetter.Text = UIHelper.LocalString("回复", "Reply");
        }

        private void GameRoom_SizeChanged(object sender, EventArgs e)
        {
        }

        protected virtual void RoundPanel_SizeChanged(object sender, System.EventArgs e)
        {
            foreach (Control ctrl in this.RoundPanel.Controls)
            {
                ctrl.Width = this.RoundPanel.Width - 10;
            }
        }

        #endregion

        #region Event Handler Region

        public override void Close()
        {
            base.Close();
        }

        #endregion


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

        public void BeforeOnBlock(Block block)
        {
            if (NeedReload)
            {
                NeedReload = false;
                reload();
            }
        }
        public void OnBlock(Block block) { }

        public void AfterOnBlock(Block block)
        {
            foreach (var tx in block.Transactions)
            {
                if (tx is SecretLetterTransaction slt)
                {
                    if (this.Operater.IsNotNull() && this.Operater.Wallet.IsNotNull())
                    {
                        var shs = this.Operater.Wallet.GetHeldAccounts().Select(m => m.ScriptHash);
                        if (shs.Select(m => m.Hash).Contains(slt.ToHash))
                            NeedReload = true;
                        if (shs.Contains(Contract.CreateSignatureRedeemScript(slt.From).ToScriptHash()))
                            NeedReload = true;
                    }
                }
            }
        }
        public void ChangeWallet(INotecase operater)
        {
            if (operater.IsNull()) return;
            this.Operater = operater;
            reload();
        }
        public void OnRebuild() { }
        public void OnFlashMessage(FlashMessage flashMessage) { }
        #endregion

        void reload()
        {

            var bizPlugin = Bapp.GetBappProvider<WalletBapp, IWalletProvider>();
            if (bizPlugin != default)
            {
                this.DoInvoke(() =>
                {
                    string html = "<html><body style='background-color: rgb(60, 63, 65);width:100%; '>";
                    this.RoundPanel.Controls.Clear();
                    foreach (var letter in bizPlugin.GetMyLetters(this.Line).OrderByDescending(m => m.Value.Index))
                    {
                        if (letter.Value.SecretLetterTransaction.TryDecrypt(this.LocalAccount.GetKey(), this.Pair.Remote, out SecretLetterBody body, out byte[] plainText))
                        {
                            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
                            html += "<div style='border-radius:5px;border:1px solid #ffffff;background-color:#ffffff;width:100%;padding:20px;margin-bottom:0px;'>";
                            html += Markdown.ToHtml(System.Text.Encoding.UTF8.GetString(plainText), pipeline);
                            html += "</div>";
                            var time = letter.Value.Timestamp.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss");
                            var from = Contract.CreateSignatureRedeemScript(letter.Value.SecretLetterTransaction.From).ToScriptHash().ToAddress();
                            var flag = $"{time}      &nbsp&nbsp&nbsp&nbsp          {from}";
                            html += $"<p style='width:99%;font-size:14px;color:#c0c0c0;margin-top:5px;'>{flag}</p>";
                        }
                    }
                    this.renderer.DocumentText = html + "</body></html>";
                });
            }
        }

        private void bt_newLetter_Click(object sender, EventArgs e)
        {
            new NewLetter(Operater, this.LocalAccount, this.Pair.Remote).ShowDialog();
        }
    }
}
