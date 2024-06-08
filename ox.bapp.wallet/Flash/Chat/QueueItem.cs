using OX.Network.P2P.Payloads;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OX.Wallets.Flash.Chat
{
    public partial class QueueItem : UserControl
    {
        public FlashQueueData QD { get; set; }
        string ChatMessage;
        public QueueItem()
        {
            InitializeComponent();
            bodyTextBox.Text = UIHelper.LocalString("没有任何信息。", "No messages were found.");
        }

        public QueueItem(FlashQueueData qd, string chatmessage)
        {
            QD = qd;
            ChatMessage = chatmessage;
            InitializeComponent();
            bodyTextBox.Text = chatmessage;
        }



        public void ResizeBubbles(int width)
        {
            this.Width = width;
        }

        private void ChatItem_Load(object sender, EventArgs e)
        {

        }
    }
}
