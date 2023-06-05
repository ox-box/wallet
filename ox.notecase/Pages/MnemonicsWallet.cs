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
    internal partial class MnemonicsWallet : OX.Wallets.UI.Forms.DarkForm
    {
        public MnemonicsWallet()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("创建钱包", "Create Wallet");
            this.lb_warn.Text = UIHelper.LocalString("请务必按照顺序牢记下面的助记词和后面你自己设置的钱包密码", "Must remember the following mnemonics and your own wallet password");
            this.lb_notify.Text = UIHelper.LocalString("助记词和密码就是你的私钥,是丢失钱包文件后唯一找回资金的方式,务必先牢记和妥善保管下面的助记词,严防被拍照截屏和告知他人", "Mnemonics and passwords are your private keys. They are the only way to recover money after losing wallet files.");
            this.lb_notify2.Text = UIHelper.LocalString("", "You must remember them and keep them properly to prevent being photographed, screenshots and other people being informed");
            this.bt_next.Text = UIHelper.LocalString("记住了,下一步 >", "In mind,Next >");
            this.bt_ok.Text = UIHelper.LocalString("确定", "OK");
            this.lb_input.Text = UIHelper.LocalString("助记词:", "Mnemonic:");
            this.lb_reinput.Text = UIHelper.LocalString("依次逐个输入刚刚记住的助记词", "Type the mnemonics you just remembered one by one");
            this.bt_input.Text = UIHelper.LocalString("输入", "Input");
            this.bt_reduce.Text = UIHelper.LocalString("回退", "Fallback");
        }
        string[] Mnemonics = default;
        public string Words { get; private set; }
        List<string> inputs = new List<string>();
        private void NmenonicsWallet_Load(object sender, EventArgs e)
        {
            Words = Mnemonic.GenerateMnemonic();
            Mnemonics = Words.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            this.RoundPanel.Controls.Clear();
            for (int i = 0; i < Mnemonics.Length; i++)
            {
                DarkLabel lb = new DarkLabel() { Text = $"{Mnemonics[i]}" };
                lb.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                lb.AutoSize = true;
                this.RoundPanel.Controls.Add(lb);
            }
        }

        private void bt_next_Click(object sender, EventArgs e)
        {
            this.inputs.Clear();
            this.bt_next.Visible = false;
            //this.bt_ok.Visible = true;
            this.RoundPanel.Controls.Clear();
            this.lb_input.Visible = true;
            this.lb_reinput.Visible = true;
            this.tb_input.Visible = true;
            this.bt_input.Visible = true;
            this.bt_reduce.Visible = true;
            this.AcceptButton = this.bt_input;
            this.tb_input.Focus();
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
        bool Verify()
        {
            if (inputs.Count < Mnemonics.Length)
            {
                bt_input.Enabled = true;
                return false;
            }
            bt_input.Enabled = false;
            return inputs.SequenceEqual(Mnemonics);
        }

    }
}
