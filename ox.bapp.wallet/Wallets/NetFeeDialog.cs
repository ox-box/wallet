﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI.Forms;

namespace OX.Wallets.Base
{
    public partial class NetFeeDialog : DarkForm
    {
        Fixed8 SystemFee;
        Fixed8 NetFee;

        public NetFeeDialog(Fixed8 SystemFee, Fixed8 NetFee)
        {
            this.SystemFee = SystemFee;
            this.NetFee = NetFee;
            InitializeComponent();
            this.ControlBox = false;
            this.CenterToParent();
            ShowCost(SystemFee + NetFee);
        }

        private void ShowCost(Fixed8 fee)
        {
            StringBuilder sb = new StringBuilder(32);

            string content = sb.AppendFormat("{0} {1} {2}", fee.ToString(), "OXC",UIHelper.LocalString("将会被消耗,确认吗？", "will be consumed, confirm?")).ToString();
            this.CostContext.Text = content;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
