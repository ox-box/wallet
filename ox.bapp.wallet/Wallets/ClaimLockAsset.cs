using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using OX.Wallets;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Bapps;


namespace OX.Wallets.Base
{
    public partial class ClaimLockAsset : DarkDialog
    {
        INotecase Notecase;
        OpenWallet Wallet;
        UInt160 Holder;
        Timer timer;
        int flag = 0;
        Fixed8 spendValue;
        Fixed8 uspendValue;
        List<CoinReference> claims = new List<CoinReference>();
        List<LockOXS> los = new List<LockOXS>();
        Dictionary<UInt160, AvatarAccount> acts = new Dictionary<UInt160, AvatarAccount>();
        public ClaimLockAsset(INotecase notecase, UInt160 holder)
        {
            this.Notecase = notecase;
            this.Wallet = notecase.Wallet as OpenWallet;
            this.Holder = holder;
            DialogButtons = DarkDialogButton.OkCancel;
            InitializeComponent();
            this.Text = UIHelper.LocalString("提取锁定的OXC分红", "Claim locked OXC bonus");
            this.Available.Text = UIHelper.LocalString("可提取:", "Available:");
            this.Unavailable.Text = UIHelper.LocalString("不可提取:", "Unavailable:");
            this.btnCancel.Visible = true;
            this.btnCancel.Text = UIHelper.LocalString("关闭", "Close");
            this.btnCancel.Click += BtnClose_Click;
            btnOk.Text = UIHelper.LocalString("提取", "Claim");
            btnOk.Enabled = false;
            timer = new Timer();
            this.timer.Enabled = true;
            this.timer.Interval = 500;
            this.timer.Tick += Timer_Tick;

            Task.Run(() =>
            {
                WalletAccount act = this.Wallet.GetAccount(holder);
                var ks = WalletBappProvider.Instance.GetAll<OutputKey, LockOXS>(WalletBizPersistencePrefixes.TX_Once_MyLockOXS);
                if (ks.IsNotNullAndEmpty())
                {
                    List<LockOXS> unspendlos = new List<LockOXS>();
                    foreach (var pair in ks.Where(m => m.Value.Holder.Equals(holder)))
                    {
                        if (pair.Value.Flag == LockOXSFlag.Spend)
                        {
                            var lockAccount = LockAssetHelper.CreateAccount(this.Wallet, pair.Value.Tx.GetContract(), act.GetKey());//lock asset account have a some private key with master account
                            acts[lockAccount.ScriptHash] = lockAccount;
                            claims.Add(new CoinReference { PrevHash = pair.Key.TxId, PrevIndex = pair.Key.N });
                            los.Add(pair.Value);
                        }
                        else if (pair.Value.Flag == LockOXSFlag.Unspend)
                        {
                            unspendlos.Add(pair.Value);
                        }
                    }
                    spendValue = OXSHelper.CalculateBonusSpend(los);
                    uspendValue = OXSHelper.CalculateBonusUnspend(unspendlos, Blockchain.Singleton.Height + 1);

                    this.DoInvoke(() =>
                    {
                        this.Available_v.Text = spendValue.ToString();
                        this.Unavailable_v.Text = uspendValue.ToString();
                        this.timer.Enabled = false;
                        this.lb_progress.Text = "";
                        this.btnOk.Enabled = true;

                    });

                }
            });
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            flag++;
            if (flag > 3) flag = 0;
            switch (flag)
            {
                case 0:
                    this.lb_progress.Text = ">";
                    break;
                case 1:
                    this.lb_progress.Text = ">>";
                    break;
                case 2:
                    this.lb_progress.Text = ">>>";
                    break;
                case 3:
                    this.lb_progress.Text = "";
                    break;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (spendValue > Fixed8.Zero)
            {
                if (acts.IsNotNullAndEmpty() && claims.IsNotNullAndEmpty())
                {
                    var tx = new ClaimTransaction
                    {
                        Claims = claims.ToArray(),
                        Attributes = new TransactionAttribute[0],
                        Inputs = new CoinReference[0],
                        Witnesses = new Witness[0],
                        Outputs = new[]
                        {
                            new TransactionOutput{
                                AssetId = Blockchain.OXC_Token.Hash,
                                Value =spendValue,
                                ScriptHash =Holder
                            }
                        }
                    };
                    tx = LockAssetHelper.Build(tx, acts.Values.ToArray());
                    if (tx.IsNotNull())
                    {
                        this.Wallet.ApplyTransaction(tx);
                        this.Notecase.Relay(tx);
                        if (this.Notecase != default)
                        {
                            string msg = $"{UIHelper.LocalString("提取OXC交易已广播", "Relay claim OXC transaction completed")}   {tx.Hash}";
                            DarkMessageBox.ShowInformation(msg, "");
                        }
                    }
                }
            }
            else
            {
                Close();
            }
        }
    }
}
