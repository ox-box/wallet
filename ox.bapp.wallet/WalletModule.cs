using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI;
using OX.Wallets.UI.Forms;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using OX.VM;
using OX.SmartContract;
using OX.Bapps;
using System.ComponentModel.Design.Serialization;
using System.Security.Claims;
using OX.IO.Json;
using OX.Ledger;
using OX.Wallets.Base.Wallets;
using OX.Wallets.States;
using OX.Wallets.Messages;
using Nethereum.Model;

namespace OX.Wallets.Base
{
    public class WalletModule : Module
    {
        public override string ModuleName { get { return "walletmodule"; } }
        public override uint Index { get { return 0; } }

        protected AccountAsset AccountAsset;
        protected EthAsset EthAsset;
        protected BTCAsset BTCAsset;
        //protected ClaimOXC ClaimForm;
        protected DockTransactionHistory TransactionHistoryView;
        protected Partners PartnersView;
        protected INotecase Operater;
        protected OXTokens OXTokens;
        public WalletModule(Bapp bapp) : base(bapp)
        {

        }
        public override void InitEvents() { }
        public override void InitWindows()
        {
            ToolStripMenuItem walletMenu = new ToolStripMenuItem();
            walletMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));

            walletMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            walletMenu.Name = "walletMenu";
            walletMenu.Size = new System.Drawing.Size(39, 21);
            walletMenu.Text = UIHelper.LocalString("&钱包", "&Wallet");

            ToolStripMenuItem accountAssetMenu = new ToolStripMenuItem();
            accountAssetMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            accountAssetMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            accountAssetMenu.Name = "accountAssetMenu";
            accountAssetMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            accountAssetMenu.Size = new System.Drawing.Size(170, 22);
            accountAssetMenu.Text = UIHelper.LocalString("&账户资产", "&Account Asset");
            accountAssetMenu.Click += AccountAssetMenu_Click;


            ToolStripMenuItem nativeAssetDetailsListmenu = new ToolStripMenuItem();
            nativeAssetDetailsListmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            nativeAssetDetailsListmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            //nativeAssetDetailsListmenu.Image = global::Example.Icons.NewFile_6276;
            nativeAssetDetailsListmenu.Name = "nativeAssetDetailsListmenu";
            nativeAssetDetailsListmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            nativeAssetDetailsListmenu.Size = new System.Drawing.Size(170, 22);
            nativeAssetDetailsListmenu.Text = UIHelper.LocalString("&原生资产详情", "&Native Asset Details");
            nativeAssetDetailsListmenu.Click += NativeAssetDetailsListmenu_Click;

            ToolStripMenuItem regAssetmenu = new ToolStripMenuItem();
            regAssetmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            regAssetmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            //regAssetmenu.Image = global::Example.Icons.NewFile_6276;
            regAssetmenu.Name = "regAssetmenu";
            regAssetmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            regAssetmenu.Size = new System.Drawing.Size(170, 22);
            regAssetmenu.Text = UIHelper.LocalString("&注册私营资产", "&Register Private Asset");
            regAssetmenu.Click += RegAssetmenu_Click;

