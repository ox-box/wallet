using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
namespace OX.Wallets.Base
{
    partial class EventList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lb_boardName = new OX.Wallets.UI.Controls.DarkLabel();
            this.bt_pre = new OX.Wallets.UI.Controls.DarkButton();
            this.bt_next = new OX.Wallets.UI.Controls.DarkButton();
            this.RoundPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.bt_pre10 = new OX.Wallets.UI.Controls.DarkButton();
            this.bt_pre100 = new OX.Wallets.UI.Controls.DarkButton();
            this.bt_next10 = new OX.Wallets.UI.Controls.DarkButton();
            this.bt_next100 = new OX.Wallets.UI.Controls.DarkButton();
            this.tb_remark = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_pageIndex = new OX.Wallets.UI.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // lb_boardName
            // 
            this.lb_boardName.AutoSize = true;
            this.lb_boardName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_boardName.Location = new System.Drawing.Point(8, 14);
            this.lb_boardName.Name = "lb_boardName";
            this.lb_boardName.Size = new System.Drawing.Size(71, 25);
            this.lb_boardName.TabIndex = 8;
            this.lb_boardName.Text = "roomid";
            // 
            // bt_pre
            // 
            this.bt_pre.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_pre.Location = new System.Drawing.Point(288, 505);
            this.bt_pre.Name = "bt_pre";
            this.bt_pre.Padding = new System.Windows.Forms.Padding(5);
            this.bt_pre.Size = new System.Drawing.Size(130, 27);
            this.bt_pre.SpecialBorderColor = null;
            this.bt_pre.SpecialFillColor = null;
            this.bt_pre.SpecialTextColor = null;
            this.bt_pre.TabIndex = 9;
            this.bt_pre.Text = "button1";
            this.bt_pre.Click += new System.EventHandler(this.bt_pre_Click);
            // 
            // bt_next
            // 
            this.bt_next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_next.Location = new System.Drawing.Point(569, 505);
            this.bt_next.Name = "bt_next";
            this.bt_next.Padding = new System.Windows.Forms.Padding(5);
            this.bt_next.Size = new System.Drawing.Size(130, 27);
            this.bt_next.SpecialBorderColor = null;
            this.bt_next.SpecialFillColor = null;
            this.bt_next.SpecialTextColor = null;
            this.bt_next.TabIndex = 9;
            this.bt_next.Text = "button1";
            this.bt_next.Click += new System.EventHandler(this.bt_next_Click);
            // 
            // RoundPanel
            // 
            this.RoundPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RoundPanel.AutoScroll = true;
            this.RoundPanel.Location = new System.Drawing.Point(0, 196);
            this.RoundPanel.Name = "RoundPanel";
            this.RoundPanel.Size = new System.Drawing.Size(1313, 289);
            this.RoundPanel.TabIndex = 0;
            // 
            // bt_pre10
            // 
            this.bt_pre10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_pre10.Location = new System.Drawing.Point(153, 505);
            this.bt_pre10.Name = "bt_pre10";
            this.bt_pre10.Padding = new System.Windows.Forms.Padding(5);
            this.bt_pre10.Size = new System.Drawing.Size(130, 27);
            this.bt_pre10.SpecialBorderColor = null;
            this.bt_pre10.SpecialFillColor = null;
            this.bt_pre10.SpecialTextColor = null;
            this.bt_pre10.TabIndex = 9;
            this.bt_pre10.Text = "button1";
            this.bt_pre10.Click += new System.EventHandler(this.bt_pre10_Click);
            // 
            // bt_pre100
            // 
            this.bt_pre100.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_pre100.Location = new System.Drawing.Point(18, 505);
            this.bt_pre100.Name = "bt_pre100";
            this.bt_pre100.Padding = new System.Windows.Forms.Padding(5);
            this.bt_pre100.Size = new System.Drawing.Size(130, 27);
            this.bt_pre100.SpecialBorderColor = null;
            this.bt_pre100.SpecialFillColor = null;
            this.bt_pre100.SpecialTextColor = null;
            this.bt_pre100.TabIndex = 9;
            this.bt_pre100.Text = "button1";
            this.bt_pre100.Click += new System.EventHandler(this.bt_pre100_Click);
            // 
            // bt_next10
            // 
            this.bt_next10.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_next10.Location = new System.Drawing.Point(704, 505);
            this.bt_next10.Name = "bt_next10";
            this.bt_next10.Padding = new System.Windows.Forms.Padding(5);
            this.bt_next10.Size = new System.Drawing.Size(130, 27);
            this.bt_next10.SpecialBorderColor = null;
            this.bt_next10.SpecialFillColor = null;
            this.bt_next10.SpecialTextColor = null;
            this.bt_next10.TabIndex = 9;
            this.bt_next10.Text = "button1";
            this.bt_next10.Click += new System.EventHandler(this.bt_next10_Click);
            // 
            // bt_next100
            // 
            this.bt_next100.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bt_next100.Location = new System.Drawing.Point(839, 505);
            this.bt_next100.Name = "bt_next100";
            this.bt_next100.Padding = new System.Windows.Forms.Padding(5);
            this.bt_next100.Size = new System.Drawing.Size(130, 27);
            this.bt_next100.SpecialBorderColor = null;
            this.bt_next100.SpecialFillColor = null;
            this.bt_next100.SpecialTextColor = null;
            this.bt_next100.TabIndex = 9;
            this.bt_next100.Text = "button1";
            this.bt_next100.Click += new System.EventHandler(this.bt_next100_Click);
            // 
            // tb_remark
            // 
            this.tb_remark.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tb_remark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_remark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_remark.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.tb_remark.Location = new System.Drawing.Point(0, 54);
            this.tb_remark.Multiline = true;
            this.tb_remark.Name = "tb_remark";
            this.tb_remark.ReadOnly = true;
            this.tb_remark.Size = new System.Drawing.Size(1313, 136);
            this.tb_remark.TabIndex = 15;
            this.tb_remark.TextChanged += new System.EventHandler(this.tb_remark_TextChanged);
            // 
            // lb_pageIndex
            // 
            this.lb_pageIndex.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_pageIndex.AutoSize = true;
            this.lb_pageIndex.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_pageIndex.Location = new System.Drawing.Point(439, 506);
            this.lb_pageIndex.Name = "lb_pageIndex";
            this.lb_pageIndex.Size = new System.Drawing.Size(71, 25);
            this.lb_pageIndex.TabIndex = 16;
            this.lb_pageIndex.Text = "roomid";
            // 
            // EventList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lb_pageIndex);
            this.Controls.Add(this.tb_remark);
            this.Controls.Add(this.bt_next100);
            this.Controls.Add(this.bt_next10);
            this.Controls.Add(this.bt_pre100);
            this.Controls.Add(this.bt_pre10);
            this.Controls.Add(this.bt_next);
            this.Controls.Add(this.bt_pre);
            this.Controls.Add(this.lb_boardName);
            this.Controls.Add(this.RoundPanel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Name = "EventList";
            this.Size = new System.Drawing.Size(1314, 543);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DarkLabel lb_boardName;
        private DarkButton bt_pre;
        private DarkButton bt_next;
        protected System.Windows.Forms.FlowLayoutPanel RoundPanel;
        private DarkButton bt_pre10;
        private DarkButton bt_pre100;
        private DarkButton bt_next10;
        private DarkButton bt_next100;
        private DarkTextBox tb_remark;
        private DarkLabel lb_pageIndex;
    }
}
