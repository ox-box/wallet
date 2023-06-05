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
using Nethereum.Util;
using OX.Cryptography.ECC;
using static NBitcoin.Scripting.OutputDescriptor;
using Nethereum.Hex.HexConvertors.Extensions;

namespace OX.Wallets.Base
{
    public partial class IssueNFT : DarkDialog, INotecaseTrigger, IModuleComponent
    {
        INotecase Operater;
        NftTransaction NftCoin;
        public Module Module { get; set; }
        public IssueNFT(INotecase operater, NftTransaction nftcoin)
        {
            InitializeComponent();
            this.Operater = operater;
            this.NftCoin = nftcoin;
        }


        private void NewEvent_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString($"发行NFT", $"Issue NFT");
            this.lb_nfthash.Text = UIHelper.LocalString("NFT CID:", "NFT CID:");
            this.lb_newowner.Text = UIHelper.LocalString("接收人:", "Recipient:");
            this.lb_sn.Text = UIHelper.LocalString("编号:", "SN:");
            this.lb_holdername.Text = UIHelper.LocalString("接收人名称:", "Recipient Name:");
            this.btnOk.Text = UIHelper.LocalString("立即发行", "Issue Now");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            this.lb_nfthash_v.Text = this.NftCoin.NftCopyright.NftID.CID;
        }
        public NftTransferTransaction buildTx(out UInt160 authSh)
        {
            authSh = default;
            if (tryParse(out MixAccountType type, out byte[] data))
            {
                authSh = Contract.CreateSignatureRedeemScript(NftCoin.Author).ToScriptHash();
                if (this.Operater.IsNull() || this.Operater.Wallet.IsNull() || !this.Operater.Wallet.ContainsAndHeld(authSh)) return default;
                NftTransferTransaction tx = new NftTransferTransaction
                {
                    NFSCopyright = new NftTransferCopyright
                    {
                        CopyrightType = 0,
                        SN = this.tb_sn.Text,
                        HolderName = this.tb_holdername.Text
                    },
                    NFSHolder = new NFSHolder { MixAccountType = type, Target = data },
                    NftChangeType = NftChangeType.Issue,
                    NFSStateKey = new NFSStateKey { NFCID = NftCoin.NftCopyright.NftID, IssueBlockIndex = 0, IssueN = 0 }
                };
                return tx;
            }
            return default;
        }
        bool tryParse(out MixAccountType type, out byte[] bs)
        {
            bs = new byte[0];
            type = MixAccountType.OX;
            var s = this.tb_newowner.Text;
            if (ECPoint.TryParse(s, ECCurve.Secp256r1, out ECPoint pubkey))
            {
                bs = pubkey.ToArray();
                type = MixAccountType.OX;
                return true;
            }
            else
            {
                if (s.IsValidEthereumAddressHexFormat())
                {
                    bs = s.HexToByteArray();
                    type = MixAccountType.Ethereum;
                    return true;
                }
            }
            return false;
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


        private void btnOk_Click(object sender, EventArgs e)
        {
            var tx = buildTx(out UInt160 sh);
            if (tx.IsNotNull())
            {
                tx = Operater.Wallet.MakeTransaction(tx, sh, sh);
                if (tx.IsNotNull())
                {
                    Operater.SignAndSendTx(tx);
                    string msg = $"{UIHelper.LocalString("发行NFT交易已广播", "Relay issue NFT transaction completed")}   {tx.Hash}";
                    DarkMessageBox.ShowInformation(msg, "");
                }
            }
        }
    }
}
