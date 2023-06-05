namespace OX.Wallets.Base
{
    partial class ClaimOXC
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClaimOXC));
            this.label1 = new OX.Wallets.UI.Controls.DarkLabel();
            this.label2 = new OX.Wallets.UI.Controls.DarkLabel();
            this.button1 = new OX.Wallets.UI.Controls.DarkButton();
            this.textBox1 = new OX.Wallets.UI.Controls.DarkTextBox();
            this.textBox2 = new OX.Wallets.UI.Controls.DarkTextBox();
            this.combo_address = new OX.Wallets.UI.Controls.DarkComboBox();
            this.lb_claim_to_address = new OX.Wallets.UI.Controls.DarkLabel();
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // button1
            // 
            resources.ApplyResources(this.button1, "button1");
            this.button1.Name = "button1";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox1, "textBox1");
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            // 
            // textBox2
            // 
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.textBox2, "textBox2");
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            // 
            // combo_address
            // 
            this.combo_address.FormattingEnabled = true;
            resources.ApplyResources(this.combo_address, "combo_address");
            this.combo_address.Name = "combo_address";
            this.combo_address.TextChanged += new System.EventHandler(this.combo_address_TextChanged);
            // 
            // lb_claim_to_address
            // 
            resources.ApplyResources(this.lb_claim_to_address, "lb_claim_to_address");
            this.lb_claim_to_address.Name = "lb_claim_to_address";
            // 
            // ClaimOXC
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lb_claim_to_address);
            this.Controls.Add(this.combo_address);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ClaimOXC";
            this.FormClosing += ClaimForm_FormClosing;
            this.Load += new System.EventHandler(this.ClaimForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

       

        #endregion

        private OX.Wallets.UI.Controls.DarkLabel label1;
        private OX.Wallets.UI.Controls.DarkLabel label2;
        private OX.Wallets.UI.Controls.DarkButton button1;
        private OX.Wallets.UI.Controls.DarkTextBox textBox1;
        private OX.Wallets.UI.Controls.DarkTextBox textBox2;
        private OX.Wallets.UI.Controls.DarkComboBox combo_address;
        private OX.Wallets.UI.Controls.DarkLabel lb_claim_to_address;
    }
}