            ToolStripMenuItem issueAssetmenu = new ToolStripMenuItem();
            issueAssetmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            issueAssetmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            //issueAssetmenu.Image = global::Example.Icons.NewFile_6276;
            issueAssetmenu.Name = "issueAssetmenu";
            issueAssetmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D)));
            issueAssetmenu.Size = new System.Drawing.Size(170, 22);
            issueAssetmenu.Text = UIHelper.LocalString("&分发私营资产", "&Distribution Private Asset");
            issueAssetmenu.Click += IssueAssetmenu_Click;

            ToolStripMenuItem tokenListmenu = new ToolStripMenuItem();
            tokenListmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            tokenListmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            //tokenListmenu.Image = global::Example.Icons.NewFile_6276;
            tokenListmenu.Name = "tokenListmenu";
            tokenListmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            tokenListmenu.Size = new System.Drawing.Size(170, 22);
            tokenListmenu.Text = UIHelper.LocalString("&私营资产详情", "&Private Asset Details");
            tokenListmenu.Click += TokenListmenu_Click;

            //ToolStripMenuItem transfermenu = new ToolStripMenuItem();
            //transfermenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            //transfermenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            ////exitmenu.Image = global::Example.Icons.NewFile_6276;
            //transfermenu.Name = "transfermenu";
            //transfermenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            //transfermenu.Size = new System.Drawing.Size(170, 22);
            //transfermenu.Text = UIHelper.LocalString("&合并转账", "&Merge Transfer");
            //transfermenu.Click += Transfermenu_Click;

            //ToolStripMenuItem claimmenu = new ToolStripMenuItem();
            //claimmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            //claimmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            ////exitmenu.Image = global::Example.Icons.NewFile_6276;
            //claimmenu.Name = "claimmenu";
            //claimmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            //claimmenu.Size = new System.Drawing.Size(170, 22);
            //claimmenu.Text = UIHelper.LocalString("&合并提取 OXC", "&Merge Claim OXC");
            //claimmenu.Click += Claimmenu_Click;

            ToolStripMenuItem exportMnemonicsMenu = new ToolStripMenuItem();
            exportMnemonicsMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            exportMnemonicsMenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            //exportMnemonicsMenu.Image = global::Example.Icons.NewFile_6276;
            exportMnemonicsMenu.Name = "exportMnemonicsMenu";
            exportMnemonicsMenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.K)));
            exportMnemonicsMenu.Size = new System.Drawing.Size(170, 22);
            exportMnemonicsMenu.Text = UIHelper.LocalString("&导出助记词", "&Export Mnemonicis");
            exportMnemonicsMenu.Click += ExportMnemonicsMenu_Click;
            ToolStripMenuItem blockRecordsmenu = new ToolStripMenuItem();
            blockRecordsmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            blockRecordsmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            blockRecordsmenu.Name = "blockRecordsmenu";
            blockRecordsmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.J)));
            blockRecordsmenu.Size = new System.Drawing.Size(170, 22);
            blockRecordsmenu.Text = UIHelper.LocalString("&查看区块", "&View Block");
            blockRecordsmenu.Click += BlockRecordsmenu_Click;
            ToolStripMenuItem recordmenu = new ToolStripMenuItem();
            recordmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            recordmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            recordmenu.Name = "recordmenu";
            recordmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.H)));
            recordmenu.Size = new System.Drawing.Size(170, 22);
            recordmenu.Text = UIHelper.LocalString("&交易记录", "&Transaction History");
            recordmenu.Click += Recordmenu_Click;
            ToolStripMenuItem partnermenu = new ToolStripMenuItem();
            partnermenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            partnermenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            partnermenu.Name = "partnermenu";
            partnermenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            partnermenu.Size = new System.Drawing.Size(170, 22);
            partnermenu.Text = UIHelper.LocalString("&交易伙伴", "&Transfer Partners");
            partnermenu.Click += Partnermenu_Click;
            ToolStripMenuItem reindexmenu = new ToolStripMenuItem();
            reindexmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            reindexmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            reindexmenu.Name = "reindexmenu";
            reindexmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            reindexmenu.Size = new System.Drawing.Size(170, 22);
            reindexmenu.Text = UIHelper.LocalString("&重建索引", "&Rebuild Index");
            reindexmenu.Click += Reindexmenu_Click;
            
            ToolStripMenuItem btcmenu = new ToolStripMenuItem();
            btcmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            btcmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            //btcmenu.Image = global::Example.Icons.NewFile_6276;
            btcmenu.Name = "btcmenu";
            btcmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            btcmenu.Size = new System.Drawing.Size(170, 22);
            btcmenu.Text = UIHelper.LocalString("&比特币账户", "&BTC Accounts");
            btcmenu.Click += Btcmenu_Click;
            ToolStripMenuItem ethnmenu = new ToolStripMenuItem();
            ethnmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            ethnmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            //ethnmenu.Image = global::Example.Icons.NewFile_6276;
            ethnmenu.Name = "ethnmenu";
            ethnmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            ethnmenu.Size = new System.Drawing.Size(170, 22);
            ethnmenu.Text = UIHelper.LocalString("&以太坊账户", "&Eth Accounts");
            ethnmenu.Click += Ethnmenu_Click;
            ToolStripMenuItem copyWalletmenu = new ToolStripMenuItem();
            copyWalletmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            copyWalletmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            //copyWalletmenu.Image = global::Example.Icons.NewFile_6276;
            copyWalletmenu.Name = "copyWalletmenu";
            copyWalletmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F5)));
            copyWalletmenu.Size = new System.Drawing.Size(170, 22);
            copyWalletmenu.Text = UIHelper.LocalString("&复制钱包文件", "&Copy Wallet File");
            copyWalletmenu.Click += CopyWalletmenu_Click;
            ToolStripMenuItem lockWalletmenu = new ToolStripMenuItem();
            lockWalletmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            lockWalletmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            //lockWalletmenu.Image = global::Example.Icons.NewFile_6276;
            lockWalletmenu.Name = "lockWalletmenu";
            lockWalletmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F6)));
            lockWalletmenu.Size = new System.Drawing.Size(170, 22);
            lockWalletmenu.Text = UIHelper.LocalString("&锁定钱包", "&Lock Wallet");
            lockWalletmenu.Click += LockWalletmenu_Click;
            //ToolStripMenuItem closewalletmenu = new ToolStripMenuItem();
            //closewalletmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            //closewalletmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            ////exitmenu.Image = global::Example.Icons.NewFile_6276;
            //closewalletmenu.Name = "closewalletmenu";
            //closewalletmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            //closewalletmenu.Size = new System.Drawing.Size(170, 22);
            //closewalletmenu.Text = UIHelper.LocalString("&关闭钱包", "&Close Wallet");
            //closewalletmenu.Click += Closewalletmenu_Click;
            ToolStripMenuItem exitmenu = new ToolStripMenuItem();
            exitmenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            exitmenu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            exitmenu.Name = "exitmenu";
            exitmenu.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            exitmenu.Size = new System.Drawing.Size(170, 22);
            exitmenu.Text = UIHelper.LocalString("&退出", "&Exit");
            exitmenu.Click += Exitmenu_Click;
            walletMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
                accountAssetMenu,
                nativeAssetDetailsListmenu,
                regAssetmenu,
                issueAssetmenu,
                tokenListmenu,
                //transfermenu,
                //claimmenu,
                exportMnemonicsMenu,
                blockRecordsmenu,
                recordmenu,
                partnermenu,
                reindexmenu,
                btcmenu,
                ethnmenu,
                copyWalletmenu,
                lockWalletmenu,
                //closewalletmenu,
                exitmenu});
            this.Container.TopMenus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            walletMenu});
        }

        private void AccountAssetMenu_Click(object sender, EventArgs e)
        {
            if (this.AccountAsset == default)
            {
                //this.AssetView = new AssetView(this.Res("view_asset"), OX.Wallets.Icons.document_16xLG);
                this.AccountAsset = new AccountAsset();
                this.AccountAsset.Module = this;
                if (this.Operater != default && this.Operater.Wallet != default)
                    this.AccountAsset.ChangeWallet(this.Operater);
                this.Container.ToolWindows.Add(this.AccountAsset);
            }

            this.Container.DockPanel.AddContent(this.AccountAsset);
        }

        private void NativeAssetDetailsListmenu_Click(object sender, EventArgs e)
        {
            new DialogNativeAsset().ShowDialog();
        }

        private void BlockRecordsmenu_Click(object sender, EventArgs e)
        {
            new ViewBlockDialog(this.Operater).ShowDialog();
        }

        private void TokenListmenu_Click(object sender, EventArgs e)
        {
            if (this.OXTokens == default)
            {
                this.OXTokens = new OXTokens();
                this.OXTokens.Module = this;
                if (this.Operater != default && this.Operater.Wallet != default)
                    this.OXTokens.ChangeWallet(this.Operater);
                this.Container.ToolWindows.Add(this.OXTokens);
            }

            this.Container.DockPanel.AddContent(this.OXTokens);
        }

        private void IssueAssetmenu_Click(object sender, EventArgs e)
        {
            using (IssueDialog dialog = new IssueDialog(this.Operater))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                var tx = dialog.GetTransaction();
                if (tx.IsNotNull())
                {
                    this.Operater.SignAndSendTx(tx);
                    string msg = $"{UIHelper.LocalString("分发私营资产交易已广播", "Relay issue private asset transaction completed")}   {tx.Hash}";
                    Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Bapp });
                    DarkMessageBox.ShowInformation(msg, "");
                }
            }
        }
        public InvocationTransaction GetTransaction(InvocationTransaction tx, Fixed8 fee, UInt160 Change_Address = null)
        {
            if (tx == null) tx = new InvocationTransaction();
            tx.Version = 1;
            //tx.Script = script;
            if (tx.Attributes == null) tx.Attributes = new TransactionAttribute[0];
            if (tx.Inputs == null) tx.Inputs = new CoinReference[0];
            if (tx.Outputs == null) tx.Outputs = new TransactionOutput[0];
            if (tx.Witnesses == null) tx.Witnesses = new Witness[0];
            ApplicationEngine engine = ApplicationEngine.Run(tx.Script, tx, testMode: true);
            if (!engine.State.HasFlag(VMState.FAULT))
            {
                tx.Gas = engine.GasConsumed - Fixed8.FromDecimal(10);
                if (tx.Gas < Fixed8.Zero) tx.Gas = Fixed8.Zero;
                tx.Gas = tx.Gas.Ceiling();
                if (tx.Size > 1024)
                {
                    Fixed8 sumFee = Fixed8.FromDecimal(tx.Size * 0.00001m) + Fixed8.FromDecimal(0.001m);
                    if (fee < sumFee)
                    {
                        fee = sumFee;
                    }
                }
                var gas = tx.Gas.Ceiling();

                InvocationTransaction result = this.Operater.Wallet.MakeTransaction(new InvocationTransaction
                {
                    Version = tx.Version,
                    Script = tx.Script,
                    Gas = tx.Gas,
                    Attributes = tx.Attributes,
                    Outputs = tx.Outputs
                }, change_address: Change_Address, fee: fee);
                return result;
            }
            else
            {
                return default;
            }



        }
        private void RegAssetmenu_Click(object sender, EventArgs e)
        {
            InvocationTransaction tx;
            using (AssetRegisterDialog dialog = new AssetRegisterDialog(this.Operater))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                tx = dialog.GetTransaction();
            }

            tx = GetTransaction(tx, Fixed8.Zero);

            if (tx.IsNotNull())
            {
                this.Operater.SignAndSendTx(tx);
                var txid = tx.Hash.ToString();
                Clipboard.SetText(txid);
                string msg = UIHelper.LocalString($"交易Id {txid}  已复制", $"tx id {txid}  copied");
                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Bapp });
                DarkMessageBox.ShowInformation(msg, "");
            }
        }



        private void LockWalletmenu_Click(object sender, EventArgs e)
        {
            if (this.Operater.Wallet is OpenWallet openWallet)
                new LockWallet(openWallet).ShowDialog();
        }

        private void CopyWalletmenu_Click(object sender, EventArgs e)
        {
            if (this.Operater.Wallet.IsNotNull() && this.Operater.Wallet is OpenWallet openWallet)
            {
                System.Collections.Specialized.StringCollection files = new System.Collections.Specialized.StringCollection();
                files.Add(openWallet.WalletPath);
                Clipboard.SetFileDropList(files);
                string msg = UIHelper.LocalString("钱包文件已经复制到粘贴板", "wallet file has been copied to the pasteboard");
                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Bapp });
            }
        }

        private void ExportMnemonicsMenu_Click(object sender, EventArgs e)
        {
            if (this.Operater.Wallet is OpenWallet openWallet)
            {
                using (VerifyPwdForMnemonic VerifyPwdForMnemonic = new VerifyPwdForMnemonic())
                {
                    if (VerifyPwdForMnemonic.ShowDialog() != DialogResult.OK || openWallet.WalletPassword != VerifyPwdForMnemonic.GetPassword()) return;
                    var mnemonic = openWallet.Mnemonics;
                    if (mnemonic.IsNotNullAndEmpty())
                    {
                        new MnemonicsWallet(mnemonic).ShowDialog();
                    }
                }
            }
        }

        private void Btcmenu_Click(object sender, EventArgs e)
        {
            if (this.BTCAsset == default)
            {
                this.BTCAsset = new BTCAsset();
                this.BTCAsset.Module = this;
                if (this.Operater != default && this.Operater.Wallet != default)
                    this.BTCAsset.ChangeWallet(this.Operater);
                this.Container.ToolWindows.Add(this.BTCAsset);
            }

            this.Container.DockPanel.AddContent(this.BTCAsset);
        }

        private void Ethnmenu_Click(object sender, EventArgs e)
        {
            if (this.EthAsset == default)
            {
                this.EthAsset = new EthAsset();
                this.EthAsset.Module = this;
                if (this.Operater != default && this.Operater.Wallet != default)
                    this.EthAsset.ChangeWallet(this.Operater);
                this.Container.ToolWindows.Add(this.EthAsset);
            }

            this.Container.DockPanel.AddContent(this.EthAsset);
        }
 

        private void Partnermenu_Click(object sender, EventArgs e)
        {
            if (this.PartnersView == default)
            {
                this.PartnersView = new Partners();
                if (this.Operater != default && this.Operater.Wallet != default)
                    this.PartnersView.ChangeWallet(this.Operater);
                this.Container.ToolWindows.Add(this.PartnersView);
            }

            this.Container.DockPanel.AddContent(this.PartnersView);
        }

        private void Recordmenu_Click(object sender, EventArgs e)
        {
            if (this.TransactionHistoryView == default)
            {
                this.TransactionHistoryView = new DockTransactionHistory();
                if (this.Operater != default && this.Operater.Wallet != default)
                    this.TransactionHistoryView.ChangeWallet(this.Operater);
                this.Container.ToolWindows.Add(this.TransactionHistoryView);
            }

            this.Container.DockPanel.AddContent(this.TransactionHistoryView);
        }


        public override void OnBappEvent(BappEvent be)
        {
            if (this.AccountAsset != default)
                this.AccountAsset.OnBappEvent(be);
            if (this.EthAsset != default)
                this.EthAsset.OnBappEvent(be);
            if (this.BTCAsset != default)
                this.BTCAsset.OnBappEvent(be);
            //if (this.ClaimForm != default)
            //this.ClaimForm.OnBappEvent(be);
            if (this.TransactionHistoryView != default)
                this.TransactionHistoryView.OnBappEvent(be);
            if (this.PartnersView != default)
                this.PartnersView.OnBappEvent(be);
            if (this.OXTokens != default)
                this.OXTokens.OnBappEvent(be);
        }


        public override void OnCrossBappMessage(CrossBappMessage message)
        {
            if (message.MessageType == -1)
            {
                var request = message.Attachment as TransferRequest;
                if (request.IsNotNull())
                {
                    using (DialogSinglePayTo dialog = new DialogSinglePayTo(this.Operater, request))
                    {
                        if (dialog.ShowDialog() != DialogResult.OK) return;
                        var tx = dialog.BuildTransaction();
                        if (tx.IsNotNull() && this.Operater.Wallet.IsNotNull())
                        {
                            this.Operater.Wallet.MixBuildAndRelaySingleOutputTransaction(tx, request.From, tx =>
                            {
                                string msg = $"{UIHelper.LocalString("交易已广播", "Relay transaction completed")}   {tx.Hash}";
                                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Bapp });
                                DarkMessageBox.ShowInformation(msg, "");
                            });
                        }
                    }
                }
            }
        }



        public override void HeartBeat(HeartBeatContext context)
        {
            if (this.AccountAsset != default)
                this.AccountAsset.HeartBeat(context);
            if (this.EthAsset != default)
                this.EthAsset.HeartBeat(context);
            if (this.BTCAsset != default)
                this.BTCAsset.HeartBeat(context);
            //if (this.ClaimForm != default)
            //this.ClaimForm.HeartBeat(context);
            if (this.TransactionHistoryView != default)
                this.TransactionHistoryView.HeartBeat(context);
            if (this.PartnersView != default)
                this.PartnersView.HeartBeat(context);
            if (this.OXTokens != default)
                this.OXTokens.HeartBeat(context);
        }
        public override void BeforeOnBlock(Block block)
        {
            if (this.AccountAsset != default)
                this.AccountAsset.BeforeOnBlock(block);
            if (this.EthAsset != default)
                this.EthAsset.BeforeOnBlock(block);
            if (this.BTCAsset != default)
                this.BTCAsset.BeforeOnBlock(block);
            //if (this.ClaimForm != default)
            //    this.ClaimForm.BeforeOnBlock(block);
            if (this.TransactionHistoryView != default)
                this.TransactionHistoryView.BeforeOnBlock(block);
            if (this.PartnersView != default)
                this.PartnersView.BeforeOnBlock(block);
            if (this.OXTokens != default)
                this.OXTokens.BeforeOnBlock(block);
        }
        public override void AfterOnBlock(Block block)
        {
            if (this.AccountAsset != default)
                this.AccountAsset.AfterOnBlock(block);
            if (this.EthAsset != default)
                this.EthAsset.AfterOnBlock(block);
            if (this.BTCAsset != default)
                this.BTCAsset.AfterOnBlock(block);
            //if (this.ClaimForm != default)
            //    this.ClaimForm.AfterOnBlock(block);
            if (this.TransactionHistoryView != default)
                this.TransactionHistoryView.AfterOnBlock(block);
            if (this.PartnersView != default)
                this.PartnersView.AfterOnBlock(block);
            if (this.OXTokens != default)
                this.OXTokens.AfterOnBlock(block);
            StateDispatcher.Instance.PostMixStateMessage(new NewBlockMessage { Block = block });
        }
        public override void OnBlock(Block block)
        {
            if (this.AccountAsset != default)
                this.AccountAsset.OnBlock(block);
            if (this.EthAsset != default)
                this.EthAsset.OnBlock(block);
            if (this.BTCAsset != default)
                this.BTCAsset.OnBlock(block);
            //if (this.ClaimForm != default)
            //    this.ClaimForm.OnBlock(block);
            if (this.TransactionHistoryView != default)
                this.TransactionHistoryView.OnBlock(block);
            if (this.PartnersView != default)
                this.PartnersView.OnBlock(block);
            if (this.OXTokens != default)
                this.OXTokens.OnBlock(block);
        }
        public override void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            if (this.AccountAsset != default)
                this.AccountAsset.ChangeWallet(operater);
            if (this.EthAsset != default)
                this.EthAsset.ChangeWallet(operater);
            if (this.BTCAsset != default)
                this.BTCAsset.ChangeWallet(operater);
            //if (this.ClaimForm != default)
            //    this.ClaimForm.ChangeWallet(operater);
            if (this.TransactionHistoryView != default)
                this.TransactionHistoryView.ChangeWallet(operater);
            if (this.PartnersView != default)
                this.PartnersView.ChangeWallet(operater);
            if (this.OXTokens != default)
                this.OXTokens.ChangeWallet(operater);
        }
        public override void OnRebuild()
        {
            if (this.AccountAsset != default)
                this.AccountAsset.OnRebuild();
            if (this.EthAsset != default)
                this.EthAsset.OnRebuild();
            if (this.BTCAsset != default)
                this.BTCAsset.OnRebuild();
            //if (this.ClaimForm != default)
            //    this.ClaimForm.OnRebuild();
            if (this.TransactionHistoryView != default)
                this.TransactionHistoryView.OnRebuild();
            if (this.PartnersView != default)
                this.PartnersView.OnRebuild();
            if (this.OXTokens != default)
                this.OXTokens.OnRebuild();
        }
        public override void OnLoadBappModuleWalletSection(JObject bappSectionObject)
        {

        }


        private void Exitmenu_Click(object sender, EventArgs e)
        {
            this.Operater.Exit();
        }

        private void Closewalletmenu_Click(object sender, EventArgs e)
        {
            this.Operater.Close();
        }

        private void Reindexmenu_Click(object sender, EventArgs e)
        {
            this.Operater.Indexer.RebuildIndex(this.Operater.Wallet);
        }

        //private void Claimmenu_Click(object sender, EventArgs e)
        //{
        //    this.ClaimForm = this.ShowDialog<ClaimOXC>(form =>
        //    {
        //        form.ChangeWallet(this.Operater);
        //    });
        //}

        //private void Transfermenu_Click(object sender, EventArgs e)
        //{
        //    Transaction tx;
        //    UInt160 change_address;
        //    Fixed8 fee;
        //    using (TransferDialog dialog = new TransferDialog(this.Operater))
        //    {
        //        if (dialog.ShowDialog() != DialogResult.OK) return;
        //        tx = dialog.GetTransaction();
        //        change_address = dialog.ChangeAddress;
        //        fee = dialog.Fee;
        //    }
        //    if (tx != null)
        //    {
        //        if (tx.Inputs.Count() > 50)
        //        {
        //            string msg = $"{UIHelper.LocalString("交易输入项太多,请多次转账", "There are too many transaction input. Please transfer multiple times")}";
        //            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Bapp });
        //            OX.Wallets.UI.Forms.DarkMessageBox.ShowInformation(msg, "");
        //            return;
        //        }
        //        this.Operater.SignAndSendTx(tx);
        //        if (this.Operater != default)
        //        {
        //            string msg = $"{UIHelper.LocalString("交易已广播", "Relay transaction completed")}   {tx.Hash}";
        //            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Bapp });
        //            OX.Wallets.UI.Forms.DarkMessageBox.ShowInformation(msg, "");
        //        }
        //    }
        //}

        private void Assetmenu_Click(object sender, EventArgs e)
        {
            //if (this.AccountAsset == default)
            //{
            //    //this.AssetView = new AssetView(this.Res("view_asset"), OX.Wallets.Icons.document_16xLG);
            //    this.AccountAsset = new AccountAsset();
            //    this.AccountAsset.Module = this;
            //    if (this.Operater != default && this.Operater.Wallet != default)
            //        this.AccountAsset.ChangeWallet(this.Operater);
            //    this.Container.ToolWindows.Add(this.AccountAsset);
            //}

            //this.Container.DockPanel.AddContent(this.AccountAsset);
        }
    }
}
