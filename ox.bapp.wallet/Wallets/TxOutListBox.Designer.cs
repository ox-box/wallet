using System.Drawing;
using OX.Wallets.UI.Controls;

namespace OX.Wallets.Base
{
    partial class TxOutListBox
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TxOutListBox));
            this.listBox1 = new OX.Wallets.UI.Controls.DarkListView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new OX.Wallets.UI.Controls.DarkButton();
            this.button2 = new OX.Wallets.UI.Controls.DarkButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            resources.ApplyResources(this.listBox1, "listBox1");
            this.listBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.listBox1.ItemHeight = 24;
            this.listBox1.Name = "listBox1";
            this.listBox1.MultiSelect = false;
            this.listBox1.SelectedIndicesChanged += new System.EventHandler(this.listBox1_SelectedIndicesChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.button2);
            this.panel1.Name = "panel1";
            // 
            // button1
            // 
            this.button1.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.SpecialBorderColor = null;
            this.button1.SpecialFillColor = null;
            this.button1.SpecialTextColor = null;
            this.button1.Text = UIHelper.LocalString("增加", "Add");
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.ForeColor = System.Drawing.Color.Black;
            resources.ApplyResources(this.button2, "button2");
            this.button2.Name = "button2";
            this.button2.SpecialBorderColor = null;
            this.button2.SpecialFillColor = null;
            this.button2.SpecialTextColor = null;
            this.button2.Text = UIHelper.LocalString("移除", "Remove");
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // TxOutListBox
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listBox1);
            this.Name = "TxOutListBox";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DarkListView listBox1;
        private System.Windows.Forms.Panel panel1;
        private DarkButton button1;
        private DarkButton button2;
    }
}
