using System;

namespace OX.Wallets
{
    public class HeartBeatContext
    {
        public static bool WalletOpened { get; private set; }
        public static uint BaseTimeStamp { get; private set; }
        public uint TimeStamp { get; private set; }
        public bool IsNormalSync { get; set; }
        static HeartBeatContext()
        {
            WalletOpened = false;
            BaseTimeStamp = DateTime.Now.ToTimestamp();
        }
        public static void SetWalletOpened()
        {
            WalletOpened = true;
        }
        public HeartBeatContext()
        {
            TimeStamp = DateTime.Now.ToTimestamp();
        }
        public bool IsOnceMinute
        {
            get
            {
                return ((TimeStamp - BaseTimeStamp) % 60 == 0) && WalletOpened;
            }
        }
        public bool IsOnceHour
        {
            get
            {
                return ((TimeStamp - BaseTimeStamp) % 3600 == 0) && WalletOpened;
            }
        }
        public bool IsOnceDay
        {
            get
            {
                return ((TimeStamp - BaseTimeStamp) % 3600 * 12 == 0) && WalletOpened;
            }
        }
    }
}
