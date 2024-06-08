using OX.IO;
using OX.Network.P2P.Payloads;
using System.IO;

namespace OX.Wallets.Flash
{
    public static class FlashStatePersistencePrefixes
    {
        public const byte FlashUnicast_Record = 0x02;
        public const byte FlashMulticast_Record = 0x03;
        public const byte FlashMulticastNotice_Record = 0x04;
        public const byte FlashUnicast_TalkLine = 0x05;
        public const byte FlashMulticast_TalkLine = 0x06;
        public const byte FlashTalk_Count = 0x07;
        public const byte FlashState_SenderRecord = 0x08;
        public const byte FlashState_LatestCount = 0x09;
        public const byte FlashState_GlobalRecord = 0x0A;
        public const byte FlashState_Record = 0x0B;
        public const byte FlashStateComment_Record = 0x0C;
        public const byte FlashState_TagRecord = 0x0D;
        public const byte FlashState_TagCount = 0x0E;
        public const byte Flash_NFTPendingRecord = 0x0F;
    }

}
