using Akka.Actor;
using OX.IO.Actors;
using OX.Ledger;
using OX.Network.P2P;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.Wallets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Net;
using System.Net.Http;
using OX.Wallets.UI.Forms;
using OX.Wallets.UI;
using OX.SmartContract;
using OX.IO;
using System.Xml;
using OX.Bapps;
using OX.Cryptography;
using System.IO;
using OX.Wallets.Base.NFT;
using OX.Cryptography.ECC;
using Nethereum.Hex.HexConvertors.Extensions;
using OX.Wallets.Flash;

namespace OX.Wallets.Base
{
    public partial class SellNFT : DarkDialog, INotecaseTrigger, IModuleComponent
    {

        INotecase Operater;
        NftTransferTransaction NftTransfer;
        MyNFTTransferKey Key;
        Fixed8 Amount;
        uint MaxIndex;
        uint MinIndex;
        public Module Module { get; set; }
        public SellNFT(INotecase operater, MyNFTTransferKey key, NftTransferTransaction nftTransfer)
        {
            InitializeComponent();
            this.Operater = operater;
            this.NftTransfer = nftTransfer;
            this.Key = key;
        }


        private void NewEvent_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString($"转售NFT", $"Sell NFT");
            this.lb_nfthash.Text = UIHelper.LocalString("NFT CID:", "NFT CID:");
            this.lb_amount.Text = UIHelper.LocalString("金额:", "Amount:");
            this.lb_maxIndex.Text = UIHelper.LocalString("最大区块高度:", "Max Block Index:");
            this.lb_minIndex.Text = UIHelper.LocalString("最小区块高度:", "Min Block Index:");
            this.lb_signature.Text = UIHelper.LocalString("签名:", "Signature:");
            this.tb_copy.Text = UIHelper.LocalString("复制签名", "copy signature");
            this.bt_build.Text = UIHelper.LocalString("生成签名", "build signature");
            this.bt_publish.Text = UIHelper.LocalString("发布", "Publish");
            this.btnOk.Text = UIHelper.LocalString("关闭", "Close");
            this.lb_nfthash_v.Text = this.NftTransfer.NFSStateKey.NFCID.CID;
        }
        public string buildSignature()
        {
            if (this.NftTransfer.NFSHolder.MixAccountType == MixAccountType.OX)
            {
                var sh = this.NftTransfer.NFSHolder.AsOXAddress();
                if (this.Operater.IsNull() || this.Operater.Wallet.IsNull() || !this.Operater.Wallet.ContainsAndHeld(sh)) return default;
                try
                {
                    var act = this.Operater.Wallet.GetAccount(sh);
                    NftTransferAuthentication auth = new NftTransferAuthentication
                    {
                        Amount = this.Amount,
                        MaxIndex = this.MaxIndex,
                        MinIndex = this.MinIndex,
                        Target = this.NftTransfer.NFSHolder,
                        PreHash = this.NftTransfer.Hash
                    };
                    MixSignatureValidator<NftTransferAuthentication> validator = new MixSignatureValidator<NftTransferAuthentication>() { Target = auth, Signature = auth.Sign(act.GetKey()) };
                    NFSStateKey nFSStateKey = new NFSStateKey
                    {
                        NFCID = this.NftTransfer.NFSStateKey.NFCID,
                        IssueBlockIndex = this.NftTransfer.NFSStateKey.IssueBlockIndex == 0 ? this.Key.Index : this.NftTransfer.NFSStateKey.IssueBlockIndex,
                        IssueN = this.NftTransfer.NFSStateKey.IssueN == 0 ? this.Key.N : this.NftTransfer.NFSStateKey.IssueN
                    };
                    NFTPending ndv = new NFTPending { Key = nFSStateKey, Validator = validator };
                    return ndv.ToArray().ToHexString();
                }
                catch
                {
                    return default;
                }
            }
            return default;
        }
        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        public void OnBappEvent(BappEvent be) { }

