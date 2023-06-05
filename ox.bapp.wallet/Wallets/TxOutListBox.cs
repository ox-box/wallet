using OX.IO.Caching;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets.Base.Wallets;
using OX.Wallets.UI.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;

namespace OX.Wallets.Base
{
    [DefaultEvent(nameof(ItemsChanged))]
    internal partial class TxOutListBox : UserControl
    {
        public event EventHandler ItemsChanged;

        public AssetDescriptor Asset { get; set; }
       public INotecase Operater { get; set; }
        public int ItemCount => listBox1.Items.Count;

        public IEnumerable<TxOutListBoxItem> Items => listBox1.Items.OfType<DarkListItem>().Select(m => m.Tag as TxOutListBoxItem);

        public bool ReadOnly
        {
            get
            {
                return !panel1.Enabled;
            }
            set
            {
                panel1.Enabled = !value;
            }
        }

        private UInt160 _script_hash = null;
        public UInt160 ScriptHash
        {
            get
            {
                return _script_hash;
            }
            set
            {
                _script_hash = value;
            }
        }

        public TxOutListBox()
        {
            InitializeComponent();
        }

        public void Clear()
        {
            if (listBox1.Items.Count > 0)
            {
                listBox1.Items.Clear();
                button2.Enabled = false;
                ItemsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public void SetItems(IEnumerable<TransactionOutput> outputs)
        {
            listBox1.Items.Clear();
            DataCache<UInt256, AssetState> cache = Blockchain.Singleton.Store.GetAssets();
            foreach (TransactionOutput output in outputs)
            {
                AssetState asset = cache.TryGet(output.AssetId);
                var item = new TxOutListBoxItem
                {
                    AssetName = $"{asset.GetName()} ({asset.Owner})",
                    AssetId = output.AssetId,
                    Value = new BigDecimal(output.Value.GetData(), 8),
                    ScriptHash = output.ScriptHash
                };
                listBox1.Items.Add(
                     new DarkListItem { Text = item.ToString(), Tag = item }
                 );
            }
            ItemsChanged?.Invoke(this, EventArgs.Empty);
        }



        private void button1_Click(object sender, EventArgs e)
        {
            using (PayToDialog dialog = new PayToDialog(this.Operater, asset: Asset, scriptHash: ScriptHash))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                var item = dialog.GetOutput();
                listBox1.Items.Add(new DarkListItem { Text = item.ToString(), Tag = item });
                ItemsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndices.Count > 0)
            {
                var f = listBox1.SelectedIndices[0];
                listBox1.Items.RemoveAt(f);
            }
            ItemsChanged?.Invoke(this, EventArgs.Empty);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using (BulkPayDialog dialog = new BulkPayDialog(this.Operater, Asset))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                foreach (var item in dialog.GetOutputs())
                {
                    listBox1.Items.Add(new DarkListItem { Text = item.ToString(), Tag = item });
                }
                ItemsChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        private void listBox1_SelectedIndicesChanged(object sender, EventArgs e)
        {
            button2.Enabled = listBox1.SelectedIndices.Count > 0;
        }
    }
}
