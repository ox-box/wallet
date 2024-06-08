using OX.Network.P2P.Payloads;
namespace OX.Web
{
    public class BuyNftViewModel
    {
        public decimal Amount;
        public uint MaxIndex;
        public uint MinIndex;
        public string Signature;

        public string CID;
        public string HolderName;
        public string SN;
        public bool Checked = false;
        public NFTPending NFTTranferData;
    }

}
