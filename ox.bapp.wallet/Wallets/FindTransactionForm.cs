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
    public partial class FindTransactionForm : DarkDialog
    {
        public FindTransactionForm()
        {
            InitializeComponent();
            this.Text = UIHelper.LocalString("查找交易", "Find Transaction");
            this.lb_txid.Text = UIHelper.LocalString("交易Id:", "Transaction Id:");
            this.btnOk.Text = UIHelper.LocalString("确定", "OK");
        }
        public string TxId { get { return this.tb_txid.Text; } }
    }
}
