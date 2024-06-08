
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
using NuGet.Protocol.Plugins;
using OX.Persistence;

namespace OX.Web.Pages
{
    public partial class AuthorFlow
    {
        public override string PageTitle => this.WebLocalString("闪态流", "Flash State Flow");
        uint PageIndex = 1;
        uint MaxPageSize = 0;
        [Parameter]
        public string authoraddress { get; set; }
        public UInt160 Author;
        IOrderedEnumerable<FlashStateRecord> Records = default;
        List<FlashStateTag> tags = new List<FlashStateTag>();
        protected override void OnWalletInit()
        {
            var alias = authoraddress;
            try
            {
                Author = authoraddress.ToScriptHash();
                if (Blockchain.Singleton.GetDomain(Author, out byte[] pdomain))
                {
                    alias = System.Text.Encoding.UTF8.GetString(pdomain);
                }

                reload();
            }
            catch
            {

            }
        }

        IOrderedEnumerable<FlashStateRecord> GetRecords(uint pageIndex)
        {
            var range = FlashMessageProvider.Instance.GetLastRangeForSender(this.Author);
            if (range == uint.MaxValue)
            {
                this.MaxPageSize = 0;
                return default;
            }
            this.MaxPageSize = range;
            return FlashMessageProvider.Instance.GetCachedFlashStateRecordsForSender(this.Author, range - pageIndex);
        }
        void reload()
        {
            this.Records = this.GetRecords(this.PageIndex-1);
            this.tags.Clear();
            if (this.Records.IsNotNullAndEmpty())
            {
                foreach (var record in this.Records)
                    if (record.FlashState.Tags.IsNotNullAndEmpty())
                    {
                        foreach (var tag in record.FlashState.Tags)
                            if (!tags.Contains(tag)) tags.Add(tag);
                    }
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
