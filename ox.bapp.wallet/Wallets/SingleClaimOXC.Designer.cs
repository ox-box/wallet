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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ClaimOXC));
            label1 = new UI.Controls.DarkLabel();
            label2 = new UI.Controls.DarkLabel();
            button1 = new UI.Controls.DarkButton();
            textBox1 = new UI.Controls.DarkTextBox();
            textBox2 = new UI.Controls.DarkTextBox();
            SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            label2.Name = "label2";
            // 
            // button1
            // 
            resources.ApplyResources(button1, "button1");
            button1.Name = "button1";
            button1.SpecialBorderColor = null;
            button1.SpecialFillColor = null;
            button1.SpecialTextColor = null;
            button1.Click += button1_Click;
            // 
            // textBox1
            // 
            textBox1.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(textBox1, "textBox1");
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            textBox2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            resources.ApplyResources(textBox2, "textBox2");
            textBox2.Name = "textBox2";
            textBox2.ReadOnly = true;
            // 
            // ClaimOXC
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(button1);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "ClaimOXC";
            FormClosing += ClaimForm_FormClosing;
            Load += ClaimForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }



        #endregion

        private OX.Wallets.UI.Controls.DarkLabel label1;
        private OX.Wallets.UI.Controls.DarkLabel label2;
        private OX.Wallets.UI.Controls.DarkButton button1;
        private OX.Wallets.UI.Controls.DarkTextBox textBox1;
        private OX.Wallets.UI.Controls.DarkTextBox textBox2;
    }
}