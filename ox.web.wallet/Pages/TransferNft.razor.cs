
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
using System.Threading;
using Nethereum.Util;
using NuGet.ContentModel;
using OX.Persistence;
using OX.Network.P2P;
using OX.Wallets.Base.NFT;
using Akka.Actor.Dsl;
using System.Text;

namespace OX.Web.Pages
{

    public partial class TransferNft
    {
        public override string PageTitle => this.WebLocalString("转售 NFT", "Transfer NFT");
        [Parameter]
        public string transferhex { get; set; }
        TransferNftViewModel Model = new TransferNftViewModel();
        NftTransferTransaction NftTransfer;
        NFSStateKey Mykey = default;
        string msg;

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
            reload();
        }

        protected override async Task MetaMaskService_AccountChangedEvent(string arg)
        {
            await base.MetaMaskService_AccountChangedEvent(arg);
            reload();

        }
        void reload()
        {
            Mykey = default;
            NftTransfer = default;
            if (this.Valid&&transferhex.IsNotNullAndEmpty())
            {
                try
                {
                    var bs = transferhex.HexToBytes();
                    var NFSStateKey = bs.AsSerializable<NFSStateKey>();
                    if (NFSStateKey.IsNotNull())
                    {
                        var nfsState = Blockchain.Singleton.CurrentSnapshot.GetNftTransfer(NFSStateKey);
                        if (nfsState.IsNotNull())
                        {
                            if (nfsState.LastNFS.NFSHolder.MixAccountType == Network.P2P.MixAccountType.Ethereum)
                            {
                                if (nfsState.LastNFS.NFSHolder.AsEthAddress().ToLower() == this.EthID.EthAddress.ToLower())
                                {
                                    Mykey = NFSStateKey;
                                    NftTransfer = nfsState.LastNFS;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    msg = this.WebLocalString($"参数格式错误", $"Parameter format error");
                }
            }
        }
        async Task Transfer()
        {
            if (this.Valid && Mykey.IsNotNull() && this.NftTransfer.IsNotNull() && this.NftTransfer.NFSHolder.IsNotNull() && this.NftTransfer.NFSHolder.MixAccountType == MixAccountType.Ethereum)
            {
                NftTransferAuthentication auth = new NftTransferAuthentication
                {
                    Amount = Fixed8.FromDecimal(this.Model.Amount),
                    MaxIndex = this.Model.MaxIndex,
                    MinIndex = this.Model.MinIndex,
                    Target = this.NftTransfer.NFSHolder,
                    PreHash = this.NftTransfer.Hash
                };
                var stringToSign = auth.ToArray().ToHexString();
                var signatureData = await this.MetaMaskService.PersonalSign(stringToSign);
                var signer = new Nethereum.Signer.EthereumMessageSigner();
                var ethaddress = signer.EncodeUTF8AndEcRecover(stringToSign, signatureData);
                if (ethaddress.ToLower() == this.EthID.EthAddress.ToLower())
                {
                    byte[] signData= Encoding.UTF8.GetBytes(signatureData);
                    MixSignatureValidator<NftTransferAuthentication> validator = new MixSignatureValidator<NftTransferAuthentication>() { Target = auth, Signature = signData };
                    NFSStateKey nFSStateKey = new NFSStateKey
                    {
                        NFCID = this.NftTransfer.NFSStateKey.NFCID,
                        IssueBlockIndex = this.NftTransfer.NFSStateKey.IssueBlockIndex == 0 ? Mykey.IssueBlockIndex : this.NftTransfer.NFSStateKey.IssueBlockIndex,
                        IssueN = this.NftTransfer.NFSStateKey.IssueN == 0 ? Mykey.IssueN : this.NftTransfer.NFSStateKey.IssueN
                    };
                    NFTTranferData ndv = new NFTTranferData { Key = nFSStateKey, Validator = validator };
                    this.Model.Signature = ndv.ToArray().ToHexString();
                }

            }
        }
    }
}
