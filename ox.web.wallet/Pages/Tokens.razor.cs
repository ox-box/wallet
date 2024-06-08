
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
using OX.Cryptography;
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
using NBitcoin.OpenAsset;

namespace OX.Web.Pages
{
    public partial class Tokens
    {
        public override string PageTitle => this.WebLocalString("资产详情", "Asset Details");

        protected override void OnBlockchainInit()
        {

        }
        void GoVote(UInt256 assetId)
        {
            var url = $"/_pc/blockchain/assetlockvote/{assetId.ToString()}";
            this.NavigationManager.NavigateTo(url, true);
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
