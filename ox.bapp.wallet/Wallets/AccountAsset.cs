using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections;
using System.Drawing;
using System.Collections.Generic;
using OX.Wallets.UI.Docking;
using OX.Wallets.UI.Controls;
using OX.Wallets;
using OX.Bapps;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets.NEP6;
using System.Drawing.Imaging;
using OX.Wallets.UI;
using OX.VM;
using OX.Wallets.UI.Forms;
using OX.Persistence;
using OX.SmartContract;
using OX.Cryptography.ECC;
using OX.Wallets.Base.Wallets;
using Nethereum.Model;
using System.Security.Principal;
using Akka.Actor.Dsl;
using OX.IO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OX.Wallets.Base
{
    public partial class AccountAsset : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        public static List<CoinReference> lockAssetKeys = new List<CoinReference>();
        public Module Module { get; set; }
        private INotecase Operater;
        uint stateChangedIndex = 0;
        #region Constructor Region

        public AccountAsset()
        {
            InitializeComponent();
            this.DockArea = DarkDockArea.Left;
            this.treeAsset.MouseDown += TreeAsset_MouseDown;
            this.DockText = UIHelper.LocalString("账户资产", "Account Assets");
        }
        #region context menus
        private void TreeAsset_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DarkContextMenu menu = new DarkContextMenu();
                ToolStripMenuItem sm;
                sm = new ToolStripMenuItem(UIHelper.LocalString("添加监视地址", "Add Watch Address"));
                sm.Click += SmImportWatchAddress_Click;
                menu.Items.Add(sm);
                DarkTreeNode[] nodes = treeAsset.SelectedNodes.ToArray();
                if (nodes.IsNotNullAndEmpty() && nodes.Length == 1)
                {
                    DarkTreeNode node = nodes[0];
                    //var nodetype = (int)node.NodeType;
                    if (node is BaseTreeNode an)
                    {
                        WalletAccount account = an.Account;
                        menu.Items.Add(new ToolStripSeparator());
                        if (account.WatchOnly)
                        {
                            //删除地址
                            sm = new ToolStripMenuItem(UIHelper.LocalString("从钱包删除该账户", "Remove Account"));
                            sm.Tag = account;
                            sm.Click += SmDeleteAccount_Click;
                            menu.Items.Add(sm);
                        }
                        else
                        {

                            //转账
                            sm = new ToolStripMenuItem(UIHelper.LocalString("转账", "Transfer"));
                            sm.Tag = account;
                            sm.Click += SmTransfer_Click;
                            menu.Items.Add(sm);
                            //提取主账号OXC
                            sm = new ToolStripMenuItem(UIHelper.LocalString("提取 OXC", "Claim OXC"));
                            sm.Tag = account;
                            sm.Click += ClaimOXC_Click;
                            menu.Items.Add(sm);
                            //选举
                            sm = new ToolStripMenuItem(UIHelper.LocalString("选举", "Election"));
                            sm.Tag = account;
                            sm.Click += Election_Click;
                            menu.Items.Add(sm);
                            //投票
                            sm = new ToolStripMenuItem(UIHelper.LocalString("投票", "Vote"));
                            sm.Tag = account;
                            sm.Click += Vote_Click;
                            menu.Items.Add(sm);
                            //全网捐赠
                            sm = new ToolStripMenuItem(UIHelper.LocalString("捐献", "Contribute"));
                            sm.Tag = account;
                            sm.Click += Contribute_Click;
                            menu.Items.Add(sm);
                            //查看私钥
                            sm = new ToolStripMenuItem(UIHelper.LocalString("查看私钥", "Show Private Key"));
                            sm.Tag = account;
                            sm.Click += SmShowKey_Click;
                            menu.Items.Add(sm);
                            //复制到粘贴板
                            sm = new ToolStripMenuItem(UIHelper.LocalString("复制账户主地址", "Copy account private address"));
                            sm.Tag = account;
                            sm.Click += SmCopy_Click;
                            menu.Items.Add(sm);
                            //复制到粘贴板
                            sm = new ToolStripMenuItem(UIHelper.LocalString("复制账户公钥", "Copy account public key"));
                            sm.Tag = account;
                            sm.Click += Sm_Click;
                            menu.Items.Add(sm);
                            //商业频道租赁
                            sm = new ToolStripMenuItem(UIHelper.LocalString("商业频道租赁", "Business Channel Lease"));
                            sm.Tag = account;
                            sm.Click += SmDetain_Click;
                            menu.Items.Add(sm);
                            //整理余额碎片
                            sm = new ToolStripMenuItem(UIHelper.LocalString("整理余额碎片", "Defragment  balance"));
                            sm.Tag = account;
                            sm.Click += Organizefragments_Click1;
                            menu.Items.Add(sm);
                            //重置简易访问码
                            sm = new ToolStripMenuItem(UIHelper.LocalString("重置简易码", "Reset Easy Code"));
                            sm.Tag = account;
                            sm.Click += ResetEasyCode_Click6;
                            menu.Items.Add(sm);
                        }
                        //备注账户名称
                        sm = new ToolStripMenuItem(UIHelper.LocalString("备注账户名称", "Note Account"));
                        sm.Tag = account;
                        sm.Click += SmNoteAccount_Click;
                        menu.Items.Add(sm);
                    }
                }
                if (menu.Items.Count > 0)
                    menu.Show(this.treeAsset, e.Location);
            }
        }

        private void Election_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            using (var dialog = new ElectionDialog(account))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var tx = dialog.GetTransaction();
                    if (tx.IsNotNull() && this.Operater.Wallet.IsNotNull())
                    {
                        this.Operater.Wallet.MixBuildAndRelaySingleOutputTransaction(tx, account.ScriptHash, tx2 =>
                        {
                            string msg = $"{UIHelper.LocalString("选举交易已广播", "Relay election transaction completed")}   {tx2.Hash}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                        });
                    }
                }
            }
        }

        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            try
            {
                var pubkey = account.GetKey().PublicKey.ToString();
                Clipboard.SetText(pubkey);
                string msg = pubkey + UIHelper.LocalString("  已复制", "  copied");
                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }


        //private void ClaimLockOXC_Click2(object sender, EventArgs e)
        //{
        //    ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
        //    WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
        //    if (this.Operater.Wallet is OpenWallet openWallet)
        //    {
        //        new ClaimLockAsset(this.Operater, account.ScriptHash).ShowDialog();
        //    }
        //}
        private void ClaimOXC_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            new SingleClaimOXC(this.Operater, account).ShowDialog();
        }


        private void Contribute_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            using (DialogSingleContributeTo dialog = new DialogSingleContributeTo(this.Operater, account.ScriptHash))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                var amountKind = dialog.GetAmountKind();
                RewardTransaction rwTx = new RewardTransaction { RewardAmount = amountKind };
                if (rwTx.IsNotNull() && this.Operater.Wallet.IsNotNull())
                {
                    this.Operater.Wallet.MixBuildAndRelaySingleOutputTransaction(rwTx, account.ScriptHash, tx2 =>
                    {
                        string msg = $"{UIHelper.LocalString("交易已广播", "Relay transaction completed")}   {tx2.Hash}";
                        Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                        DarkMessageBox.ShowInformation(msg, "");
                    });
                }

            }
        }
        private void Vote_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;

            using (VotingDialog dialog = new VotingDialog(this.Operater.Wallet, account.ScriptHash))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                try
                {
                    var tx = dialog.GetTransaction();
                    this.Operater.SignAndSendTx(tx);
                    string msg = $"{UIHelper.LocalString("投票交易已广播", "Relay voting transaction completed")}   {tx.Hash}";
                    Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                    DarkMessageBox.ShowInformation(msg, "");
                }
                catch
                {
                    return;
                }
            }
        }
        private void SmCopy_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            try
            {
                var address = account.ScriptHash.ToAddress();
                Clipboard.SetText(address);
                string msg = address + UIHelper.LocalString("  已复制", "  copied");
                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }
        private void Organizefragments_Click1(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;

            new DialogDefragment(this.Operater, account).ShowDialog();
        }

        private void SmTransfer_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            KeyPair kp = account.GetKey();
            using (DialogSinglePayTo dialog = new DialogSinglePayTo(this.Operater, account))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                var tx = dialog.BuildTransaction();
                if (tx.IsNotNull() && this.Operater.Wallet.IsNotNull())
                {
                    this.Operater.Wallet.MixBuildAndRelaySingleOutputTransaction(tx, account.ScriptHash, tx =>
                    {
                        string msg = $"{UIHelper.LocalString("交易已广播", "Relay transaction completed")}   {tx.Hash}";
                        Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                        DarkMessageBox.ShowInformation(msg, "");
                    });
                }
            }
        }
        private void SmDetain_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            using (DetainDialog dialog = new DetainDialog(account))
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var tx = dialog.GetTransaction();
                    if (tx.IsNotNull() && this.Operater.Wallet.IsNotNull())
                    {
                        this.Operater.Wallet.MixBuildAndRelaySingleOutputTransaction(tx, account.ScriptHash, tx2 =>
                        {
                            string msg = $"{UIHelper.LocalString("商业频道租赁交易已广播", "Relay business channel lease transaction completed")}   {tx.Hash}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                        });
                    }
                }
            }
        }
        private void SmShowKey_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            if (this.Operater.Wallet is OpenWallet openWallet)
            {
                using (VerifyPwdForMnemonic VerifyPwdForMnemonic = new VerifyPwdForMnemonic())
                {
                    if (VerifyPwdForMnemonic.ShowDialog() != DialogResult.OK || openWallet.WalletPassword != VerifyPwdForMnemonic.GetPassword()) return;
                    using (DialogShowKey dialog = new DialogShowKey(account))
                    {
                        dialog.ShowDialog();
                    }
                }
            }
        }
        private void ResetEasyCode_Click6(object sender, EventArgs e)
        {
            if (this.Operater != default && this.Operater.Wallet != default)
            {
                ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
                WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
                if (this.Operater.Wallet is NEP6Wallet wallet)
                {
                    new EasyCodeAccountDialog(wallet, account).ShowDialog();
                }
            }
        }
        private void SmNoteAccount_Click(object sender, System.EventArgs e)
        {
            if (this.Operater != default && this.Operater.Wallet != default)
            {
                ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
                WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
                using (var nad = new NoteAccountDialog(account))
                {
                    if (nad.ShowDialog() != DialogResult.OK) return;
                    account.Label = nad.Label;
                    if (this.Operater.Wallet is NEP6Wallet wallet)
                    {
                        wallet.Save();
                        reloadAsset();
                    }
                }
            }
        }
        private void SmDeleteAccount_Click(object sender, System.EventArgs e)
        {
            if (this.Operater != default && this.Operater.Wallet != default)
            {
                if (DarkMessageBox.ShowInformation(UIHelper.LocalString("你确认要从钱包删除这个账户吗?", "Are you sure you want to delete this account from your wallet?"), UIHelper.LocalString("删除账户", "Delete Account"), DarkDialogButton.OkCancel) == DialogResult.OK)
                {
                    ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
                    WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
                    this.Operater.Wallet.DeleteAccount(account.ScriptHash);
                    if (this.Operater.Wallet is NEP6Wallet wallet)
                        wallet.Save();
                    reloadAsset();
                }
            }
        }
        private void SmImportWatchAddress_Click(object sender, System.EventArgs e)
        {
            using (DialogImportWatchAccount dialog = new DialogImportWatchAccount())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                try
                {
                    WalletAccount account = this.Operater.Wallet.CreateAccount(dialog.Address);
                    if (this.Operater.Wallet is NEP6Wallet wallet)
                        wallet.Save();
                    reloadAsset();
                }
                catch
                {
                }
            }
        }
        #endregion

        public void Clear()
        {
            this.treeAsset.Nodes.Clear();
        }

        #endregion
        #region IBlockChainTrigger
        public void OnBappEvent(BappEvent be) { }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
        }

        public void AfterOnBlock(Block block)
        {
            if (this.Operater.IsNotNull() && this.Operater.Wallet.IsNotNull())
            {
                foreach (var tx in block.Transactions)
                {
                    if (tx is LockAssetTransaction lat)
                    {
                        if (lat.IsNotNull() && lat.LockContract.Equals(Blockchain.LockAssetContractScriptHash))
                        {
                            var holder = Contract.CreateSignatureRedeemScript(lat.Recipient).ToScriptHash();
                            if (this.Operater.Wallet.ContainsAndHeld(holder))
                            {
                                stateChangedIndex = block.Index + 1;
                            }
                        }
                    }
                    foreach (KeyValuePair<CoinReference, TransactionOutput> kp in tx.References)
                    {
                        if (this.Operater.Wallet.Contains(kp.Value.ScriptHash))
                            stateChangedIndex = block.Index + 1;
                    }
                    foreach (var output in tx.Outputs)
                    {
                        if (this.Operater.Wallet.Contains(output.ScriptHash))
                            stateChangedIndex = block.Index + 1;
                    }
                }
            }
        }
        public void BeforeOnBlock(Block block)
        {
        }
        public void OnBlock(Block block)
        {
        }

        public void HeartBeat(HeartBeatContext context)
        {

            if (stateChangedIndex > 0 && this.Operater.IsNotNull() && this.Operater.Wallet.IsNotNull())
            {
                //reloadAsset();
                this.bt_Fresh.Text = UIHelper.LocalString("刷新账户状态", "Refresh account status");
            }

            stateChangedIndex = 0;
        }


        public void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            reloadAsset();
        }
        public void OnRebuild()
        {
            this.DoInvoke(() =>
            {
                this.Clear();
            });
        }
        void reloadAsset()
        {
            this.DoInvoke(() =>
            {
                this.Clear();
                if (this.Operater.IsNotNull() && this.Operater.Wallet.IsNotNull())
                {
                    var acts = this.Operater.Wallet.GetAccounts();
                    this.bt_Fresh.Text = UIHelper.LocalString($"{acts.Count()}个账户", $"{acts.Count()} accounts");
                    foreach (var act in acts)
                    {
                        AccountNode node = new AccountNode(this.Operater.Wallet, act);
                        this.treeAsset.Nodes.Add(node);
                    }
                }
            });

        }
        #endregion

        private void bt_Fresh_Click(object sender, EventArgs e)
        {
            this.reloadAsset();
        }
    }
}
