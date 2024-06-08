using OX.Wallets.UI.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OX.Wallets.Flash.State
{
    public partial class TagControl : DarkButton
    {
        public string TagText { get; private set; }
        public TagControl(string tag) : base()
        {
            TagText = tag;
            this.Text = tag;
            this.Click += TagControl_Click;
        }

        private void TagControl_Click(object sender, EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }
    }
}
