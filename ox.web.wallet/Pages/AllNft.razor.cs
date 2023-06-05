
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

namespace OX.Web.Pages
{
    public partial class AllNft
    {
        public override string PageTitle => UIHelper.LocalString("所有NFT", "All NFTs");
        int PageIndex = 1;
        int RecordCount = 0;
        NftTransaction[] NFTS = new NftTransaction[0];
        protected override void OnWalletInit()
        {
            RecordCount = (int)GetNFTCount();
            PageIndex= RecordCount / 10;
            if (RecordCount % 10 > 0)
                PageIndex++;
            reload();
        }
        uint GetNFTCount()
        {
            var ks = WalletBappProvider.Instance.GetWalletSetting(WalletSettingKind.NFTCoin_Counter);
            if (ks.IsNull()) return 0;
            return BitConverter.ToUInt32(ks.Data);
        }



        void reload()
        {
            if (PageIndex > 0)
            {
                this.NFTS = new NftTransaction[0];
                var nfts = WalletBappProvider.Instance.GetAll<NFTCoinKey, NftTransaction>(WalletBizPersistencePrefixes.NFT_Coin, BitConverter.GetBytes(PageIndex - 1));
                if (nfts.IsNotNullAndEmpty())
                {
                    this.NFTS = nfts.Select(m => m.Value).ToArray();
                }
            }
        }
        void OnPageChange(PaginationEventArgs args)
        {
            PageIndex = args.Page;
            reload();
        }
    }
}
