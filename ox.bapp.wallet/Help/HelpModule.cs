using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI;
using OX.Network.P2P.Payloads;
using OX.Bapps;
using OX.Wallets.NEP6;
using System.ComponentModel.Design.Serialization;
using System.Security.Claims;
using OX.Wallets.UI.Forms;
using System.Security.Cryptography;
using OX.IO;
using OX.IO.Json;
using OX.SmartContract;
using OX.VM;
using System.Security.Principal;
using OX.Ledger;
using Akka.Actor.Dsl;


namespace OX.Wallets.Base.Help
{
    public class HelpModule : Module
    {
        public override string ModuleName { get { return "wallethelpmodule"; } }
        public override uint Index { get { return int.MaxValue; } }

        protected INotecase Operater;
        DialogAbout about;
        public HelpModule(Bapp bapp) : base(bapp)
        {

        }
        public override void InitEvents() { }
        public override void InitWindows()
        {
            ToolStripMenuItem walletMenu = new ToolStripMenuItem();
            walletMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);

            walletMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            walletMenu.Name = "walletHelpMenu";
            walletMenu.Size = new System.Drawing.Size(39, 21);
            walletMenu.Text = UIHelper.LocalString("&帮助", "&Help");
            //signature
            ToolStripMenuItem signMenu = new ToolStripMenuItem();
            signMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            signMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //isingMenu.Image = global::Example.Icons.NewFile_6276;
            signMenu.Name = "isingMenu";
            signMenu.ShortcutKeys = Keys.Control | Keys.S;
            signMenu.Size = new System.Drawing.Size(170, 22);
            signMenu.Text = UIHelper.LocalString("&数据签名", "&Data Signature");
            signMenu.Click += IsingMenu_Click;
            //signature
            ToolStripMenuItem pubkeyViewMenu = new ToolStripMenuItem();
            pubkeyViewMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            pubkeyViewMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //isingMenu.Image = global::Example.Icons.NewFile_6276;
            pubkeyViewMenu.Name = "pubkeyViewMenu";
            pubkeyViewMenu.ShortcutKeys = Keys.Control | Keys.P;
            pubkeyViewMenu.Size = new System.Drawing.Size(170, 22);
            pubkeyViewMenu.Text = UIHelper.LocalString("&公钥查验", "&Public Key Check");
            pubkeyViewMenu.Click += PubkeyViewMenu_Click;
            //introduce
            ToolStripMenuItem introducemenu = new ToolStripMenuItem();
            introducemenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            introducemenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            introducemenu.Name = "introducemenu";
            introducemenu.ShortcutKeys = Keys.Control | Keys.G;
            introducemenu.Size = new System.Drawing.Size(170, 22);
            introducemenu.Text = UIHelper.LocalString("&钱包社区", "&Notecase Community");
            introducemenu.Click += IntroduceMenu_Click;
            //introduce
            ToolStripMenuItem copyApiUrlmenu = new ToolStripMenuItem();
            copyApiUrlmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            copyApiUrlmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            copyApiUrlmenu.Name = "copyApiUrlmenu";
            copyApiUrlmenu.ShortcutKeys = Keys.Control | Keys.A;
            copyApiUrlmenu.Size = new System.Drawing.Size(170, 22);
            copyApiUrlmenu.Text = UIHelper.LocalString("&复制WebApi地址", "&Copy Web Api Url");
            copyApiUrlmenu.Click += CopyApiUrlmenu_Click;
            //about
            ToolStripMenuItem aboutmenu = new ToolStripMenuItem();
            aboutmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            aboutmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            aboutmenu.Name = "aboutmenu";
            aboutmenu.ShortcutKeys = Keys.Control | Keys.C;
            aboutmenu.Size = new System.Drawing.Size(170, 22);
            aboutmenu.Text = UIHelper.LocalString("&关于", "&About");
            aboutmenu.Click += Aboutmenu_Click;

            walletMenu.DropDownItems.AddRange(new ToolStripItem[] {
                signMenu,
                pubkeyViewMenu,
                introducemenu,
                copyApiUrlmenu,
                aboutmenu});
            Container.TopMenus.Items.AddRange(new ToolStripItem[] {
            walletMenu});
        }

        private void PubkeyViewMenu_Click(object sender, EventArgs e)
        {
            new DialogCheckPubKey().ShowDialog();
        }

