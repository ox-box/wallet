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
    internal partial class ImportPrivateKey : OX.Wallets.UI.Forms.DarkForm
    {
        public ImportPrivateKey()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("导入私钥重建钱包", "Rebuild Wallet by private key");
            this.lb_import.Text = UIHelper.LocalString("私钥:", "Private Key:");
            this.bt_ok.Text = UIHelper.LocalString("导入", "Import");
            this.AcceptButton = this.bt_ok;
        }
        public string GetKey()
        {
            return this.tb_privateKey.Text;
        }



        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void bt_ok_Click(object sender, EventArgs e)
        {

        }
    }
}
