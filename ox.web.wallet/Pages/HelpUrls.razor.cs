
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

namespace OX.Web.Pages
{
    public partial class HelpUrls
    {
        public override string PageTitle => this.WebLocalString("门户地址", "Portal Urls");

        protected override void OnBlockchainInit()
        {
              
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
