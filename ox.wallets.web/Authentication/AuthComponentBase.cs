
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using OX.Wallets.States;
using System.Security.Claims;
using System.Threading.Tasks;
using OX.MetaMask;
namespace OX.Wallets.Authentication
{
    public abstract class AuthComponentBase : WebBoxComponentBase, IDisposable
    {
        public abstract string PageTitle { get; }
        [Inject]
        public IMetaMaskService MetaMaskService { get; set; } = default!;
        [Inject]
        protected IHttpContextAccessor HttpContextAccessor { get; set; }
       



        protected OXUser User { get; private set; }
        public bool IsSelf { get { return User.IsNotNull() && User.IsSelf; } }
        protected override async Task OnInitializedAsync()
        {
            this.OnAuthInitialized();
            await this.OnInit();
            await base.OnInitializedAsync();
        }
        protected abstract Task OnInit();
        public abstract void OnDispose();
        protected void OnAuthInitialized()
        {
            var user = HttpContextAccessor?.HttpContext?.User;
            if (user.IsNotNull() && user is OXUser oxuser)
            {
                this.User = oxuser;
            }
        }
        
      

    }
}
