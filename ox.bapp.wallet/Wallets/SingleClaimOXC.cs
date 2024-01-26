using Akka.Actor;
using OX.IO.Actors;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.Wallets;
using OX.Bapps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OX.Wallets.UI.Forms;
using OX.Wallets.UI;

namespace OX.Wallets.Base
{
    public partial class SingleClaimOXC : DarkForm, INotecaseTrigger, IModuleComponent
    {
        INotecase Operater;
        WalletAccount Account;
        public Module Module { get; set; }
        public SingleClaimOXC(INotecase notecase, WalletAccount account)
        {
            this.Operater = notecase;
            this.Account = account;
            InitializeComponent();
        }

        private void CalculateBonusUnavailable(uint height)
        {
            var unspent = this.Operater.Wallet.FindUnspentCoins(Account.ScriptHash)
                .Where(p => p.Output.AssetId.Equals(Blockchain.OXS_Token.Hash))
                .Select(p => p.Reference);

            ICollection<CoinReference> references = new HashSet<CoinReference>();

            foreach (var group in unspent.GroupBy(p => p.PrevHash))
            {
                if (!Blockchain.Singleton.ContainsTransaction(group.Key))
                    continue; // not enough of the chain available
                foreach (var reference in group)
                    references.Add(reference);
            }

            using (Snapshot snapshot = Blockchain.Singleton.GetSnapshot())
            {
                textBox2.Text = snapshot.CalculateBonus(references, height).ToString();
            }
        }

        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("提取OXC", "OXC Claim");
            this.label1.Text = UIHelper.LocalString("可提取:", "Available:");
            this.label2.Text = UIHelper.LocalString("不可提取:", "Unavailable:");
            this.button1.Text = UIHelper.LocalString("全部提取", "Claim All");
            using (Snapshot snapshot = Blockchain.Singleton.GetSnapshot())
            {
                Fixed8 bonus_available = snapshot.CalculateBonus(this.GetUnclaimedCoins().Select(p => p.Reference));
                textBox1.Text = bonus_available.ToString();
                if (bonus_available == Fixed8.Zero) button1.Enabled = false;
                CalculateBonusUnavailable(snapshot.Height + 1);
            }
        }

        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            CoinReference[] claims = this.GetUnclaimedCoins().Select(p => p.Reference).ToArray();
            if (claims.Length == 0) return;
            try
            {
                using (Snapshot snapshot = Blockchain.Singleton.GetSnapshot())
                    this.Operater.SignAndSendTx(new ClaimTransaction
                    {
                        Claims = claims,
                        Attributes = new TransactionAttribute[0],
                        Inputs = new CoinReference[0],
                        Outputs = new[]
                        {
                        new TransactionOutput
                        {
                            AssetId = Blockchain.OXC_Token.Hash,
                            Value = snapshot.CalculateBonus(claims),
                            ScriptHash = this.Account.ScriptHash
                        }
                    }
                    });
            }
            catch (Exception ex)
            {
                DarkMessageBox.ShowError(ex.ToString(), "");
            }

            Close();
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
        public IEnumerable<Coin> GetUnclaimedCoins()
        {

            return from p in this.Operater.Wallet.GetCoins(new UInt160[] { Account.ScriptHash })
                   where p.Output.AssetId.Equals(Blockchain.OXS_Token.Hash)
                   where p.State.HasFlag(CoinState.Confirmed) && p.State.HasFlag(CoinState.Spent)
                   where !p.State.HasFlag(CoinState.Claimed) && !p.State.HasFlag(CoinState.Frozen)
                   select p;
        }
        public void OnRebuild()
        {
        }
    }
}
