
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
    public partial class AccountAsset
    {
        public override string PageTitle => UIHelper.LocalString("账户资产", "Account Asset");
        EthAssetBalanceState[] BalanceState = new EthAssetBalanceState[0];
        protected override void OnWalletInit()
        {
            if (this.Valid && this.Box.Notecase.Wallet is OpenWallet openWallet)
            {
                var bs = openWallet.QueryBalanceState(this.EthID);
                BalanceState = bs.ToArray();
            }
        }

        protected override async Task MetaMaskService_AccountChangedEvent(string arg)
        {
            await base.MetaMaskService_AccountChangedEvent(arg);
            this.BalanceState = new EthAssetBalanceState[0];
            if (this.Valid && this.Box.Notecase.Wallet is OpenWallet openWallet)
            {
                var bs = openWallet.QueryBalanceState(this.EthID);
                BalanceState = bs.ToArray();
            }
            //StateHasChanged();
        }

    }
}
