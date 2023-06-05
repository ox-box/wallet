
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Nethereum.Util;
using OX.MetaMask;
using OX.Wallets.Authentication;
using OX.Wallets.Eths;
using OX.Wallets.States;
using System.Security.Claims;
using System.Threading.Tasks;
namespace OX.Wallets.States
{
    public abstract class StatesComponentBase : AuthComponentBase
    {
        [Inject]
        protected IStateDispatch StateDispatcher { get; set; }
        //[Inject]
        //protected IEthereumContext EthereumContext { get; set; }
      
        public EthID EthID { get; set; }
        public bool HaveEthID { get { return EthID.IsNotNull(); } }

        public bool HasMetaMask { get; set; }
        public string? InitializeMsg { get; set; }
        public string? SelectedChain { get; set; }
        public string? TransactionCount { get; set; }
        public string? SignedData { get; set; }
        public string? SignedDataV4 { get; set; }
        public string? PersonalSigned { get; set; }
        public string? FunctionResult { get; set; }
        public string? RpcResult { get; set; }
        public int? Chain { get; set; }


        protected override async Task OnInit()
        {
            IMetaMaskService.AccountChangedEvent += MetaMaskService_AccountChangedEvent;
            IMetaMaskService.ChainChangedEvent += MetaMaskService_ChainChangedEvent;
            IMetaMaskService.OnConnectEvent += IMetaMaskService_OnConnectEvent;
            IMetaMaskService.OnDisconnectEvent += IMetaMaskService_OnDisconnectEvent;
            StateDispatcher.NodeStateNotice += StateDispatcher_NodeStateNotice;
            StateDispatcher.MixStateNotice += StateDispatcher_MixStateNotice;
            StateDispatcher.ServerStateNotice += StateDispatcher_ServerStateNotice;
            //EthereumContext.EthAddressChanged += EthereumContext_EthAddressChanged;
            HasMetaMask = await MetaMaskService.HasMetaMask();
            if (HasMetaMask)
                await MetaMaskService.ListenToEvents();

            bool isSiteConnected = await MetaMaskService.IsSiteConnected();
            if (isSiteConnected)
            {
                await GetSelectedAddress();
                await GetSelectedNetwork();
            }
            this.OnStateInit();
        }



        public async Task ConnectMetaMask()
        {
            try
            {
                await MetaMaskService.ConnectMetaMask();

                await GetSelectedAddress();
            }
            catch (UserDeniedException)
            {
                InitializeMsg = "User Denied";
            }
            catch (Exception ex)
            {
                InitializeMsg = ex.ToString();
            }
        }

        public async Task GetSelectedAddress()
        {
            var SelectedAddress = await MetaMaskService.GetSelectedAddress();
            this.EthID = default;
            if (SelectedAddress.IsNotAnEmptyAddress())
            {
                this.EthID = new EthID(SelectedAddress);
            }
            Console.WriteLine($"Address: {SelectedAddress}");
        }

        public async Task GetSelectedNetwork()
        {
            var chainInfo = await MetaMaskService.GetSelectedChain();
            Chain = (int)chainInfo.chain;

            SelectedChain = $"ChainID: {chainInfo.chainId}, Name: {chainInfo.chain.ToString()}";
            Console.WriteLine($"ChainID: {chainInfo.chainId}");
        }
        //protected abstract void EthereumContext_EthAddressChanged(string ethAddress, int? chain);

        protected abstract void OnStateInit();

        protected abstract void IMetaMaskService_OnConnectEvent();

        protected abstract void IMetaMaskService_OnDisconnectEvent();

        protected abstract Task MetaMaskService_ChainChangedEvent((long, Chain) arg);


        protected abstract Task MetaMaskService_AccountChangedEvent(string arg);

        protected abstract void StateDispatcher_ServerStateNotice(IServerStateMessage message);


        protected abstract void StateDispatcher_MixStateNotice(IMixStateMessage message);


        protected abstract void StateDispatcher_NodeStateNotice(INodeStateMessage message);



        public override void OnDispose()
        {
            IMetaMaskService.AccountChangedEvent -= MetaMaskService_AccountChangedEvent;
            IMetaMaskService.ChainChangedEvent -= MetaMaskService_ChainChangedEvent;
            IMetaMaskService.OnConnectEvent -= IMetaMaskService_OnConnectEvent;
            IMetaMaskService.OnDisconnectEvent -= IMetaMaskService_OnDisconnectEvent;
            StateDispatcher.NodeStateNotice -= StateDispatcher_NodeStateNotice;
            StateDispatcher.MixStateNotice -= StateDispatcher_MixStateNotice;
            StateDispatcher.ServerStateNotice -= StateDispatcher_ServerStateNotice;
            //EthereumContext.EthAddressChanged -= EthereumContext_EthAddressChanged;
        }

    }
}
