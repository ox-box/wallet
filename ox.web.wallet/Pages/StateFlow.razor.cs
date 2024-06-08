
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
using AntDesign;
using OX.Wallets.Base;
using OX.Wallets.Flash;
using Org.BouncyCastle.Asn1.Pkcs;

namespace OX.Web.Pages
{
    public partial class StateFlow
    {
        public override string PageTitle => this.WebLocalString("闪态流", "Flash State Flow");
        uint PageIndex = 1;
        uint MaxPageSize = 0;
        [Parameter]
        public string taghex { get; set; }
        public string tag;
        public FlashStateTag FlashStateTag;
        public Func<uint, IOrderedEnumerable<FlashStateRecord>> CurrentFunc { get; set; } = default;
        IOrderedEnumerable<FlashStateRecord> Records = default;
        protected override void OnWalletInit()
        {
            var bs = taghex.HexToBytes();
            tag = System.Text.Encoding.UTF8.GetString(bs);
            FlashStateTag = new FlashStateTag { Data = bs };
            if (tag == "latest")
            {
                this.CurrentFunc = pageIndex =>
                {
                    var range = FlashMessageProvider.Instance.GetLastRangeForSender(UInt160.Zero);
                    if (range == uint.MaxValue)
                    {
                        this.MaxPageSize = 0;
                        return default;
                    }
                    this.MaxPageSize = range;
                    return FlashMessageProvider.Instance.GetCachedFlashStateRecordsForLatest(range - pageIndex);
                };
            }
            else if (tag == "hot")
            {
                this.CurrentFunc = pageIndex =>
                {
                    if (pageIndex >= 100) return default;
                    this.MaxPageSize = 100;
                    return FlashMessageProvider.Instance.GetCachedHotFlashStateRecords(pageIndex);
                };
            }
            else
            {
                this.CurrentFunc = pageIndex =>
                {
                    FlashStateTag fst = new FlashStateTag(this.tag);
                    var range = FlashMessageProvider.Instance.GetLastRangeForTag(fst);
                    if (range == uint.MaxValue)
                    {
                        this.MaxPageSize = 0;
                        return default;
                    }
                    this.MaxPageSize = range;
                    return FlashMessageProvider.Instance.GetCachedFlashStateRecordsForTag(fst, range - pageIndex);
                };
            }
            reload();
        }


        void reload()
        {
            if (this.CurrentFunc.IsNotNull())
            {
                this.Records = this.CurrentFunc(this.PageIndex-1);
            }
        }
        void OnPageChange(PaginationEventArgs args)
        {
            PageIndex = (uint)args.Page;
            reload();
        }
        void GoTag(FlashStateTag tag)
        {
            var url = $"/_pc/flashstate/stateflow/{tag.Data.ToHexString()}";
            this.NavigationManager.NavigateTo(url, true);
            this.StateHasChanged();
        }
    }
}
