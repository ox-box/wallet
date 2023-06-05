using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets;
using OX.Wallets.NEP6;
using OX.Wallets.UI.Forms;

namespace OX.Wallets
{
    public partial class NoteAccountDialog : DarkDialog
    {
        WalletAccount Account;
        public NoteAccountDialog(WalletAccount account)
        {
            InitializeComponent();
            this.Account = account;
            btnOk.Text = UIHelper.LocalString("确定", "OK");
            this.label1.Text = UIHelper.LocalString("地址:", "Address:");
            this.label2.Text = UIHelper.LocalString("备注:", "Label:");
            this.textBox1.Text = account.Address;
            this.textBox2.Text = account.Label;
        }
        public string Label { get { return this.textBox2.Text; } }
    }
}
