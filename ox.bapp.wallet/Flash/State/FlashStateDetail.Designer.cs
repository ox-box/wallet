namespace OX.Wallets.Flash.State
{
    partial class FlashStateDetail
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            renderer = new System.Windows.Forms.WebBrowser();
            bt_close = new UI.Controls.DarkButton();
            bt_ok = new UI.Controls.DarkButton();
            SuspendLayout();
            // 
            // renderer
            // 
            renderer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            renderer.Location = new System.Drawing.Point(16, 11);
            renderer.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            renderer.MinimumSize = new System.Drawing.Size(27, 29);
            renderer.Name = "renderer";
            renderer.Size = new System.Drawing.Size(1306, 1107);
            renderer.TabIndex = 4;
            renderer.DocumentCompleted += renderer_DocumentCompleted;
            // 
            // bt_close
            // 
            bt_close.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            bt_close.Location = new System.Drawing.Point(1210, 1147);
            bt_close.Name = "bt_close";
            bt_close.Padding = new System.Windows.Forms.Padding(5);
            bt_close.Size = new System.Drawing.Size(112, 34);
            bt_close.SpecialBorderColor = null;
            bt_close.SpecialFillColor = null;
            bt_close.SpecialTextColor = null;
            bt_close.TabIndex = 5;
            bt_close.Text = "darkButton1";
            bt_close.Click += bt_close_Click;
            // 
            // bt_ok
            // 
            bt_ok.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            bt_ok.Location = new System.Drawing.Point(1062, 1147);
            bt_ok.Name = "bt_ok";
            bt_ok.Padding = new System.Windows.Forms.Padding(5);
            bt_ok.Size = new System.Drawing.Size(112, 34);
            bt_ok.SpecialBorderColor = null;
            bt_ok.SpecialFillColor = null;
            bt_ok.SpecialTextColor = null;
            bt_ok.TabIndex = 6;
            bt_ok.Text = "darkButton1";
            bt_ok.Click += btnOk_Click;
            // 
            // FlashStateDetail
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1338, 1204);
            Controls.Add(bt_ok);
            Controls.Add(bt_close);
            Controls.Add(renderer);
            Name = "FlashStateDetail";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "NewLetter";
            Load += NewLetter_Load;
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.WebBrowser renderer;
        private UI.Controls.DarkButton bt_close;
        private UI.Controls.DarkButton bt_ok;
    }
}