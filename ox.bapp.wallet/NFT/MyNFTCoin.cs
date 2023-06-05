using OX.Wallets.UI.Config;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;
using OX.Wallets.UI.Forms;
using OX.Wallets;
using OX.Bapps;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets.NEP6;
using System.Drawing.Imaging;
using OX.Wallets.UI;
using OX.Persistence;
using OX.IO;
using OX.SmartContract;
using System.Collections;
using System.Drawing;
using System.Security.Permissions;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System;

namespace OX.Wallets.Base
{
    public partial class MyNFTCoin : DarkDocument, INotecaseTrigger, IModuleComponent
    {
        public Module Module { get; set; }
        protected INotecase Operater;
        //BoardKey Key;
        //Board Board;
        protected uint CurrentPageIndex;
        #region Constructor Region

        public MyNFTCoin()
        {
            InitializeComponent();
            this.RoundPanel.SizeChanged += RoundPanel_SizeChanged;
            this.SizeChanged += GameRoom_SizeChanged;
            this.DockText = UIHelper.LocalString("我铸造的NFT", "My coin NFTs");
        }



        private void GameRoom_SizeChanged(object sender, EventArgs e)
        {
        }

        protected virtual void RoundPanel_SizeChanged(object sender, System.EventArgs e)
        {

        }

        #endregion

        #region Event Handler Region

        public override void Close()
        {
            var result = DarkMessageBox.ShowWarning(UIHelper.LocalString($"确定要退出我的NFT列表吗?", $"Are you sure you want to exit my NFTs?"), UIHelper.LocalString("退出藏品列表", "exit NFT list"), DarkDialogButton.YesNo);
            if (result == DialogResult.No)
                return;
            base.Close();
        }

        #endregion




        #region IBlockChainTrigger

        public void OnBappEvent(BappEvent be)
        {

        }

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
            if (this.Operater.IsNotNull() && this.Operater.Wallet.IsNotNull())
                foreach (var tx in block.Transactions)
                {
                    if (tx is NftTransaction nftcoin)
                    {
                        var sh = Contract.CreateSignatureRedeemScript(nftcoin.Author).ToScriptHash();
                        if (this.Operater.Wallet.ContainsAndHeld(sh))
                            LoadNftCoins();
                    }
                }
        }
        public virtual void ChangeWallet(INotecase operater)
        {
            this.Operater = operater;
            LoadNftCoins();
        }
        public void OnRebuild()
        {
        }
        #endregion
        void LoadNftCoins()
        {
            this.DoInvoke(() =>
            {
                var bizPlugin = WalletBappProvider.Instance;
                if (bizPlugin != default && this.Operater.IsNotNull() && this.Operater.Wallet.IsNotNull())
                {
                    this.RoundPanel.Controls.Clear();
                    foreach (var act in this.Operater.Wallet.GetHeldAccounts())
                    {
                        var ps = bizPlugin.GetAll<MyNFTCoinKey, NftTransaction>(WalletBizPersistencePrefixes.NFT_Coin_My, act.ScriptHash);
                        foreach (var p in ps)
                        {
                            var nftConrol = new NFTCoinAvatarControl(this.Operater, p.Value);
                            this.RoundPanel.Controls.Add(nftConrol);
                        }
                    }
                }
            });
        }
    }
}
