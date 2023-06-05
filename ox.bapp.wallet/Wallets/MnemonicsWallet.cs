using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using Akka.Actor;
using OX.Wallets.UI.Controls;
using OX.Wallets;

namespace OX.Wallets.Base
{
    internal partial class MnemonicsWallet : OX.Wallets.UI.Forms.DarkForm
    {
        string Nmemonics;
        public MnemonicsWallet(string nmemonics)
        {
            this.Nmemonics = nmemonics;
            InitializeComponent();
            this.Text = UIHelper.LocalString("导出助记词", "Export Mnomonics");
            this.bt_next.Text = UIHelper.LocalString("复制", "Copy");
            this.bt_ok.Text = UIHelper.LocalString("关闭", "Close");
        }
        public string Words { get; private set; }
        List<string> inputs = new List<string>();
        private void NmenonicsWallet_Load(object sender, EventArgs e)
        {

            var ms = Nmemonics.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            this.RoundPanel.Controls.Clear();
            for (int i = 0; i < ms.Length; i++)
            {
                DarkLabel lb = new DarkLabel() { Text = $"{ms[i]}" };
                lb.Font = new System.Drawing.Font("Microsoft YaHei UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
                lb.AutoSize = true;
                this.RoundPanel.Controls.Add(lb);
            }
        }






        private void bt_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void bt_next_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(this.Nmemonics);
            string msg = UIHelper.LocalString("助记词已复制", "nmononics  copied");
            OX.Wallets.UI.Forms.DarkMessageBox.ShowInformation(msg, "");
        }
    }
}
