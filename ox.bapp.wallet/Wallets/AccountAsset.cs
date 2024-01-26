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

namespace OX.Wallets.Base
{
    public partial class AccountAsset : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        public static List<OutputKey> lockAssetKeys = new List<OutputKey>();
        public Module Module { get; set; }
        private INotecase Operater;
        uint stateChangedIndex = 0;
        #region Constructor Region

        public AccountAsset()
        {
            InitializeComponent();
            this.DockArea = DarkDockArea.Left;
            this.treeAsset.MouseDown += TreeAsset_MouseDown;
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
                            //查看私钥
                            sm = new ToolStripMenuItem(UIHelper.LocalString("查看私钥", "Show Private Key"));
                            sm.Tag = account;
                            sm.Click += SmShowKey_Click;
                            menu.Items.Add(sm);
                            //商业频道租赁
                            sm = new ToolStripMenuItem(UIHelper.LocalString("商业频道租赁", "Business Channel Lease"));
                            sm.Tag = account;
                            sm.Click += SmDetain_Click;
                            menu.Items.Add(sm);
                            //转账
                            sm = new ToolStripMenuItem(UIHelper.LocalString("从该账户转账", "Transfer from this account"));
                            sm.Tag = account;
                            sm.Click += SmTransfer_Click;
                            menu.Items.Add(sm);
                            //转账到以太坊映射地址
                            sm = new ToolStripMenuItem(UIHelper.LocalString("转账到以太坊映射地址", "Transfer to Ethereum map address"));
                            sm.Tag = account;
                            sm.Click += TransferToEthMap_Click5;
                            menu.Items.Add(sm);
                            //锁仓转帐
                            sm = new ToolStripMenuItem(UIHelper.LocalString("转账到锁仓账户", "Transfer from this account for lock"));
                            sm.Tag = account;
                            sm.Click += TransferLock_Click2;
                            menu.Items.Add(sm);
                            //整理余额碎片
                            sm = new ToolStripMenuItem(UIHelper.LocalString("整理该账户余额碎片", "Defragment this account balance"));
                            sm.Tag = account;
                            sm.Click += Organizefragments_Click1;
                            menu.Items.Add(sm);
                            //复制到粘贴板
                            sm = new ToolStripMenuItem(UIHelper.LocalString("复制账户主地址", "Copy account private address"));
                            sm.Tag = account;
                            sm.Click += SmCopy_Click;
                            menu.Items.Add(sm);
                            //提取主账号OXC
                            sm = new ToolStripMenuItem(UIHelper.LocalString("提取 OXC", "Claim OXC"));
                            sm.Tag = account;
                            sm.Click += ClaimOXC_Click;
                            menu.Items.Add(sm);
                            //提取锁仓账号OXC
                            sm = new ToolStripMenuItem(UIHelper.LocalString("提取锁仓资产的OXC", "Claim locked asset OXC"));
                            sm.Tag = account;
                            sm.Click += ClaimLockOXC_Click2;
                            menu.Items.Add(sm);
                            //投票
                            sm = new ToolStripMenuItem(UIHelper.LocalString("投票", "Vote"));
                            sm.Tag = account;
                            sm.Click += Vote_Click;
                            menu.Items.Add(sm);
                            //全网捐赠
                            sm = new ToolStripMenuItem(UIHelper.LocalString("从该账户捐献", "Contribute from this account"));
                            sm.Tag = account;
                            sm.Click += Contribute_Click;
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
                        if (node is BalanceNode bn)
                        {
                            menu.Items.Add(new ToolStripSeparator());
                            sm = new ToolStripMenuItem(UIHelper.LocalString("解锁所有到期资产", "Unlock all matured assets"));
                            sm.Tag = new Tuple<WalletAccount, UInt256>(bn.Account, bn.AssetID);
                            sm.Click += UnlockAsset_Click;
                            menu.Items.Add(sm);
                        }
                        else if (node is LockRootTreeNode lrtb)
                        {
                            menu.Items.Add(new ToolStripSeparator());
                            sm = new ToolStripMenuItem(UIHelper.LocalString("解锁所有到期资产", "Unlock all matured assets"));
                            sm.Tag = new Tuple<WalletAccount, UInt256>(lrtb.Account, lrtb.AssetID);
                            sm.Click += UnlockAsset_Click;
                            menu.Items.Add(sm);
                        }
                        else
                      if (node is LockAccountTreeNode lat)
                        {
                            bool ok = false;
                            if (lat.LockAssetMerge.Tx.IsTimeLock)
                            {
                                ok = DateTime.Now.ToTimestamp() > lat.LockAssetMerge.Tx.LockExpiration;
                            }
                            else
                            {
                                ok = Blockchain.Singleton.Height > lat.LockAssetMerge.Tx.LockExpiration;
                            }
                            if (ok)
                            {
                                menu.Items.Add(new ToolStripSeparator());
                                //单条解锁
                                sm = new ToolStripMenuItem(UIHelper.LocalString("解锁资产", "Unlock Asset"));
                                sm.Tag = new Tuple<OutputKey, LockAssetMerge>(lat.OutputKey, lat.LockAssetMerge);
                                sm.Click += SingleUnlock_Click;
                                menu.Items.Add(sm);
                            }
                        }
                    }
                }
                if (menu.Items.Count > 0)
                    menu.Show(this.treeAsset, e.Location);
            }
        }
        private void UnlockAsset_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<WalletAccount, UInt256> tag = ToolStripMenuItem.Tag as Tuple<WalletAccount, UInt256>;
            var bizPlugin = WalletBappProvider.Instance;
            if (bizPlugin.IsNotNull() && this.Operater.Wallet is OpenWallet openWallet)
            {
                this.DoInvoke(() =>
                {
                    Dictionary<UInt160, AvatarAccount> acts = new Dictionary<UInt160, AvatarAccount>();
                    List<CoinReference> crs = new List<CoinReference>();

                    var rs = bizPlugin.GetMyAllLockAssets()?.Where(m => m.Value.Tx.Recipient.Equals(tag.Item1.GetKey().PublicKey) && m.Value.Output.AssetId.Equals(tag.Item2));
                    int c = 0;
                    Fixed8 amount = Fixed8.Zero;
                    foreach (var r in rs)
                    {
                        if ((r.Value.Tx.IsTimeLock && DateTime.Now.ToTimestamp() > r.Value.Tx.LockExpiration) || (!r.Value.Tx.IsTimeLock && Blockchain.Singleton.Height > r.Value.Tx.LockExpiration))
                        {
                            c++;
                            amount += r.Value.Output.Value;
                            var lockAccount = LockAssetHelper.CreateAccount(openWallet, r.Value.Tx.GetContract(), tag.Item1.GetKey());//lock asset account have a some private key with master account
                            acts[lockAccount.ScriptHash] = lockAccount;
                            crs.Add(new CoinReference { PrevHash = r.Key.TxId, PrevIndex = r.Key.N });
                        }
                    }

                    
                    if (acts.IsNotNullAndEmpty() && crs.IsNotNullAndEmpty())
                    {
                        ContractTransaction tx = new ContractTransaction
                        {
                            Attributes = new TransactionAttribute[0],
                            Outputs = new TransactionOutput[] { new TransactionOutput { AssetId = tag.Item2, ScriptHash = tag.Item1.ScriptHash, Value = amount } },
                            Inputs = crs.ToArray(),
                            Witnesses = new Witness[0]
                        };
                        tx = LockAssetHelper.Build(tx, acts.Values.ToArray());
                        if (tx.IsNotNull())
                        {
                            this.Operater.Wallet.ApplyTransaction(tx);
                            this.Operater.Relay(tx);
                            if (this.Operater != default)
                            {
                                string msg = $"{UIHelper.LocalString("批量解锁资产交易已广播", "Relay batch unlock asset transaction completed")}   {tx.Hash}";
                                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                                DarkMessageBox.ShowInformation(msg, "");
                            }
                        }
                    }
                });
            }

        }
        private void SingleUnlock_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Tuple<OutputKey, LockAssetMerge> p = ToolStripMenuItem.Tag as Tuple<OutputKey, LockAssetMerge>;
            var sh = Contract.CreateSignatureRedeemScript(p.Item2.Tx.Recipient).ToScriptHash();
            WalletAccount act = this.Operater.Wallet.GetAccount(sh);
            if (this.Operater.Wallet is OpenWallet openWallet)
            {
                var account = LockAssetHelper.CreateAccount(openWallet, p.Item2.Tx.GetContract(), act.GetKey());//lock asset account have a some private key with master account
                if (account != null)
                {
                    //KeyPair kp = account.GetKey();
                    TransactionOutput output = new TransactionOutput { AssetId = p.Item2.Output.AssetId, Value = p.Item2.Output.Value, ScriptHash = sh };
                    ContractTransaction tx = new ContractTransaction
                    {
                        Attributes = new TransactionAttribute[0],
                        Outputs = new TransactionOutput[] { output },
                        Inputs = new CoinReference[] { new CoinReference { PrevHash = p.Item1.TxId, PrevIndex = p.Item1.N } },
                        Witnesses = new Witness[0]
                    };
                    tx = LockAssetHelper.Build(tx, new AvatarAccount[] { account });
                    if (tx.IsNotNull())
                    {
                        this.Operater.Wallet.ApplyTransaction(tx);
                        this.Operater.Relay(tx);
                        if (this.Operater != default)
                        {
                            string msg = $"{UIHelper.LocalString("解锁资产交易已广播", "Relay unlock asset transaction completed")}   {tx.Hash}";
                            Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                            DarkMessageBox.ShowInformation(msg, "");
                        }
                    }
                }
            }
        }
        private void ClaimLockOXC_Click2(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            if (this.Operater.Wallet is OpenWallet openWallet)
            {
                new ClaimLockAsset(this.Operater, account.ScriptHash).ShowDialog();
            }
        }
        private void ClaimOXC_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            new SingleClaimOXC(this.Operater, account).ShowDialog();
        }

        private void TransferLock_Click2(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            using (DialogLockTransfer dialog = new DialogLockTransfer(this.Operater, account))
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
                lat = this.Operater.Wallet.MakeTransaction(lat, account.ScriptHash, account.ScriptHash);
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
        private void Contribute_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            using (DialogSingleContributeTo dialog = new DialogSingleContributeTo(this.Operater, account.ScriptHash))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                var amountKind = dialog.GetAmountKind();
                RewardTransaction rwTx = new RewardTransaction { RewardAmount = amountKind };
                var tx = this.Operater.Wallet.MakeTransaction(rwTx, account.ScriptHash, account.ScriptHash);

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

            new DialogDefragment(this.Operater, account.ScriptHash).ShowDialog();
        }
        private void TransferToEthMap_Click5(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;

            KeyPair kp = account.GetKey();
            using (DialogSinglePayToEth dialog = new DialogSinglePayToEth(this.Operater, account.ScriptHash))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                TxOutListBoxItem item = dialog.GetOutput(out string ethAddress, out uint lockIndex);
                if (item.IsNotNull())
                {
                    SingleTransactionWrapper<EthereumMapTransaction> stw = new SingleTransactionWrapper<EthereumMapTransaction>(account.ScriptHash, item.ToTxOutput());
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
        private void SmTransfer_Click(object sender, System.EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            WalletAccount account = ToolStripMenuItem.Tag as WalletAccount;
            KeyPair kp = account.GetKey();
            using (DialogSinglePayTo dialog = new DialogSinglePayTo(this.Operater, account.ScriptHash))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                TxOutListBoxItem item = dialog.GetOutput();
                if (item.IsNotNull())
                {
                    SingleTransactionWrapper<ContractTransaction> stw = new SingleTransactionWrapper<ContractTransaction>(account.ScriptHash, item.ToTxOutput());
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
                var bizPlugin = WalletBappProvider.Instance;
                if (bizPlugin.IsNotNull() && this.Operater.IsNotNull() && this.Operater.Wallet.IsNotNull())
                {
                    var acts = this.Operater.Wallet.GetAccounts();
                    this.bt_Fresh.Text = UIHelper.LocalString($"{acts.Count()}个账户", $"{acts.Count()} accounts");
                    var assetRecords = bizPlugin.GetMyAllLockAssets();
                    foreach (var act in acts)
                    {
                        var lockAssetRecords = act.WatchOnly ? default : assetRecords.Where(m => m.Value.Tx.Recipient.Equals(act.GetKey().PublicKey));
                        AccountNode node = new AccountNode(this.Operater.Wallet, act, lockAssetRecords);
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
