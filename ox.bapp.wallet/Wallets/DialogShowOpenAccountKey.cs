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
using OX.Cryptography;
namespace OX.Wallets.Base
{
    public partial class DialogShowOpenAccountKey : DarkDialog
    {
        public DialogShowOpenAccountKey(OpenAccount account, string password)
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("查看私钥", "Show Private Key");
            this.btnOk.Text = UIHelper.LocalString("确定", "OK");
            this.lb_address.Text = UIHelper.LocalString("地址:", "Address:");
            this.lb_publicKey.Text = UIHelper.LocalString("公钥:", "Public Key:");
            this.Hex.Text = UIHelper.LocalString("私钥:", "Private Key:");
            var key = account.GetPrivateKey(password);
            tbAddress.Text = account.Address;
            tbPublickey.Text = account.PublicKey;
            if (account.AccountKind == 0)
            {
                tbHex.Text = Export(key);
            }
            else if (account.AccountKind == 60)
                tbHex.Text = key.ToHexString();
        }
        public string Export(byte[] privatekey)
        {
            byte[] data = new byte[34];
            data[0] = 0x80;
            Buffer.BlockCopy(privatekey, 0, data, 1, 32);
            data[33] = 0x01;
            string wif = data.Base58CheckEncode();
            Array.Clear(data, 0, data.Length);
            return wif;
        }
    }
}
