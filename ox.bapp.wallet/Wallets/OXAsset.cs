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
using OX.Wallets.UI.Forms;
using OX.Persistence;
using OX.Cryptography.ECC;
using OX.Wallets.Base.Wallets;

namespace OX.Wallets.Base
{
    public partial class OXAsset : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        private INotecase Operater;
        private bool balance_changed = true;
        #region Constructor Region

        public OXAsset()
        {
            InitializeComponent();
            this.DockArea = DarkDockArea.Left;
            this.treeAsset.MouseDown += TreeAsset_MouseDown;
        }

        private void TreeAsset_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DarkContextMenu menu = new DarkContextMenu();
                ToolStripMenuItem sm;
                ////-4,创建新地址
                //sm = new ToolStripMenuItem(UIHelper.LocalString("创建新账户", "New Account"));
                //sm.Click += SmNewAddress_Click;
                //menu.Items.Add(sm);
                ////-3,创建新地址
                //sm = new ToolStripMenuItem(UIHelper.LocalString("导入私钥", "Import Private Key"));
                //sm.Click += SmImportWif_Click;
                //menu.Items.Add(sm);
                //-3,导入监视地址
                sm = new ToolStripMenuItem(UIHelper.LocalString("添加监视地址", "Add Watch Address"));
                sm.Click += SmImportWatchAddress_Click;
                menu.Items.Add(sm);
                sm = new ToolStripMenuItem(UIHelper.LocalString("查看原生资产详情", "View Native Asset Details"));
                sm.Click += Sm_Click4;
                menu.Items.Add(sm);
                DarkTreeNode[] nodes = treeAsset.SelectedNodes.ToArray();
                if (nodes != null && nodes.Length == 1)
                {
                    AssetTreeNode AssetTreeNode = null;
                    DarkTreeNode node = nodes[0];
                    if (node is AssetLeaftTreeNode leaftNode)
                    {
                        AssetTreeNode = leaftNode.ParentNode as AssetTreeNode;
                    }
                    else
                    {
                        AssetTreeNode = node as AssetTreeNode;
                    }
                    if (AssetTreeNode != default && this.Operater != default)
                    {
                        WalletAccount account = this.Operater.Wallet.GetAccount(OX.Wallets.Wallet.ToScriptHash(AssetTreeNode.Balance.Address));
                        if (account != null)
                        {
                            menu.Items.Add(new ToolStripSeparator());

                            if (account.WatchOnly)
                            {
                                //删除地址
                                sm = new ToolStripMenuItem(UIHelper.LocalString("从钱包删除该账户", "Remove Account"));
                                sm.Tag = AssetTreeNode.Balance;
                                sm.Click += SmDeleteAccount_Click;
                                menu.Items.Add(sm);
                            }
                            else
                            {
                                //查看私钥
                                sm = new ToolStripMenuItem(UIHelper.LocalString("查看私钥", "Show Private Key"));
                                sm.Tag = AssetTreeNode.Balance;
                                sm.Click += SmShowKey_Click;
                                menu.Items.Add(sm);
                                //查看私营私钥
                                sm = new ToolStripMenuItem(UIHelper.LocalString("查看私营资产", "View Private Assets"));
                                sm.Tag = AssetTreeNode.Balance;
                                sm.Click += Sm_Click3;
                                menu.Items.Add(sm);
                                //商业频道租赁
                                sm = new ToolStripMenuItem(UIHelper.LocalString("商业频道租赁", "Business Channel Lease"));
                                sm.Tag = AssetTreeNode.Balance;
                                sm.Click += SmDetain_Click;
                                menu.Items.Add(sm);
                                //转账
                                sm = new ToolStripMenuItem(UIHelper.LocalString("从该账户单独转账", "Transfer from this account"));
                                sm.Tag = AssetTreeNode.Balance;
                                sm.Click += SmTransfer_Click;
                                menu.Items.Add(sm);
                                //转账到以太坊映射地址
                                sm = new ToolStripMenuItem(UIHelper.LocalString("转账到以太坊映射地址", "Transfer to Ethereum map address"));
                                sm.Tag = AssetTreeNode.Balance;
                                sm.Click += Sm_Click5;
                                menu.Items.Add(sm);
                                //整理余额碎片
                                sm = new ToolStripMenuItem(UIHelper.LocalString("整理该账户余额碎片", "Defragment this account balance"));
                                sm.Tag = AssetTreeNode.Balance;
                                sm.Click += Sm_Click1;
                                menu.Items.Add(sm);
                                //复制到粘贴板
                                sm = new ToolStripMenuItem(UIHelper.LocalString("复制账户主地址到剪贴板", "Copy account private address to clipboard"));
                                sm.Tag = AssetTreeNode.Balance;
                                sm.Click += SmCopy_Click;
                                menu.Items.Add(sm);
                                //投票
                                if (AssetTreeNode.Balance.GTS > Fixed8.Zero)
                                {
                                    sm = new ToolStripMenuItem(UIHelper.LocalString("投票", "Vote"));
                                    sm.Tag = AssetTreeNode.Balance;
                                    sm.Click += Vote_Click;
                                    menu.Items.Add(sm);
                                }
                                //全网捐赠
                                sm = new ToolStripMenuItem(UIHelper.LocalString("从该账户单独捐献", "Contribute from this account"));
                                sm.Tag = AssetTreeNode.Balance;
                                sm.Click += Sm_Click;
                                menu.Items.Add(sm);
                                //锁仓转帐
                                sm = new ToolStripMenuItem(UIHelper.LocalString("从该账户转账到锁仓账户", "Transfer from this account for lock"));
                                sm.Tag = AssetTreeNode.Balance;
                                sm.Click += Sm_Click2;
                                menu.Items.Add(sm);
                            }
                            //备注账户名称
                            sm = new ToolStripMenuItem(UIHelper.LocalString("备注账户名称", "Note Account"));
                            sm.Tag = account;
                            sm.Click += SmNoteAccount_Click;
                            menu.Items.Add(sm);
                        }
                    }
                }
                if (menu.Items.Count > 0)
                    menu.Show(this.treeAsset, e.Location);
            }
        }

        private void Sm_Click5(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            BalanceModel Balance = ToolStripMenuItem.Tag as BalanceModel;
            WalletAccount account = this.Operater.Wallet.GetAccount(OX.Wallets.Wallet.ToScriptHash(Balance.Address));
            if (account != null)
            {
                KeyPair kp = account.GetKey();
                UInt160 from = OX.Wallets.Wallet.ToScriptHash(Balance.Address);
                using (DialogSinglePayToEth dialog = new DialogSinglePayToEth(this.Operater, from))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    TxOutListBoxItem item = dialog.GetOutput(out string ethAddress,out uint lockIndex);
                    if (item.IsNotNull())
                    {
                        SingleTransactionWrapper<EthereumMapTransaction> stw = new SingleTransactionWrapper<EthereumMapTransaction>(from, item.ToTxOutput());
                        EthereumMapTransaction ct = stw.Get();
                        ct.EthereumAddress = ethAddress;
                        ct.LockExpirationIndex = lockIndex;
                        ct.EthMapContract = Blockchain.EthereumMapContractScriptHash;
                        var tx = this.Operater.Wallet.MakeTransaction<EthereumMapTransaction>(ct, stw.From, stw.From);
                        //var tx = this.Operater.Wallet.MakeSingleTransaction(stw);
                        if (tx != null)
                        {
                            if (tx.Inputs.Count() > 20)
                            {
                                string msg = $"{UIHelper.LocalString("交易输入项太多,请分为多次转账", "There are too many transaction input. Please transfer multiple times")}";
                                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                                DarkMessageBox.ShowInformation(msg, "");
                                return;
                            }
                            this.Operater.SignAndSendTx(tx);
                            if (this.Operater != default)
                            {
                                string msg = $"{UIHelper.LocalString("交易已广播", "Relay transaction completed")}   {tx.Hash}";
                                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                                DarkMessageBox.ShowInformation(msg, "");
                            }
                        }
                    }
                }
            }
        }

        private void Sm_Click4(object sender, EventArgs e)
        {
            new DialogNativeAsset().ShowDialog();
        }

        private void Sm_Click3(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            BalanceModel Balance = ToolStripMenuItem.Tag as BalanceModel;
            if (this.Operater.Wallet is OpenWallet openWallet)
            {
                WalletAccount account = openWallet.GetAccount(OX.Wallets.Wallet.ToScriptHash(Balance.Address));
                using (DialogViewPrivateAssets dialog = new DialogViewPrivateAssets(this.Operater, openWallet, account))
                {
                    dialog.ShowDialog();
                }
            }
        }

        private void Sm_Click1(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            BalanceModel Balance = ToolStripMenuItem.Tag as BalanceModel;
            WalletAccount account = this.Operater.Wallet.GetAccount(OX.Wallets.Wallet.ToScriptHash(Balance.Address));
            if (account != null)
            {
                KeyPair kp = account.GetKey();
                UInt160 from = OX.Wallets.Wallet.ToScriptHash(Balance.Address);
                new DialogDefragment(this.Operater, from).ShowDialog();
            }
        }

        private void Sm_Click2(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            BalanceModel Balance = ToolStripMenuItem.Tag as BalanceModel;
            WalletAccount account = this.Operater.Wallet.GetAccount(OX.Wallets.Wallet.ToScriptHash(Balance.Address));
            if (account != null)
            {
                KeyPair kp = account.GetKey();
                UInt160 from = OX.Wallets.Wallet.ToScriptHash(Balance.Address);
                using (DialogLockTransfer dialog = new DialogLockTransfer(this.Operater, from))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    var output = dialog.GetOutput(out ECPoint ecp, out bool isTime, out uint expiration);
                    if (isTime)
                    {
                        if (expiration - DateTime.Now.ToTimestamp() < 3600)
                        {
                            string msg = $"{UIHelper.LocalString("锁定的时间太短", "Locking time is too short")}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                            return;
                        }
                    }
                    else
                    {
                        if (expiration - Blockchain.Singleton.Height < 100)
                        {
                            string msg = $"{UIHelper.LocalString("锁定的区块高度太低", "Locked block height is too low")}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                            return;
                        }
                    }
                    LockAssetTransaction lat = new LockAssetTransaction
                    {
                        LockContract = Blockchain.LockAssetContractScriptHash,
                        IsTimeLock = isTime,
                        LockExpiration = expiration,
                        Flag = 0,
                        Recipient = ecp
                    };
                    output.ScriptHash = lat.GetContract().ScriptHash;
                    lat.Outputs = new TransactionOutput[] { output };
                    lat = this.Operater.Wallet.MakeTransaction(lat, from, from);
                    if (lat != null)
                    {
                        if (lat.Inputs.Count() > 20)
                        {
                            string msg = $"{UIHelper.LocalString("交易输入项太多,请分为多次转账", "There are too many transaction input. Please transfer multiple times")}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                            return;
                        }
                        this.Operater.SignAndSendTx(lat);
                        if (this.Operater != default)
                        {
                            string msg = $"{UIHelper.LocalString("交易已广播", "Relay transaction completed")}   {lat.Hash}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                        }
                    }
                }
            }
        }

        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            BalanceModel Balance = ToolStripMenuItem.Tag as BalanceModel;
            WalletAccount account = this.Operater.Wallet.GetAccount(OX.Wallets.Wallet.ToScriptHash(Balance.Address));
            if (account != null)
            {
                KeyPair kp = account.GetKey();
                UInt160 from = OX.Wallets.Wallet.ToScriptHash(Balance.Address);
                using (DialogSingleContributeTo dialog = new DialogSingleContributeTo(this.Operater, from))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    var amountKind = dialog.GetAmountKind();
                    RewardTransaction rwTx = new RewardTransaction { RewardAmount = amountKind };
                    var tx = this.Operater.Wallet.MakeTransaction(rwTx, from, from);

                    if (tx != null)
                    {
                        if (tx.Inputs.Count() > 20)
                        {
                            string msg = $"{UIHelper.LocalString("交易输入项太多,请分为多次捐献", "There are too many transaction input. Please contribute multiple times")}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                            return;
                        }
                        this.Operater.SignAndSendTx(tx);
                        if (this.Operater != default)
                        {
                            string msg = $"{UIHelper.LocalString("交易已广播", "Relay transaction completed")}   {tx.Hash}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                        }
                    }
                }
            }
        }

        //private void SmImportWif_Click(object sender, System.EventArgs e)
        //{
        //    using (DialogImportWif dialog = new DialogImportWif())
        //    {
        //        if (dialog.ShowDialog() != DialogResult.OK) return;
        //        //listView1.SelectedIndices.Clear();
        //        string wif = dialog.Wif;
        //        try
        //        {
        //            WalletAccount account = this.Operater.Wallet.Import(wif);
        //            BalanceModel bm = new BalanceModel()
        //            {
        //                Address = account.Address,
        //                GTS = Fixed8.Zero,
        //                GTC = Fixed8.Zero
        //            };
        //            this.AddBalanceModel(bm);
        //            if (this.Operater != default && this.Operater.Wallet is NEP6Wallet wallet)
        //                wallet.Save();
        //        }
        //        catch
        //        {
        //        }
        //    }
        //}
        private void SmImportWatchAddress_Click(object sender, System.EventArgs e)
        {
            using (DialogImportWatchAccount dialog = new DialogImportWatchAccount())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                try
                {
                    WalletAccount account = this.Operater.Wallet.CreateAccount(dialog.Address);
                    BalanceModel bm = new BalanceModel()
                    {
                        Address = account.Address,
                        GTS = Fixed8.Zero,
                        GTC = Fixed8.Zero
                    };
                    this.AddBalanceModel(bm);
                    if (this.Operater.Wallet is NEP6Wallet wallet)
                        wallet.Save();
                }
                catch
                {
                }
            }
        }
        //private void SmNewAddress_Click(object sender, System.EventArgs e)
        //{
        //    WalletAccount account = this.Operater.Wallet.CreateAccount();
        //    BalanceModel bm = new BalanceModel()
        //    {
        //        Address = account.Address,
        //        GTS = Fixed8.Zero,
        //        GTC = Fixed8.Zero
        //    };
        //    this.AddBalanceModel(bm);
        //    if (this.Operater.Wallet is NEP6Wallet wallet)
        //        wallet.Save();
        //}
        private void SmShowKey_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            BalanceModel Balance = ToolStripMenuItem.Tag as BalanceModel;
            if (this.Operater.Wallet is OpenWallet openWallet)
            {
                using (VerifyPwdForMnemonic VerifyPwdForMnemonic = new VerifyPwdForMnemonic())
                {
                    if (VerifyPwdForMnemonic.ShowDialog() != DialogResult.OK || openWallet.WalletPassword != VerifyPwdForMnemonic.GetPassword()) return;
                    WalletAccount account = openWallet.GetAccount(OX.Wallets.Wallet.ToScriptHash(Balance.Address));
                    using (DialogShowKey dialog = new DialogShowKey(account))
                    {
                        dialog.ShowDialog();
                    }
                }
            }
        }
        private void SmDetain_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            BalanceModel Balance = ToolStripMenuItem.Tag as BalanceModel;
            WalletAccount account = this.Operater.Wallet.GetAccount(OX.Wallets.Wallet.ToScriptHash(Balance.Address));
            using (DetainDialog dialog = new DetainDialog(account))
            {
                var result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    var tx = dialog.GetTransaction();
                    if (tx.IsNotNull())
                    {
                        tx = this.Operater.Wallet.MakeTransaction(tx);
                        if (tx.IsNotNull())
                        {
                            this.Operater.SignAndSendTx(tx);
                            string msg = $"{UIHelper.LocalString("商业频道租赁交易已广播", "Relay business channel lease transaction completed")}   {tx.Hash}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });

                            DarkMessageBox.ShowInformation(msg, "");
                        }
                    }
                }
            }
        }
        private void SmTransfer_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            BalanceModel Balance = ToolStripMenuItem.Tag as BalanceModel;
            WalletAccount account = this.Operater.Wallet.GetAccount(OX.Wallets.Wallet.ToScriptHash(Balance.Address));
            if (account != null)
            {
                KeyPair kp = account.GetKey();
                UInt160 from = OX.Wallets.Wallet.ToScriptHash(Balance.Address);
                using (DialogSinglePayTo dialog = new DialogSinglePayTo(this.Operater, from))
                {
                    if (dialog.ShowDialog() != DialogResult.OK) return;
                    TxOutListBoxItem item = dialog.GetOutput();
                    if (item.IsNotNull())
                    {
                        SingleTransactionWrapper<ContractTransaction> stw = new SingleTransactionWrapper<ContractTransaction>(from, item.ToTxOutput());
                        //Transaction tx = new ContractTransaction();
                        //List<TransactionAttribute> attributes = new List<TransactionAttribute>();
                        //tx.Attributes = attributes.ToArray();
                        //tx.Outputs = new TransactionOutput[] { item.ToTxOutput() };
                        var tx = this.Operater.Wallet.MakeSingleTransaction(stw);
                        if (tx != null)
                        {
                            if (tx.Inputs.Count() > 20)
                            {
                                string msg = $"{UIHelper.LocalString("交易输入项太多,请分为多次转账", "There are too many transaction input. Please transfer multiple times")}";
                                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                                DarkMessageBox.ShowInformation(msg, "");
                                return;
                            }
                            this.Operater.SignAndSendTx(tx);
                            if (this.Operater != default)
                            {
                                string msg = $"{UIHelper.LocalString("交易已广播", "Relay transaction completed")}   {tx.Hash}";
                                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                                DarkMessageBox.ShowInformation(msg, "");
                            }
                        }
                    }
                }
            }
        }
        private void SmCopy_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            BalanceModel Balance = ToolStripMenuItem.Tag as BalanceModel;
            try
            {
                Clipboard.SetText(Balance.Address);
                string msg = Balance.Address + UIHelper.LocalString("  已复制", "  copied");
                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }
        private void Vote_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            BalanceModel Balance = ToolStripMenuItem.Tag as BalanceModel;
            if (Balance.GTS > Fixed8.Zero)
                using (VotingDialog dialog = new VotingDialog(this.Operater.Wallet, Balance.Address.ToScriptHash()))
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

            try
            {
                Clipboard.SetText(Balance.Address);
                string msg = Balance.Address + UIHelper.LocalString("  已复制", "  copied");
                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }
        private void SmDeleteAccount_Click(object sender, System.EventArgs e)
        {
            if (this.Operater != default && this.Operater.Wallet != default)
            {
                if (DarkMessageBox.ShowInformation(UIHelper.LocalString("你确认要从钱包删除这个账户吗?", "Are you sure you want to delete this account from your wallet?"), UIHelper.LocalString("删除账户", "Delete Account"), DarkDialogButton.OkCancel) == DialogResult.OK)
                {
                    ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
                    BalanceModel Balance = ToolStripMenuItem.Tag as BalanceModel;
                    this.Operater.Wallet.DeleteAccount(OX.Wallets.Wallet.ToScriptHash(Balance.Address));
                    AssetTreeNode node = this.AssetNodes[Balance.Address];
                    if (node != null)
                    {
                        this.treeAsset.Nodes.Remove(node);
                    }
                    if (this.Operater.Wallet is NEP6Wallet wallet)
                        wallet.Save();
                    balance_changed = true;
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
                        using (Snapshot snapshot = Blockchain.Singleton.GetSnapshot())
                        {
                            IEnumerable<Coin> coins = this.Operater.Wallet?.GetCoins().Where(p => !p.State.HasFlag(CoinState.Spent)) ?? Enumerable.Empty<Coin>();

                            var balance_oxs = coins.Where(p => p.Output.AssetId.Equals(Blockchain.OXS_Token.Hash)).GroupBy(p => p.Output.ScriptHash).ToDictionary(p => p.Key, p => p.Sum(i => i.Output.Value));
                            var balance_oxc = coins.Where(p => p.Output.AssetId.Equals(Blockchain.OXC_Token.Hash)).GroupBy(p => p.Output.ScriptHash).ToDictionary(p => p.Key, p => p.Sum(i => i.Output.Value));
                            foreach (var act in this.Operater.Wallet.GetAccounts())
                            {
                                Fixed8 ans = balance_oxs.ContainsKey(act.ScriptHash) ? balance_oxs[act.ScriptHash] : Fixed8.Zero;
                                Fixed8 anc = balance_oxc.ContainsKey(act.ScriptHash) ? balance_oxc[act.ScriptHash] : Fixed8.Zero;
                                BalanceModel balanceModel = new BalanceModel()
                                {
                                    OnlyWatch = act.WatchOnly,
                                    Address = act.Address,
                                    GTS = ans,
                                    GTC = anc,
                                    Label = act.Label
                                };
                                this.AddBalanceModel(balanceModel);
                            }
                        }
                    }
                }
            }
        }

        public Dictionary<string, AssetTreeNode> AssetNodes
        {
            get
            {
                return this.treeAsset.Nodes.Cast<AssetTreeNode>().ToDictionary(m => m.Balance.Address);

            }
        }
        public void Clear()
        {
            this.treeAsset.Nodes.Clear();
        }
        public void AddBalanceModel(BalanceModel model)
        {
            if (this.AssetNodes.TryGetValue(model.Address, out AssetTreeNode control))
            {
                control.SetBalance(model);
            }
            else
            {
                this.treeAsset.Nodes.Add(new AssetTreeNode(model));
            }
        }
        #endregion
        #region IBlockChainTrigger
        public void OnBappEvent(BappEvent be) { }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
        }

        public void AfterOnBlock(Block block)
        {
            this.balance_changed = true;
        }
        public void BeforeOnBlock(Block block)
        {
        }
        public void OnBlock(Block block)
        {
        }

        public void HeartBeat(HeartBeatContext context)
        {
            doAsset();
        }
        void doAsset()
        {
            if (this.balance_changed)
                using (Snapshot snapshot = Blockchain.Singleton.GetSnapshot())
                {
                    IEnumerable<Coin> coins = this.Operater.Wallet?.GetCoins().Where(p => !p.State.HasFlag(CoinState.Spent)) ?? Enumerable.Empty<Coin>();

                    var balance_oxs = coins.Where(p => p.Output.AssetId.Equals(Blockchain.OXS_Token.Hash)).GroupBy(p => p.Output.ScriptHash).ToDictionary(p => p.Key, p => p.Sum(i => i.Output.Value));
                    var balance_oxc = coins.Where(p => p.Output.AssetId.Equals(Blockchain.OXC_Token.Hash)).GroupBy(p => p.Output.ScriptHash).ToDictionary(p => p.Key, p => p.Sum(i => i.Output.Value));
                    foreach (var act in this.Operater.Wallet.GetAccounts())
                    {
                        Fixed8 ans = balance_oxs.ContainsKey(act.ScriptHash) ? balance_oxs[act.ScriptHash] : Fixed8.Zero;
                        Fixed8 anc = balance_oxc.ContainsKey(act.ScriptHash) ? balance_oxc[act.ScriptHash] : Fixed8.Zero;
                        BalanceModel balanceModel = new BalanceModel()
                        {
                            OnlyWatch = act.WatchOnly,
                            Address = act.Address,
                            GTS = ans,
                            GTC = anc,
                            Label = act.Label
                        };
                        this.AddBalanceModel(balanceModel);
                    }
                    balance_changed = false;
                }
        }

        public void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            this.Clear();
            doAsset();
        }
        public void OnRebuild() { }
        #endregion
    }
}
