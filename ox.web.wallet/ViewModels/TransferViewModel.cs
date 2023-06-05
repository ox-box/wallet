namespace OX.Web
{
    public class TransferViewModel
    {
        public string FromEthAddress;
        public UInt160 FromOXAddress;
        public string OxAddress;

        public decimal Amount;
    }
    public class TransferEthViewModel
    {
        public string FromEthAddress;
        public UInt160 FromOXAddress;
        public string ToEthAddress;
        public uint LockExprationIndex  = 0;
        public decimal Amount;
    }
}
