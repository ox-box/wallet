
using Microsoft.AspNetCore.Components;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using OX.Wallets;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;
using OX.Network.P2P.Payloads;
using OX;
using OX.IO;
using OX.Cryptography.ECC;
using OX.Ledger;
using OX.SmartContract;
using OX.Cryptography.AES;
using OX.Web.Models;
using OX.Wallets.Hubs;
using Microsoft.AspNetCore.SignalR.Client;
using OX.Wallets.Authentication;
using Microsoft.DotNet.Scaffolding.Shared.CodeModifier.CodeChange;
using OX.Wallets.States;
using OX.Bapps;
using OX.Wallets.Eths;
using OX.MetaMask;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace OX.Web.Pages
{
    public partial class Mobile
    {
        public override string PageTitle => this.WebLocalString("首页", "Home");
        [Parameter]
        public string accesscode { get; set; }
        protected override async Task OnInit()
        {
            if (accesscode.IsNotNullAndEmpty())
            {
                await this.SetLocalStorage("_ox_box_easy_code", accesscode);
            }
        }
        public override async Task OnAuthInitialized()
        {
            await Task.CompletedTask;
        }
        public override async Task OnInitCompleted()
        {
            await Task.CompletedTask;
        }
        public override void OnDispose()
        {

        }

        void Go(string url)
        {
            NavigationManager.NavigateTo(url);
        }
        public async void ChangeLanguage(string language)
        {
            var u = language == "English" ? "en-us" : "zh-cn";
            this.Language = u;
            await this.SetLocalStorage("_ox_box_language", u);            
            await InvokeAsync(StateHasChanged);
        }
    }
}
