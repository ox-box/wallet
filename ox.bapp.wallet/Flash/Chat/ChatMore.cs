using OX.Wallets.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OX.Wallets.Flash.Chat
{
    public partial class ChatMore : DarkButton
    {

        public ChatMore() : base()
        {
            this.Text = UIHelper.LocalString("更多...", "More...");
            this.Click += ChatMore_Click;
        }

        private void ChatMore_Click(object sender, EventArgs e)
        {
        }

        public void ResetSize(int maxwidth)
        {
            this.Width = maxwidth;
            ResumeLayout();
        }


    }
}
