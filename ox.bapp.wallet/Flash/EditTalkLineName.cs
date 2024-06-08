using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Markdig;
using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
using OX.Cryptography;
using OX.Cryptography.ECC;
using OX.Network.P2P.Payloads;
using System.Diagnostics;
using OX.Wallets.UI.Config;

namespace OX.Wallets.Flash
{
    public partial class EditTalkLineName : DarkDialog
    {

        public EditTalkLineName(string label)
        {
            InitializeComponent();
            this.tb_linename.Text = label;
        }
        public string GetLabel()
        {
            return tb_linename.Text;
        }

        private void EditTalkLineName_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("修改线路名称", "Edit talk line name");
            this.lb_linename.Text = UIHelper.LocalString("线路名称:", "Talk Line Name:");
            this.btnOk.Text = UIHelper.LocalString("创建", "Create");
            this.btnCancel.Text = UIHelper.LocalString("关闭", "Close");
        }
    }
}
