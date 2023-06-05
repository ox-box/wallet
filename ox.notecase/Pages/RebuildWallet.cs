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
    internal partial class RebuildWallet : OX.Wallets.UI.Forms.DarkForm
    {
        public RebuildWallet()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("重建钱包", "Rebuild Wallet");
            this.lb_warn.Text = UIHelper.LocalString("请输入助记词来重建钱包", "Please input mnemonic to rebuild Wallet");
            this.lb_notify.Text = UIHelper.LocalString("助记词和后面输入的密码都一致才能重建原来的钱包,否则创建的将是新钱包", "Only when the mnemonics are the same as the password entered later can the original wallet be rebuilt, otherwise the new wallet will be created");
            this.bt_ok.Text = UIHelper.LocalString("确定", "OK");
            this.lb_input.Text = UIHelper.LocalString("助记词:", "Mnemonic:");
            this.bt_input.Text = UIHelper.LocalString("输入", "Input");
            this.bt_reduce.Text = UIHelper.LocalString("回退", "Fallback");
            this.AcceptButton = this.bt_input;
        }
        List<string> inputs = new List<string>();
        public string Words { get { return string.Join(' ', this.inputs); } }
        private void MnemonicsWallet_Load(object sender, EventArgs e)
        {

        }



        private void textBox_TextChanged(object sender, EventArgs e)
        {

        }

        private void bt_input_Click(object sender, EventArgs e)
        {
            var s = this.tb_input.Text;
            if (s.IsNotNullAndEmpty())
            {
                s = s.Trim();
                this.inputs.Add(s);
                DarkLabel lb = new DarkLabel() { Text = s };
                lb.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                lb.AutoSize = true;
                this.RoundPanel.Controls.Add(lb);
                if (Verify())
                {
                    this.bt_ok.Visible = true;
                    this.AcceptButton = this.bt_ok;
                }
            }
            this.tb_input.Clear();
            this.tb_input.Focus();
        }

        private void bt_reduce_Click(object sender, EventArgs e)
        {
            if (this.inputs.IsNotNullAndEmpty())
            {
                this.inputs.RemoveAt(this.inputs.Count - 1);
                this.RoundPanel.Controls.RemoveAt(this.RoundPanel.Controls.Count - 1);
                if (Verify())
                {
                    this.bt_ok.Visible = true;
                    this.AcceptButton = this.bt_ok;
                }
            }
        }
        public bool Verify()
        {
            try
            {
                return Mnemonic.Verification(this.Words);
            }
            catch
            {
                return false;
            }
        }

    }
}
