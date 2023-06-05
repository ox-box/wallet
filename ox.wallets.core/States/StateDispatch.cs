using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Security.Claims;
using OX.IO;


namespace OX.Wallets.States
{
    public interface IStateDispatch
    {
        event Action<INodeStateMessage> NodeStateNotice;
        event Action<IMixStateMessage> MixStateNotice;
        event Action<IServerStateMessage> ServerStateNotice;
        void PostNodeStateMessage(INodeStateMessage stateMessage);
        void PostMixStateMessage(IMixStateMessage stateMessage);
        void PostServerStateMessage(IServerStateMessage stateMessage);
    }
    public class StateDispatcher : IStateDispatch
    {
        static StateDispatcher _instance;
        public static StateDispatcher Instance
        {
            get
            {
                if (_instance.IsNull())
                    _instance = new StateDispatcher();
                return _instance;
            }
        }
        public StateDispatcher()
        {
            _instance = this;
        }
        public event Action<INodeStateMessage> NodeStateNotice;
        public event Action<IMixStateMessage> MixStateNotice;
        public event Action<IServerStateMessage> ServerStateNotice;
        public void PostNodeStateMessage(INodeStateMessage stateMessage)
        {
            NodeStateNotice?.Invoke(stateMessage);
        }
        public void PostMixStateMessage(IMixStateMessage stateMessage)
        {
            MixStateNotice?.Invoke(stateMessage);
        }
        public void PostServerStateMessage(IServerStateMessage stateMessage)
        {
            ServerStateNotice?.Invoke(stateMessage);
        }
    }
}
