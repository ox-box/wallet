using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Markdig;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using OX.Cryptography;
using OX.Cryptography.ECC;
using OX.Network.P2P.Payloads;
using System.Diagnostics;
using OX.Wallets.UI.Config;
using OX.Wallets.Flash.Chat;

namespace OX.Wallets.Flash
{
    public partial class ChatQueue : DarkForm
    {
        Queue<QueueItem> queueItems = new Queue<QueueItem>();
        public ChatQueue()
        {
            InitializeComponent();
            this.itemsPanel.SizeChanged += ItemsPanel_SizeChanged;
            this.bt_close.Text = UIHelper.LocalString("关闭", "Close");
        }

        private void ItemsPanel_SizeChanged(object sender, EventArgs e)
        {
            foreach (var control in this.itemsPanel.Controls)
            {
                if (control is QueueItem qi)
                {
                    qi.ResizeBubbles((int)(itemsPanel.ClientRectangle.Width -20));
                }
            }
        }

        private void EditTalkLineName_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("消息队列", "Message Queue");
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
        public void Enqueue(FlashQueueData qd, string chatmessage)
        {
            QueueItem item = new QueueItem(qd, chatmessage);
            item.ResizeBubbles((int)(itemsPanel.ClientRectangle.Width-20));
            item.Dock = DockStyle.Top;
            this.itemsPanel.Controls.Add(item);
            this.queueItems.Enqueue(item);
            this.itemsPanel.ScrollControlIntoView(item);
        }
        public FlashQueueData Dequeue()
        {
            if (this.queueItems.Count == 0) return default;
            var item = this.queueItems.Dequeue();
            this.itemsPanel.Controls.Remove(item);
            return item.QD;
        }

        private void bt_close_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
