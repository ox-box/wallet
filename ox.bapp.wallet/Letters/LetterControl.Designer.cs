namespace OX.Wallets.Letters
{
    partial class LetterControl
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
            dt_title = new UI.Controls.DarkTitle();
            renderer = new System.Windows.Forms.WebBrowser();
            SuspendLayout();
            // 
            // dt_title
            // 
            dt_title.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            dt_title.AutoSize = true;
            dt_title.Location = new System.Drawing.Point(17, 11);
            dt_title.Name = "dt_title";
            dt_title.Size = new System.Drawing.Size(97, 24);
            dt_title.TabIndex = 16;
            dt_title.Text = "darkTitle1";
            // 
            // renderer
            // 
            renderer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            renderer.Location = new System.Drawing.Point(7, 47);
            renderer.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            renderer.MinimumSize = new System.Drawing.Size(27, 29);
            renderer.Name = "renderer";
            renderer.Size = new System.Drawing.Size(1207, 291);
            renderer.TabIndex = 17;
            // 
            // LetterControl
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            Controls.Add(renderer);
            Controls.Add(dt_title);
            Name = "LetterControl";
            Size = new System.Drawing.Size(1221, 346);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private UI.Controls.DarkTitle dt_title;
        private System.Windows.Forms.WebBrowser renderer;
    }
}
