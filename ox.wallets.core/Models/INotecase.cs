using OX.Network.P2P.Payloads;

namespace OX.Wallets
{
    public interface INotecase
    {
        Wallet Wallet { get; }
        WalletIndexer Indexer { get; }
        void Relay(IInventory inventory);
        void Close();
        void Exit();
    }
}
