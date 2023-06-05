namespace OX.Wallets.Base
{
    partial class NFTTransferAvatarControl
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lb_issueNum = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_lastPrice = new OX.Wallets.UI.Controls.DarkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.Location = new System.Drawing.Point(2, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(295, 295);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.DoubleClick += new System.EventHandler(this.NFTCoinAvatarControl_DoubleClick);
            // 
            // lb_issueNum
            // 
            this.lb_issueNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_issueNum.AutoSize = true;
            this.lb_issueNum.Font = new System.Drawing.Font("Microsoft YaHei UI", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lb_issueNum.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_issueNum.Location = new System.Drawing.Point(9, 302);
            this.lb_issueNum.Name = "lb_issueNum";
            this.lb_issueNum.Size = new System.Drawing.Size(76, 19);
            this.lb_issueNum.TabIndex = 1;
            this.lb_issueNum.Text = "darkLabel1";
            // 
            // lb_lastPrice
            // 
            this.lb_lastPrice.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lb_lastPrice.AutoSize = true;
            this.lb_lastPrice.Font = new System.Drawing.Font("Microsoft YaHei UI", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lb_lastPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_lastPrice.Location = new System.Drawing.Point(8, 325);
            this.lb_lastPrice.Name = "lb_lastPrice";
            this.lb_lastPrice.Size = new System.Drawing.Size(76, 19);
            this.lb_lastPrice.TabIndex = 4;
            this.lb_lastPrice.Text = "darkLabel1";
            // 
            // NFTCoinAvatarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lb_lastPrice);
            this.Controls.Add(this.lb_issueNum);
            this.Controls.Add(this.pictureBox1);
            this.Name = "NFTCoinAvatarControl";
            this.Size = new System.Drawing.Size(300, 350);
            this.Load += new System.EventHandler(this.NFTCoinControl_Load);
            this.DoubleClick += new System.EventHandler(this.NFTCoinAvatarControl_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private UI.Controls.DarkLabel lb_issueNum;
        private UI.Controls.DarkLabel lb_lastPrice;
    }
}
