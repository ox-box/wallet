using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using OX.IO;


namespace OX.Wallets.States
{
    public enum NodeStateMessageKind : byte
    {
        Transfer = 1
    }
    public abstract class Message
    {
        public string GetMessageType()
        {
            return this.GetType().FullName;
        }
    }
    public interface INodeStateMessage
    {
        NodeStateMessageKind StateMessageKind { get; }
    }
    public abstract class NodeStateMessage : Message, INodeStateMessage
    {
        public abstract NodeStateMessageKind StateMessageKind { get; }
    }

    public enum MixStateMessageKind : byte
    {
        NewBlock = 1
    }
    public interface IMixStateMessage
    {
        MixStateMessageKind StateMessageKind { get; }
    }
    public abstract class MixStateMessage : Message, IMixStateMessage
    {
        public abstract MixStateMessageKind StateMessageKind { get; }
    }

    public enum ServerStateMessageKind : byte
    {
        ChatMessage = 1,
        BlockMessage = 2,
        TransactionMessage = 3,
        BalanceMessage = 4,
        BeatMessage = 5,
        ApplicationMessage = 6
    }
    public interface IServerStateMessage
    {
        ServerStateMessageKind StateMessageKind { get; }
    }
    public abstract class ServerStateMessage : Message, IServerStateMessage
    {
        public abstract ServerStateMessageKind StateMessageKind { get; }
    }
}
