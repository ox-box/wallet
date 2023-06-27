namespace OX.Wallets.Base.DNP
{
    partial class UrlPort
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
            pictureBox1 = new System.Windows.Forms.PictureBox();
            bt_copy = new UI.Controls.DarkButton();
            darkLabel1 = new UI.Controls.DarkLabel();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            pictureBox1.Location = new System.Drawing.Point(3, 3);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(521, 448);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 0;
            pictureBox1.TabStop = false;
            // 
            // bt_copy
            // 
            bt_copy.Location = new System.Drawing.Point(203, 490);
            bt_copy.Name = "bt_copy";
            bt_copy.Padding = new System.Windows.Forms.Padding(5);
            bt_copy.Size = new System.Drawing.Size(112, 34);
            bt_copy.SpecialBorderColor = null;
            bt_copy.SpecialFillColor = null;
            bt_copy.SpecialTextColor = null;
            bt_copy.TabIndex = 1;
            bt_copy.Text = "darkButton1";
            bt_copy.Click += bt_copy_Click;
            // 
            // darkLabel1
            // 
            darkLabel1.AutoSize = true;
            darkLabel1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel1.Location = new System.Drawing.Point(3, 461);
            darkLabel1.Name = "darkLabel1";
            darkLabel1.Size = new System.Drawing.Size(106, 24);
            darkLabel1.TabIndex = 2;
            darkLabel1.Text = "darkLabel1";
            // 
            // UrlPort
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Controls.Add(darkLabel1);
            Controls.Add(bt_copy);
            Controls.Add(pictureBox1);
            Margin = new System.Windows.Forms.Padding(0);
            Name = "UrlPort";
            Size = new System.Drawing.Size(524, 528);
            Load += UrlPort_Load;
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private UI.Controls.DarkButton bt_copy;
        private UI.Controls.DarkLabel darkLabel1;
    }
}
