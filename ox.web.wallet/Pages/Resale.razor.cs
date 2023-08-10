
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
using OX.Wallets.Base.NFT;
using OX.Persistence;

namespace OX.Web.Pages
{
    public partial class Resale
    {

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
        public override string PageTitle => this.WebLocalString("待售NFT", "For sale NFTs");
        public PublishResaleViewModel Model { get; set; } = new PublishResaleViewModel();
        bool DrawerVisible = false;
        protected override void OnWalletInit()
        {
            reload();
        }




        void reload()
        {

        }
        void OpenPublish()
        {
            this.DrawerVisible = true;
        }
        void ClosePublish()
        {
            this.DrawerVisible = false;
        }
        private async void GoPublish()
        {
            if (this.Model.IsNotNull() && this.Model.Data.IsNotNullAndEmpty())
            {
                var ndv = this.Model.Data.HexToBytes().AsSerializable<NFTTranferData>();
                if (ndv.IsNull()) return;
                if (ndv.IsNull() || ndv.Validator.IsNull() || ndv.Key.IsNull() || !ndv.Validator.Verify()) return;
                if (ndv.Validator.Target.Amount <= Fixed8.Zero || ndv.Validator.Target.MaxIndex < ndv.Validator.Target.MinIndex || ndv.Validator.Target.MaxIndex <= Blockchain.Singleton.Height) return;
                var lastNftDonate = Blockchain.Singleton.CurrentSnapshot.GetNftTransfer(ndv.Key);
                if (lastNftDonate.IsNull()) return;
                var nfc = Blockchain.Singleton.CurrentSnapshot.GetNftState(ndv.Key.NFCID);
                if (nfc.IsNull()) return;
                if (!ndv.Validator.Target.PreHash.Equals(lastNftDonate.LastNFS.Hash)) return;
                if (NFTBook.Instance.IsNotNull())
                {
                    if (NFTBook.Instance.Append(ndv))
                        NFTBook.Instance.SaveWallet();
                    ClosePublish();
                    await InvokeAsync(StateHasChanged);
                }
            }
        }
    }
}
