
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
using Nethereum.Hex.HexConvertors.Extensions;
using System.IO;
using OX.Wallets.Flash.Chat;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Drawing;
using Microsoft.AspNetCore.Components.Web;

namespace OX.Web.Pages
{
    public partial class NewFlashState
    {
        public override string PageTitle => this.WebLocalString("更新闪态", "Update Flash State");
        TextWrapper StateText = new TextWrapper { Text = string.Empty };
        bool success = false;
        bool loading = false;
        protected override void OnWalletInit()
        {

        }

        private async void Commit()
        {
            if (this.HaveEthID)
            {
                var mapAddress = this.EthID.MapAddress;
                var accountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(mapAddress);
                if (accountState.IsNotNull() && Blockchain.Singleton.AllowFlashMessage(accountState, out uint _))
                {
                    loading = true;
                    byte[] textData = new byte[0];
                    var text = this.StateText.Text;
                    if (text.IsNotNullAndEmpty())
                    {
                        textData = System.Text.Encoding.UTF8.GetBytes(text);
                    }
                    FlashStateTag[] tags = default;
                    if (this.lstTags.IsNotNullAndEmpty())
                    {
                        tags = this.lstTags.Select(m => new FlashStateTag(m)).ToArray();
                    }
                    byte[] imageData = new byte[0];
                    if (this.files.IsNotNullAndEmpty())
                    {
                        var stream = this.files.First().OpenReadStream();
                        MemoryStream ms = new MemoryStream();
                        await stream.CopyToAsync(ms);
                        ms.Position = 0;
                        if (ImageCompressHelper.CompressImage(ms, FlashState.MaxImageDataSize, out byte[] bs))
                        {
                            imageData = bs;
                        }
                    }
                    var fs = new FlashState(mapAddress, Blockchain.Singleton.HeaderHeight, textData, imageData, tags);

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
                            this.StateText = new TextWrapper { Text = string.Empty };
                            success = true;
                        }
                    }
                    loading = false;
                }
            }
            clear();
            StateHasChanged();
            await Task.CompletedTask;
        }
        List<UploadFileItem> fileList = new List<UploadFileItem>();
        IList<IBrowserFile> files = new List<IBrowserFile>();
        byte[] imageData = new byte[0];
        private void UploadFiles(InputFileChangeEventArgs e)
        {
            if (e.File.Size <= 512000)
            {
                clear();
                files.Add(e.File);
                fileList = files.Select(file => new UploadFileItem { FileName = file.Name, Size = file.Size }).ToList();
            }
        }
        void clear()
        {
            lstTags.Clear();
            imageData = new byte[0];
            fileList.Clear();
            files.Clear();
        }
        private bool inputVisible { get; set; } = false;
        private bool _animate;
        private Dictionary<string, string> _eachTagAnimationClass = new();
        private int _i;
        string _inputValue;
        Input<string> _inputRef;
        List<string> lstTags { get; set; } = new List<string>();

        void ValueChange(ChangeEventArgs value)
        {
            lstTags.Add(value.Value.ToString());
        }

        void OnClose(string item)
        {
            lstTags.Remove(item);
        }

        async Task OnClosing(CloseEventArgs<MouseEventArgs> args, string key)
        {
            _eachTagAnimationClass[key] = $"animate-shrink";
            await Task.Delay(500);
        }

        async Task HandleInputConfirm()
        {
            if (string.IsNullOrEmpty(_inputValue))
            {
                CancelInput();
                return;
            }

            string res = lstTags.Find(s => s == _inputValue);
            string tag = _inputValue;

            if (string.IsNullOrEmpty(res))
            {
                _eachTagAnimationClass.Add(_inputValue, "animate-grow");
                lstTags.Add(_inputValue);
            }

            CancelInput();

            StateHasChanged();
            await Task.Delay(500);
            _eachTagAnimationClass[tag] = "";
        }

        void CancelInput()
        {
            this._inputValue = "";
            this.inputVisible = false;
        }

    }
}
