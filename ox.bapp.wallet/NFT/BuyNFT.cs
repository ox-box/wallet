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
using Akka.Util;

namespace OX.Wallets.Base
{
    public partial class BuyNFT : DarkDialog, INotecaseTrigger, IModuleComponent
    {
        public class AccountDescriptor
        {
            public WalletAccount Account;
            public override string ToString()
            {
                return Account.Address;
            }
        }
        INotecase Operater;
        public Module Module { get; set; }
        public BuyNFT(INotecase operater)
        {
            InitializeComponent();
            this.Operater = operater;
        }


        private void NewEvent_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString($"购买NFT", $"Buy NFT");
            this.lb_from.Text = UIHelper.LocalString("出资账户:", "Contributor:");
            this.lb_amount.Text = UIHelper.LocalString("转售价格:", "Transfer Amount:");
            this.lb_nfthash.Text = UIHelper.LocalString("NFT CID:", "NFT CID:");
            this.lb_fullname.Text = UIHelper.LocalString("持有人全名:", "Holder Fullname");
            this.lb_sn.Text = UIHelper.LocalString("NFT编号:", "NFT SN:");
            this.lb_signature.Text = UIHelper.LocalString("签名:", "Signature:");
            this.btnOk.Text = UIHelper.LocalString("购买", "Buy");
            foreach (var act in this.Operater.Wallet.GetHeldAccounts())
            {
                this.cbAccounts.Items.Add(new AccountDescriptor { Account = act });
            }
            this.cbAccounts.SelectedIndex = 0;
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







