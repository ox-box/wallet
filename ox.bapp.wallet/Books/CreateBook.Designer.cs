namespace OX.Wallets.Base
{
    partial class CreateBook
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateBook));
            this.cbAccounts = new OX.Wallets.UI.Controls.DarkComboBox();
            this.lb_from = new OX.Wallets.UI.Controls.DarkLabel();
            this.lb_StorageType = new OX.Wallets.UI.Controls.DarkLabel();
            this.rb_outchain = new OX.Wallets.UI.Controls.DarkRadioButton();
            this.rb_onchain = new OX.Wallets.UI.Controls.DarkRadioButton();
            this.tb_name = new OX.Wallets.UI.Controls.DarkTextBox();
            this.lb_name = new OX.Wallets.UI.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            resources.ApplyResources(this.btnCancel, "btnCancel");
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
            // lb_StorageType
            // 
            resources.ApplyResources(this.lb_StorageType, "lb_StorageType");
            this.lb_StorageType.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_StorageType.Name = "lb_StorageType";
            // 
            // rb_outchain
            // 
            resources.ApplyResources(this.rb_outchain, "rb_outchain");
            this.rb_outchain.Checked = true;
            this.rb_outchain.Name = "rb_outchain";
            this.rb_outchain.SpecialBorderColor = null;
            this.rb_outchain.SpecialFillColor = null;
            this.rb_outchain.SpecialTextColor = null;
            this.rb_outchain.TabStop = true;
            // 
            // rb_onchain
            // 
            resources.ApplyResources(this.rb_onchain, "rb_onchain");
            this.rb_onchain.Name = "rb_onchain";
            this.rb_onchain.SpecialBorderColor = null;
            this.rb_onchain.SpecialFillColor = null;
            this.rb_onchain.SpecialTextColor = null;
            // 
            // tb_name
            // 
            this.tb_name.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(69)))), ((int)(((byte)(73)))), ((int)(((byte)(74)))));
            this.tb_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tb_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            resources.ApplyResources(this.tb_name, "tb_name");
            this.tb_name.Name = "tb_name";
            // 
            // lb_name
            // 
            resources.ApplyResources(this.lb_name, "lb_name");
            this.lb_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lb_name.Name = "lb_name";
            // 
            // CreateBook
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tb_name);
            this.Controls.Add(this.lb_name);
            this.Controls.Add(this.rb_onchain);
            this.Controls.Add(this.rb_outchain);
            this.Controls.Add(this.lb_StorageType);
            this.Controls.Add(this.cbAccounts);
            this.Controls.Add(this.lb_from);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateBook";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ClaimForm_FormClosing);
            this.Load += new System.EventHandler(this.ClaimForm_Load);
            this.Controls.SetChildIndex(this.lb_from, 0);
            this.Controls.SetChildIndex(this.cbAccounts, 0);
            this.Controls.SetChildIndex(this.lb_StorageType, 0);
            this.Controls.SetChildIndex(this.rb_outchain, 0);
            this.Controls.SetChildIndex(this.rb_onchain, 0);
            this.Controls.SetChildIndex(this.lb_name, 0);
            this.Controls.SetChildIndex(this.tb_name, 0);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private UI.Controls.DarkComboBox cbAccounts;
        private UI.Controls.DarkLabel lb_from;
        private UI.Controls.DarkLabel lb_StorageType;
        private UI.Controls.DarkRadioButton rb_outchain;
        private UI.Controls.DarkRadioButton rb_onchain;
        private UI.Controls.DarkTextBox tb_name;
        private UI.Controls.DarkLabel lb_name;
    }
}