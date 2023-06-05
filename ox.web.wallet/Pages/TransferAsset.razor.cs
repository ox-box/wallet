
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

namespace OX.Web.Pages
{
    public class FormItemLayout
    {
        public ColLayoutParam LabelCol { get; set; }
        public ColLayoutParam WrapperCol { get; set; }
    }
    public partial class TransferAsset
    {
        public override string PageTitle => UIHelper.LocalString("转帐", "Transfer");
        [Parameter]
        public string assetid { get; set; }
        [Parameter]
        public string kind { get; set; }
        [Parameter]
        public string targetaddr { get; set; }
        [Parameter]
        public string amount { get; set; }
        [Parameter]
        public string height { get; set; }

        AssetState AssetState { get; set; }

        string msg;
        bool loading = false;
        bool loading2 = false;
        string activeKey = "1";
        public EthAssetBalanceState BalanceState = new EthAssetBalanceState();
        TransferViewModel model { get; set; } = new TransferViewModel();
        TransferEthViewModel modelEth { get; set; } = new TransferEthViewModel();
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
            AssetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(UInt256.Parse(this.assetid));

            if (this.EthID.IsNotNull() && this.Box.Notecase.Wallet is OpenWallet openWallet)
            {
                var balanceState = openWallet.QueryBalanceState(this.EthID);
                if (balanceState.IsNotNull())
                {
                    this.BalanceState = balanceState.TryGetBalance(AssetState.AssetId);
                }
            }
            if (this.HaveEthID)
            {
                this.model.FromEthAddress = this.EthID.EthAddress;
                this.model.FromOXAddress = this.EthID.MapAddress;


                this.modelEth.FromEthAddress = this.EthID.EthAddress;
                this.modelEth.FromOXAddress = this.EthID.MapAddress;
                this.modelEth.LockExprationIndex = 0;
                
            }
            if (kind == "1")
            {
                try
                {
                    activeKey = "1";
                    var sh = targetaddr.ToScriptHash();
                    this.model.OxAddress = targetaddr;
                    var amt = decimal.Parse(amount);
                    this.model.Amount = amt;
                }
                catch
                {

                }
            }
            else if (kind == "2")
            {
                try
                {
                    activeKey = "2";
                    this.modelEth.ToEthAddress = targetaddr;
                    var amt = decimal.Parse(amount);
                    this.modelEth.Amount = amt;
                    uint.TryParse(height, out this.modelEth.LockExprationIndex);
                }
                catch
                {

                }
            }
        }

        protected override async Task MetaMaskService_AccountChangedEvent(string arg)
        {
            await base.MetaMaskService_AccountChangedEvent(arg);
            AssetState = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(UInt256.Parse(this.assetid));
            this.BalanceState = new EthAssetBalanceState();
            if (this.EthID.IsNotNull() && this.Box.Notecase.Wallet is OpenWallet openWallet)
            {
                var balanceState = openWallet.QueryBalanceState(this.EthID);
                if (balanceState.IsNotNull())
                {
                    this.BalanceState = balanceState.TryGetBalance(AssetState.AssetId);
                }
            }
            if (this.HaveEthID)
            {
                this.model.FromEthAddress = this.EthID.EthAddress;
                this.model.FromOXAddress = this.EthID.MapAddress;


                this.modelEth.FromEthAddress = this.EthID.EthAddress;
                this.modelEth.FromOXAddress = this.EthID.MapAddress;
                this.modelEth.LockExprationIndex = 0;
                
            }
        }
        private async void HandleSubmit()
        {
            loading = true;
            try
            {
                var shTarget = this.model.OxAddress.ToScriptHash();
                if (this.Box.Notecase.Wallet is OpenWallet openWallet && this.EthID.AllowTransaction())
                {
                    var allutxos = openWallet.GetAllEthereumMapUTXOs();
                    if (allutxos.IsNotNullAndEmpty())
                    {
                        var us = allutxos.Where(m => m.Value.Output.AssetId == this.AssetState.AssetId && m.Value.EthAddress == this.EthID.EthAddress && m.Value.LockExpirationIndex < Blockchain.Singleton.Height);
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
                            var amt = Fixed8.FromDecimal(this.model.Amount);
                            if (utxos.SortSearch(amt.GetInternalValue(), excludedUtxoKeys, out EthMapUTXO[] selectedUtxos, out long remainder))
                            {
                                List<TransactionOutput> outputs = new List<TransactionOutput>();
                                outputs.Add(new TransactionOutput { AssetId = this.AssetState.AssetId, Value = amt, ScriptHash = shTarget });
                                if (remainder > 0)
                                {
                                    outputs.Add(new TransactionOutput { AssetId = this.AssetState.AssetId, Value = new Fixed8(remainder), ScriptHash = this.EthID.MapAddress });
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
                                ContractTransaction tx = new ContractTransaction
                                {
                                    Attributes = new TransactionAttribute[0],
                                    Outputs = outputs.ToArray(),
                                    Inputs = inputs.ToArray(),
                                    Witnesses = new Witness[0]
                                };
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
                                        msg = UIHelper.LocalString($"广播以太坊映射转帐交易成功  {tx.Hash}", $"Relay transfer ethereum map asset transaction completed  {tx.Hash}");
                                        loading = false;
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
            loading = false;
        }
        private async void HandleSubmit2()
        {
            loading2 = true;
            try
            {
                var toEthAddress = this.modelEth.ToEthAddress;
                if (this.Box.Notecase.Wallet is OpenWallet openWallet && toEthAddress.IsValidEthereumAddressHexFormat() && this.EthID.AllowTransaction())
                {
                    var shTarget = toEthAddress.BuildMapAddress(this.modelEth.LockExprationIndex);
                    var allutxos = openWallet.GetAllEthereumMapUTXOs();
                    if (allutxos.IsNotNullAndEmpty())
                    {
                        var us = allutxos.Where(m => m.Value.Output.AssetId == this.AssetState.AssetId && m.Value.EthAddress == this.EthID.EthAddress && m.Value.LockExpirationIndex < Blockchain.Singleton.Height);
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
                            var amt = Fixed8.FromDecimal(this.modelEth.Amount);
                            if (utxos.SortSearch(amt.GetInternalValue(), excludedUtxoKeys, out EthMapUTXO[] selectedUtxos, out long remainder))
                            {
                                List<TransactionOutput> outputs = new List<TransactionOutput>();
                                outputs.Add(new TransactionOutput { AssetId = this.AssetState.AssetId, Value = amt, ScriptHash = shTarget });
                                if (remainder > 0)
                                {
                                    outputs.Add(new TransactionOutput { AssetId = this.AssetState.AssetId, Value = new Fixed8(remainder), ScriptHash = this.EthID.MapAddress });
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
                                EthereumMapTransaction tx = new EthereumMapTransaction
                                {
                                    LockExpirationIndex = this.modelEth.LockExprationIndex,
                                    EthereumAddress = toEthAddress,
                                    EthMapContract = Blockchain.EthereumMapContractScriptHash,
                                    Attributes = new TransactionAttribute[0],
                                    Outputs = outputs.ToArray(),
                                    Inputs = inputs.ToArray(),
                                    Witnesses = new Witness[0]
                                };
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
                                        msg = UIHelper.LocalString($"广播以太坊映射转帐交易成功  {tx.Hash}", $"Relay transfer ethereum map asset transaction completed  {tx.Hash}");
                                        loading2 = false;
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
            loading2 = false;
        }
    }
}
