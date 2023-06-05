using OX.Wallets.Base.NFT;
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
        public NFTTranferData NFTTranferData;
    }

}
