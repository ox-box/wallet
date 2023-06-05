
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
using AntDesign;
using OX.Wallets.Base;
using Akka.Actor.Dsl;
using NuGet.Protocol.Plugins;
using System.Text;
using OX.Wallets.Base.NFT;
using OX.Persistence;
using System.Windows.Forms;
using AntDesign.Charts;

namespace OX.Web.Pages
{
    public partial class NftLine
    {
        public override string PageTitle => UIHelper.LocalString("成交走势", "Transfer Trend");

        [Parameter]
        public string nftidhex { get; set; }
       
        NftLineViewModel[] Points = new NftLineViewModel[0];
        protected override void OnWalletInit()
        {
        }
      

    }
}
