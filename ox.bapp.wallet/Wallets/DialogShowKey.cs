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
using OX.Ledger;

namespace OX.Wallets.Base
{
    public partial class DialogShowKey : DarkDialog
    {
        public DialogShowKey(WalletAccount account)
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("查看私钥", "Show Private Key");
            this.btnOk.Text = UIHelper.LocalString("确定", "OK");
            this.lb_address.Text = UIHelper.LocalString("地址:", "Address:");
            this.lb_publicKey.Text = UIHelper.LocalString("公钥:", "Public Key:");
            this.lb_wif.Text = UIHelper.LocalString("私钥:", "Private Key:");
            KeyPair key = account.GetKey();
            tbAddress.Text = account.Address;
            tbPublickey.Text = key.PublicKey.EncodePoint(true).ToHexString();
            tbHex.Text = key.PrivateKey.ToHexString();
            tb_wif.Text = key.Export();
        }
    }
}
