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
using OX.IO.Data.LevelDB;
using System.Security.Principal;
using OX.IO;
using System.Security.Cryptography.Xml;
using Nethereum.Model;
using Akka.Actor.Dsl;

namespace OX.Wallets.Base
{
    public partial class EthAsset : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        class EthAccountAsset
        {
            public OpenAccount Account;
            public UInt256 AssetId;
            public string AssetName;
            public EthAccountAsset(OpenAccount account, UInt256 asetId = default, string assetName = null)
            {
                this.Account = account;
                this.AssetId = asetId;
                AssetName = assetName;
            }
        }
        public Module Module { get; set; }
        private INotecase Operater;
        #region Constructor Region

        public EthAsset()
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
                DarkTreeNode[] nodes = treeAsset.SelectedNodes.ToArray();
                if (nodes != null && nodes.Length == 1)
                {
                    DarkTreeNode node = nodes[0];
                    var nodeType = (int)node.NodeType;
                    EthAccountAsset openaccount = node.Tag as EthAccountAsset;
                    //查看私钥
                    sm = new ToolStripMenuItem(UIHelper.LocalString("查看私钥", "Show Private Key"));
                    sm.Tag = node.Tag;
                    sm.Click += Sm_Click;
                    menu.Items.Add(sm);
                    //复制以太坊地址
                    sm = new ToolStripMenuItem(UIHelper.LocalString("复制以太坊地址", "Copy Ethereum Address"));
                    sm.Tag = node.Tag;
                    sm.Click += Sm_Click3;
                    menu.Items.Add(sm);
                    if (openaccount.Account.HaveMapAccount)
                    {
                        sm = new ToolStripMenuItem(UIHelper.LocalString("复制映射地址", "Copy Map Address"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click1;
                        menu.Items.Add(sm);
                        if (nodeType == 3 && openaccount.Account.LastTransferHeight + 10 < Blockchain.Singleton.Height)
                        {
                            sm = new ToolStripMenuItem(UIHelper.LocalString("从映射地址转帐", "Transfer from Map Address"));
                            sm.Tag = node.Tag;
                            sm.Click += Sm_Click2;
                            menu.Items.Add(sm);
                        }
                    }
                }
                if (menu.Items.Count > 0)
                    menu.Show(this.treeAsset, e.Location);

            }
        }

        private void Sm_Click3(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            EthAccountAsset openaccount = ToolStripMenuItem.Tag as EthAccountAsset;
            try
            {
                var addr = openaccount.Account.Address;
                Clipboard.SetText(addr);
                string msg = addr + UIHelper.LocalString("  已复制", "  copied");
                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }

        private void Sm_Click2(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            EthAccountAsset openaccount = ToolStripMenuItem.Tag as EthAccountAsset;
            var from = openaccount.Account.MapAccount.MapScriptHash;
            var provider = WalletBappProvider.Instance;
            if (provider == null) return;
            if (this.Operater.Wallet is OpenWallet openWallet)
            {
                var mus = provider.GetEthMapUTXOs(from, openaccount.AssetId);
                if (mus.IsNotNullAndEmpty())
                {
                    var balance = mus.Sum(m => m.Value.Value);
                    using (DialogEthMapPayTo dialog = new DialogEthMapPayTo(this.Operater, openaccount.AssetId, openaccount.AssetName, balance, from))
                    {
                        if (dialog.ShowDialog() != DialogResult.OK) return;
                        var output = dialog.BuildOutput();
                        List<UTXO> utxos = new List<UTXO>();
                        foreach (var r in mus)
                        {
                            utxos.Add(new UTXO
                            {
                                Address = r.Value.ScriptHash,
                                Value = r.Value.Value.GetInternalValue(),
                                TxId = r.Key.TxId,
                                N = r.Key.N
                            });
                        }
                        List<string> excludedUtxoKeys = new List<string>();
                        if (utxos.SortSearch(output.Value.GetInternalValue(), excludedUtxoKeys, out UTXO[] selectedUtxos, out long remainder))
                        {
                            List<TransactionOutput> outputs = new List<TransactionOutput>();
                            outputs.Add(output);
                            if (remainder > 0)
                            {
                                outputs.Add(new TransactionOutput { AssetId = openaccount.AssetId, Value = new Fixed8(remainder), ScriptHash = from });
                            }
                            List<CoinReference> inputs = new List<CoinReference>();
                            foreach (var utxo in selectedUtxos)
                            {
                                inputs.Add(new CoinReference { PrevHash = utxo.TxId, PrevIndex = utxo.N });
                            }
                            ContractTransaction tx = new ContractTransaction
                            {
                                Attributes = new TransactionAttribute[0],
                                Outputs = outputs.ToArray(),
                                Inputs = inputs.ToArray(),
                                Witnesses = new Witness[0]
                            };
                            var key = openaccount.Account.GetPrivateKey(openWallet.WalletPassword);
                            var ecKey = new Nethereum.Signer.EthECKey(key, true);
                            var signer = new Nethereum.Signer.EthereumMessageSigner();
                            var signMessage = signer.EncodeUTF8AndSign(tx.InputOutputHash.ToArray().ToHexString(), ecKey);
                            tx.Attributes = new TransactionAttribute[] { new TransactionAttribute { Usage = TransactionAttributeUsage.EthSignature, Data = System.Text.Encoding.UTF8.GetBytes(signMessage) } };
                            EthereumMapTransaction emt = new EthereumMapTransaction { EthereumAddress = openaccount.Account.Address };
                            var oxKey = openWallet.GetHeldAccounts().First().GetKey();
                            var account = LockAssetHelper.CreateAccount(openWallet, emt.GetContract(), oxKey);
                            tx = LockAssetHelper.Build(tx, new AvatarAccount[] { account });
                            if (tx.IsNotNull())
                            {
                                this.Operater.Wallet.ApplyTransaction(tx);
                                this.Operater.Relay(tx);
                                openaccount.Account.LastTransferHeight = Blockchain.Singleton.Height;
                                if (this.Operater != default)
                                {
                                    string msg = UIHelper.LocalString($"广播以太坊映射转帐交易成功  {tx.Hash}", $"Relay transfer ethereum map asset transaction completed  {tx.Hash}");
                                    //Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                                    DarkMessageBox.ShowInformation(msg, "");
                                }
                            }
                        }
                    }
                }
            }
        }

        private void Sm_Click1(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            EthAccountAsset openaccount = ToolStripMenuItem.Tag as EthAccountAsset;
            try
            {
                var addr = openaccount.Account.MapAccount.MapScriptHash.ToAddress();
                Clipboard.SetText(addr);
                string msg = addr + UIHelper.LocalString("  已复制", "  copied");
                Bapp.PushCrossBappMessage(new CrossBappMessage() { Content = msg, From = this.Module.Bapp });
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }

        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            EthAccountAsset openaccount = ToolStripMenuItem.Tag as EthAccountAsset;
            if (this.Operater.Wallet is OpenWallet openwallet)
            {
                using (VerifyPwdForMnemonic VerifyPwdForMnemonic = new VerifyPwdForMnemonic())
                {
                    if (VerifyPwdForMnemonic.ShowDialog() != DialogResult.OK || openwallet.WalletPassword != VerifyPwdForMnemonic.GetPassword()) return;
                    new DialogShowOpenAccountKey(openaccount.Account, openwallet.WalletPassword).ShowDialog();
                }

            }
        }

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
            var provider = WalletBappProvider.Instance;
            if (provider == null) return;
            foreach (var tx in block.Transactions)
            {
                //utxo
                if (tx.Outputs.IsNotNullAndEmpty())
                {
                    for (ushort k = 0; k < tx.Outputs.Length; k++)
                    {
                        var output = tx.Outputs[k];

                        if (this.Operater.Wallet is OpenWallet openWallet && openWallet.TryGetEthAccount(output.ScriptHash, out OpenAccount _))
                        {
                            reload();
                        }
                    }
                }
            }
        }
        public void BeforeOnBlock(Block block)
        {
            var provider = WalletBappProvider.Instance;
            if (provider == null) return;
            foreach (var tx in block.Transactions)
            {
                //txo
                foreach (KeyValuePair<CoinReference, TransactionOutput> kp in tx.References)
                {
                    var ethMapOutputKey = new EthMapOutputKey { TxId = kp.Key.PrevHash, N = kp.Key.PrevIndex };
                    if (provider.EthMapUTXO.ContainsKey(ethMapOutputKey))
                    {
                        reload();
                    }
                }
               
            }
        }
        public void OnBlock(Block block)
        {
        }

