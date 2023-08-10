
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
    public partial class Explorer
    {
        public override string PageTitle => this.WebLocalString("区块浏览", "Block Explorer");
        [Parameter]
        public string? blockindex { get; set; }
        uint Index;
        Block block { get; set; }
        protected override void OnBlockchainInit()
        {
            if (blockindex != null)
            {
                if (!uint.TryParse(blockindex, out Index))
                    NavigationManager.NavigateTo("/");
                if (Index != 0)
                {
                    block = Blockchain.Singleton.CurrentSnapshot.GetBlock(Index);
                    if (block.IsNull())
                        NavigationManager.NavigateTo("/");
                }
            }
        }
        public void Previous()
        {
            if (this.Index > 0)
            {
                this.Index--;
                this.blockindex = this.Index.ToString();
                var b = Blockchain.Singleton.CurrentSnapshot.GetBlock(Index);
                if (b.IsNotNull())
                {
                    block = b;
                }
            }
        }
        public void Next()
        {
            this.Index++;
            this.blockindex = this.Index.ToString();
            var b = Blockchain.Singleton.CurrentSnapshot.GetBlock(Index);
            if (b.IsNotNull())
            {
                block = b;
            }
        }
        public void OnSearch()
        {
            if (uint.TryParse(blockindex, out Index))
            {
                var b = Blockchain.Singleton.CurrentSnapshot.GetBlock(Index);
                if (b.IsNotNull())
                {
                    block = b;
                }
            }
            //StateHasChanged();
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
