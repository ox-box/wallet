using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI.Forms;
using OX.Wallets;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;

namespace OX.Wallets.Base
{
    public partial class ViewTransactionForm : DarkForm
    {
        public ViewTransactionForm()
        {
            InitializeComponent();
        }

        private void ViewTransactionForm_Load(object sender, EventArgs e)
        {
            //this.Text = UIHelper.LocalString("查看交易", "View Transaction");
        }
        public ViewTransactionForm ViewTx(Transaction tx)
        {
            this.darkTreeView1.Nodes.Clear();
            var txState = Blockchain.Singleton.CurrentSnapshot.Transactions.TryGet(tx.Hash);
            var index = txState?.BlockIndex;
            this.Text = tx.Type.ToString() + "   /    " + index.ToString() + "   /    " + tx.Hash.ToString();
            DarkTreeNode node = new DarkTreeNode();
            node.Text = UIHelper.LocalString($"系统费:{tx.SystemFee}", $"System Fee:{tx.SystemFee}");
            this.darkTreeView1.Nodes.Add(node);
            node = new DarkTreeNode();
            node.Text = UIHelper.LocalString($"网络费:{tx.NetworkFee}", $"Network Fee:{tx.NetworkFee}");
            this.darkTreeView1.Nodes.Add(node);
            node = new DarkTreeNode();
            var oxcTotal = tx.References.Where(m => m.Value.AssetId.Equals(Blockchain.OXC)).Sum(m => m.Value.Value);
            var oxsTotal = tx.References.Where(m => m.Value.AssetId.Equals(Blockchain.OXS)).Sum(m => m.Value.Value);
            node.Text = UIHelper.LocalString($"交易输入  oxs:{oxsTotal}   oxc:{oxcTotal}", $"Transaction Inputs  oxs:{oxsTotal}   oxc:{oxcTotal}");
            int i = 0;
            foreach (var refer in tx.References)
            {
                var n1 = new DarkTreeNode();
                n1.Text = i.ToString();
                var subnode = new DarkTreeNode();
                subnode.Text = UIHelper.LocalString($"地址:{refer.Value.ScriptHash.ToAddress()}", $"Address:{refer.Value.ScriptHash.ToAddress()}");
                n1.Nodes.Add(subnode);
                subnode = new DarkTreeNode();
                subnode.Text = UIHelper.LocalString($"资产类型:{refer.Value.AssetId}", $"Asset Type:{refer.Value.AssetId}");
                n1.Nodes.Add(subnode);
                subnode = new DarkTreeNode();
                subnode.Text = UIHelper.LocalString($"金额:{refer.Value.Value}", $"Amount:{refer.Value.Value}");
                n1.Nodes.Add(subnode);
                node.Nodes.Add(n1);
                i++;
            }
            this.darkTreeView1.Nodes.Add(node);
            node = new DarkTreeNode();
            var oxcAll = tx.Outputs.Where(m => m.AssetId.Equals(Blockchain.OXC)).Sum(m => m.Value);
            var oxsAll = tx.Outputs.Where(m => m.AssetId.Equals(Blockchain.OXS)).Sum(m => m.Value);
            node.Text = UIHelper.LocalString($"交易输出    oxs:{oxsAll}   oxc:{oxcAll}", $"Transaction Outputs    oxs:{oxsAll}   oxc:{oxcAll}");
            i = 0;
            foreach (var otp in tx.Outputs)
            {
                var n1 = new DarkTreeNode();
                n1.Text = i.ToString();
                var subnode = new DarkTreeNode();
                subnode.Text = UIHelper.LocalString($"地址:{otp.ScriptHash.ToAddress()}", $"Address:{otp.ScriptHash.ToAddress()}");
                n1.Nodes.Add(subnode);
                subnode = new DarkTreeNode();
                subnode.Text = UIHelper.LocalString($"资产类型:{otp.AssetId}", $"Asset Type:{otp.AssetId}");
                n1.Nodes.Add(subnode);
                subnode = new DarkTreeNode();
                subnode.Text = UIHelper.LocalString($"金额:{otp.Value}", $"Amount:{otp.Value}");
                n1.Nodes.Add(subnode);
                node.Nodes.Add(n1);
                i++;
            }
            this.darkTreeView1.Nodes.Add(node);
            return this;
        }
    }
}
