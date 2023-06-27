
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using OX.Wallets.States;
using System.Security.Claims;
using System.Threading.Tasks;
using OX.MetaMask;
using OX.Wallets.Messages;
using OX.Network.P2P.Payloads;
using OX.Ledger;
using OX.Persistence;

namespace OX.Wallets.Authentication
{
    public abstract class MobileAuthComponentBase : WebBoxComponentBase, IDisposable
    {
        public abstract string PageTitle { get; }

        [Inject]
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
        [Inject]
        protected ILocalStorageService LocalStorage { get; set; }
        [Inject]
        protected IStateDispatch StateDispatcher { get; set; }
        public Block LastBlock { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await this.OnAuthInitialized();
            await this.OnInit();
            await base.OnInitializedAsync();
            await this.OnInitCompleted();
            StateDispatcher.MixStateNotice += StateDispatcher_MixStateNotice;
        }

        public virtual void StateDispatcher_MixStateNotice(IMixStateMessage message)
        {
            if (message.StateMessageKind == MixStateMessageKind.NewBlock)
            {
                NewBlockMessage msg = message as NewBlockMessage;
                this.LastBlock = msg.Block;

                InvokeAsync(StateHasChanged);
            }
        }

        protected abstract Task OnInit();
        public new void Dispose()
        {
            this.OnDispose();
            StateDispatcher.MixStateNotice -= StateDispatcher_MixStateNotice;
            base.Dispose();
        }
        public abstract void OnDispose();
        public abstract Task OnAuthInitialized();
        public abstract Task OnInitCompleted();

        public async Task SetLocalStorage(string key, string value)
        {
            await LocalStorage.SetItemAsync(key, value);
        }
        public async ValueTask<string> GetLocalStorage(string key)
        {
            return await LocalStorage.GetItemAsync<string>(key);
        }

    }
}
