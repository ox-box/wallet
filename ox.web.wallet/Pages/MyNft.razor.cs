
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

namespace OX.Web.Pages
{
    public partial class MyNft
    {
        public override string PageTitle => this.WebLocalString("我持有的NFT", "My hold NFTs");

        IEnumerable<KeyValuePair<EthNftTransferKey, NftTransferTransaction>> Data = default;
        protected override void OnWalletInit()
        {

            reload();
        }



        void reload()
        {
            if (this.Valid)
            {
                Data = WalletBappProvider.Instance.GetAll<EthNftTransferKey, NftTransferTransaction>(WalletBizPersistencePrefixes.NFT_Transfer_Record_Server, new StringWrapper(this.EthID.EthAddress.ToLower()));
            }
        }

    }
}
