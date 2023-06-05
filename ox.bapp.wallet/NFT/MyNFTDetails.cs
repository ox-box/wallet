using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Network.P2P.Payloads;
using OX.Wallets.UI.Forms;
using OX.SmartContract;
using OX.Ledger;
using OX.Persistence;
using System.Security.Cryptography;
using NBitcoin;

namespace OX.Wallets.Base
{
    public partial class MyNFTDetails : DarkDialog
    {
        INotecase Operator;
        NftTransferTransaction NftTransfer;
        MyNFTTransferKey Key;
        NFCState nftState;
        StockControl SC;
        public MyNFTDetails(INotecase notecase, MyNFTTransferKey key, NftTransferTransaction nfttransfer)
        {
            this.Operator = notecase;
            this.NftTransfer = nfttransfer;
            this.Key = key;
            InitializeComponent();
            this.Text = nfttransfer.NFSStateKey.NFCID.CID;
            this.lb_nfthash.Text = UIHelper.LocalString("NFT 文件名:", "NFT File Name:");
            this.lb_author.Text = UIHelper.LocalString("作者:", "Author:");

            this.bt_copyNftHash.Text = UIHelper.LocalString("复制CID", "Copy CID");
            this.bt_showDonates.Text = UIHelper.LocalString("发行详情", "Copies Detail");
            this.bt_issue.Text = UIHelper.LocalString("转售NFT", "Transfer NFT");
            this.bt_preview.Text = UIHelper.LocalString("在线预览", "Preview");
            this.bt_nodepreview.Text = UIHelper.LocalString("节点预览", "Node Preview");
            this.btnOk.Text = UIHelper.LocalString("关闭", "Close");
            uint count = 0;
            uint transferCount = 0;
            Fixed8 totalAmount = Fixed8.Zero;
            nftState = Blockchain.Singleton.CurrentSnapshot.GetNftState(nfttransfer.NFSStateKey.NFCID);
            if (nftState.IsNotNull())
            {
                count = nftState.TotalIssue;
                transferCount = nftState.TotalTransfer;
                totalAmount = nftState.TotalAmountTransfer;
                var countStr = UIHelper.LocalString($"已发行 {count} 份,转售{transferCount}次,累计交易{totalAmount} OXC", $"{count} copies issued,Resale {transferCount} times,Total {totalAmount} OXC") + "     " + nftState.NFC.NftCopyright.AuthorName;
                this.lb_nftMsg.Text = countStr;
                this.tb_nfthash.Text = nftState.NFC.NftCopyright.NftName;
                this.tb_mark.Text = nftState.NFC.NftCopyright.Description;
                this.tb_author.Text = Contract.CreateSignatureRedeemScript(nftState.NFC.Author).ToScriptHash().ToAddress();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_copyNftHash_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(this.NftTransfer.NFSStateKey.NFCID.CID);
                string msg = this.NftTransfer.NFSStateKey.NFCID.CID + UIHelper.LocalString("  已复制", "  copied");
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }



        private void bt_showDonates_Click(object sender, EventArgs e)
        {
            if (nftState.IsNotNull())
                new NFTDonateDetails(this.Operator, nftState.NFC).ShowDialog();
        }

        private void bt_issue_Click(object sender, EventArgs e)
        {
            if (this.nftState.IsNotNull())
            {
                var sh = Contract.CreateSignatureRedeemScript(nftState.NFC.Author).ToScriptHash();
                if (this.Operator.Wallet.ContainsAndHeld(sh))
                    new SellNFT(this.Operator,this.Key, NftTransfer).ShowDialog();
            }
        }

        private void NFTDetails_Load(object sender, EventArgs e)
        {
            var records = WalletBappProvider.Instance.GetAll<NFTSellKey, NFTSellValue>(WalletBizPersistencePrefixes.NFT_Transfer_Record, this.NftTransfer.NFSStateKey.NFCID);
            if (SC.IsNull())
            {
                SC = new StockControl(this.Operator);
                SC.Dock = System.Windows.Forms.DockStyle.Fill;
                SC.Margin = new Padding(2);
                SC.ResetNullGraph();
                SC.ShowLeftScale = true;
                SC.ShowRightScale = true;
                SC.RightPixSpace = 20;
                SC.RightOrderSpace = 20;
                this.panel1.Controls.Add(SC);
            }
            SC.Init(records);
        }

        private void bt_preview_Click(object sender, EventArgs e)
        {
            if (this.nftState.IsNotNull())
            {
                var url = $"https://ipfs.io/ipfs/{nftState.NFC.NftCopyright.NftID.CID}/{nftState.NFC.NftCopyright.NftName}";
                OXRunTime.OpenUrl(url);
            }
        }

        private void bt_nodepreview_Click(object sender, EventArgs e)
        {
            if (this.nftState.IsNotNull())
            {
                var url = $"ipfs://{nftState.NFC.NftCopyright.NftID.CID}/{nftState.NFC.NftCopyright.NftName}";
                OXRunTime.OpenUrl(url);
            }
        }
    }
}
