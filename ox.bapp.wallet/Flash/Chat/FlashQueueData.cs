using OX.Network.P2P.Payloads;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OX.Cryptography.ECC;

namespace OX.Wallets.Flash.Chat
{
    public abstract class FlashQueueData
    {
        public KeyPair Key;
        public abstract FlashMessageType FlashMessageType { get; }
        public byte[] Data;
    }
    public class UnicastFlashQueueData : FlashQueueData
    {
        public override FlashMessageType FlashMessageType => FlashMessageType.FlashUnicast;
        public ECPoint Remote;
    }
    public class MulticastFlashQueueData : FlashQueueData
    {
        public override FlashMessageType FlashMessageType => FlashMessageType.FlashMulticast;
        public byte[] ShareKey;
    }
}