        public void OnCrossBappMessage(CrossBappMessage message)
        {
        }
        public void HeartBeat(HeartBeatContext context)
        {

        }
        public void BeforeOnBlock(Block block)
        {
        }
        public void OnBlock(Block block)
        {
        }
        public void AfterOnBlock(Block block)
        {
        }
        public void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
        }
        public void OnRebuild()
        {
        }




        private void tb_copy_Click(object sender, EventArgs e)
        {
            try
            {
                var signature = this.tb_signature.Text;
                Clipboard.SetText(signature);
                string msg = UIHelper.LocalString("NFT转售签名已复制", "NFT sale signature  copied");
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }

        private void bt_build_Click(object sender, EventArgs e)
        {
            this.tb_signature.Text = buildSignature();
        }

        private void tb_amount_TextChanged(object sender, EventArgs e)
        {
            Amount = Fixed8.Zero;
            if (!Fixed8.TryParse(this.tb_amount.Text, out Amount))
            {
                this.bt_build.Enabled = false;
                return;
            }
            if (Amount <= Fixed8.Zero)
            {
                this.bt_build.Enabled = false;
                return;
            }
            if (!uint.TryParse(this.tb_minIndex.Text, out MinIndex))
            {
                this.bt_build.Enabled = false;
                return;
            }
            if (MinIndex <= Blockchain.Singleton.HeaderHeight && MinIndex != 0)
            {
                this.bt_build.Enabled = false;
                return;
            }
            if (!uint.TryParse(this.tb_maxIndex.Text, out MaxIndex))
            {
                this.bt_build.Enabled = false;
                return;
            }
            if (MaxIndex < Blockchain.Singleton.HeaderHeight + 100 && MaxIndex != 0)
            {
                this.bt_build.Enabled = false;
                return;
            }
            if (MinIndex > MaxIndex)
            {
                this.bt_build.Enabled = false;
                return;
            }
            this.bt_build.Enabled = true;
        }

        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void bt_publish_Click(object sender, EventArgs e)
        {
            if (this.NftTransfer.NFSHolder.MixAccountType == MixAccountType.OX)
            {
                try
                {
                    var ndv = this.tb_signature.Text.HexToBytes().AsSerializable<NFTPending>();
                    if (ndv.IsNull()) return;
                    if (ndv.IsNull() || ndv.Validator.IsNull() || ndv.Key.IsNull() || !ndv.Validator.Verify()) return;
                    if (ndv.Validator.Target.Amount <= Fixed8.Zero || ndv.Validator.Target.MaxIndex < ndv.Validator.Target.MinIndex || (ndv.Validator.Target.MaxIndex >0&&ndv.Validator.Target.MaxIndex <= Blockchain.Singleton.Height)) return;
                    var lastNftDonate = Blockchain.Singleton.CurrentSnapshot.GetNftTransfer(ndv.Key);
                    if (lastNftDonate.IsNull()) return;
                    var nfc = Blockchain.Singleton.CurrentSnapshot.GetNftState(ndv.Key.NFCID);
                    if (nfc.IsNull()) return;
                    if (!ndv.Validator.Target.PreHash.Equals(lastNftDonate.LastNFS.Hash)) return;
                    var auth = Contract.CreateSignatureRedeemScript(ECPoint.DecodePoint(ndv.Validator.Target.Target.Target, ECCurve.Secp256r1)).ToScriptHash();
                    var account = this.Operater.Wallet.GetAccount(auth);
                    if (account.IsNotNull() && !account.WatchOnly)
                    {
                        var accountState = Blockchain.Singleton.CurrentSnapshot.Accounts.TryGet(auth);
                        if (accountState.IsNotNull() && Blockchain.Singleton.AllowFlashMessage(accountState, out uint _))
                        {
                            var fs = new FlashNFTPending(auth, Blockchain.Singleton.HeaderHeight, new NFTPending[] { ndv });
                            if (this.Operater.SignAndSendFlashMessage(fs))
                                FlashMessageProvider.Instance.AppendNFTPending(ndv);
                        }
                    }
                }
                catch
                {

                }
            }
        }
    }
}
