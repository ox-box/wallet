using OX.Network.P2P.Payloads;
using System;

namespace OX.Wallets.Flash.Chat
{
    public class MulticastChatboxInfo
    {
        public Tuple<UInt256, MulticastTalkLineValue> TP { get; set; }
        public WalletAccount LocalAccount { get; set; }
        public byte[] Keys { get; set; }
        public string LocalAddress { get; set; }      
        public string TalkLabel { get; set; }
        public string ChatPlaceholder = "Please enter a message...";
        public byte[] Attachment { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentType { get; set; }
    }
}
