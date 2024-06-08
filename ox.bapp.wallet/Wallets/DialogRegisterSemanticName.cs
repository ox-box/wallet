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
using System.Text.RegularExpressions;
using OX.Persistence;

namespace OX.Wallets.Base
{
    public partial class DialogRegisterSemanticName : DarkDialog
    {
        const string pattern = @"^[a-z0-9_-]+$";
        string oldName = string.Empty;
        public DialogRegisterSemanticName(WalletAccount account, byte[] bs)
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("注册语义名", "Register Semantic Name");
            this.btnOk.Text = UIHelper.LocalString("确定", "OK");
            this.btnCancel.Text = UIHelper.LocalString("取消", "Cancel");
            this.lb_address.Text = account.Address;
            this.lb_name.Text = UIHelper.LocalString("语义名:", "Semantic Name:");
            if (bs.IsNotNullAndEmpty())
            {
                oldName = System.Text.Encoding.UTF8.GetString(bs);
                this.tb_name.Text = oldName;
                this.tb_name.ReadOnly = true;
            }
            this.btnOk.Enabled = false;
        }
        public string GetName()
        {
            return this.tb_name.Text;
        }
        private void tbPublickey_TextChanged(object sender, EventArgs e)
        {
            var s = this.tb_name.Text;
            var length = s.Length;
            if (length > 20 || !Match(s))
            {
                if (s.IsNotNullAndEmpty())
                {
                    s = s.Substring(0, s.Length - 1);
                    this.tb_name.Clear();
                    this.tb_name.AppendText(s);
                }
            }
            this.btnOk.Enabled = oldName.IsNullOrEmpty() && length >= 6 && length <= 20 && Match(s);
        }
        bool Match(string s)
        {
            return Regex.IsMatch(s, pattern);
        }
    }
}