        public void HeartBeat(HeartBeatContext context)
        {
        }


        public void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            reload();
        }
        public void OnRebuild()
        {
        }
        void reload()
        {
            this.DoInvoke(() =>
            {
                this.Clear();
                if (this.Operater.Wallet is OpenWallet openwallet)
                {
                    var provider = WalletBappProvider.Instance;
                    foreach (var act in openwallet.EthAccounts)
                    {
                        DarkTreeNode node = new DarkTreeNode(act.Address) { Tag = new EthAccountAsset(act), NodeType = 1 };
                        if (act.HaveMapAccount)
                        {
                            var subnode = new DarkTreeNode(UIHelper.LocalString($"映射地址: {act.MapAccount.MapScriptHash.ToAddress()}", $"Map Address: {act.MapAccount.MapScriptHash.ToAddress()}")) { Tag = new EthAccountAsset(act), NodeType = 2 };
                            var utxos = provider.GetEthMapUTXOs(act.MapAccount.MapScriptHash);
                            if (utxos.IsNotNullAndEmpty())
                            {
                                foreach (var g in utxos.Select(m => m.Value).GroupBy(m => m.AssetId))
                                {
                                    var assetId = g.Key;
                                    var assetname = string.Empty;

                                    if (assetId.Equals(Blockchain.OXS))
                                        assetname = "OXS";
                                    else if (assetId.Equals(Blockchain.OXC))
                                        assetname = "OXC";
                                    else
                                    {
                                        var state = Blockchain.Singleton.Store.GetAssets().TryGet(assetId);
                                        assetname = state.GetName();
                                    }
                                    var n3 = new DarkTreeNode($"{assetname} :   {g.Sum(m => m.Value)}") { Tag = new EthAccountAsset(act, assetId, assetname), NodeType = 3 };
                                    var n4 = new DarkTreeNode(UIHelper.LocalString($"资产Id: {assetId.ToString()}", $"AssetId: {assetId.ToString()}")) { Tag = new EthAccountAsset(act, assetId, assetname), NodeType = 3 };
                                    n3.Nodes.Add(n4);
                                    subnode.Nodes.Add(n3);
                                }
                            }
                            node.Nodes.Add(subnode);
                        }
                        this.treeAsset.Nodes.Add(node);
                    }
                }
            });
        }
        #endregion
    }
}
