namespace OX.Web
{
    public class TransferNftViewModel
    {
        public decimal Amount;
        public uint MaxIndex;
        public uint MinIndex;
        public OX.Network.P2P.Payloads.NFTPending Pending;
        public string Signature;
    }

}