        private void panel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.lb_lockmsg.Text = string.Empty;
            var fullname = this.tb_fullname.Text;
            if (fullname.IsNullOrEmpty())
            {
                string msg = UIHelper.LocalString("持有全名不能为空", "Holder full name cannot be empty");
                DarkMessageBox.ShowInformation(msg, "");
                return;
            }
            var sn = this.tb_sn.Text;
            if (sn.IsNullOrEmpty())
            {
                string msg = UIHelper.LocalString("NFT编号不能为空", "NFT sn cannot bee empty");
                DarkMessageBox.ShowInformation(msg, "");
                this.tb_sn.Clear();
                return;
            }
            try
            {
                var ndv = this.tb_signature.Text.HexToBytes().AsSerializable<NFTTranferData>();
                if (ndv.IsNull() || ndv.Validator.IsNull() || ndv.Key.IsNull() || !ndv.Validator.Verify())
                {
                    string msg = UIHelper.LocalString("签名验证失败", "Signature verify failed");
                    DarkMessageBox.ShowInformation(msg, "");
                    this.tb_signature.Clear();
                    this.lb_nfthash_v.Text = String.Empty;
                    return;
                }

                if (ndv.Validator.Target.Amount < Fixed8.Zero || ndv.Validator.Target.MaxIndex < ndv.Validator.Target.MinIndex)
                {
                    string msg = UIHelper.LocalString("签名内容不合格", "The signature content is invalid");
                    DarkMessageBox.ShowInformation(msg, "");
                    this.tb_signature.Clear();
                    this.lb_nfthash_v.Text = String.Empty;
                    return;
                }
                this.lb_nfthash_v.Text = ndv.Key.NFCID.CID;
                var nft = Blockchain.Singleton.Store.GetNftState(ndv.Key.NFCID);
                if (nft.IsNull())
                {
                    string msg = UIHelper.LocalString("NFT没找到", "not found NFT");
                    DarkMessageBox.ShowInformation(msg, "");
                    this.tb_signature.Clear();
                    this.lb_nfthash_v.Text = String.Empty;
                    return;
                }

                var donateState = Blockchain.Singleton.Store.GetNftTransfer(ndv.Key);
                if (donateState.IsNull())
                {
                    string msg = UIHelper.LocalString("NFT转售错误", "NFT transfer error");
                    DarkMessageBox.ShowInformation(msg, "");
                    this.tb_signature.Clear();
                    this.lb_nfthash_v.Text = String.Empty;
                    return;
                }
                if (donateState.LastNFS.Hash != ndv.Validator.Target.PreHash)
                {
                    string msg = UIHelper.LocalString("NFT转售错误", "NFT transfer error");
                    DarkMessageBox.ShowInformation(msg, "");
                    this.tb_signature.Clear();
                    this.lb_nfthash_v.Text = String.Empty;
                    return;
                }
                if (donateState.LastNFS.NftChangeType == NftChangeType.Issue && nft.NFC.FirstResaleLock > 0)
                {
                    if (Blockchain.Singleton.Height <= ndv.Key.IssueBlockIndex + nft.NFC.FirstResaleLock * 10000)
                    {
                        string msg = UIHelper.LocalString("NFT禁售期未到", "NFT  lockdown period has not yet arrived");
                        DarkMessageBox.ShowInformation(msg, "");
                        return;
                    }
                }
                if (this.cbAccounts.SelectedItem.IsNotNull() && this.cbAccounts.SelectedItem is AccountDescriptor ad)
                {
                    NftTransferTransaction tx = new NftTransferTransaction
                    {
                        NFSStateKey = ndv.Key,
                        NFSCopyright = new NftTransferCopyright { HolderName = fullname, SN = sn },
                        NftChangeType = NftChangeType.Transfer,
                        NFSHolder = new NFSHolder { MixAccountType = MixAccountType.OX, Target = ad.Account.GetKey().PublicKey.ToArray() },
                        Auth = ndv.Validator
                    };
                    UInt160 sh = default;
                    var oldHolder = ndv.Validator.Target.Target;
                    if (oldHolder.MixAccountType == MixAccountType.OX)
                    {
                        sh = oldHolder.AsOXAddress();
                    }
                    else
                    {
                        sh = oldHolder.AsEthAddress().BuildMapAddress();
                    }
                    tx.Outputs = new TransactionOutput[] { new TransactionOutput { ScriptHash = sh, AssetId = Blockchain.OXC, Value = ndv.Validator.Target.Amount } };
                    if (tx.IsNotNull())
                    {
                        tx = this.Operater.Wallet.MakeTransaction(tx, ad.Account.ScriptHash, ad.Account.ScriptHash);
                        if (tx.IsNotNull())
                        {
                            this.Operater.SignAndSendTx(tx);
                            string msg = $"{UIHelper.LocalString("购买NFT交易已广播", "Relay buy NFT transaction completed")}   {tx.Hash}";
                            DarkMessageBox.ShowInformation(msg, "");
                        }
                    }
                }
            }
            catch
            {
                string msg = UIHelper.LocalString("签名验证失败", "Signature verify failed");
                DarkMessageBox.ShowInformation(msg, "");
                this.tb_signature.Clear();
                this.lb_nfthash_v.Text = String.Empty;
            }
        }

        private void tb_signature_TextChanged(object sender, EventArgs e)
        {
            this.lb_nfthash_v.Text = string.Empty;
            var ndv = this.tb_signature.Text.HexToBytes().AsSerializable<NFTTranferData>();
            if (ndv.IsNull() || ndv.Validator.IsNull() || ndv.Key.IsNull() || !ndv.Validator.Verify())
            {
                string msg = UIHelper.LocalString("签名验证失败", "Signature verify failed");
                DarkMessageBox.ShowInformation(msg, "");
                this.tb_signature.Clear();
                this.lb_nfthash_v.Text = String.Empty;
                return;
            }
            if (ndv.Validator.Target.Amount < Fixed8.Zero || ndv.Validator.Target.MaxIndex < ndv.Validator.Target.MinIndex)
            {
                string msg = UIHelper.LocalString("签名内容不合格", "The signature content is invalid");
                DarkMessageBox.ShowInformation(msg, "");
                this.tb_signature.Clear();
                this.lb_nfthash_v.Text = String.Empty;
                return;
            }
            this.lb_nfthash_v.Text = ndv.Key.NFCID.CID;
            this.lb_amount_v.Text = $"{ndv.Validator.Target.Amount}  OXC";
        }
    }
}
