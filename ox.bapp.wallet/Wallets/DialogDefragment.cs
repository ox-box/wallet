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



namespace OX.Wallets.Base
{
    public partial class DialogDefragment : DarkDialog
    {

        INotecase Operater;
        Timer timer;
        int flag = 0;
        int fg2 = 0;
        public DialogDefragment()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("整理账户余额碎片", "DialogDefragment account balance");
            btnOk.Text = UIHelper.LocalString("停止", "Stop");
            timer = new Timer();
            this.timer.Enabled = true;
            this.timer.Interval = 500;
            this.timer.Tick += Timer_Tick;
        }
        public static List<List<T>> SplitRange<T>(List<T> list, int rangeLength)
        {
            var count = list.Count();
            var r = count / rangeLength;
            var y = count % rangeLength;
            List<List<T>> lists = new List<List<T>>();
            for (int i = 0; i < r; i++)
            {
                lists.Add(list.GetRange(i * rangeLength, rangeLength));
            }
            if (y > 0)
                lists.Add(list.GetRange(r * rangeLength, y));
            return lists;
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            flag++;
            fg2++;
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
            if (fg2 == 120) fg2 = 0;
            if (fg2 == 1)
            {
                var coingroupss = this.Operater.Wallet.FindMixUnspentUtxos(this.Account.ScriptHash).GroupBy(m => m.AssetId);
                //var coingroupss = this.Operater.Wallet.FindUnspentCoins(this.From).GroupBy(m => m.Output.AssetId);
                bool ok = false;
                foreach (var group in coingroupss)
                {
                    if (group.Count() > OpenWallet.MAXTRANSACTIONCOUNT || this.Operater.Wallet.GetMyAvailableLockAssetUTXONumber(this.Account.ScriptHash) > 0)
                    {
                        var rangs = SplitRange(group.ToList(), OpenWallet.MAXTRANSACTIONCOUNT).Take(OpenWallet.MAXTRANSACTIONCOUNT);
                        foreach (var rang in rangs)
                        {
                            List<CoinReference> crfs = new List<CoinReference>();
                            List<AvatarAccount> avatars = new List<AvatarAccount>();
                            ContractTransaction ct = new ContractTransaction { Outputs = new TransactionOutput[] { new TransactionOutput { AssetId = group.Key, ScriptHash = this.Account.ScriptHash, Value = rang.Sum(m => m.Amount) } }, Attributes = new TransactionAttribute[0], Witnesses = new Witness[0] };
                            foreach (var utxo in rang)
                            {
                                if (utxo.IsLockCoin)
                                {
                                    avatars.Add(LockAssetHelper.CreateAccount(this.Operater.Wallet, utxo.LockCoin.Value.Tx.GetContract(), this.Account.GetKey()));
                                    crfs.Add(utxo.LockCoin.Key);
                                }
                                else
                                {
                                    avatars.Add(LockAssetHelper.CreateAccount(this.Operater.Wallet, this.Account.Contract, this.Account.GetKey()));
                                    crfs.Add(utxo.UnlockCoin.Reference);
                                }
                            }
                            ct.Inputs = crfs.ToArray();
                            var tx = LockAssetHelper.Build(ct, avatars.ToArray());
                            if (tx.IsNotNull())
                            {
                                this.Operater.Wallet.ApplyTransaction(tx);
                                this.Operater.Relay(tx);
                            }
                        }
                        ok = true;
                    }
                }
                if (!ok)
                {
                    this.timer.Enabled = false;
                    this.Close();
                }
            }
        }

        public WalletAccount Account;
        public DialogDefragment(INotecase operater, WalletAccount account) : this()
        {
            this.Operater = operater;
            this.Account = account;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.timer.Enabled = false;
            this.Close();
        }
    }
}
