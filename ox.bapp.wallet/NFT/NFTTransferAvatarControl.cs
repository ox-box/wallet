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
using OX.SmartContract;
using OX.Bapps;
using OX.Wallets.UI.Forms;
using System.IO;
using System.Net.Http;
using OX.Ledger;
using OX.Persistence;
using NBitcoin;
using OX.Wallets.Eths;

namespace OX.Wallets.Base
{
    public partial class NFTTransferAvatarControl : UserControl
    {
        NftTransferTransaction NftTransfer;
        MyNFTTransferKey Key;
        NFCState nftState;
        INotecase Operator;
        public NFTTransferAvatarControl(INotecase notecase, MyNFTTransferKey key, NftTransferTransaction nfttransfer)
        {
            this.Operator = notecase;
            this.NftTransfer = nfttransfer;
            this.Key = key;
            InitializeComponent();
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;


            uint count = 0;
            nftState = Blockchain.Singleton.CurrentSnapshot.GetNftState(nfttransfer.NFSStateKey.NFCID);
            if (nftState.IsNotNull())
                count = nftState.TotalIssue;
            var msg = UIHelper.LocalString($"已发行 {count} 份", $"{count} copies issued");
            if (nftState.IsNotNull())
                msg += "      " + nftState.NFC.NftCopyright.AuthorName.Omit(4);
            this.lb_issueNum.Text = msg;
            this.lb_lastPrice.Text = nftState.NFC.NftCopyright.NftName.Omit(8);
        }

        private void NFTCoinControl_Load(object sender, EventArgs e)
        {
            if (nftState.IsNotNull())
            {
                //System.Threading.Tasks.Task.Run(() =>
                //{
                //    this.DoInvoke(() =>
                //    {
                try
                {
                    var url = $"https://ipfs.io/ipfs/{nftState.NFC.NftCopyright.NftID.CID}/{nftState.NFC.NftCopyright.NftName}";
                    this.pictureBox1.LoadAsync(url);
                }
                catch { }
                //    });
                //});
            }
        }

        private void NFTCoinAvatarControl_DoubleClick(object sender, EventArgs e)
        {
            new MyNFTDetails(this.Operator, this.Key, this.NftTransfer).ShowDialog();
        }
    }
}
