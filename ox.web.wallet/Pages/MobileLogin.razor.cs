
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
    public partial class MobileLogin
    {
        public override string PageTitle => this.WebLocalString("简易认证", "Easy Authorize");

        public EasyAuthorizeViewModel EasyAuthorizeViewModel { get; set; } = new EasyAuthorizeViewModel { EasyCode = string.Empty };
        protected override async Task OnInit()
        {
            await Task.CompletedTask;
        }
        public override async Task OnAuthInitialized()
        {
            await this.SetLocalStorage("_ox_box_easy_code", string.Empty);
        }
        public override async Task OnInitCompleted()
        {
            await Task.CompletedTask;
        }
        public override void OnDispose()
        {

        }

        async Task Authentication()
        {
            var box = WebBox.GetWebBox<WalletWebBox>();
            if (box.IsNotNull() && EasyAuthorizeViewModel.IsNotNull() && EasyAuthorizeViewModel.EasyCode.IsNotNullAndEmpty())
            {
                if (box.Notecase.IsNotNull() && box.Notecase.Wallet.IsNotNull())
                {
                    var act = box.GetWalletAccountByAccessCode(EasyAuthorizeViewModel.EasyCode);
                    if (act.IsNotNull())
                    {
                        await this.SetLocalStorage("_ox_box_easy_code", EasyAuthorizeViewModel.EasyCode);
                        NavigationManager.NavigateTo("/_m");
                    }
                }
            }
        }
    }
}
