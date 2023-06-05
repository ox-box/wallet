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


namespace OX.Wallets.Base
{
    public partial class DialogImportWatchAccount : DarkDialog
    {
        public DialogImportWatchAccount()
        {
            InitializeComponent();
            btnOk.Text = UIHelper.LocalString("确定", "OK");
            btnOk.Enabled = false;
        }
        public UInt160 Address { get { return OX.Wallets.Wallet.ToScriptHash(tbAddress.Text); } }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (tbAddress.TextLength == 0)
            {
                btnOk.Enabled = false;
                return;
            }
            try
            {
                OX.Wallets.Wallet.ToScriptHash(tbAddress.Text);
            }
            catch
            {
                btnOk.Enabled = false;
                return;
            }
            btnOk.Enabled = true;
        }
    }
}
