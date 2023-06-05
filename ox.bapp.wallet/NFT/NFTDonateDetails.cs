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
using OX.Wallets.UI.Controls;
using NBitcoin;
using OX.Ledger;

namespace OX.Wallets.Base
{
    public partial class NFTDonateDetails : DarkDialog
    {
        public INotecase Operater;
        public NftTransaction NftCoin;
        public int count;
        public NFTDonateDetails(INotecase notecase, NftTransaction nftCoin)
        {
            this.Operater = notecase;
            this.NftCoin = nftCoin;
            InitializeComponent();
            this.Text = nftCoin.NftCopyright.NftID.CID;
            this.btnOk.Text = UIHelper.LocalString("关闭", "Close");
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NFTDonateDetails_Load(object sender, EventArgs e)
        {
            var issueRecords = WalletBappProvider.Instance.GetAll<NFSStateKey, NftTransferTransaction>(WalletBizPersistencePrefixes.NFT_Issue_Record, this.NftCoin.NftCopyright.NftID);
            if (issueRecords.IsNotNullAndEmpty())
            {
                foreach (var r in issueRecords)
                {
                    if (r.Value.NFSHolder.Verify())
                    {
                        string addr = string.Empty;
                        if (r.Value.NFSHolder.MixAccountType == Network.P2P.MixAccountType.OX)
                        {
                            addr = r.Value.NFSHolder.AsOXAddress().ToAddress();
                        }
                        else
                        {
                            addr = r.Value.NFSHolder.AsEthAddress();
                        }
                        var button = new DarkButton { Text = addr };
                        button.Height = 35;
                        button.Width = 500;
                        button.Margin = new Padding { All = 5 };
                        button.Click += Button_Click;
                        button.Tag = r.Value;
                        this.flowLayoutPanel1.Controls.Add(button);
                    }
                }
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            DarkButton bt = sender as DarkButton;
            //var nftdonate = (KeyValuePair<NFTDonateKey, NFTDonateTransaction>)bt.Tag;
            //uint issueIndex = nftdonate.DonateAuthentication.Target.NFTDonateType == NFTDonateType.Issue ? DonateKey.Index : nftdonate.NFTDonateStateKey.IssueBlockIndex;
            //ushort issueN = nftdonate.DonateAuthentication.Target.NFTDonateType == NFTDonateType.Issue ? DonateKey.N : nftdonate.NFTDonateStateKey.IssueN;
            //MyNFTDonateKey kye = new MyNFTDonateKey { NewOwner = nftdonate.NFTOwner, Index = };
            //new MyNFTDetails(this.Operater, this.NftCoin, nftdonate.Key, nftdonate.Value, this.count, null, null).ShowDialog();
        }
    }
}
