using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using OX.Wallets.UI.Forms;
using OX.Wallets;
namespace OX.Notecase
{
    internal partial class SeedManage : DarkForm
    {
        public SeedManage()
        {
            InitializeComponent();
        }

        private void SeedManage_Load(object sender, EventArgs e)
        {
            this.Text = UIHelper.LocalString("种子节点", "Seed Node");
            this.button1.Text = UIHelper.LocalString("添加", "Add");
            this.button2.Text = UIHelper.LocalString("移除", "Remove");
            this.button3.Text = UIHelper.LocalString("关闭", "Close");
            if (Settings.Default.ExtSeeds != default)
            {
                foreach (var seed in Settings.Default.ExtSeeds)
                {
                    this.listBox1.Items.Add(new OX.Wallets.UI.Controls.DarkListItem(seed));
                }
            }
            else
            {
                Settings.Default.ExtSeeds = new string[] { };
                Settings.Default.Save();
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            var ids = this.listBox1.SelectedIndices;
            if (ids != default && ids.Count == 1)
            {
                var index = ids.FirstOrDefault();
                var obj = this.listBox1.Items[index];
                this.listBox1.Items.Remove(obj);
                var extSeeds = new List<string>(Settings.Default.ExtSeeds);
                string seed = obj.Text;
                if (extSeeds.Contains(seed))
                {
                    extSeeds.Remove(seed);
                    Settings.Default.ExtSeeds = extSeeds.ToArray();
                    Settings.Default.Save();
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var text = this.textBox1.Text.Trim();
            if (text.Length > 0)
            {
                var ts = text.Split(':');
                if (ts.Length == 2)
                {
                    this.listBox1.Items.Insert(0, new OX.Wallets.UI.Controls.DarkListItem(text));
                    var extSeeds = new List<string>(Settings.Default.ExtSeeds);
                    extSeeds.Add(text);
                    Settings.Default.ExtSeeds = extSeeds.ToArray();
                    Settings.Default.Save();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
