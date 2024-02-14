using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI;
using OX.Network.P2P.Payloads;
using OX.Bapps;
using OX.Wallets.UI.Forms;
using System.ComponentModel.Design.Serialization;
using System.Security.Claims;
using OX.Ledger;
using OX.IO;
using OX.IO.Json;
using static NBitcoin.Scripting.OutputDescriptor;

namespace OX.Wallets.Base.NFT
{
    public class NFTModule : Module
    {
        public override string ModuleName { get { return "walletnftmodule"; } }
        public override uint Index { get { return int.MaxValue - 10; } }

        public NFTBook Book { get; set; }
        protected INotecase Operater;
        //NewOnChainNFTCoin NftCoinOnChain;
        NewOutNFTCoin NftCoinOutChain;
        MyNFTCoin myNFTCoin;
        MyNFTDonate myNFTDonate;
        NFTView NFTView;
        public NFTModule(Bapp bapp) : base(bapp)
        {

        }
        public override void InitEvents() { }
        public override void InitWindows()
        {
            ToolStripMenuItem walletMenu = new ToolStripMenuItem();
            walletMenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);

            walletMenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            walletMenu.Name = "walletNFTMenu";
            walletMenu.Size = new System.Drawing.Size(39, 21);
            walletMenu.Text = UIHelper.LocalString("&NFT", "&NFT");
            //all nft
            ToolStripMenuItem allNFTmenu = new ToolStripMenuItem();
            allNFTmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            allNFTmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //allNFTmenu.Image = global::Example.Icons.NewFile_6276;
            allNFTmenu.Name = "allNFTmenu";
            allNFTmenu.ShortcutKeys = Keys.Control | Keys.A;
            allNFTmenu.Size = new System.Drawing.Size(170, 22);
            allNFTmenu.Text = UIHelper.LocalString("&所有NFT", "&All NFT");
            allNFTmenu.Click += AllNFTmenu_Click;

            ToolStripMenuItem coinnftoutchainmenu = new ToolStripMenuItem();
            coinnftoutchainmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            coinnftoutchainmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            coinnftoutchainmenu.Name = "coinnftoutchainmenu";
            coinnftoutchainmenu.ShortcutKeys = Keys.Control | Keys.T;
            coinnftoutchainmenu.Size = new System.Drawing.Size(170, 22);
            coinnftoutchainmenu.Text = UIHelper.LocalString("&铸造NFT", "&Coin NFT");
            coinnftoutchainmenu.Click += Coinnftoutchainmenu_Click;
            //coin nft
            ToolStripMenuItem mynftmenu = new ToolStripMenuItem();
            mynftmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            mynftmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            mynftmenu.Name = "mynftmenu";
            mynftmenu.ShortcutKeys = Keys.Control | Keys.M;
            mynftmenu.Size = new System.Drawing.Size(170, 22);
            mynftmenu.Text = UIHelper.LocalString("&我铸造的NFT", "&My coin NFTs");
            mynftmenu.Click += Mynftmenu_Click;

            ToolStripMenuItem mydonatenftmenu = new ToolStripMenuItem();
            mydonatenftmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            mydonatenftmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //exitmenu.Image = global::Example.Icons.NewFile_6276;
            mydonatenftmenu.Name = "mydonatenftmenu";
            mydonatenftmenu.ShortcutKeys = Keys.Control | Keys.D;
            mydonatenftmenu.Size = new System.Drawing.Size(170, 22);
            mydonatenftmenu.Text = UIHelper.LocalString("&我持有的NFT", "&My hold NFTs");
            mydonatenftmenu.Click += Mydonatenftmenu_Click;



            ToolStripMenuItem buyNftmenu = new ToolStripMenuItem();
            buyNftmenu.BackColor = System.Drawing.Color.FromArgb(60, 63, 65);
            buyNftmenu.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            //buyNftmenu.Image = global::Example.Icons.NewFile_6276;
            buyNftmenu.Name = "buyNftmenu";
            buyNftmenu.ShortcutKeys = Keys.Control | Keys.B;
            buyNftmenu.Size = new System.Drawing.Size(170, 22);
            buyNftmenu.Text = UIHelper.LocalString("&购买NFT", "&Buy NFT");
            buyNftmenu.Click += BuyNftmenu_Click;

            walletMenu.DropDownItems.AddRange(new ToolStripItem[] {
                allNFTmenu,
                coinnftoutchainmenu,
                buyNftmenu,
                mynftmenu,
                mydonatenftmenu});
            Container.TopMenus.Items.AddRange(new ToolStripItem[] {
            walletMenu});
        }

        private void BuyNftmenu_Click(object sender, EventArgs e)
        {
            new BuyNFT(Operater).ShowDialog();
        }

        private void AllNFTmenu_Click(object sender, EventArgs e)
        {
            if (NFTView == default)
            {
                NFTView = new NFTView();
                NFTView.Module = this;
                if (Operater != default && Operater.Wallet != default)
                    NFTView.ChangeWallet(Operater);
            }
            Container.DockPanel.AddContent(NFTView);
        }

        private void Mydonatenftmenu_Click(object sender, EventArgs e)
        {
            if (myNFTDonate == default)
            {
                myNFTDonate = new MyNFTDonate();
                myNFTDonate.Module = this;
                if (Operater != default && Operater.Wallet != default)
                    myNFTDonate.ChangeWallet(Operater);
                Container.ToolWindows.Add(myNFTDonate);
            }
            Container.DockPanel.AddContent(myNFTDonate);
        }

        private void ReceiveNftmenu_Click(object sender, EventArgs e)
        {
            //new ReceiveNFT(Operater).ShowDialog();
        }

