using OX.Bapps;
using OX.IO;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.Text;
//using NBitcoin;

namespace OX.Wallets.Base
{
    public partial class NFTView : DarkDocument, INotecaseTrigger, IModuleComponent
    {

        //static Dictionary<uint, RoomLine> Lines = new Dictionary<uint, RoomLine>();
        public Module Module { get; set; }
        protected INotecase Operater;
        protected uint CurrentIndex;
        bool ReShowLast = false;
        bool ReshowCurrent = false;
        List<NftID> CoinHash = new List<NftID>();
        #region Constructor Region

        public NFTView()
        {
            InitializeComponent();
            this.bt_pre.Text = UIHelper.LocalString("< 前 10 个", "< Pre 10");
            this.bt_pre10.Text = UIHelper.LocalString("< 前 100 个", "< Pre 100");
            this.bt_pre100.Text = UIHelper.LocalString("< 前 1000 个", "< Pre 1000");
            this.bt_next.Text = UIHelper.LocalString("后 10 个 >", "Next 10 >");
            this.bt_next10.Text = UIHelper.LocalString("后 100 个 >", "Next 100 >");
            this.bt_next100.Text = UIHelper.LocalString("后 1000 个 >", "Next 1000 >");
            this.bt_last.Text = UIHelper.LocalString("最近", "Current");
            this.DockText = UIHelper.LocalString("所有数字藏品", "All NFT");
            this.RoundPanel.SizeChanged += RoundPanel_SizeChanged;
            this.SizeChanged += GameRoom_SizeChanged;
        }

        private void GameRoom_SizeChanged(object sender, EventArgs e)
        {
        }

        protected virtual void RoundPanel_SizeChanged(object sender, System.EventArgs e)
        {
            foreach (Control ctrl in this.RoundPanel.Controls)
            {
                if (ctrl is DarkTitle dt)
                    dt.Width = this.RoundPanel.Width - 10;
                if (ctrl is Panel pl)
                    pl.Width = this.RoundPanel.Width - 10;
            }
            int w = this.RoundPanel.Size.Width - 30;
            IEnumerator itr = this.RoundPanel.Controls.GetEnumerator();
            List<Control> cs = new List<Control>();
            while (itr.MoveNext())
            {
                cs.Add(itr.Current as Control);
            }
            this.RoundPanel.Controls.Clear();
            foreach (var c in cs)
            {
                this.RoundPanel.Controls.Add(c);
            }
        }

        #endregion

        #region Event Handler Region

        public override void Close()
        {
            base.Close();
        }

        #endregion

        public void ResetIndex(bool forceReset = false)
        {
            var ci = GetNFTCount() / 10;
            if (CurrentIndex != ci)
            {
                CurrentIndex = ci;
                ShowIndex();
            }
            else if (forceReset)
            {
                ShowIndex();
            }

        }
        uint GetNFTCount()
        {
            var ks = WalletBappProvider.Instance.GetWalletSetting(WalletSettingKind.NFTCoin_Counter);
            if (ks.IsNull()) return 0;
            return BitConverter.ToUInt32(ks.Data);
        }
        public void ShowIndex()
        {
            this.RoundPanel.Controls.Clear();
            this.CoinHash.Clear();
            foreach (var p in WalletBappProvider.Instance.GetAll<NFTCoinKey, NftTransaction>(WalletBizPersistencePrefixes.NFT_Coin, BitConverter.GetBytes(this.CurrentIndex)))
            {
                //var donates = WalletBappProvider.Instance.GetAll<NFTDonateKey, NFTDonateTransaction>(WalletBizPersistencePrefixes.NFT_Donate, p.Value.Hash);

                var nftConrol = new NFTCoinAvatarControl(this.Operater, p.Value);
                this.CoinHash.Add(p.Value.NftCopyright.NftID);
                this.RoundPanel.Controls.Add(nftConrol);
            }
            this.RoundPanel_SizeChanged(this.RoundPanel, System.EventArgs.Empty);
        }

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
            if (ReShowLast)
            {
                ReShowLast = false;
                this.DoInvoke(() =>
                {
                    ResetIndex(true);
                });
            }
            if (ReshowCurrent)
            {
                ReshowCurrent = false;
                this.DoInvoke(() =>
                {
                    ShowIndex();
                });
            }
        }
        public void OnBlock(Block block) { }

        public void AfterOnBlock(Block block)
        {
            this.DoInvoke(() =>
            {
                foreach (var tx in block.Transactions)
                {
                    if (tx is NftTransaction nftcoint)
                    {
                        ReShowLast = true;
                        break;
                    }
                    else if (tx is NftTransferTransaction nftdonate)
                    {
                        if (this.CoinHash.Contains(nftdonate.NFSStateKey.NFCID))
                        {
                            ReshowCurrent = true;
                            break;
                        }
                    }
                }
            });
        }
        public void ChangeWallet(INotecase operater)
        {
            bool needResetIndex = false;
            if (this.Operater.IsNull())
            {
                needResetIndex = true;
            }
            this.Operater = operater;
            if (needResetIndex)
                this.ResetIndex(true);
        }
        public void OnRebuild() { }
        #endregion



        private void bt_pre_Click(object sender, System.EventArgs e)
        {
            if (this.CurrentIndex > 0)
            {
                this.CurrentIndex -= 1;
                this.ShowIndex();
            }
        }

        private void bt_next_Click(object sender, System.EventArgs e)
        {
            if (GetNFTCount() > (this.CurrentIndex + 1) * 10)
            {
                this.CurrentIndex += 1;
                this.ShowIndex();
            }
        }



        private void bt_pre10_Click(object sender, EventArgs e)
        {
            if (this.CurrentIndex > 10)
            {
                this.CurrentIndex -= 10;
                this.ShowIndex();
            }
        }

        private void bt_pre100_Click(object sender, EventArgs e)
        {
            if (this.CurrentIndex > 100)
            {
                this.CurrentIndex -= 100;
                this.ShowIndex();
            }

        }

        private void bt_next10_Click(object sender, EventArgs e)
        {
            if (GetNFTCount() > (this.CurrentIndex + 1) * 10)
            {
                this.CurrentIndex += 10;
                this.ShowIndex();
            }
        }

        private void bt_next100_Click(object sender, EventArgs e)
        {
            if (GetNFTCount() > (this.CurrentIndex + 1) * 10)
            {
                this.CurrentIndex += 100;
                this.ShowIndex();
            }
        }




        private void RiddlesHashView_Load(object sender, EventArgs e)
        {

        }

        private void darkButton1_Click(object sender, EventArgs e)
        {
            ResetIndex();
        }
    }
}
