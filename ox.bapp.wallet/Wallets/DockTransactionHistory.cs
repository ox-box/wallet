using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using System;
using System.Windows.Forms;
using System.Linq;
using OX.Persistence;
using OX.Bapps;

namespace OX.Wallets.Base
{
    public partial class DockTransactionHistory : DarkToolWindow, INotecaseTrigger, IModuleComponent
    {
        class HistoryItem
        {
            public string TxId;
            public uint? Index;
            public uint DT;
            public override bool Equals(object obj)
            {
                if (obj is HistoryItem item)
                    return item.TxId == this.TxId;
                return base.Equals(obj);
            }
            public override int GetHashCode()
            {
                return TxId.GetHashCode();
            }
        }
        public Module Module { get; set; }
        public INotecase Operater;
        int TXCount = 100;
        #region Constructor Region

        public DockTransactionHistory()
        {
            InitializeComponent();
            this.lstHistory.MouseDown += LstHistory_MouseDown;
        }

        private void LstHistory_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                DarkContextMenu menu = new DarkContextMenu();
                var sm = new ToolStripMenuItem(UIHelper.LocalString("查找交易", "Find Transaction"));
                sm.Click += Find_Click;
                menu.Items.Add(sm);
                var itemid = this.lstHistory.SelectedIndices?.FirstOrDefault();
                if (itemid.HasValue)
                {
                    var item = this.lstHistory.Items[itemid.Value];
                    sm = new ToolStripMenuItem(UIHelper.LocalString("复制交易ID到粘贴板", "Copy txid to clipboard"));
                    sm.Tag = item.Tag;
                    sm.Click += Sm_Click;
                    menu.Items.Add(sm);
                    sm = new ToolStripMenuItem(UIHelper.LocalString("查看交易", "View Transaction"));
                    sm.Tag = item.Tag;
                    sm.Click += View_Click;
                    menu.Items.Add(sm);
                }
                var m = this.TXCount == int.MaxValue ? UIHelper.LocalString("仅显示100条交易", "Only List 100 Transactions") : UIHelper.LocalString("显示所有交易", "List All Transactions");
                sm = new ToolStripMenuItem(m);
                sm.Click += Sm_Click1;
                menu.Items.Add(sm);
                if (menu.Items.Count > 0)
                    menu.Show(this.lstHistory, e.Location);
            }
        }

        private void Sm_Click1(object sender, EventArgs e)
        {
            if (this.TXCount == int.MaxValue) this.TXCount = 100; else this.TXCount = int.MaxValue;
            ChangeWallet(this.Operater);
        }

        private void Sm_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            HistoryItem hi = ToolStripMenuItem.Tag as HistoryItem;
            try
            {
                Clipboard.SetText(hi.TxId);
            }
            catch (Exception) { }
        }
        private void Find_Click(object sender, EventArgs e)
        {
            using (FindTransactionForm dialog = new FindTransactionForm())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                var txid = dialog.TxId;
                if (txid.IsNotNullAndEmpty())
                {
                    txid = txid.Trim();
                    if (UInt256.TryParse(txid, out UInt256 hash))
                    {
                        var tx = Blockchain.Singleton.GetTransaction(UInt256.Parse(txid));
                        if (tx.IsNotNull())
                        {
                            new ViewTransactionForm().ViewTx(tx).ShowDialog();
                        }
                    }
                }
            }
        }
        private void View_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem ToolStripMenuItem = sender as ToolStripMenuItem;
            HistoryItem hi = ToolStripMenuItem.Tag as HistoryItem;
            var tx = Blockchain.Singleton.GetTransaction(UInt256.Parse(hi.TxId));
            if (tx.IsNotNull())
            {
                new ViewTransactionForm().ViewTx(tx).ShowDialog();
            }
        }
        #endregion
        #region IBlockChainTrigger
        public void OnBappEvent(BappEvent be) { }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
        }
        public void BeforeOnBlock(Block block)
        {
        }
        public void OnBlock(Block block)
        {
        }

        public void AfterOnBlock(Block block)
        {
            if (this.Visible)
            {
                this.DoInvoke(new Action(RefreshConfirmations));
            }
        }
        public void HeartBeat(HeartBeatContext context)
        {
            if (this.Visible)
            {
            }
        }

        public void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            this.DoInvoke(() =>
            {
                this.lstHistory.Items.Clear();
                using (Snapshot snapshot = Blockchain.Singleton.GetSnapshot())
                    foreach (var i in this.Operater.Wallet.GetTransactions().Select(p => snapshot.Transactions.TryGet(p)).Where(p => p != null && p.Transaction != null).Select(p => new
                    {
                        p.Transaction,
                        p.BlockIndex,
                        Time = snapshot.GetHeader(p.BlockIndex).Timestamp
                    }).OrderByDescending(p => p.Time).Take(TXCount).OrderBy(p => p.Time))
                    {
                        AddTransaction(i.Transaction, i.BlockIndex, i.Time);
                    }
                this.Operater.Wallet.WalletTransaction += Wallet_WalletTransaction;
            });
        }
        public void OnRebuild()
        {
        }
        private void Wallet_WalletTransaction(object sender, WalletTransactionEventArgs e)
        {
            BeginInvoke(new Action<Transaction, uint?, uint>(AddTransaction), e.Transaction, e.Height, e.Time);
        }
        private void RefreshConfirmations()
        {
            var graphic = this.CreateGraphics();
            var st = graphic.MeasureString(" ", this.Font).Width;
            var mst = st * 40;
            var mst2 = st * 20;
            foreach (var item in this.lstHistory.Items)
            {
                var hi = item.Tag as HistoryItem;
                uint? height = hi.Index;
                int? confirmations = (int)Blockchain.Singleton.Height - (int?)height + 1;
                if (confirmations <= 0) confirmations = null;
                string s = confirmations?.ToString() ?? UIHelper.LocalString("未确认", "Unconfirmed");
                var strdt = hi.DT.ToDateTime().ToString();
                var ldt = graphic.MeasureString(strdt, this.Font).Width;
                int n = (int)((mst - ldt) / st);
                for (int i = 0; i < n; i++)
                    strdt += " ";
                var ls2 = graphic.MeasureString(s, this.Font).Width;
                int n2 = (int)((mst2 - ls2) / st);
                for (int i = 0; i < n2; i++)
                    s += " ";
                item.Text = $"{strdt}  {s}  {hi.TxId}";
            }
        }
        private void AddTransaction(Transaction tx, uint? height, uint time)
        {
            var graphic = this.CreateGraphics();
            var st = graphic.MeasureString(" ", this.Font).Width;
            var mst = st * 40;
            var mst2 = st * 20;
            int? confirmations = (int)Blockchain.Singleton.Height - (int?)height + 1;
            if (confirmations <= 0) confirmations = null;
            string confirmations_str = confirmations?.ToString() ?? UIHelper.LocalString("未确认", "Unconfirmed");
            string txid = tx.Hash.ToString();
            var strdt = time.ToDateTime().ToString();
            var ldt = graphic.MeasureString(strdt, this.Font).Width;
            int n = (int)((mst - ldt) / st);
            for (int i = 0; i < n; i++)
                strdt += " ";
            var ls2 = graphic.MeasureString(confirmations_str, this.Font).Width;
            int n2 = (int)((mst2 - ls2) / st);
            for (int i = 0; i < n2; i++)
                confirmations_str += " ";
            var item = new DarkListItem()
            {
                Tag = new HistoryItem() { TxId = txid, Index = height, DT = time },
                Text = $"{strdt}  {confirmations_str}  {txid}"
            };
            var selectedItem = this.lstHistory.Items.FirstOrDefault(m =>
            {
                var dli = m.Tag as HistoryItem;
                return dli.TxId == txid;
            });
            if (selectedItem != default)
            {
                selectedItem.Text = item.Text;
                selectedItem.Tag = item.Tag;
            }
            else
            {
                this.lstHistory.Items.Insert(0, item);
            }

        }

        #endregion
    }
}