        private void IsingMenu_Click(object sender, EventArgs e)
        {
            new SignatureDialog(Operater).ShowDialog();
        }

        private void CopyApiUrlmenu_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Container.WebApiUrl);
        }

        public override void OnBappEvent(BappEvent be) { }

        public override void OnCrossBappMessage(CrossBappMessage message)
        {
        }
        public override void HeartBeat(HeartBeatContext context)
        {

        }
        public override void BeforeOnBlock(Block block)
        {
        }
        public override void OnBlock(Block block)
        {
        }
        public override void AfterOnBlock(Block block)
        {
        }
        public override void ChangeWallet(INotecase operater)
        {
            Operater = operater;

        }
        public override void OnRebuild()
        {

        }
        public override void OnLoadBappModuleWalletSection(JObject bappSectionObject)
        {
        }
        private void Aboutmenu_Click(object sender, EventArgs e)
        {
            //var acts = this.Operater.Wallet.GetHeldAccounts().ToArray();

            //using (ScriptBuilder sb = new ScriptBuilder())
            //{
            //    sb.EmitPush(acts[0].GetKey().PublicKey);
            //    sb.EmitPush(acts[1].GetKey().PublicKey);
            //    var bs = acts[2].ScriptHash.ToArray().Concat(acts[3].ScriptHash.ToArray()).Concat(acts[4].ScriptHash.ToArray()).ToArray();
            //    sb.EmitPush(bs);
            //    sb.EmitPush(true);
            //    //sb.EmitAppCall(SmartContractHelper.OutputRestrictionContractScriptHash);
            //    sb.EmitAppCall(UInt160.Parse("0xbec9fde513f1fdf97d28e3ffed2e5c4a32ae90c2"));
            //    var contract = Contract.Create(new[] { ContractParameterType.Signature }, sb.ToArray());
            //    var sh = contract.ScriptHash;
            //    var address = sh.ToAddress();
            //    var account = LockAssetHelper.CreateAccount(this.Operater.Wallet as OpenWallet, contract, acts[0].GetKey());//lock asset account have a some private key with master account
            //    if (account != null)
            //    {
            //        TransactionOutput output = new TransactionOutput { AssetId = Blockchain.OXC, Value = Fixed8.One, ScriptHash = acts[2].ScriptHash };
            //        //TransactionOutput output2 = new TransactionOutput { AssetId = Blockchain.OXC, Value = Fixed8.One, ScriptHash = "AQ64D2ZXA53skK89eLRAjZ4NbyoSDrQ182".ToScriptHash() };
            //        ContractTransaction tx = new ContractTransaction
            //        {
            //            //Attributes = new TransactionAttribute[] { new TransactionAttribute { Usage = TransactionAttributeUsage.RelatedScriptHash, Data = "AQ64D2ZXA53skK89eLRAjZ4NbyoSDrQ182".ToScriptHash().ToArray() } },
            //            Attributes = new TransactionAttribute[] { new TransactionAttribute { Usage = TransactionAttributeUsage.RelatedPublicKey, Data = acts[1].GetKey().PublicKey.EncodePoint(true) } },
            //            Outputs = new TransactionOutput[] { output },
            //            Inputs = new CoinReference[] { new CoinReference { PrevHash = UInt256.Parse("0x0b609f71649c830dc58ac0bb484ef68ea72737d949ab73af232111ddcc9f92ee"), PrevIndex = 0 } },
            //            Witnesses = new Witness[0]
            //        };
            //        tx = LockAssetHelper.Build(tx, new WalletAccount[] { /*acts[1],*/ account });
            //        if (tx.IsNotNull())
            //        {
            //            this.Operater.Wallet.ApplyTransaction(tx);
            //            this.Operater.Relay(tx);
            //            if (this.Operater != default)
            //            {
            //                string msg = $"Relay unlock asset transaction completed  {tx.Hash}";
            //                //Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
            //                DarkMessageBox.ShowInformation(msg, "");
            //            }
            //        }
            //    }

            //}

            about = this.ShowDialog<DialogAbout>(form =>
            {
                form.ChangeWallet(Operater);
            });
        }

        private void IntroduceMenu_Click(object sender, EventArgs e)
        {
            Bapp.PushCrossBappMessage(new CrossBappMessage() { MessageType = 1, Attachment = "105720-1" });
        }
    }
}
