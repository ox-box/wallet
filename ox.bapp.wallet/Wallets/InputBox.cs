﻿using System.Windows.Forms;

namespace OX.Wallets.Base
{
    internal partial class InputBox : OX.Wallets.UI.Forms.DarkForm
    {
        private InputBox(string text, string caption, string content)
        {
            InitializeComponent();
            this.Text = caption;
            groupBox1.Text = text;
            textBox1.Text = content;
        }

        public static string Show(string text, string caption, string content = "")
        {
            using (InputBox dialog = new InputBox(text, caption, content))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return null;
                return dialog.textBox1.Text;
            }
        }
    }
}
