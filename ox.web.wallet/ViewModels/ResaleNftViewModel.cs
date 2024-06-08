using OX.Ledger;
using OX.Network.P2P.Payloads;
namespace OX.Web
{
    public class PublishResaleViewModel
    {
        public string Data;
    }
    public class ResaleNftViewModel
    {
        public string IssueId;
        public NFTPending NFTTranferData;
        public NFCState NFCState;
    }

}
