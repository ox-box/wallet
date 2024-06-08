using NBitcoin;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.SmartContract;
using OX.Wallets.Base;
using OX.Wallets.Base.Flash;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static NBitcoin.Scripting.OutputDescriptor;

namespace OX.Wallets.Flash.Chat
{
    public partial class UnicastChatbox : UserControl
    {
        public INotecase Operator;
        public UnicastChatboxInfo chatbox_info;
        public OpenFileDialog fileDialog = new OpenFileDialog();
        public string initialdirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        ChatItem lastChatItem;
        bool isLoadedOld = false;
        ChatMore BottonMore = default;
        uint Range = 0;
        ChatQueue CQ = new ChatQueue();
        public UnicastChatbox(INotecase notecase, UnicastChatboxInfo _chatbox_info)
        {
            this.Operator = notecase;
            InitializeComponent();
            this.sendButton.Text = UIHelper.LocalString("发送", "Send");
            this.Load += UnicastChatbox_Load;
            this.topPanel.BackColor = OX.Wallets.UI.Config.Colors.BlueSelection;
            this.bottomPanel.BackColor = OX.Wallets.UI.Config.Colors.BlueSelection;
            this.sendButton.BackColor = OX.Wallets.UI.Config.Colors.BlueSelection;
            chatbox_info = _chatbox_info;

            talkLabel.Text = chatbox_info.TalkLabel;
            talkName.Text = chatbox_info.TalkName;
            chatTextbox.Text = chatbox_info.ChatPlaceholder;

            chatTextbox.Enter += ChatEnter;
            chatTextbox.Leave += ChatLeave;
            sendButton.Click += SendMessage;
            attachButton.Click += BuildAttachment;
            queueButton.Click += QueueButton_Click;

            chatTextbox.KeyDown += OnEnter;

            //AddMessage(null, out ChatItem ctr);
            BottonMore = new ChatMore();
            BottonMore.ResetSize(itemsPanel.Width);
            BottonMore.Height = 50;
            BottonMore.Dock = DockStyle.Top;
            BottonMore.Click += BottonMore_Click;
            this.itemsPanel.Controls.Add(BottonMore);
            BottonMore.BringToFront();
        }

        private void QueueButton_Click(object sender, EventArgs e)
        {
            this.CQ.ShowDialog();
        }

        private void UnicastChatbox_Load(object sender, EventArgs e)
        {
        }

        private void BottonMore_Click(object sender, EventArgs e)
        {
            if (Range > 0)
            {
                var newRange = Range - 1;
                var newRs = FlashMessageProvider.Instance.GetUnicastRecords(this.chatbox_info.TP.Item1, newRange);

                if (newRs.IsNotNullAndEmpty())
                {
                    List<IChatModel> msgs = new List<IChatModel>();
                    foreach (var r in newRs)
                    {
                        var cm = BuildChatModel(r);
                        if (cm.IsNotNull())
                        {
                            msgs.Add(cm);
                        }
                    }

                    AddRangeMessage(msgs);
                    this.itemsPanel.Controls.SetChildIndex(this.BottonMore, int.MaxValue);
                    Range = newRange;
                }

            }
        }
        public void ClearRecords()
        {
            this.itemsPanel.Controls.Clear();
        }
        public void AddMessage(IChatModel message, out ChatItem chatItem)
        {
            chatItem = new ChatItem(message);
            chatItem.MouseDoubleClick += ChatItem_MouseDoubleClick;
            chatItem.Name = "chatItem" + itemsPanel.Controls.Count;
            chatItem.Dock = DockStyle.Top;
            itemsPanel.Controls.Add(chatItem);
            chatItem.BringToFront();

            chatItem.ResizeBubbles((int)(itemsPanel.Width * 0.7));
            if (lastChatItem.IsNull() || chatItem.ChatModel.RecordIndex > lastChatItem.ChatModel.RecordIndex)
                lastChatItem = chatItem;
            itemsPanel.ScrollControlIntoView(chatItem);
        }
        public void AddRangeMessage(IEnumerable<IChatModel> messages)
        {
            ChatItem chatItem = default;
            List<ChatItem> list = new List<ChatItem>();
            foreach (var message in messages.OrderBy(m => m.RecordIndex))
            {
                chatItem = new ChatItem(message);
                chatItem.MouseDoubleClick += ChatItem_MouseDoubleClick;
                chatItem.Name = "r" + message.RecordIndex.ToString();
                chatItem.Dock = DockStyle.Top;
                list.Add(chatItem);
                //itemsPanel.Controls.Add(chatItem);
                chatItem.BringToFront();
                chatItem.ResizeBubbles((int)(itemsPanel.Width * 0.7));
                if (lastChatItem.IsNull() || chatItem.ChatModel.RecordIndex > lastChatItem.ChatModel.RecordIndex)
                    lastChatItem = chatItem;
            }
            list.Reverse();
            itemsPanel.Controls.AddRange(list.ToArray());
            itemsPanel.ScrollControlIntoView(chatItem);
        }

