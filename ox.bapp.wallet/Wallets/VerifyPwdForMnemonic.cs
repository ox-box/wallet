using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OX.Wallets.UI.Forms;

namespace OX.Wallets.Base
{
    public partial class VerifyPwdForMnemonic : DarkDialog
    {
        public VerifyPwdForMnemonic()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("验证钱包密码", "Verify Wallet Password");
            this.btnOk.Text = UIHelper.LocalString("确定", "OK");
            this.lb_pwd.Text = UIHelper.LocalString("密码:", "Password:");
        }
        public string GetPassword()
        {
            return this.tb_pwd.Text;
        }
    }
}
