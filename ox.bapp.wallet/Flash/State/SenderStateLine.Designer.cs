using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;
namespace OX.Wallets.Flash.State
{
    partial class SenderStateLine
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
            bt_pre = new DarkButton();
            bt_next = new DarkButton();
            renderer = new System.Windows.Forms.WebBrowser();
            lb_alias = new DarkLabel();
            lb_address = new DarkLabel();
            bt_follow = new DarkButton();
            bt_first = new DarkButton();
            SuspendLayout();
            // 
            // bt_pre
            // 
            bt_pre.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_pre.Location = new System.Drawing.Point(301, 500);
            bt_pre.Name = "bt_pre";
            bt_pre.Padding = new System.Windows.Forms.Padding(5);
            bt_pre.Size = new System.Drawing.Size(288, 36);
            bt_pre.SpecialBorderColor = null;
            bt_pre.SpecialFillColor = null;
            bt_pre.SpecialTextColor = null;
            bt_pre.TabIndex = 1;
            bt_pre.Text = "darkButton2";
            bt_pre.Click += bt_pre_Click;
            // 
            // bt_next
            // 
            bt_next.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_next.Location = new System.Drawing.Point(595, 500);
            bt_next.Name = "bt_next";
            bt_next.Padding = new System.Windows.Forms.Padding(5);
            bt_next.Size = new System.Drawing.Size(288, 36);
            bt_next.SpecialBorderColor = null;
            bt_next.SpecialFillColor = null;
            bt_next.SpecialTextColor = null;
            bt_next.TabIndex = 2;
            bt_next.Text = "darkButton2";
            bt_next.Click += bt_next_Click;
            // 
            // renderer
            // 
            renderer.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            renderer.Location = new System.Drawing.Point(7, 72);
            renderer.Margin = new System.Windows.Forms.Padding(7, 8, 7, 8);
            renderer.MinimumSize = new System.Drawing.Size(27, 29);
            renderer.Name = "renderer";
            renderer.Size = new System.Drawing.Size(1176, 421);
            renderer.TabIndex = 48;
            renderer.DocumentCompleted += renderer_DocumentCompleted;
            // 
            // lb_alias
            // 
            lb_alias.AutoSize = true;
            lb_alias.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            lb_alias.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_alias.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_alias.Location = new System.Drawing.Point(18, 1);
            lb_alias.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_alias.Name = "lb_alias";
            lb_alias.Size = new System.Drawing.Size(135, 41);
            lb_alias.TabIndex = 49;
            lb_alias.Text = "Claim to:";
            // 
            // lb_address
            // 
            lb_address.AutoSize = true;
            lb_address.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_address.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            lb_address.Location = new System.Drawing.Point(20, 43);
            lb_address.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            lb_address.Name = "lb_address";
            lb_address.Size = new System.Drawing.Size(82, 25);
            lb_address.TabIndex = 50;
            lb_address.Text = "Claim to:";
            // 
            // bt_follow
            // 
            bt_follow.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            bt_follow.Location = new System.Drawing.Point(1005, 16);
            bt_follow.Name = "bt_follow";
            bt_follow.Padding = new System.Windows.Forms.Padding(5);
            bt_follow.Size = new System.Drawing.Size(112, 36);
            bt_follow.SpecialBorderColor = null;
            bt_follow.SpecialFillColor = null;
            bt_follow.SpecialTextColor = null;
            bt_follow.TabIndex = 51;
            bt_follow.Text = "darkButton2";
            bt_follow.Click += bt_follow_Click;
            // 
            // bt_first
            // 
            bt_first.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            bt_first.Location = new System.Drawing.Point(7, 500);
            bt_first.Name = "bt_first";
            bt_first.Padding = new System.Windows.Forms.Padding(5);
            bt_first.Size = new System.Drawing.Size(288, 36);
            bt_first.SpecialBorderColor = null;
            bt_first.SpecialFillColor = null;
            bt_first.SpecialTextColor = null;
            bt_first.TabIndex = 52;
            bt_first.Text = "darkButton2";
            bt_first.Click += bt_first_Click;
            // 
            // SenderStateLine
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(bt_first);
            Controls.Add(bt_follow);
            Controls.Add(lb_address);
            Controls.Add(lb_alias);
            Controls.Add(bt_next);
            Controls.Add(bt_pre);
            Controls.Add(renderer);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Name = "SenderStateLine";
            Size = new System.Drawing.Size(1190, 543);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.WebBrowser renderer;
        private DarkButton bt_pre;
        private DarkButton bt_next;
        private DarkLabel lb_alias;
        private DarkLabel lb_address;
        private DarkButton bt_follow;
        private DarkButton bt_first;
    }
}
