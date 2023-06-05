using Akka.Actor;
using OX.IO.Actors;
using OX.Ledger;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using OX.Wallets.UI.Forms;
using OX.Wallets.UI;
using OX.SmartContract;
using OX.IO;
using System.Xml;
using OX.Bapps;
using OX.Cryptography;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Text;
using OX.Wallets.UI.Controls;

namespace OX.Wallets.Base
{
    public partial class ViewBlockDialog : DarkDialog
    {

        INotecase Operater;
        public Module Module { get; set; }
        public ViewBlockDialog(INotecase operater)
        {
            InitializeComponent();
            this.Operater = operater;
        }


        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("查看区块", "View Block");
            this.lb_txs.Text = UIHelper.LocalString("交易:", "Transaction:");
            this.lb_blocknonce.Text = UIHelper.LocalString("区块随机数:", "Block Nonce:");
            this.lb_blockIndex.Text = UIHelper.LocalString("区块高度:", "Block Height:");
            this.lb_blockHash.Text = UIHelper.LocalString("区块哈希:", "Block Hash:");
            this.bt_query.Text = UIHelper.LocalString("查询", "Query");
            this.btnOk.Text = UIHelper.LocalString("关闭", "Close");
        }

        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }






        private void bt_query_Click(object sender, EventArgs e)
        {
            var s = this.tb_blockIndex.Text;
            if (s.IsNotNullAndEmpty())
            {
                if (uint.TryParse(s, out uint index))
                {
                    query(index);
                }
            }
        }
        void query(uint index)
        {
            this.tb_blockNonce.Clear();
            this.lstHistory.Items.Clear();
            this.tb_blockHash.Clear();
            var hash = Blockchain.Singleton.GetBlockHash(index);
            if (hash.IsNotNull())
            {
                var block = Blockchain.Singleton.GetBlock(hash);
                this.tb_blockNonce.Text = block.ConsensusData.ToString();
                this.tb_blockHash.Text = block.Hash.ToString();
                foreach (var tx in block.Transactions)
                {
                    var node = new DarkListItem(tx.Hash.ToString());
                    node.Tag = tx;
                    this.lstHistory.Items.Add(node);
                }
            }
        }

        private void lstHistory_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DarkContextMenu menu = new DarkContextMenu();
                var itemid = this.lstHistory.SelectedIndices?.FirstOrDefault();
                if (itemid.HasValue)
                {
                    var item = this.lstHistory.Items[itemid.Value];
                    var sm = new ToolStripMenuItem(UIHelper.LocalString("查看交易", "View Transaction"));
                    sm.Tag = item.Tag;
                    sm.Click += Sm_Click;
                    menu.Items.Add(sm);
                }
                if (menu.Items.Count > 0)
                    menu.Show(this.lstHistory, e.Location);
            }
        }

        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            Transaction tx = ToolStripMenuItem.Tag as Transaction;
            if (tx.IsNotNull())
            {
                new ViewTransactionForm().ViewTx(tx).ShowDialog();
            }
        }
    }
}
