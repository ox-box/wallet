
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
using OX.Wallets.UI.Forms;
using Nethereum.Hex.HexConvertors.Extensions;

namespace OX.Web.Pages
{

    public partial class BuyNft
    {
        public override string PageTitle => UIHelper.LocalString("购买NFT", "Buy NFT");
        [Parameter]
        public string issueid { get; set; }

        BuyNftViewModel Model = new BuyNftViewModel();

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
            this.Model = new BuyNftViewModel();
            if (issueid.IsNotNullAndEmpty() && NFTBook.Instance.IsNotNull() && NFTBook.Instance.Records.TryGetValue(issueid, out NFTBookRecord record))
            {
                this.Model.Signature = record.Auth;
                OnBuy();
            }
        }
        void OnBuy()
        {
            if (this.Model.Signature.IsNotNullAndEmpty())
            {
                NFTTranferData ndv = default;
                try
                {
                    ndv = this.Model.Signature.HexToBytes().AsSerializable<NFTTranferData>();
                }
                catch
                {
                    msg = UIHelper.LocalString("签名数据格式错误", "Signature data format error");
                    reload();
                    return;
                }
                if (ndv.IsNull() || ndv.Validator.IsNull() || ndv.Key.IsNull() || !ndv.Validator.Verify())
                {
                    msg = UIHelper.LocalString("签名验证失败", "Signature verify failed");
                    reload();
                    return;
                }
                if (ndv.Validator.Target.Amount < Fixed8.Zero || ndv.Validator.Target.MaxIndex < ndv.Validator.Target.MinIndex)
                {
                    msg = UIHelper.LocalString("签名内容不合格", "The signature content is invalid");
                    reload();
                    return;
                }
                var nft = Blockchain.Singleton.Store.GetNftState(ndv.Key.NFCID);
                if (nft.IsNull())
                {
                    msg = UIHelper.LocalString("NFT没找到", "not found NFT");
                    return;
                }
                var donateState = Blockchain.Singleton.Store.GetNftTransfer(ndv.Key);
                if (donateState.IsNull())
                {
                    msg = UIHelper.LocalString("NFT转售错误", "NFT transfer error");
                    reload();
                    return;
                }
                if (donateState.LastNFS.Hash != ndv.Validator.Target.PreHash)
                {
                    msg = UIHelper.LocalString("NFT转售错误", "NFT transfer error");
                    reload();
                    return;
                }
                if (donateState.LastNFS.NftChangeType == NftChangeType.Issue && nft.NFC.FirstResaleLock > 0)
                {
                    if (Blockchain.Singleton.Height <= ndv.Key.IssueBlockIndex + nft.NFC.FirstResaleLock * 10000)
                    {
                        msg = UIHelper.LocalString("NFT禁售期未到", "NFT  lockdown period has not yet arrived");
                        return;
                    }
                }
                this.Model.CID = ndv.Key.NFCID.CID;
                this.Model.Amount = (decimal)ndv.Validator.Target.Amount;
                this.Model.MaxIndex = ndv.Validator.Target.MaxIndex;
                this.Model.MinIndex = ndv.Validator.Target.MinIndex;
                this.Model.Checked = true;
                this.Model.NFTTranferData = ndv;
                this.msg = string.Empty;
            }

        }
        void DoBuy()
        {
            NftTransferTransaction tx = new NftTransferTransaction
            {
                NFSStateKey = this.Model.NFTTranferData.Key,
                NFSCopyright = new NftTransferCopyright { HolderName = this.Model.HolderName, SN = this.Model.SN },
                NftChangeType = NftChangeType.Transfer,
                NFSHolder = new NFSHolder { MixAccountType = MixAccountType.Ethereum, Target = this.EthID.EthAddress.ToLower().HexToByteArray() },
                Auth = this.Model.NFTTranferData.Validator
            };
            UInt160 sh = default;
            var oldHolder = this.Model.NFTTranferData.Validator.Target.Target;
            if (oldHolder.MixAccountType == MixAccountType.OX)
            {
                sh = oldHolder.AsOXAddress();
            }
            else
            {
                sh = oldHolder.AsEthAddress().BuildMapAddress();
            }
            DoTransfer(tx, sh, this.Model.NFTTranferData.Validator.Target.Amount);

        }
        private async void DoTransfer(OX.Network.P2P.Payloads.Transaction tx, UInt160 shTarget, Fixed8 amt)
        {
            try
            {
                if (this.Box.Notecase.Wallet is OpenWallet openWallet && this.EthID.AllowTransaction())
                {
                    var allutxos = openWallet.GetAllEthereumMapUTXOs();
                    if (allutxos.IsNotNullAndEmpty())
                    {
                        var us = allutxos.Where(m => m.Value.Output.AssetId == Blockchain.OXC && m.Value.EthAddress == this.EthID.EthAddress && m.Value.LockExpirationIndex < Blockchain.Singleton.Height);
                        if (us.IsNotNullAndEmpty())
                        {
                            List<EthMapUTXO> utxos = new List<EthMapUTXO>();
                            foreach (var r in us)
                            {
                                utxos.Add(new EthMapUTXO
                                {
                                    Address = r.Value.Output.ScriptHash,
                                    Value = r.Value.Output.Value.GetInternalValue(),
                                    TxId = r.Key.PrevHash,
                                    N = r.Key.PrevIndex,
                                    EthAddress = r.Value.EthAddress,
                                    LockExpirationIndex = r.Value.LockExpirationIndex
                                });
                            }
                            List<string> excludedUtxoKeys = new List<string>();
                            if (utxos.SortSearch(amt.GetInternalValue() + tx.SystemFee.GetInternalValue(), excludedUtxoKeys, out EthMapUTXO[] selectedUtxos, out long remainder))
                            {
                                List<TransactionOutput> outputs = new List<TransactionOutput>();
                                outputs.Add(new TransactionOutput { AssetId = Blockchain.OXC, Value = amt, ScriptHash = shTarget });
                                if (remainder > 0)
                                {
                                    outputs.Add(new TransactionOutput { AssetId = Blockchain.OXC, Value = new Fixed8(remainder), ScriptHash = this.EthID.MapAddress });
                                }
                                List<CoinReference> inputs = new List<CoinReference>();
                                Dictionary<UInt160, Contract> contracts = new Dictionary<UInt160, Contract>();
                                foreach (var utxo in selectedUtxos)
                                {
                                    inputs.Add(new CoinReference { PrevHash = utxo.TxId, PrevIndex = utxo.N });
                                    EthereumMapTransaction emt = new EthereumMapTransaction { EthereumAddress = utxo.EthAddress, LockExpirationIndex = utxo.LockExpirationIndex };
                                    var c = emt.GetContract();
                                    var esh = c.ScriptHash;
                                    if (!contracts.ContainsKey(esh))
                                        contracts[esh] = c;
                                }
                                tx.Outputs = outputs.ToArray();
                                tx.Inputs = inputs.ToArray();
                                var stringToSign = tx.InputOutputHash.ToArray().ToHexString();
                                var signatureData = await this.MetaMaskService.PersonalSign(stringToSign);
                                var signer = new Nethereum.Signer.EthereumMessageSigner();
                                var ethaddress = signer.EncodeUTF8AndEcRecover(stringToSign, signatureData);
                                if (ethaddress.ToLower() == this.EthID.EthAddress.ToLower())
                                {
                                    tx.Attributes = new TransactionAttribute[] { new TransactionAttribute { Usage = TransactionAttributeUsage.EthSignature, Data = System.Text.Encoding.UTF8.GetBytes(signatureData) } };
                                    var oxKey = openWallet.GetHeldAccounts().First().GetKey();
                                    List<AvatarAccount> avatars = new List<AvatarAccount>();
                                    foreach (var c in contracts)
                                    {
                                        avatars.Add(LockAssetHelper.CreateAccount(openWallet, c.Value, oxKey));
                                    }
                                    tx = LockAssetHelper.Build(tx, avatars.ToArray());
                                    if (tx.IsNotNull())
                                    {
                                        this.Box.Notecase.Wallet.ApplyTransaction(tx);
                                        this.Box.Notecase.Relay(tx);
                                        this.EthID.SetLastTransactionIndex(Blockchain.Singleton.Height);
                                        msg = UIHelper.LocalString($"广播转售NFT交易成功  {tx.Hash}", $"Relay transfer nft map  transaction completed  {tx.Hash}");
                                        StateHasChanged();
                                    }
                                }
                            }
                        }
                    }

                }
            }
            catch
            {
                msg = UIHelper.LocalString($"内部错误", $"internal error");
            }
        }
    }
}