        private void Coinnftoutchainmenu_Click(object sender, EventArgs e)
        {
            using (NftCoinOutChain = new NewOutNFTCoin(Operater))
            {
                if (NftCoinOutChain.ShowDialog() != DialogResult.OK) return;
                var tx = NftCoinOutChain.GetTransaction(out UInt160 from);
                if (tx.IsNotNull() && this.Operater.Wallet.IsNotNull())
                {
                    this.Operater.Wallet.MixBuildAndRelaySingleOutputTransaction(tx, from, tx2 =>
                    {
                        string msg = $"{UIHelper.LocalString("铸造NFT交易已广播", "Relay coin NFT transaction completed")}   {tx2.Hash}";
                        DarkMessageBox.ShowInformation(msg, "");
                    });
                }
                
            }
        }
       

        private void Mynftmenu_Click(object sender, EventArgs e)
        {
            if (myNFTCoin == default)
            {
                myNFTCoin = new MyNFTCoin();
                myNFTCoin.Module = this;
                if (Operater != default && Operater.Wallet != default)
                    myNFTCoin.ChangeWallet(Operater);
                Container.ToolWindows.Add(myNFTCoin);
            }
            Container.DockPanel.AddContent(myNFTCoin);
        }

        public override void OnBappEvent(BappEvent be)
        {
            //if (NftCoinOnChain != default)
            //    NftCoinOnChain.OnBappEvent(be);
            if (myNFTCoin != default)
                myNFTCoin.OnBappEvent(be);
            if (NftCoinOutChain != default)
                NftCoinOutChain.OnBappEvent(be);
            if (myNFTDonate != default)
                myNFTDonate.OnBappEvent(be);
            if (NFTView != default)
                NFTView.OnBappEvent(be);

        }



        public override void OnCrossBappMessage(CrossBappMessage message)
        {
            //if (NftCoinOnChain != default)
            //    NftCoinOnChain.OnCrossBappMessage(message);
            if (myNFTCoin != default)
                myNFTCoin.OnCrossBappMessage(message);
            if (NftCoinOutChain != default)
                NftCoinOutChain.OnCrossBappMessage(message);
            if (myNFTDonate != default)
                myNFTDonate.OnCrossBappMessage(message);
            if (NFTView != default)
                NFTView.OnCrossBappMessage(message);

        }
        public override void HeartBeat(HeartBeatContext context)
        {
            //if (NftCoinOnChain != default)
            //    NftCoinOnChain.HeartBeat(context);
            if (myNFTCoin != default)
                myNFTCoin.HeartBeat(context);
            if (NftCoinOutChain != default)
                NftCoinOutChain.HeartBeat(context);
            if (myNFTDonate != default)
                myNFTDonate.HeartBeat(context);
            if (NFTView != default)
                NFTView.HeartBeat(context);

        }
        public override void BeforeOnBlock(Block block)
        {
            //if (NftCoinOnChain != default)
            //    NftCoinOnChain.BeforeOnBlock(block);
            if (myNFTCoin != default)
                myNFTCoin.BeforeOnBlock(block);
            if (NftCoinOutChain != default)
                NftCoinOutChain.BeforeOnBlock(block);
            if (myNFTDonate != default)
                myNFTDonate.BeforeOnBlock(block);
            if (NFTView != default)
                NFTView.BeforeOnBlock(block);

        }
        public override void OnBlock(Block block)
        {
            //if (NftCoinOnChain != default)
            //    NftCoinOnChain.OnBlock(block);
            if (myNFTCoin != default)
                myNFTCoin.OnBlock(block);
            if (NftCoinOutChain != default)
                NftCoinOutChain.OnBlock(block);
            if (myNFTDonate != default)
                myNFTDonate.OnBlock(block);
            if (NFTView != default)
                NFTView.OnBlock(block);

        }
        public override void AfterOnBlock(Block block)
        {
            //if (NftCoinOnChain != default)
            //    NftCoinOnChain.AfterOnBlock(block);
            if (myNFTCoin != default)
                myNFTCoin.AfterOnBlock(block);
            if (NftCoinOutChain != default)
                NftCoinOutChain.AfterOnBlock(block);
            if (myNFTDonate != default)
                myNFTDonate.AfterOnBlock(block);
            if (NFTView != default)
                NFTView.AfterOnBlock(block);
            if (block.Index % 10 == 0 && this.Book.Check())
                this.Book.SaveWallet();
        }
        public override void ChangeWallet(INotecase operater)
        {
            Operater = operater;
            //if (NftCoinOnChain != default)
            //    NftCoinOnChain.ChangeWallet(operater);
            if (myNFTCoin != default)
                myNFTCoin.ChangeWallet(operater);
            if (NftCoinOutChain != default)
                NftCoinOutChain.ChangeWallet(operater);
            if (myNFTDonate != default)
                myNFTDonate.ChangeWallet(operater);
            if (NFTView != default)
                NFTView.ChangeWallet(operater);

        }

        public override void OnRebuild()
        {
            //if (NftCoinOnChain != default)
            //    NftCoinOnChain.OnRebuild();
            if (myNFTCoin != default)
                myNFTCoin.OnRebuild();
            if (NftCoinOutChain != default)
                NftCoinOutChain.OnRebuild();
            if (myNFTDonate != default)
                myNFTDonate.OnRebuild();
            if (NFTView != default)
                NFTView.OnRebuild();

        }
        public override void OnLoadBappModuleWalletSection(JObject bappSectionObject)
        {
            var bookjson = bappSectionObject["book"];
            this.Book = NFTBook.BuildNFTBook((JArray)bookjson,SaveData);
        }
        public void SaveData()
        {
            if (this.Operater.Wallet is OpenWallet openWallet)
            {
                this.moduleWalletSection["book"] = this.Book.ToJson();
                openWallet.Save();
            }
        }
    }
}
