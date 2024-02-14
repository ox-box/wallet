namespace OX.Wallets.Base
{
    partial class ElectionDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ElectionDialog));
            lb_pubkey = new UI.Controls.DarkLabel();
            label3 = new UI.Controls.DarkLabel();
            SuspendLayout();
            // 
            // btnOk
            // 
            resources.ApplyResources(btnOk, "btnOk");
            // 
            // btnCancel
            // 
            resources.ApplyResources(btnCancel, "btnCancel");
            // 
            // btnClose
            // 
            resources.ApplyResources(btnClose, "btnClose");
            // 
            // btnYes
            // 
            resources.ApplyResources(btnYes, "btnYes");
            // 
            // btnNo
            // 
            resources.ApplyResources(btnNo, "btnNo");
            // 
            // btnRetry
            // 
            resources.ApplyResources(btnRetry, "btnRetry");
            // 
            // btnIgnore
            // 
            resources.ApplyResources(btnIgnore, "btnIgnore");
            // 
            // lb_pubkey
            // 
            resources.ApplyResources(lb_pubkey, "lb_pubkey");
            lb_pubkey.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_pubkey.Name = "lb_pubkey";
            // 
            // label3
            // 
            resources.ApplyResources(label3, "label3");
            label3.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            label3.Name = "label3";
            // 
            // ElectionDialog
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(label3);
            Controls.Add(lb_pubkey);
            DialogButtons = UI.Forms.DarkDialogButton.OkCancel;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ElectionDialog";
            ShowInTaskbar = false;
            Load += ElectionDialog_Load;
            Controls.SetChildIndex(lb_pubkey, 0);
            Controls.SetChildIndex(label3, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OX.Wallets.UI.Controls.DarkLabel lb_pubkey;
        private OX.Wallets.UI.Controls.DarkLabel label3;
    }
}