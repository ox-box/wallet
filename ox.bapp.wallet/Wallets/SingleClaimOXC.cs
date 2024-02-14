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
using Org.BouncyCastle.Asn1.X509;
using System.Security.Claims;
using Akka.Actor.Dsl;

namespace OX.Wallets.Base
{
    public partial class SingleClaimOXC : DarkForm, INotecaseTrigger, IModuleComponent
    {
        INotecase Operater;
        WalletAccount Account;
        public Module Module { get; set; }
        List<CoinReference> claims = new List<CoinReference>();
        List<LockOXS> los = new List<LockOXS>();
        Dictionary<UInt160, AvatarAccount> acts = new Dictionary<UInt160, AvatarAccount>();
        Fixed8 LockAvailable = Fixed8.Zero;
        Fixed8 LockUnavailable = Fixed8.Zero;
        public SingleClaimOXC(INotecase notecase, WalletAccount account)
        {
            this.Operater = notecase;
            this.Account = account;
            InitializeComponent();
        }

        private Fixed8 CalculateBonusUnavailable(uint height)
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
                return snapshot.CalculateBonus(references, height);
            }
        }

        private void ClaimForm_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("提取OXC", "OXC Claim");
            this.lb_Available.Text = UIHelper.LocalString("可提取:", "Available:");
            this.lb_Unavailable.Text = UIHelper.LocalString("不可提取:", "Unavailable:");
            this.bt_claim.Text = UIHelper.LocalString("全部提取", "Claim All");

            using (Snapshot snapshot = Blockchain.Singleton.GetSnapshot())
            {
                Fixed8 bonus_available = snapshot.CalculateBonus(this.GetUnclaimedCoins().Select(p => p.Reference));
                if (bonus_available > Fixed8.Zero)
                {
                    var avatarAccount = LockAssetHelper.CreateAccount(this.Operater.Wallet, this.Account.Contract, this.Account.GetKey());//lock asset account have a some private key with master account
                    acts[avatarAccount.ScriptHash] = avatarAccount;
                }
                LockAvailable += bonus_available;


                LockUnavailable += CalculateBonusUnavailable(snapshot.Height + 1);
                var ks = WalletBappProvider.Instance.GetAll<CoinReference, LockOXS>(WalletBizPersistencePrefixes.TX_Once_MyLockOXS);
                if (ks.IsNotNullAndEmpty())
                {
                    List<LockOXS> unspendlos = new List<LockOXS>();
                    foreach (var pair in ks.Where(m => m.Value.Holder.Equals(this.Account.ScriptHash)))
                    {
                        if (pair.Value.Flag == LockOXSFlag.Spend)
                        {
                            var lockAccount = LockAssetHelper.CreateAccount(this.Operater.Wallet, pair.Value.Tx.GetContract(), this.Account.GetKey());//lock asset account have a some private key with master account
                            acts[lockAccount.ScriptHash] = lockAccount;
                            claims.Add(pair.Key);
                            los.Add(pair.Value);
                        }
                        else if (pair.Value.Flag == LockOXSFlag.Unspend)
                        {
                            unspendlos.Add(pair.Value);
                        }
                    }
                    LockAvailable += OXSHelper.CalculateBonusSpend(los);
                    LockUnavailable += OXSHelper.CalculateBonusUnspend(unspendlos, snapshot.Height + 1);
                    tb_Available.Text = LockAvailable.ToString();
                    tb_Unavailable.Text = LockUnavailable.ToString();
                    if (LockAvailable == Fixed8.Zero) bt_claim.Enabled = false;
                }
            }


        }

        private void ClaimForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (LockAvailable > Fixed8.Zero)
            {
                List<CoinReference> list = new List<CoinReference>();
                CoinReference[] nativeClaims = this.GetUnclaimedCoins().Select(p => p.Reference).ToArray();
                if (nativeClaims.IsNotNullAndEmpty())
                    list.AddRange(nativeClaims);
                if (claims.IsNotNullAndEmpty())
                    list.AddRange(claims);
                if (acts.IsNotNullAndEmpty() && list.IsNotNullAndEmpty())
                {
                    var tx = new ClaimTransaction
                    {
                        Claims = list.ToArray(),
                        Attributes = new TransactionAttribute[0],
                        Inputs = new CoinReference[0],
                        Witnesses = new Witness[0],
                        Outputs = new[]
                        {
                            new TransactionOutput{
                                AssetId = Blockchain.OXC_Token.Hash,
                                Value =LockAvailable,
                                ScriptHash =this.Account.ScriptHash
                            }
                        }
                    };
                    tx = LockAssetHelper.Build(tx, acts.Values.ToArray());
                    if (tx.IsNotNull())
                    {
                        this.Operater.Wallet.ApplyTransaction(tx);
                        this.Operater.Relay(tx);
                        if (this.Operater != default)
                        {
                            string msg = $"{UIHelper.LocalString("提取OXC交易已广播", "Relay claim OXC transaction completed")}   {tx.Hash}";
                            DarkMessageBox.ShowInformation(msg, "");
                        }
                        Close();
                    }
                }
            }
            else
            {
                Close();
            }
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
