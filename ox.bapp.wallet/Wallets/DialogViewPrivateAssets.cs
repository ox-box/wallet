using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using OX.Wallets;
using OX.Ledger;
using OX.Bapps;
using OX.Cryptography.ECC;
using OX.Network.P2P.Payloads;

namespace OX.Wallets.Base
{
    public partial class DialogViewPrivateAssets : DarkDialog
    {
        INotecase Operater;
        OpenWallet Wallet;
        WalletAccount Account;
        public DialogViewPrivateAssets(INotecase notecase, OpenWallet wallet, WalletAccount account)
        {
            this.Operater = notecase;
            this.Wallet = wallet;
            this.Account = account;
            InitializeComponent();
            this.Text = UIHelper.LocalString("查看私营资产", "View Private Assets");
            this.btnOk.Text = UIHelper.LocalString("确定", "OK");
            //KeyPair key = account.GetKey();
            foreach (UInt256 asset_id in wallet.FindUnspentCoins(account.ScriptHash).Select(p => p.Output.AssetId).Distinct())
            {
                if (asset_id.Equals(Blockchain.OXC) || asset_id.Equals(Blockchain.OXS)) continue;
                var state = Blockchain.Singleton.Store.GetAssets().TryGet(asset_id);
                var assetName = state.GetName();
                var node = new DarkTreeNode { Text = UIHelper.LocalString($"资产Id:   {asset_id.ToString()}", $"Asset Id:   {asset_id.ToString()}") };
                node.Tag = asset_id;
                var subnode = new DarkTreeNode { Text = UIHelper.LocalString($"资产名称:{assetName}", $"Asset Name:{assetName}") };
                subnode.Tag = asset_id;
                node.Nodes.Add(subnode);
                var balance = wallet.GeAccountAvailable(account.ScriptHash, asset_id).ToString();
                subnode = new DarkTreeNode { Text = UIHelper.LocalString($"资产余额:{balance}", $"Asset Balance:{balance}") };
                subnode.Tag = asset_id;
                node.Nodes.Add(subnode);

                var cap = state.Amount == -Fixed8.Satoshi ? UIHelper.LocalString("无限", "unlimited") : state.Amount.ToString();
                subnode = new DarkTreeNode { Text = UIHelper.LocalString($"总量:{cap}", $"Cap:{cap}") };
                subnode.Tag = asset_id;
                node.Nodes.Add(subnode);
                var Available = state.Available.ToString();
                subnode = new DarkTreeNode { Text = UIHelper.LocalString($"已发行:{Available}", $"Issued:{Available}") };
                subnode.Tag = asset_id;
                node.Nodes.Add(subnode);
                subnode = new DarkTreeNode { Text = UIHelper.LocalString($"精度:{state.Precision}", $"Precision:{state.Precision}") };
                subnode.Tag = asset_id;
                node.Nodes.Add(subnode);
                this.darkTreeView1.Nodes.Add(node);
            }
            this.darkTreeView1.MouseDown += TreeAsset_MouseDown;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }
        private void TreeAsset_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DarkContextMenu menu = new DarkContextMenu();
                ToolStripMenuItem sm;

                DarkTreeNode[] nodes = darkTreeView1.SelectedNodes.ToArray();
                if (nodes != null && nodes.Length == 1)
                {
                    DarkTreeNode node = nodes[0];
                    sm = new ToolStripMenuItem(UIHelper.LocalString("转移资产到锁仓账户", "Transfer this asset for lock"));
                    sm.Tag = node.Tag;
                    sm.Click += Sm_Click;
                    menu.Items.Add(sm);
                    sm = new ToolStripMenuItem(UIHelper.LocalString("复制资产Id", "Copy Asset Id"));
                    sm.Tag = node.Tag;
                    sm.Click += Sm_Click1;
                    menu.Items.Add(sm);
                }
                if (menu.Items.Count > 0)
                    menu.Show(this.darkTreeView1, e.Location);
            }
        }

        private void Sm_Click1(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            UInt256 asset_id = ToolStripMenuItem.Tag as UInt256;
            try
            {
                var str = asset_id.ToString();
                Clipboard.SetText(str);
                string msg = str + UIHelper.LocalString("  已复制", "  copied");
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }

        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            UInt256 asset_id = ToolStripMenuItem.Tag as UInt256;
            using (DialogPrivateAssetLockTransfer dialog = new DialogPrivateAssetLockTransfer(this.Wallet, this.Account, asset_id))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                var output = dialog.GetOutput(out ECPoint ecp, out bool isTime, out uint expiration);
                if (isTime)
                {
                    if (expiration - DateTime.Now.ToTimestamp() < 3600)
                    {
                        string msg = $"{UIHelper.LocalString("锁定的时间太短", "Locking time is too short")}";
                        DarkMessageBox.ShowInformation(msg, "");
                        return;
                    }
                }
                else
                {
                    if (expiration - Blockchain.Singleton.Height < 100)
                    {
                        string msg = $"{UIHelper.LocalString("锁定的区块高度太低", "Locked block height is too low")}";
                        DarkMessageBox.ShowInformation(msg, "");
                        return;
                    }
                }
                LockAssetTransaction lat = new LockAssetTransaction
                {
                    LockContract = Blockchain.LockAssetContractScriptHash,
                    IsTimeLock = isTime,
                    LockExpiration = expiration,
                    Recipient = ecp
                };
                output.ScriptHash = lat.GetContract().ScriptHash;
                lat.Outputs = new TransactionOutput[] { output };
                lat = this.Wallet.MakeTransaction(lat, this.Account.ScriptHash, this.Account.ScriptHash);
                if (lat != null)
                {
                    if (lat.Inputs.Count() > 20)
                    {
                        string msg = $"{UIHelper.LocalString("交易输入项太多,请分为多次转账", "There are too many transaction input. Please transfer multiple times")}";
                        DarkMessageBox.ShowInformation(msg, "");
                        return;
                    }
                    this.Operater.SignAndSendTx(lat);
                    if (this.Operater != default)
                    {
                        string msg = $"{UIHelper.LocalString("交易已广播", "Relay transaction completed")}   {lat.Hash}";
                        DarkMessageBox.ShowInformation(msg, "");
                    }
                }
            }
        }
    }
}
