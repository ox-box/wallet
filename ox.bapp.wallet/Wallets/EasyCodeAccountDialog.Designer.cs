namespace OX.Wallets
{
    partial class EasyCodeAccountDialog
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
            label1 = new UI.Controls.DarkLabel();
            label2 = new UI.Controls.DarkLabel();
            textBox1 = new UI.Controls.DarkTextBox();
            textBox2 = new UI.Controls.DarkTextBox();
            btReset = new UI.Controls.DarkButton();
            flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(166, 18);
            btnCancel.Click += btnCancel_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new System.Drawing.Point(18, 18);
            // 
            // btnYes
            // 
            btnYes.Location = new System.Drawing.Point(18, 18);
            btnYes.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            // 
            // btnNo
            // 
            btnNo.Location = new System.Drawing.Point(18, 18);
            btnNo.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            // 
            // btnRetry
            // 
            btnRetry.Location = new System.Drawing.Point(18, 18);
            btnRetry.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            btnRetry.Click += btnRetry_Click;
            // 
            // btnIgnore
            // 
            btnIgnore.Location = new System.Drawing.Point(708, 18);
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            label1.Location = new System.Drawing.Point(35, 41);
            label1.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(50, 24);
            label1.TabIndex = 0;
            label1.Text = "地址:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            label2.Location = new System.Drawing.Point(35, 93);
            label2.Margin = new System.Windows.Forms.Padding(5, 0, 5, 0);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(50, 24);
            label2.TabIndex = 1;
            label2.Text = "名称:";
            // 
            // textBox1
            // 
            textBox1.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            textBox1.Location = new System.Drawing.Point(145, 37);
            textBox1.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            textBox1.Name = "textBox1";
            textBox1.ReadOnly = true;
            textBox1.Size = new System.Drawing.Size(894, 30);
            textBox1.TabIndex = 4;
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            textBox2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            textBox2.Location = new System.Drawing.Point(145, 89);
            textBox2.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(894, 30);
            textBox2.TabIndex = 4;
            // 
            // btReset
            // 
            btReset.Location = new System.Drawing.Point(1047, 85);
            btReset.Name = "btReset";
            btReset.Padding = new System.Windows.Forms.Padding(5);
            btReset.Size = new System.Drawing.Size(112, 34);
            btReset.SpecialBorderColor = null;
            btReset.SpecialFillColor = null;
            btReset.SpecialTextColor = null;
            btReset.TabIndex = 5;
            btReset.Text = "darkButton1";
            btReset.Click += btReset_Click;
            // 
            // flowLayoutPanel1
            // 
            flowLayoutPanel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            flowLayoutPanel1.Location = new System.Drawing.Point(8, 140);
            flowLayoutPanel1.Name = "flowLayoutPanel1";
            flowLayoutPanel1.Size = new System.Drawing.Size(1680, 519);
            flowLayoutPanel1.TabIndex = 6;
            // 
            // EasyCodeAccountDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1706, 751);
            Controls.Add(flowLayoutPanel1);
            Controls.Add(btReset);
            Controls.Add(textBox2);
            Controls.Add(textBox1);
            Controls.Add(label2);
            Controls.Add(label1);
            DialogButtons = UI.Forms.DarkDialogButton.RetryCancel;
            Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
            Name = "EasyCodeAccountDialog";
            Text = "EasyCodeAccountDialog:";
            FormClosing += EasyCodeAccountDialog_FormClosing;
            Load += EasyCodeAccountDialog_Load;
            Controls.SetChildIndex(label1, 0);
            Controls.SetChildIndex(label2, 0);
            Controls.SetChildIndex(textBox1, 0);
            Controls.SetChildIndex(textBox2, 0);
            Controls.SetChildIndex(btReset, 0);
            Controls.SetChildIndex(flowLayoutPanel1, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OX.Wallets.UI.Controls.DarkLabel label1;
        private OX.Wallets.UI.Controls.DarkLabel label2;
        private OX.Wallets.UI.Controls.DarkTextBox textBox1;
        private OX.Wallets.UI.Controls.DarkTextBox textBox2;
        private UI.Controls.DarkButton btReset;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
    }
}