using OX.Network.P2P.Payloads;

namespace OX.Wallets
{
    public interface INotecase
    {
        OpenWallet Wallet { get; }
        WalletIndexer Indexer { get; }
        WalletIndexer GetIndexer(string walletpath);
        void Relay(IInventory inventory);
        void Close();
        void Exit();
    }
}
