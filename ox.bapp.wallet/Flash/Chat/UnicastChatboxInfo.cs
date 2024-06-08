using OX.Network.P2P.Payloads;
using System;

namespace OX.Wallets.Flash.Chat
{
    public class UnicastChatboxInfo
    {
        public Tuple<UInt256, UnicastTalkLineValue> TP { get; set; }
        public WalletAccount LocalAccount { get; set; }
        public string LocalAddress { get; set; }
        public string RemoteAddress { get; set; }
        public string TalkLabel { get; set; }
        public string TalkName { get; set; }
        public string ChatPlaceholder = "Please enter a message...";
        public byte[] Attachment { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentType { get; set; }
    }
}
