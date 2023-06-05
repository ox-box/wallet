using OX.Ledger;
using OX.Wallets.Base.NFT;
namespace OX.Web
{
    public class PublishResaleViewModel
    {
        public string Data;
    }
    public class ResaleNftViewModel
    {
        public string IssueId;
        public string Auth;
        public NFTTranferData NFTTranferData;
        public NFCState NFCState;
    }

}
