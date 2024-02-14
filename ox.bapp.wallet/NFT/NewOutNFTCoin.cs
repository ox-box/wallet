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

namespace OX.Wallets.Base
{
    public partial class NewOutNFTCoin : DarkDialog, INotecaseTrigger, IModuleComponent
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
        public NewOutNFTCoin(INotecase operater)
        {
            InitializeComponent();
            this.Operater = operater;
        }


        private void NewEvent_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString($"铸造NFT", $"Coin NFT");
            this.lb_image.Text = UIHelper.LocalString("预览:", "Preview:");
            this.lb_remark.Text = UIHelper.LocalString("NFT介绍:", "NFT Mark:");
            this.lb_from.Text = UIHelper.LocalString("作者:", "Author:");
            this.lb_filename.Text = UIHelper.LocalString("NFT 文件名:", "NFT File Name:");
            this.lb_cid.Text = UIHelper.LocalString("IPFS CID:", "IPFS CID:");
            this.lb_authorname.Text = UIHelper.LocalString("作者名称:", "Author Name:");
            this.bt_preview.Text = UIHelper.LocalString("在线预览", "Preview");
            this.bt_nodepreview.Text = UIHelper.LocalString("节点预览", "Node Preview");
            this.btnOk.Text = UIHelper.LocalString("铸造", "Do Coin");
            this.lb_lock_p.Text = UIHelper.LocalString("* 10000 区块", "* 10000 blocks");
            this.lb_lock.Text = UIHelper.LocalString("首发锁定:", "Issue Lock:");
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            initAccounts();
        }
        public NftTransaction GetTransaction(out UInt160 from)
        {
            from = default;

            if (this.cbAccounts.SelectedItem.IsNotNull() && this.cbAccounts.SelectedItem is AccountDescriptor ad)
            {
                var key = ad.Account.GetKey();
                if (key.IsNotNull())
                {
                    from = ad.Account.ScriptHash;
                    NftTransaction tx = new NftTransaction(key.PublicKey)
                    {
                        ContentType = 0,
                        FirstResaleLock = (byte)this.nun_lock.Value,
                        NftCopyright = new NftCoinCopyright
                        {
                            NftID = new NftID { CID = this.tb_cid.Text, IDType = 0 },
                            NftName = this.tb_filename.Text,
                            Seal = "0",
                            AuthorName = this.tb_authorname.Text,
                            Description = this.tb_remark.Text
                        }
                    };
                    return tx;
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
            initAccounts();
        }
        public void OnRebuild()
        {
        }
        void initAccounts()
        {
            if (this.Operater.IsNotNull())
            {
                this.DoInvoke(() =>
                {
                    this.cbAccounts.Items.Clear();
                    foreach (var act in this.Operater.Wallet.GetHeldAccounts())
                    {
                        this.cbAccounts.Items.Add(new AccountDescriptor { Account = act });
                    }
                    this.cbAccounts.SelectedIndex = 0;
                });
            }
        }

        private void darkButton1_Click(object sender, EventArgs e)
        {

            var u = this.tb_filename.Text;
            var cid = this.tb_cid.Text;
            var url = $"https://ipfs.io/ipfs/{cid}/{u}";
            if (url.IsNotNullAndEmpty()) url = url.Trim();
            try
            {
                //HttpClient client = new HttpClient();
                //var bs = client.GetByteArrayAsync(url).Result;

                this.pictureBox1.Load(url);
            }
            catch
            {
                DarkMessageBox.ShowError(UIHelper.LocalString("无法预览,可能是因为IPFS网关被切断,如果你安装了本地IPFS节点,请用节点预览", "Unable to preview, possibly due to IPFS gateway being disconnected. If you have installed a local IPFS node, please use node preview"), "");
            }
        }

        private void bt_nodepreview_Click(object sender, EventArgs e)
        {
            var u = this.tb_filename.Text;
            var cid = this.tb_cid.Text;
            var url = $"ipfs://{cid}/{u}";
            OXRunTime.OpenUrl(url);
        }
    }
}
