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
using OX.Wallets.Eths;

namespace OX.Wallets.Base
{
    public partial class NFTCoinAvatarControl : UserControl
    {
        public NftTransaction NftCoin;
        NFCState nftState;
        INotecase Operator;
        public NFTCoinAvatarControl(INotecase notecase, NftTransaction nftcoin)
        {
            this.Operator = notecase;
            this.NftCoin = nftcoin;
            InitializeComponent();
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;

            uint count = 0;
            nftState = Blockchain.Singleton.CurrentSnapshot.GetNftState(nftcoin.NftCopyright.NftID);
            if (nftState.IsNotNull())
                count = nftState.TotalIssue;
            this.lb_issueNum.Text = UIHelper.LocalString($"已发行 {count} 份", $"{count} copies issued") + "      " + nftcoin.NftCopyright.AuthorName.Omit(4);
            this.lb_lastPrice.Text = nftcoin.NftCopyright.NftName.Omit(8);
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
                            var url = $"https://ipfs.io/ipfs/{NftCoin.NftCopyright.NftID.CID}/{NftCoin.NftCopyright.NftName}";
                            this.pictureBox1.LoadAsync(url);
                        }
                        catch { }
                //    });
                //});

            }
        }

        private void NFTCoinAvatarControl_DoubleClick(object sender, EventArgs e)
        {
            new NFTDetails(this.Operator, this.NftCoin).ShowDialog();
        }
    }
}
