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

namespace OX.Wallets.Base
{
    public partial class OXLockAsset : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        class AddressAsset
        {
            public UInt160 SH;
            public UInt256 Asset;
        }
        public Module Module { get; set; }
        private INotecase Operater;
        Dictionary<OutputKey, DarkTreeNode> nodes = new Dictionary<OutputKey, DarkTreeNode>();
        #region Constructor Region

        public OXLockAsset()
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
                if (nodes.IsNotNullAndEmpty() && nodes.Length == 1)
                {
                    DarkTreeNode node = nodes[0];
                    var nodetype = (int)node.NodeType;
                    if (nodetype == 0)
                    {
                        sm = new ToolStripMenuItem(UIHelper.LocalString("提取锁定的OXC分红", "Claim locked OXC bonus"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click2;
                        menu.Items.Add(sm);
                    }
                    else if (nodetype == 1)
                    {
                        sm = new ToolStripMenuItem(UIHelper.LocalString("解锁所有到期资产", "Unlock all matured assets"));
                        sm.Tag = node.Tag;
                        sm.Click += Sm_Click1;
                        menu.Items.Add(sm);
                    }
                    else if (nodetype == 2)
                    {
                        KeyValuePair<OutputKey, LockAssetMerge> p = (KeyValuePair<OutputKey, LockAssetMerge>)node.Tag;
                        bool ok = false;
                        if (p.Value.Tx.IsTimeLock)
                        {
                            ok = DateTime.Now.ToTimestamp() > p.Value.Tx.LockExpiration;
                        }
                        else
                        {
                            ok = Blockchain.Singleton.Height > p.Value.Tx.LockExpiration;
                        }
                        if (ok)
                        {
                            sm = new ToolStripMenuItem(UIHelper.LocalString("解锁资产", "Unlock Asset"));
                            sm.Tag = p;
                            sm.Click += Sm_Click;
                            menu.Items.Add(sm);
                        }
                    }
                }
                if (menu.Items.Count > 0)
                    menu.Show(this.treeAsset, e.Location);
            }
        }

        private void Sm_Click2(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            UInt160 holder = ToolStripMenuItem.Tag as UInt160;
            if (this.Operater.Wallet is OpenWallet openWallet)
            {
                new ClaimLockAsset(this.Operater, holder).ShowDialog();
            }
        }

