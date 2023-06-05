
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
using OX.Persistence;

namespace OX.Web.Pages
{
    public partial class Address
    {
        public override string PageTitle => UIHelper.LocalString("地址浏览", "Address Explorer");
        [Parameter]
        public string? addr { get; set; }
        UInt160 SH;
        AccountState AccountState { get; set; }
        protected override void OnBlockchainInit()
        {
            if (addr != null)
            {
                try
                {
                    SH = addr.ToScriptHash();
                }
                catch
                {
                    NavigationManager.NavigateTo("/");
                }
                if (SH.IsNotNull())
                    AccountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(SH);
            }
        }

        public void OnSearch()
        {
            if (addr != null)
            {
                try
                {
                    SH = addr.ToScriptHash();
                }
                catch
                {
                    NavigationManager.NavigateTo("/");
                }
                if (SH.IsNotNull())
                    AccountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(SH);
            }
        }
        protected override void StateDispatcher_ServerStateNotice(IServerStateMessage message)
        {

        }


        protected override void StateDispatcher_MixStateNotice(IMixStateMessage message)
        {

        }


        protected override void StateDispatcher_NodeStateNotice(INodeStateMessage message)
        {

        }

    }
}
