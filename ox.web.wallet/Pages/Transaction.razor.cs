
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
    public partial class Transaction
    {
        public override string PageTitle => UIHelper.LocalString("交易浏览", "Transaction Explorer");
        [Parameter]
        public string? txid { get; set; }
        UInt256 TxHash;
        OX.Network.P2P.Payloads.Transaction TX { get; set; }
        protected override void OnBlockchainInit()
        {
            if (txid != null)
            {
                if (!UInt256.TryParse(txid, out TxHash))
                    NavigationManager.NavigateTo("/");
                TX = Blockchain.Singleton.CurrentSnapshot.GetTransaction(TxHash);
                if (TX.IsNull())
                    NavigationManager.NavigateTo("/");
            }
        }

        public void OnSearch()
        {
            if (UInt256.TryParse(txid, out TxHash))
            {
                var t = Blockchain.Singleton.CurrentSnapshot.GetTransaction(TxHash);
                if (t.IsNotNull())
                {
                    TX = t;
                }
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