        private void Sm_Click1(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            AddressAsset aa = ToolStripMenuItem.Tag as AddressAsset;
            //var sh = Contract.CreateSignatureRedeemScript(p.Key).ToScriptHash();
            WalletAccount act = this.Operater.Wallet.GetAccount(aa.SH);
            var bizPlugin = WalletBappProvider.Instance;
            if (bizPlugin.IsNotNull() && this.Operater.Wallet is OpenWallet openWallet)
            {
                System.Threading.Tasks.Task.Run(() =>
                {
                    this.DoInvoke(() =>
                    {
                        Dictionary<UInt160, AvatarAccount> acts = new Dictionary<UInt160, AvatarAccount>();
                        List<CoinReference> crs = new List<CoinReference>();
                        Dictionary<UInt256, TransactionOutput> outputs = new Dictionary<UInt256, TransactionOutput>();
                        int c = 0;
                        var p = bizPlugin.GetMyAllLockAssets().GroupBy(m => m.Value.Tx.Recipient);
                        foreach (var group in p)
                        {
                            if (Contract.CreateSignatureRedeemScript(group.Key).ToScriptHash().Equals(aa.SH))
                            {
                                foreach (var g in group.Where(m => m.Value.Output.AssetId.Equals(aa.Asset)))
                                {
                                    bool ok = false;
                                    if (g.Value.Tx.IsTimeLock)
                                    {
                                        ok = DateTime.Now.ToTimestamp() > g.Value.Tx.LockExpiration;
                                    }
                                    else
                                    {
                                        ok = Blockchain.Singleton.Height > g.Value.Tx.LockExpiration;
                                    }
                                    if (ok)
                                    {
                                        c++;
                                        var lockAccount = LockAssetHelper.CreateAccount(openWallet, g.Value.Tx.GetContract(), act.GetKey());//lock asset account have a some private key with master account
                                        acts[lockAccount.ScriptHash] = lockAccount;
                                        crs.Add(new CoinReference { PrevHash = g.Key.TxId, PrevIndex = g.Key.N });
                                        if (!outputs.TryGetValue(g.Value.Output.AssetId, out TransactionOutput output))
                                        {
                                            output = new TransactionOutput { AssetId = g.Value.Output.AssetId, Value = g.Value.Output.Value, ScriptHash = aa.SH };
                                            outputs[g.Value.Output.AssetId] = output;
                                        }
                                        else
                                        {
                                            output.Value += g.Value.Output.Value;
                                        }
                                        if (c >= 20) break;
                                    }
                                }
                            }
                        }
                        if (acts.IsNotNullAndEmpty() && crs.IsNotNullAndEmpty() && outputs.IsNotNullAndEmpty())
                        {
                            ContractTransaction tx = new ContractTransaction
                            {
                                Attributes = new TransactionAttribute[0],
                                Outputs = outputs.Values.ToArray(),
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
                });
            }

        }

        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            KeyValuePair<OutputKey, LockAssetMerge> p = (KeyValuePair<OutputKey, LockAssetMerge>)ToolStripMenuItem.Tag;
            var sh = Contract.CreateSignatureRedeemScript(p.Value.Tx.Recipient).ToScriptHash();
            WalletAccount act = this.Operater.Wallet.GetAccount(sh);
            if (this.Operater.Wallet is OpenWallet openWallet)
            {
                var account = LockAssetHelper.CreateAccount(openWallet, p.Value.Tx.GetContract(), act.GetKey());//lock asset account have a some private key with master account
                if (account != null)
                {
                    //KeyPair kp = account.GetKey();
                    TransactionOutput output = new TransactionOutput { AssetId = p.Value.Output.AssetId, Value = p.Value.Output.Value, ScriptHash = sh };
                    ContractTransaction tx = new ContractTransaction
                    {
                        Attributes = new TransactionAttribute[0],
                        Outputs = new TransactionOutput[] { output },
                        Inputs = new CoinReference[] { new CoinReference { PrevHash = p.Key.TxId, PrevIndex = p.Key.N } },
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
            foreach (var tx in block.Transactions)
            {
                if (tx is LockAssetTransaction lat)
                {
                    if (lat.IsNotNull() && lat.LockContract.Equals(Blockchain.LockAssetContractScriptHash))
                    {
                        var holder = Contract.CreateSignatureRedeemScript(lat.Recipient).ToScriptHash();
                        if (this.Operater.Wallet.ContainsAndHeld(holder))
                        {
                            refreshLockAsset();
                        }
                    }
                }
                //txo
                foreach (KeyValuePair<CoinReference, TransactionOutput> kp in tx.References)
                {
                    //watch lock asset
                    OutputKey outputkey = new OutputKey { TxId = kp.Key.PrevHash, N = kp.Key.PrevIndex };
                    if (nodes.ContainsKey(outputkey))
                    {
                        refreshLockAsset();
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
        }


        public void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            refreshLockAsset();
        }
        public void OnRebuild()
        {
            this.DoInvoke(() =>
            {
                this.Clear();
            });
        }
        void refreshLockAsset()
        {
            this.DoInvoke(() =>
            {
                this.Clear();
                nodes.Clear();
                var bizPlugin = WalletBappProvider.Instance;
                if (bizPlugin.IsNotNull() && this.Operater.IsNotNull() && this.Operater.Wallet.IsNotNull())
                {
                    var assetRecords = bizPlugin.GetMyAllLockAssets();
                    if (assetRecords.IsNotNull())
                    {
                        var groups = assetRecords.GroupBy(m => m.Value.Tx.Recipient);
                        foreach (var act in this.Operater.Wallet.GetHeldAccounts())
                        {
                            var group = groups.FirstOrDefault(m => m.Key.Equals(act.GetKey().PublicKey));
                            //var sh = Contract.CreateSignatureRedeemScript(group.Key).ToScriptHash();
                            DarkTreeNode node = new DarkTreeNode() { Text = act.Address };
                            node.NodeType = 0;
                            node.Tag = act.ScriptHash;
                            if (group.IsNotNullAndEmpty())
                            {
                                foreach (var g in group.GroupBy(m => m.Value.Output.AssetId))
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
                                    DarkTreeNode assetnode = new DarkTreeNode() { Text = assetname };
                                    assetnode.NodeType = 1;
                                    assetnode.Tag = new AddressAsset { SH = act.ScriptHash, Asset = assetId };

                                    foreach (var t in g.OrderBy(m => m.Value.Tx.LockExpiration))
                                    {
                                        DarkTreeNode subnode = new DarkTreeNode() { Text = UIHelper.LocalString($"锁仓金额:{t.Value.Output.Value}", $"Lock Amount:{t.Value.Output.Value}") };
                                        subnode.NodeType = 2;
                                        subnode.Tag = t;
                                        var subsubnode = new DarkTreeNode() { Text = UIHelper.LocalString($"资产名称:{assetname}", $"Asset Name:{assetname}") };
                                        subsubnode.NodeType = 2;
                                        subsubnode.Tag = t;
                                        subnode.Nodes.Add(subsubnode);
                                        subsubnode = new DarkTreeNode() { Text = UIHelper.LocalString($"资产Id:{assetId.ToString()}", $"Asset Id:{assetId.ToString()}") };
                                        subsubnode.NodeType = 2;
                                        subsubnode.Tag = t;
                                        subnode.Nodes.Add(subsubnode);
                                        subsubnode = new DarkTreeNode() { Text = UIHelper.LocalString($"锁仓地址:{t.Value.Output.ScriptHash.ToAddress()}", $"Lock Address:{t.Value.Output.ScriptHash.ToAddress()}")  };
                                        subsubnode.NodeType = 2;
                                        subsubnode.Tag = t;
                                        subnode.Nodes.Add(subsubnode);
                                        var locktype = t.Value.Tx.IsTimeLock ? "锁定时间" : "锁定区块";
                                        var locktypeen = t.Value.Tx.IsTimeLock ? "Lock Time" : "Lock Block";
                                        subsubnode = new DarkTreeNode() { Text = UIHelper.LocalString($"锁定类型:{locktype}", $"Lock Type:{locktypeen}") };
                                        subsubnode.NodeType = 2;
                                        subsubnode.Tag = t;
                                        subnode.Nodes.Add(subsubnode);
                                        var expstr = t.Value.Tx.IsTimeLock ? t.Value.Tx.LockExpiration.ToDateTime().ToString("yyyy-MM-dd HH:mm:ss") : t.Value.Tx.LockExpiration.ToString();
                                        subsubnode = new DarkTreeNode() { Text = UIHelper.LocalString($"到期:{expstr}", $"Expire:{expstr}") };
                                        subsubnode.NodeType = 2;
                                        subsubnode.Tag = t;
                                        subnode.Nodes.Add(subsubnode);
                                        nodes[t.Key] = subnode;
                                        assetnode.Nodes.Add(subnode);
                                    }
                                    node.Nodes.Add(assetnode);
                                }
                            }
                            this.treeAsset.Nodes.Add(node);
                        }

                    }
                }
            });

        }
        #endregion
    }
}
