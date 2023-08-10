
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using Nethereum.Util;
using OX.MetaMask;
using OX.Wallets;
using OX.Wallets.Authentication;
using OX.Wallets.Eths;
using OX.Wallets.States;
using System.Security.Claims;
using System.Threading.Tasks;
namespace OX.Web.Models
{
    public abstract class BlockchainComponentBase : AuthComponentBase
    {
        [Inject]
        protected IStateDispatch StateDispatcher { get; set; }
        [CascadingParameter]
        public EventCallback LanguageCallBack { get; set; }
        protected override async Task OnInit()
        {
            if (OXRunTime.RunMode != RunMode.Server)
                NavigationManager.NavigateTo("/");
            StateDispatcher.NodeStateNotice += StateDispatcher_NodeStateNotice;
            StateDispatcher.MixStateNotice += StateDispatcher_MixStateNotice;
            StateDispatcher.ServerStateNotice += StateDispatcher_ServerStateNotice;

            this.OnBlockchainInit();

            await Task.CompletedTask;
        }
        protected override async void OnInitWebBox()
        {
            if (this.Language.IsNotNullAndEmpty())
            {
                await this.LanguageCallBack.InvokeAsync(this.Language);
            }
            base.OnInitWebBox();
        }

        protected abstract void OnBlockchainInit();



        protected abstract void StateDispatcher_ServerStateNotice(IServerStateMessage message);


        protected abstract void StateDispatcher_MixStateNotice(IMixStateMessage message);


        protected abstract void StateDispatcher_NodeStateNotice(INodeStateMessage message);



        public override void OnDispose()
        {
            StateDispatcher.NodeStateNotice -= StateDispatcher_NodeStateNotice;
            StateDispatcher.MixStateNotice -= StateDispatcher_MixStateNotice;
            StateDispatcher.ServerStateNotice -= StateDispatcher_ServerStateNotice;
        }

    }
}
