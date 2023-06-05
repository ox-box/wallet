namespace OX.Wallets.Base
{
    partial class SignatureDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SignatureDialog));
            this.panel = new System.Windows.Forms.Panel();
            this.bt_signature = new OX.Wallets.UI.Controls.DarkButton();
            this.tb_output = new OX.Wallets.UI.Controls.DarkTextBox();
            this.rb_hex = new OX.Wallets.UI.Controls.DarkRadioButton();
            this.rb_text = new OX.Wallets.UI.Controls.DarkRadioButton();
            this.lb_output = new OX.Wallets.UI.Controls.DarkLabel();
            this.cbAccounts = new OX.Wallets.UI.Controls.DarkComboBox();
            this.lb_from = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_input = new OX.Wallets.UI.Controls.DarkLabel();
            this.tb_input = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_type = new OX.Wallets.UI.Controls.DarkLabel();
            this.panel.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnClose
            // 
            resources.ApplyResources(this.btnClose, "btnClose");
            // 
            // btnYes
            // 
            resources.ApplyResources(this.btnYes, "btnYes");
            // 
            // btnNo
            // 
            resources.ApplyResources(this.btnNo, "btnNo");
            // 
            // btnRetry
            // 
            resources.ApplyResources(this.btnRetry, "btnRetry");
            // 
            // btnIgnore
            // 
            resources.ApplyResources(this.btnIgnore, "btnIgnore");
            // 
            // panel
            // 
            this.panel.Controls.Add(this.bt_signature);
            this.panel.Controls.Add(this.tb_output);
            this.panel.Controls.Add(this.rb_hex);
            this.panel.Controls.Add(this.rb_text);
            this.panel.Controls.Add(this.lb_output);
            this.panel.Controls.Add(this.cbAccounts);
            this.panel.Controls.Add(this.lb_from);
            this.panel.Controls.Add(this.lb_input);
            this.panel.Controls.Add(this.tb_input);
            this.panel.Controls.Add(this.lb_type);
            resources.ApplyResources(this.panel, "panel");
            this.panel.Name = "panel";
            // 
            // bt_signature
            // 
            resources.ApplyResources(this.bt_signature, "bt_signature");
            this.bt_signature.Name = "bt_signature";
            this.bt_signature.SpecialBorderColor = null;
            this.bt_signature.SpecialFillColor = null;
            this.bt_signature.SpecialTextColor = null;
            this.bt_signature.Click += new System.EventHandler(this.bt_signature_Click);
            // 
            // tb_output
            // 
            this.tb_output.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_output.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_output, "tb_output");
            this.tb_output.Name = "tb_output";
            // 
            // rb_hex
            // 
            resources.ApplyResources(this.rb_hex, "rb_hex");
            this.rb_hex.Name = "rb_hex";
            this.rb_hex.SpecialBorderColor = null;
            this.rb_hex.SpecialFillColor = null;
            this.rb_hex.SpecialTextColor = null;
            // 
            // rb_text
            // 
            resources.ApplyResources(this.rb_text, "rb_text");
            this.rb_text.Checked = true;
            this.rb_text.Name = "rb_text";
            this.rb_text.SpecialBorderColor = null;
            this.rb_text.SpecialFillColor = null;
            this.rb_text.SpecialTextColor = null;
            this.rb_text.TabStop = true;
            // 
            // lb_output
            // 
            resources.ApplyResources(this.lb_output, "lb_output");
            this.lb_output.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_output.Name = "lb_output";
            // 
            // cbAccounts
            // 
            this.cbAccounts.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            resources.ApplyResources(this.cbAccounts, "cbAccounts");
            this.cbAccounts.Name = "cbAccounts";
            this.cbAccounts.SpecialBorderColor = null;
            this.cbAccounts.SpecialFillColor = null;
            this.cbAccounts.SpecialTextColor = null;
            // 
            // lb_from
            // 
            resources.ApplyResources(this.lb_from, "lb_from");
            this.lb_from.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_from.Name = "lb_from";
            // 
            // lb_input
            // 
            resources.ApplyResources(this.lb_input, "lb_input");
            this.lb_input.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_input.Name = "lb_input";
            // 
            // tb_input
            // 
            this.tb_input.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_input.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_input.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_input, "tb_input");
            this.tb_input.Name = "tb_input";
            // 
            // lb_type
            // 
            resources.ApplyResources(this.lb_type, "lb_type");
            this.lb_type.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_type.Name = "lb_type";
            // 
            // SignatureDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel);
            this.DialogButtons = OX.Wallets.UI.Forms.DarkDialogButton.OkCancel;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SignatureDialog";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClaimForm_FormClosing);
            this.Load += new System.EventHandler(this.ClaimForm_Load);
            this.Controls.SetChildIndex(this.panel, 0);
            this.panel.ResumeLayout(false);
            this.panel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel;
        private UI.Controls.DarkLabel lb_type;
        private UI.Controls.DarkTextBox tb_input;
        private UI.Controls.DarkLabel lb_input;
        private UI.Controls.DarkComboBox cbAccounts;
        private UI.Controls.DarkLabel lb_from;
        private UI.Controls.DarkLabel lb_output;
        private UI.Controls.DarkRadioButton rb_text;
        private UI.Controls.DarkTextBox tb_output;
        private UI.Controls.DarkRadioButton rb_hex;
        private UI.Controls.DarkButton bt_signature;
    }
}