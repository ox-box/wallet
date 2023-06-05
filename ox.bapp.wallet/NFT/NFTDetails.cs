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

namespace OX.Wallets.Base
{
    public partial class NFTDetails : DarkDialog
    {
        INotecase Operator;
        public NftTransaction NftCoin;
        string Author;
        StockControl SC;
        public NFTDetails(INotecase notecase, NftTransaction nftcoin)
        {
            this.Operator = notecase;
            this.NftCoin = nftcoin;

            Author = Contract.CreateSignatureRedeemScript(NftCoin.Author).ToScriptHash().ToAddress();
            InitializeComponent();
            this.Text = nftcoin.NftCopyright.NftID.CID;
            this.lb_nfthash.Text = UIHelper.LocalString("NFT 文件名:", "NFT File Name:");
            this.lb_author.Text = UIHelper.LocalString("作者:", "Author:");
            this.tb_author.Text = this.Author;
            this.bt_copyNftHash.Text = UIHelper.LocalString("复制CID", "Copy CID");
            this.bt_copyAuthor.Text = UIHelper.LocalString("复制", "Copy");
            this.bt_showDonates.Text = UIHelper.LocalString("发行详情", "Copies Detail");
            this.bt_issue.Text = UIHelper.LocalString("发行NFT", "Issue NFT");
            this.bt_preview.Text = UIHelper.LocalString("在线预览", "Preview");
            this.bt_nodepreview.Text = UIHelper.LocalString("节点预览", "Node Preview");
            this.btnOk.Text = UIHelper.LocalString("关闭", "Close");
            uint count = 0;
            uint transferCount = 0;
            Fixed8 totalAmount = Fixed8.Zero;
            var nftState = Blockchain.Singleton.CurrentSnapshot.GetNftState(nftcoin.NftCopyright.NftID);
            if (nftState.IsNotNull())
            {
                count = nftState.TotalIssue;
                transferCount = nftState.TotalTransfer;
                totalAmount = nftState.TotalAmountTransfer;
            }

            var countStr = UIHelper.LocalString($"已发行 {count} 份,转售{transferCount}次,累计交易{totalAmount} OXC", $"{count} copies issued,Resale {transferCount} times,Total {totalAmount} OXC") + "     " + nftcoin.NftCopyright.AuthorName;
            this.lb_nftMsg.Text = countStr;
            this.tb_nfthash.Text = nftcoin.NftCopyright.NftName;
            this.tb_mark.Text = nftcoin.NftCopyright.Description;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_copyNftHash_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(this.NftCoin.NftCopyright.NftID.CID);
                string msg = this.NftCoin.NftCopyright.NftID.CID + UIHelper.LocalString("  已复制", "  copied");
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }

        private void bt_copyAuthor_Click(object sender, EventArgs e)
        {
            try
            {
                Clipboard.SetText(this.Author);
                string msg = this.Author + UIHelper.LocalString("  已复制", "  copied");
                DarkMessageBox.ShowInformation(msg, "");
            }
            catch (Exception) { }
        }

        private void bt_showDonates_Click(object sender, EventArgs e)
        {
            new NFTDonateDetails(this.Operator, this.NftCoin).ShowDialog();
        }

        private void bt_issue_Click(object sender, EventArgs e)
        {
            var sh = Contract.CreateSignatureRedeemScript(this.NftCoin.Author).ToScriptHash();
            if (this.Operator.Wallet.ContainsAndHeld(sh))
                new IssueNFT(this.Operator, NftCoin).ShowDialog();
        }

        private void NFTDetails_Load(object sender, EventArgs e)
        {
            var authSh = Contract.CreateSignatureRedeemScript(NftCoin.Author).ToScriptHash();
            var ok = this.Operator.Wallet.ContainsAndHeld(authSh);
            this.bt_issue.Visible = ok;
            var records = WalletBappProvider.Instance.GetAll<NFTSellKey, NFTSellValue>(WalletBizPersistencePrefixes.NFT_Transfer_Record, this.NftCoin.NftCopyright.NftID);
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

            var url = $"https://ipfs.io/ipfs/{NftCoin.NftCopyright.NftID.CID}/{NftCoin.NftCopyright.NftName}";
            OXRunTime.OpenUrl(url);
        }

        private void bt_nodepreview_Click(object sender, EventArgs e)
        {
            var url = $"ipfs://{NftCoin.NftCopyright.NftID.CID}/{NftCoin.NftCopyright.NftName}";
            OXRunTime.OpenUrl(url);
        }
    }
}
