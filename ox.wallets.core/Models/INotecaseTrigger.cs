using OX.Bapps;
using OX.Network.P2P.Payloads;

namespace OX.Wallets
{
    public interface INotecaseTrigger
    {
        void HeartBeat(HeartBeatContext context);
        void ChangeWallet(INotecase operater);
        void OnBappEvent(BappEvent bappEvent);
        void OnCrossBappMessage(CrossBappMessage message);
        void OnBlock(Block block);
        void BeforeOnBlock(Block block);
        void AfterOnBlock(Block block);
        void OnRebuild();
    }
}
