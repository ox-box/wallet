
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
    public partial class BalanceDetail
    {
        public override string PageTitle => this.WebLocalString("余额明细", "Balance Detail");
        [Parameter]
        public string assetid { get; set; }
        AssetState AssetState;
        EthOutputMerge[] Outputs;
        protected override void OnWalletInit()
        {
            ReloadData();
        }

        protected override async Task MetaMaskService_AccountChangedEvent(string arg)
        {
            await base.MetaMaskService_AccountChangedEvent(arg);
            ReloadData();
        }
        void ReloadData()
        {
            if (this.Valid && this.Box.Notecase.Wallet is OpenWallet openWallet)
            {
                AssetState = default;
                if (UInt256.TryParse(this.assetid, out UInt256 aid))
                {
                    AssetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(aid);
                    var utxos = openWallet.GetAllEthereumMapUTXOs();
                    var us = utxos.Where(m => m.Value.EthAddress.ToLower() == this.EthID.EthAddress && m.Value.Output.AssetId == aid);
                    if (us.IsNotNullAndEmpty())
                    {
                        Outputs = us.Select(m => m.Value).OrderBy(m => m.LockExpirationIndex).ToArray();
                    }
                }
            }
        }
    }
}
