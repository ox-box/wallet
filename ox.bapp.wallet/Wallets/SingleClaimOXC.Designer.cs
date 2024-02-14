namespace OX.Wallets.Base
{
    partial class SingleClaimOXC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SingleClaimOXC));
            lb_Available = new UI.Controls.DarkLabel();
            lb_Unavailable = new UI.Controls.DarkLabel();
            bt_claim = new UI.Controls.DarkButton();
            tb_Available = new UI.Controls.DarkTextBox();
            tb_Unavailable = new UI.Controls.DarkTextBox();
            SuspendLayout();
            // 
            // lb_Available
            // 
            resources.ApplyResources(lb_Available, "lb_Available");
            lb_Available.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_Available.Name = "lb_Available";
            // 
            // lb_Unavailable
            // 
            resources.ApplyResources(lb_Unavailable, "lb_Unavailable");
            lb_Unavailable.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_Unavailable.Name = "lb_Unavailable";
            // 
            // bt_claim
            // 
            resources.ApplyResources(bt_claim, "bt_claim");
            bt_claim.Name = "bt_claim";
            bt_claim.SpecialBorderColor = null;
            bt_claim.SpecialFillColor = null;
            bt_claim.SpecialTextColor = null;
            bt_claim.Click += button1_Click;
            // 
            // tb_Available
            // 
            tb_Available.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_Available.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tb_Available.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_Available, "tb_Available");
            tb_Available.Name = "tb_Available";
            tb_Available.ReadOnly = true;
            // 
            // tb_Unavailable
            // 
            tb_Unavailable.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_Unavailable.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tb_Unavailable.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(tb_Unavailable, "tb_Unavailable");
            tb_Unavailable.Name = "tb_Unavailable";
            tb_Unavailable.ReadOnly = true;
            // 
            // SingleClaimOXC
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(tb_Unavailable);
            Controls.Add(tb_Available);
            Controls.Add(bt_claim);
            Controls.Add(lb_Unavailable);
            Controls.Add(lb_Available);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "SingleClaimOXC";
            FormClosing += ClaimForm_FormClosing;
            Load += ClaimForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private OX.Wallets.UI.Controls.DarkLabel lb_Available;
        private OX.Wallets.UI.Controls.DarkLabel lb_Unavailable;
        private OX.Wallets.UI.Controls.DarkButton bt_claim;
        private OX.Wallets.UI.Controls.DarkTextBox tb_Available;
        private OX.Wallets.UI.Controls.DarkTextBox tb_Unavailable;
    }
}