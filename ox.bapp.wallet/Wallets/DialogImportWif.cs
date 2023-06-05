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

namespace OX.Wallets.Base
{
    public partial class DialogImportWif : DarkDialog
    {
        public DialogImportWif()
        {
            InitializeComponent();
            btnOk.Text = UIHelper.LocalString("确定", "OK");
            btnOk.Enabled = false;
        }
        public string Wif { get { return tbWif.Text; } }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            btnOk.Enabled = tbWif.TextLength > 0;
        }
    }
}
