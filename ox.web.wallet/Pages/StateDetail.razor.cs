
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
using OX.Persistence;
using static System.Net.Mime.MediaTypeNames;
using Nethereum.Hex.HexConvertors.Extensions;

namespace OX.Web.Pages
{
    public partial class StateDetail
    {
        public override string PageTitle => this.WebLocalString("闪态", "Flash State");
        uint PageIndex = 0;
        uint MaxPageSize = 0;
        [Parameter]
        public string statehash { get; set; }
        FlashStateRecord FlashStateRecord = default;
        IEnumerable<KeyValuePair<FlashStateCommentKey, FlashStateCommentValue>> CMS;
        bool DrawerVisible = false;
        TextWrapper commentText = new TextWrapper { Text = string.Empty };
        UInt256 SelectedCommentHash = null;
        protected override void OnWalletInit()
        {
            reload();
        }
        void CloseDoComment()
        {
            this.DrawerVisible = false;
        }
        private async void Comment()
        {
            if (this.HaveEthID && this.FlashStateRecord.IsNotNull())
            {
                var mapAddress = this.EthID.MapAddress;
                var accountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(mapAddress);
                if (accountState.IsNotNull() && Blockchain.Singleton.AllowFlashMessage(accountState, out uint _))
                {
                    var text = this.commentText.Text;
                    if (text.IsNotNullAndEmpty())
                    {
                        CloseDoComment();
                        var textData = System.Text.Encoding.UTF8.GetBytes(text);
                        StateComment sc = new StateComment { StateHash = this.FlashStateRecord.FlashState.Hash, ParentCommentHash = this.SelectedCommentHash ?? UInt256.Zero, Data = textData };
                        var fs = new FlashStateComment(mapAddress, Blockchain.Singleton.HeaderHeight, new StateComment[] { sc });
                        var stringToSign = fs.GetRequireEthSignatureData().ToHex(true); ;
                        var signatureData = await this.MetaMaskService.PersonalSign(stringToSign);
                        var signer = new Nethereum.Signer.EthereumMessageSigner();
                        var ethaddress = signer.EncodeUTF8AndEcRecover(stringToSign, signatureData);
                        if (ethaddress.ToLower() == this.EthID.EthAddress.ToLower())
                        {
                            fs.EthSignature = signatureData.HexToByteArray();
                            if (fs.Size <= FlashMessage.MaxFlashMessageSize)
                            {
                                this.Box.Notecase.Relay(fs);
                                this.commentText = new TextWrapper { Text = string.Empty };
                                StateHasChanged();
                            }
                        }

                    }

                }
            }
            await Task.CompletedTask;
        }


        void reload()
        {
            FlashStateRecord = default;
            CMS = default;
            if (UInt256.TryParse(this.statehash, out UInt256 hash))
            {
                FlashStateRecord = FlashMessageProvider.Instance.GetFlashStatRecord(hash);
                CMS = FlashMessageProvider.Instance.GetFlashStateComments(hash);
            }
        }
        public bool SearchRoot(IEnumerable<KeyValuePair<FlashStateCommentKey, FlashStateCommentValue>> pool, UInt256 phash, out UInt256 rootHash)
        {
            var p = pool.FirstOrDefault(m => m.Key.CommentHash == phash);
            if (p.Equals(new KeyValuePair<FlashStateCommentKey, FlashStateCommentValue>()))
            {
                rootHash = default;
                return false;
            }
            rootHash = p.Key.CommentHash;
            if (p.Key.ParentCommentHash == UInt256.Zero) return true;
            return SearchRoot(pool, p.Key.ParentCommentHash, out rootHash);
        }
        void Reply(UInt256 commentHash)
        {
            SelectedCommentHash = commentHash;
            this.DrawerVisible = true;
        }
        void GoTag(FlashStateTag tag)
        {
            var url = $"/_pc/flashstate/stateflow/{tag.Data.ToHexString()}";
            this.NavigationManager.NavigateTo(url, true);
            this.StateHasChanged();
        }
    }
}
