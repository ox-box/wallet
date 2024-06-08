using OX.Ledger;

namespace OX.Web
{
    public class AssetLockVoteViewModel
    {
        public uint Height;
        public decimal Amount;
        public AssetLockVoteViewModel()
        {
            Height = Blockchain.Singleton.HeaderHeight - Blockchain.Singleton.HeaderHeight % 10000 + 10000;
            Amount = 0;
        }
    }
}
