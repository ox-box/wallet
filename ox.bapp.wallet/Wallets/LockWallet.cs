using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using OX;
using OX.Ledger;
using OX.Network.P2P;
using OX.Wallets.NEP6;
using OX.Network.P2P.Payloads;
using OX.Wallets;
using OX.Wallets.UI.Forms;
using OX.Wallets.Mnemonics;
using OX.Bapps;

namespace OX.Wallets.Base
{
    public partial class LockWallet : DarkForm
    {
        OpenWallet Wallet;
        public LockWallet(OpenWallet wallet)
        {
            Wallet = wallet;
            InitializeComponent();
            this.AcceptButton = btOpenWallet;
            this.lb2.Text = UIHelper.LocalString("解锁密码:", "Unlock Password:");
            this.btOpenWallet.Text = UIHelper.LocalString("解锁", "Unlock");
        }


        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btOpenWallet_Click(object sender, EventArgs e)
        {
            var act = Wallet.GetAccounts().FirstOrDefault();
            if (act.IsNotNull() && act is NEP6Account nepAct)
            {
                if (nepAct.VerifyPassword(this.tbPwd.Text))
                    this.Close();
                else
                {
                    this.tbPwd.Text = String.Empty;
                    DarkMessageBox.ShowError(UIHelper.LocalString("密码错误", "invalid password"), String.Empty);
                }
            }
        }
    }
}
