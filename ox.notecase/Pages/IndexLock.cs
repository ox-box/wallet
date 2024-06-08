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

namespace OX.Notecase.Pages
{
    public partial class IndexLock : DarkForm
    {
        public IndexLock()
        {
            InitializeComponent();
            this.lblHeader.Text = UIHelper.LocalString("正在重建区块链索引 ...", "Rebuilding blockchain index ...");
            this.btOpenWallet.Text = UIHelper.LocalString("退出", "Exit");
            this.lb2.Text = string.Empty;
        }
        public void SetMessage(string msg)
        {
            this.lb2.Text = msg;
        }

        private void Form_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.Close();
        }

        private void btOpenWallet_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
