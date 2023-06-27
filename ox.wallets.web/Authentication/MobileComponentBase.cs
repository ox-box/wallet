
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR.Client;
using OX.Wallets.States;
using System.Security.Claims;
using System.Threading.Tasks;
using OX.MetaMask;
using OX.Ledger;

namespace OX.Wallets.Authentication
{
    public abstract class MobileComponentBase : MobileAuthComponentBase, IDisposable
    {
        public string accessCode;
        public WalletAccount Account;

        protected override async Task OnInit()
        {
            accessCode = await this.GetLocalStorage("_ox_box_easy_code");
            if (accessCode.IsNullOrEmpty())
            {
                NavigationManager.NavigateTo("/_m/easyauthorize");
            }
            var box = WebBox.Boxes.FirstOrDefault();
            if (box.IsNull()) NavigationManager.NavigateTo("/_m");
            Account = box.GetWalletAccountByAccessCode(accessCode);
            if (Account.IsNull()) NavigationManager.NavigateTo("/_m/easyauthorize");
            await Task.CompletedTask;
        }
        
    }
}
