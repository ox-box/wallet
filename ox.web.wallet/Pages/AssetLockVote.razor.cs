
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
using AntDesign;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Akka.Util;

namespace OX.Web.Pages
{
    public partial class AssetLockVote
    {
        public class FormItemLayout
        {
            public ColLayoutParam LabelCol { get; set; }
            public ColLayoutParam WrapperCol { get; set; }
        }
        public override string PageTitle => this.WebLocalString("资产锁仓投票", "Asset Lock Vote");
        [Parameter]
        public string? assetid { get; set; }
        AssetState AssetState;
        string assetInfo;
        Dictionary<uint, Fixed8> votes = new Dictionary<uint, Fixed8>();
        AssetLockVoteViewModel Model = new AssetLockVoteViewModel();
        private readonly FormItemLayout _formItemLayout = new FormItemLayout
        {
            LabelCol = new ColLayoutParam
            {
                Xs = new EmbeddedProperty { Span = 24 },
                Sm = new EmbeddedProperty { Span = 7 },
            },

            WrapperCol = new ColLayoutParam
            {
                Xs = new EmbeddedProperty { Span = 24 },
                Sm = new EmbeddedProperty { Span = 12 },
                Md = new EmbeddedProperty { Span = 10 },
            }
        };

        private readonly FormItemLayout _submitFormLayout = new FormItemLayout
        {
            WrapperCol = new ColLayoutParam
            {
                Xs = new EmbeddedProperty { Span = 24, Offset = 0 },
                Sm = new EmbeddedProperty { Span = 10, Offset = 7 },
            }
        };
        protected override void OnWalletInit()
        {
            if (assetid != null)
            {
                if (!UInt256.TryParse(assetid, out UInt256 astID))
                    NavigationManager.NavigateTo("/");
                if (astID.IsNotNull())
                {
                    AssetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(astID);
                    if (AssetState.IsNotNull())
                    {
                        assetInfo = $"{AssetState.GetName()}      ({AssetState.AssetId.ToString()})";
                        var daoVoteList = Blockchain.Singleton.CurrentSnapshot.DaoVoteList.TryGet(astID);
                        if (daoVoteList.IsNotNull() && daoVoteList.Votes.IsNotNullAndEmpty())
                        {
                            votes = daoVoteList.Votes;
                        }
                    }
                }
            }
        }
        private void HandleSubmit()
        {
            if (this.Valid && this.Model.IsNotNull() && this.AssetState.IsNotNull())
            {
                if (this.Model.Amount > 0)
                {
                    if (this.Model.Height % 10000 == 0 && this.Model.Height > Blockchain.Singleton.HeaderHeight)
                    {
                        DaoVote dv = new DaoVote { Index = this.Model.Height, AssetId = this.AssetState.AssetId };
                        var url = $"/_pc/wallet/transferasset/{this.AssetState.AssetId.ToString()}/2/{this.EthID.EthAddress}/{this.Model.Amount.ToString()}/{this.Model.Height}/{dv.ToArray().ToHexString()}";
                        this.NavigationManager.NavigateTo(url, true);
                        this.StateHasChanged();
                    }
                    else
                    {
                        this.Model.Height = 0;
                        this.StateHasChanged();
                    }
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
