using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Akka.Actor;
using OX.Wallets.Mnemonics;
using OX.Wallets.UI.Controls;
using OX.Wallets;

namespace OX.Notecase
{
    internal partial class CreateWallet : OX.Wallets.UI.Forms.DarkDialog
    {
        public CreateWallet()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("创建钱包", "Create Wallet");
            this.lb_file.Text = UIHelper.LocalString("钱包路径:", "Wallet Path:");
            this.lb_pwd.Text = UIHelper.LocalString("密码:", "Password:");
            this.lb_repwd.Text = UIHelper.LocalString("确认密码:", "Re-Password:");
            this.bt_browser.Text = UIHelper.LocalString("浏览", "Browser");
            this.lb_actNum.Text = UIHelper.LocalString("账户数:", "Account Number:");
            this.darkTextBox2.UseSystemPasswordChar = true;
            this.darkTextBox3.UseSystemPasswordChar = true;
            this.btnOk.Text = UIHelper.LocalString("确定", "OK");
        }

        public string Password
        {
            get
            {
                return this.darkTextBox2.Text;
            }
        }

        public string WalletPath
        {
            get
            {
                return darkTextBox1.Text;
            }
            set
            {
                darkTextBox1.Text = value;
            }
        }

        public int AccountNumber
        {
            get
            {
                if (rb_20.Checked) return 20;
                if (rb_100.Checked) return 100;
                return 7;
            }
        }
        private void textBox_TextChanged(object sender, EventArgs e)
        {
            if (darkTextBox1.TextLength == 0 || darkTextBox2.TextLength == 0 || darkTextBox3.TextLength == 0)
            {
                btnOk.Enabled = false;
                return;
            }
            if (darkTextBox2.Text != darkTextBox3.Text)
            {
                btnOk.Enabled = false;
                return;
            }
            btnOk.Enabled = true;
        }

        private void bt_ok_Click(object sender, EventArgs e)
        {

        }

        private void bt_browser_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                this.darkTextBox1.Text = saveFileDialog1.FileName;
            }
        }

        private void bt_ok_Click_1(object sender, EventArgs e)
        {

        }
    }
}