        private void ChatItem_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lastChatItem.IsNotNull())
                itemsPanel.ScrollControlIntoView(lastChatItem);

        }

        void ChatLeave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(chatTextbox.Text))
            {
                chatTextbox.Text = chatbox_info.ChatPlaceholder;
                chatTextbox.ForeColor = Color.Gray;
            }
        }

        void ChatEnter(object sender, EventArgs e)
        {
            chatTextbox.ForeColor = Color.Black;
            if (chatTextbox.Text == chatbox_info.ChatPlaceholder)
            {
                chatTextbox.Text = "";
            }
        }
        void SendMessage(object sender, EventArgs e)
        {
            string chatmessage = chatTextbox.Text.Trim();
            if (chatmessage.IsNotNullAndEmpty())
            {
                var accountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(this.chatbox_info.LocalAccount.ScriptHash);
                if (accountState.IsNotNull() && Blockchain.Singleton.AllowFlashMessage(accountState, out uint _))
                {
                    var bs = System.Text.Encoding.UTF8.GetBytes(chatmessage);
                    var fu = new FlashUnicast(this.chatbox_info.LocalAccount.GetKey(), this.chatbox_info.TP.Item2.Remote, Blockchain.Singleton.HeaderHeight, bs);
                    fu.ContentType = FlashMessageContentType.Text;
                    this.Operator.SignAndSendFlashMessage(fu);
                    //chatTextbox.SelectAll();
                    //chatTextbox.Clear();
                    chatTextbox.ResetText();

                    chatTextbox.Text = string.Empty;
                }
            }
        }
        void EnqueuMessage(object sender, EventArgs e)
        {
            string chatmessage = chatTextbox.Text.Trim();
            if (chatmessage.IsNotNullAndEmpty())
            {
                var accountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(this.chatbox_info.LocalAccount.ScriptHash);
                if (accountState.IsNotNull())
                {
                    var bs = System.Text.Encoding.UTF8.GetBytes(chatmessage);
                    UnicastFlashQueueData UnicastFlashQueueData = new UnicastFlashQueueData { Key = this.chatbox_info.LocalAccount.GetKey(), Remote = this.chatbox_info.TP.Item2.Remote, Data = bs };

                    this.CQ.Enqueue(UnicastFlashQueueData, chatmessage);
                    //chatTextbox.SelectAll();
                    //chatTextbox.Clear();
                    chatTextbox.ResetText();

                    chatTextbox.Text = string.Empty;
                }
            }
        }

        void BuildAttachment(object sender, EventArgs e)
        {
            fileDialog.InitialDirectory = initialdirectory;
            fileDialog.Reset();
            fileDialog.Multiselect = false;
            fileDialog.Filter = UIHelper.LocalString("图片|*.gif;*.jpg;*.jpeg;*.bmp;*.jfif;*.png;", "Image|*.gif;*.jpg;*.jpeg;*.bmp;*.jfif;*.png;");
            var result = fileDialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string selected = fileDialog.FileName;
                if (ImageCompressHelper.CompressImage(selected, FlashMessage.MaxFlashMessageSize - 500, out byte[] bs))
                {
                    var accountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(this.chatbox_info.LocalAccount.ScriptHash);
                    if (accountState.IsNotNull() && Blockchain.Singleton.AllowFlashMessage(accountState, out uint _))
                    {
                        var fu = new FlashUnicast(this.chatbox_info.LocalAccount.GetKey(), this.chatbox_info.TP.Item2.Remote, Blockchain.Singleton.HeaderHeight, bs);
                        fu.ContentType = FlashMessageContentType.Image;
                        this.Operator.SignAndSendFlashMessage(fu);
                        //chatTextbox.Text = this.chatbox_info.ChatPlaceholder;
                    }
                }
            }
        }



        void OnEnter(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == (Keys.Control | Keys.Alt) && e.KeyCode == Keys.Enter)
            {
                EnqueuMessage(this, null);
            }
            else if (e.Modifiers == Keys.Shift && e.KeyCode == Keys.Enter)
            {

                SendMessage(this, null);
            }

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            foreach (var control in itemsPanel.Controls)
            {
                if (control is ChatItem)
                {
                    (control as ChatItem).ResizeBubbles((int)(itemsPanel.Width * 0.7));
                }
                else if (control is ChatItem)
                {
                    (control as ChatMore).ResetSize(itemsPanel.Width);
                }
            }
        }

        private void topPanel_Paint(object sender, PaintEventArgs e)
        {

        }
        public void ReloadOldRecords()
        {
            if (!isLoadedOld)
            {
                List<IChatModel> msgs = new List<IChatModel>();
                var rs = FlashMessageProvider.Instance.GetLastUnicastRecords(this.chatbox_info.TP.Item1, out Range);
                if (rs.IsNotNullAndEmpty())
                {
                    foreach (var r in rs)
                    {
                        var cm = BuildChatModel(r);
                        if (cm.IsNotNull())
                        {
                            msgs.Add(cm);
                        }
                    }
                }

                AddRangeMessage(msgs.ToArray());
                isLoadedOld = true;
                this.itemsPanel.Controls.SetChildIndex(this.BottonMore, int.MaxValue);
            }
        }
        IChatModel BuildChatModel(KeyValuePair<TalkLineKey, FlashUnicastRecord> r)
        {
            var t = new Tuple<TalkLineKey, FlashUnicastRecord>(r.Key, r.Value);
            return BuildChatModel(t);
        }
        IChatModel BuildChatModel(Tuple<TalkLineKey, FlashUnicastRecord> r)
        {
            if (r.Item2.FlashUnicast.TryDecrypt(this.chatbox_info.LocalAccount.GetKey(), this.chatbox_info.TP.Item2.Remote, out byte[] plaintText))
            {
                var senderSH = Contract.CreateSignatureRedeemScript(r.Item2.FlashUnicast.Sender).ToScriptHash();
                var sender = senderSH.ToAddress();
                if (Blockchain.Singleton.GetDomain(senderSH, out byte[] domain))
                {
                    sender = System.Text.Encoding.UTF8.GetString(domain);
                }
                var isRemote = r.Item1.TalkKind == TalkKind.Inbox;
                IChatModel msg = default;
                if (r.Item2.FlashUnicast.ContentType == FlashMessageContentType.Text)
                {
                    var str = System.Text.Encoding.UTF8.GetString(plaintText);
                    msg = new TextChatModel
                    {
                        Body = str,
                        Author = sender,
                        Time = r.Item2.Timestamp.ToDateTime(),
                        IsRemote = isRemote,
                        RecordIndex = r.Item2.RecordIndex
                    };
                }
                else
                {
                    try
                    {
                        var image = Image.FromStream(new MemoryStream(plaintText));
                        if (image.IsNotNull())
                        {
                            msg = new ImageChatModel
                            {
                                Image = image,
                                Author = sender,
                                Time = r.Item2.Timestamp.ToDateTime(),
                                IsRemote = isRemote,
                                RecordIndex = r.Item2.RecordIndex
                            };
                        }
                    }
                    catch
                    {

                    }

                }
                return msg;
            }
            return default;
        }
        public void OnFlashMessage(FlashMessage flashMessage)
        {
            var t = FlashMemoryHelper.UnicastQueue.FirstOrDefault(m => m.Item1.FMHash == flashMessage.Hash);
            if (t.IsNotNull())
            {
                var cm = BuildChatModel(t);
                if (cm.IsNotNull())
                {
                    this.DoInvoke(() =>
                    {
                        AddMessage(cm, out ChatItem _);
                    });
                }
            }
        }
        public void HeartBeat(HeartBeatContext context)
        {
            uint expireIndex = 0;
            var accountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(this.chatbox_info.LocalAccount.ScriptHash);
            var ok = accountState.IsNotNull() && Blockchain.Singleton.AllowFlashMessage(accountState, out expireIndex);
            this.sendButton.Enabled = ok;
            if (ok)
            {
                this.sendButton.Text = UIHelper.LocalString("发送", "Send");
                if (context.IsOnceMinute && FlashMemoryHelper.LastQueueTimeStamp != context.TimeStamp)
                {
                    var qd = this.CQ.Dequeue();
                    if (qd.IsNotNull() && qd is UnicastFlashQueueData ufqd)
                    {
                        var fu = new FlashUnicast(ufqd.Key, ufqd.Remote, Blockchain.Singleton.HeaderHeight, ufqd.Data);
                        fu.ContentType = FlashMessageContentType.Text;
                        this.Operator.SignAndSendFlashMessage(fu);
                        FlashMemoryHelper.LastQueueTimeStamp = context.TimeStamp;
                    }
                }
            }
            else
            {
                this.sendButton.Text = (expireIndex - Blockchain.Singleton.HeaderHeight).ToString();
            }
        }
        public void ResetLabel()
        {
            talkLabel.Text = chatbox_info.TalkLabel;
        }

    }
}
