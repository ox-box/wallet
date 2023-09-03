using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using System.Windows.Forms;
using OX.Bapps;
using OX.Network.P2P.Payloads;
using OX.Ledger;
using System.Linq;
using System;

namespace OX.Wallets.Base
{
    public partial class DialogNativeAsset : DarkDialog
    {
        #region Constructor Region
        public Module Module { get; set; }
        public DialogNativeAsset()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("原生资产详情", "Native Asset Details");

            btnOk.Text = UIHelper.LocalString("关闭", "Close");
        }

        #endregion

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {

        }

        private void btnClose_Click(object sender, System.EventArgs e)
        {

        }

        private void btnYes_Click(object sender, System.EventArgs e)
        {

        }

        private void btnNo_Click(object sender, System.EventArgs e)
        {

        }

        private void btnAbort_Click(object sender, System.EventArgs e)
        {

        }

        private void btnRetry_Click(object sender, System.EventArgs e)
        {

        }

        private void btnIgnore_Click(object sender, System.EventArgs e)
        {

        }

        private void DialogNativeAsset_Load(object sender, System.EventArgs e)
        {
            var block = Blockchain.Singleton.CurrentSnapshot.Blocks.TryGet(Blockchain.Singleton.CurrentBlockHash);
            this.lb_total_gas.Text = UIHelper.LocalString($"GAS总量： {block.SystemFeeAmount}", $"Total GAS: {block.SystemFeeAmount}");
            this.lb_OXS_Name.Text = $"OXS ({Blockchain.OXS.ToString()})";
            this.lb_OXC_Name.Text = $"OXC ({Blockchain.OXC.ToString()})";
            var oxsIssued = Blockchain.Singleton.CurrentSnapshot.Assets.TryGet(Blockchain.OXS).Available;
            this.lb_OXS_Issued.Text = UIHelper.LocalString($"已发行： {oxsIssued}", $"Issued: {oxsIssued}");
            var oxcIssued = WalletBappProvider.Instance.TotalIssuedOXC;
            this.lb_OXC_Issued.Text = UIHelper.LocalString($"已发行： {oxcIssued}", $"Issued: {oxcIssued}");



            Fixed8 totalLockOXS = Fixed8.Zero;
            Fixed8 totalLockOXC = Fixed8.Zero;
            Fixed8 s1 = Fixed8.Zero, s2 = Fixed8.Zero, s3 = Fixed8.Zero, s4 = Fixed8.Zero, s5 = Fixed8.Zero, s6 = Fixed8.Zero;
            Fixed8 c1 = Fixed8.Zero, c2 = Fixed8.Zero, c3 = Fixed8.Zero, c4 = Fixed8.Zero, c5 = Fixed8.Zero, c6 = Fixed8.Zero;
            if (WalletBappProvider.Instance.AllLockAssets.IsNotNullAndEmpty())
            {
                var tsoxs = WalletBappProvider.Instance.AllLockAssets.Values.Where(m => m.Output.AssetId.Equals(Blockchain.OXS));
                if (tsoxs.IsNotNullAndEmpty())
                {
                    totalLockOXS = tsoxs.Sum(m => m.Output.Value);
                    var tsoxsTime = tsoxs.Where(m => m.Tx.IsTimeLock);
                    var tsoxsBlock = tsoxs.Where(m => !m.Tx.IsTimeLock);
                    var t1 = DateTime.Now.AddDays(182).ToTimestamp();
                    var t2 = DateTime.Now.AddDays(365).ToTimestamp();
                    var t3 = DateTime.Now.AddDays(365 + 182).ToTimestamp();
                    var ts = DateTime.Now.ToTimestamp();
                    var t1p = tsoxsTime.Where(m => m.Tx.LockExpiration - ts > t1);
                    if (t1p.IsNotNullAndEmpty())
                    {
                        s1 = t1p.Sum(m => m.Output.Value);
                    }
                    var t2p = tsoxsTime.Where(m => m.Tx.LockExpiration - ts > t2);
                    if (t2p.IsNotNullAndEmpty())
                    {
                        s2 = t2p.Sum(m => m.Output.Value);
                    }
                    var t3p = tsoxsTime.Where(m => m.Tx.LockExpiration - ts > t3);
                    if (t3p.IsNotNullAndEmpty())
                    {
                        s3 = t3p.Sum(m => m.Output.Value);
                    }
                    var h = Blockchain.Singleton.HeaderHeight;
                    var b1p = tsoxsBlock.Where(m => m.Tx.LockExpiration - h > 1000000);
                    if (b1p.IsNotNullAndEmpty())
                    {
                        s4 = b1p.Sum(m => m.Output.Value);
                    }
                    var b2p = tsoxsBlock.Where(m => m.Tx.LockExpiration - h > 2000000);
                    if (b2p.IsNotNullAndEmpty())
                    {
                        s5 = b2p.Sum(m => m.Output.Value);
                    }
                    var b3p = tsoxsBlock.Where(m => m.Tx.LockExpiration - h > 3000000);
                    if (b3p.IsNotNullAndEmpty())
                    {
                        s6 = b3p.Sum(m => m.Output.Value);
                    }
                }
                var tsoxc = WalletBappProvider.Instance.AllLockAssets.Values.Where(m => m.Output.AssetId.Equals(Blockchain.OXC));
                if (tsoxc.IsNotNullAndEmpty())
                {
                    totalLockOXC = tsoxc.Sum(m => m.Output.Value);
                    var tsoxcTime = tsoxc.Where(m => m.Tx.IsTimeLock);
                    var tsoxcBlock = tsoxc.Where(m => !m.Tx.IsTimeLock);
                    var t1 = DateTime.Now.AddDays(182).ToTimestamp();
                    var t2 = DateTime.Now.AddDays(365).ToTimestamp();
                    var t3 = DateTime.Now.AddDays(365 + 182).ToTimestamp();
                    var ts = DateTime.Now.ToTimestamp();
                    var t1p = tsoxcTime.Where(m => m.Tx.LockExpiration - ts > t1);
                    if (t1p.IsNotNullAndEmpty())
                    {
                        c1 = t1p.Sum(m => m.Output.Value);
                    }
                    var t2p = tsoxcTime.Where(m => m.Tx.LockExpiration - ts > t2);
                    if (t2p.IsNotNullAndEmpty())
                    {
                        c2 = t2p.Sum(m => m.Output.Value);
                    }
                    var t3p = tsoxcTime.Where(m => m.Tx.LockExpiration - ts > t3);
                    if (t3p.IsNotNullAndEmpty())
                    {
                        c3 = t3p.Sum(m => m.Output.Value);
                    }
                    var h = Blockchain.Singleton.HeaderHeight;
                    var b1p = tsoxcBlock.Where(m => m.Tx.LockExpiration - h > 1000000);
                    if (b1p.IsNotNullAndEmpty())
                    {
                        c4 = b1p.Sum(m => m.Output.Value);
                    }
                    var b2p = tsoxcBlock.Where(m => m.Tx.LockExpiration - h > 2000000);
                    if (b2p.IsNotNullAndEmpty())
                    {
                        c5 = b2p.Sum(m => m.Output.Value);
                    }
                    var b3p = tsoxcBlock.Where(m => m.Tx.LockExpiration - h > 3000000);
                    if (b3p.IsNotNullAndEmpty())
                    {
                        c6 = b3p.Sum(m => m.Output.Value);
                    }
                }
            }
            this.lb_OXS_Locked.Text = UIHelper.LocalString($"总锁仓： {totalLockOXS}", $"Total Lock: {totalLockOXS}");
            this.lb_OXC_Locked.Text = UIHelper.LocalString($"总锁仓： {totalLockOXC}", $"Total Lock: {totalLockOXC}");

            var x = oxcIssued - totalLockOXC;
            x -= Fixed8.One * block.SystemFeeAmount;

            this.lb_lq_oxs.Text = UIHelper.LocalString($"流通总量： {oxsIssued - totalLockOXS}", $"Total Liquid: {oxsIssued - totalLockOXS}");
            this.lb_lq_oxc.Text = UIHelper.LocalString($"流通总量： {x}", $"Total Liquid: {x}");

            this.lb_s_1.Text = UIHelper.LocalString($"剩余锁仓时间超过半年： {s1}", $"Remaining total lock later 0.5 year: {s1}");
            this.lb_s_2.Text = UIHelper.LocalString($"剩余锁仓时间超过1年： {s2}", $"Remaining total lock later 1 year: {s2}");
            this.lb_s_3.Text = UIHelper.LocalString($"剩余锁仓时间超过1.5年： {s3}", $"Remaining total lock later 1.5 year: {s3}");

            this.lb_s_4.Text = UIHelper.LocalString($"剩余锁仓区块>1000000： {s4}", $"Remaining total lock blocks than 1000000: {s4}");
            this.lb_s_5.Text = UIHelper.LocalString($"剩余锁仓区块>2000000： {s5}", $"Remaining total lock blocks than 2000000: {s5}");
            this.lb_s_6.Text = UIHelper.LocalString($"剩余锁仓区块>3000000： {s6}", $"Remaining total lock blocks than 3000000: {s6}");


            this.lb_c_1.Text = UIHelper.LocalString($"剩余锁仓时间超过半年： {c1}", $"Remaining total lock later 0.5 year: {c1}");
            this.lb_c_2.Text = UIHelper.LocalString($"剩余锁仓时间超过1年： {c2}", $"Remaining total lock later 1 year: {c2}");
            this.lb_c_3.Text = UIHelper.LocalString($"剩余锁仓时间超过1.5年： {c3}", $"Remaining total lock later 1.5 year: {c3}");

            this.lb_c_4.Text = UIHelper.LocalString($"剩余锁仓区块>1000000： {c4}", $"Remaining total lock blocks than 1000000: {c4}");
            this.lb_c_5.Text = UIHelper.LocalString($"剩余锁仓区块>2000000： {c5}", $"Remaining total lock blocks than 2000000: {c5}");
            this.lb_c_6.Text = UIHelper.LocalString($"剩余锁仓区块>3000000： {c6}", $"Remaining total lock blocks than 3000000: {c6}");

        }
    }
}
