using OX.Network.P2P.Payloads;
using OX.Wallets.States;

namespace OX.Wallets.Messages
{
    public class NewBlockMessage : MixStateMessage
    {
        public override MixStateMessageKind StateMessageKind => MixStateMessageKind.NewBlock;
        public Block Block { get; set; }

    }
}
