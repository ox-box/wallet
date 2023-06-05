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
    public partial class NewPartnerDialog : DarkDialog
    {
        public NewPartnerDialog()
        {
            InitializeComponent();
            btnOk.Text = UIHelper.LocalString("确定", "OK");
        }


       
        public NEP6Partner GetPartner()
        {
            string address = this.textBox1.Text;
            string name = this.textBox2.Text;
            string mobile = this.textBox3.Text;
            string remark = this.textBox4.Text;
            if (string.IsNullOrWhiteSpace(address) || string.IsNullOrWhiteSpace(name)) return default;
            try
            {
                OX.Wallets.Wallet.ToScriptHash(address);
            }
            catch
            {
                return default;
            }
            return new NEP6Partner(address, name, mobile, remark);
        }
    }
}
