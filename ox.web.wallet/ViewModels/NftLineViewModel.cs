using OX.Network.P2P;
using OX.Wallets.Base.NFT;
using System.Text.Json.Serialization;

namespace OX.Web
{
    public class NftLineViewModel
    {
        [JsonPropertyName("Date")]
        public string Date { get; set; }
        public int scales { get; set; }
    }

}
