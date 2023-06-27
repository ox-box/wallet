using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Nethereum.Model;
using OX.Wallets;
using OX.Wallets.Base.DNP;
using OX.Wallets.NEP6;
using OX.Wallets.UI.Forms;

namespace OX.Wallets
{
    public partial class EasyCodeAccountDialog : DarkDialog
    {
        NEP6Wallet Wallet;
        WalletAccount Account;
        bool close = false;
        public EasyCodeAccountDialog(NEP6Wallet wallet, WalletAccount account)
        {
            InitializeComponent();
            this.Wallet = wallet;
            this.Account = account;
            this.Text = UIHelper.LocalString("重置简易码", "Reset Easy Code");
            this.btnCancel.Text = UIHelper.LocalString("关闭", "Close");
            this.btnRetry.Text = UIHelper.LocalString("复制简易码", "Copy Code");
            this.btReset.Text = UIHelper.LocalString("重置", "Reset");
            this.label1.Text = UIHelper.LocalString("地址:", "Address:");
            this.label2.Text = UIHelper.LocalString("简易码:", "Easy Code:");
            this.textBox1.Text = account.Address;
            this.textBox2.Text = account.AccessCode;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.close = true;
            this.Close();
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            string newCode = Guid.NewGuid().ToString("N");
            while (this.Wallet.GetHeldAccounts().FirstOrDefault(m => m.AccessCode == newCode).IsNotNull())
            {
                newCode = Guid.NewGuid().ToString("N");
            }

            Account.AccessCode = newCode;
            this.textBox2.Text = newCode;
            this.Wallet.Save();
            reloadImage(this.Account.AccessCode);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {

        }
        void reloadImage(string code)
        {
            this.flowLayoutPanel1.Controls.Clear();
            foreach (var url in OXRunTime.WebApiUrls)
            {
                var u = $"{url}/_m/accesscode/{code}";
                this.flowLayoutPanel1.Controls.Add(new UrlPort(u));
            }
        }

        private void EasyCodeAccountDialog_Load(object sender, EventArgs e)
        {
            reloadImage(this.Account.AccessCode);
        }

        private void btnRetry_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.textBox2.Text);
        }

        private void EasyCodeAccountDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !this.close;
        }
    }
}
