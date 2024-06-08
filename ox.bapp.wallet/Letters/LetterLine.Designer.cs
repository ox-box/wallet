using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
namespace OX.Wallets.Letters
{
    partial class LetterLine
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
            RoundPanel = new System.Windows.Forms.FlowLayoutPanel();
            lb_key = new DarkLabel();
            bt_newLetter = new DarkButton();
            renderer = new System.Windows.Forms.WebBrowser();
            SuspendLayout();
            // 
            // RoundPanel
            // 
            RoundPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            RoundPanel.AutoScroll = true;
            RoundPanel.Location = new System.Drawing.Point(0, 507);
            RoundPanel.Name = "RoundPanel";
            RoundPanel.Size = new System.Drawing.Size(1189, 36);
            RoundPanel.TabIndex = 0;
            // 
            // lb_key
            // 
            lb_key.AutoSize = true;
            lb_key.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_key.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_key.Location = new System.Drawing.Point(21, 20);
            lb_key.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_key.Name = "lb_key";
            lb_key.Size = new System.Drawing.Size(0, 25);
            lb_key.TabIndex = 33;
            // 
            // bt_newLetter
            // 
            bt_newLetter.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_newLetter.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            bt_newLetter.Location = new System.Drawing.Point(1004, 20);
            bt_newLetter.Name = "bt_newLetter";
            bt_newLetter.Padding = new System.Windows.Forms.Padding(5);
            bt_newLetter.Size = new System.Drawing.Size(155, 34);
            bt_newLetter.SpecialBorderColor = null;
            bt_newLetter.SpecialFillColor = null;
            bt_newLetter.SpecialTextColor = null;
            bt_newLetter.TabIndex = 47;
            bt_newLetter.Text = "darkButton1";
            bt_newLetter.Click += bt_newLetter_Click;
            // 
            // renderer
            // 
            renderer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            renderer.Location = new System.Drawing.Point(7, 65);
            renderer.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            renderer.MinimumSize = new System.Drawing.Size(27, 29);
            renderer.Name = "renderer";
            renderer.Size = new System.Drawing.Size(1176, 431);
            renderer.TabIndex = 48;
            // 
            // LetterLine
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(renderer);
            Controls.Add(bt_newLetter);
            Controls.Add(lb_key);
            Controls.Add(RoundPanel);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Name = "LetterLine";
            Size = new System.Drawing.Size(1190, 543);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        protected System.Windows.Forms.FlowLayoutPanel RoundPanel;
        private DarkLabel lb_key;
        private DarkButton bt_newLetter;
        private System.Windows.Forms.WebBrowser renderer;
    }
}
