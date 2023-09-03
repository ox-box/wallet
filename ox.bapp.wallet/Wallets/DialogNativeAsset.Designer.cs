using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;

namespace OX.Wallets.Base
{
    partial class DialogNativeAsset
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogNativeAsset));
            pnlMain = new System.Windows.Forms.Panel();
            lb_lq_oxc = new DarkLabel();
            lb_lq_oxs = new DarkLabel();
            lb_c_6 = new DarkLabel();
            lb_c_5 = new DarkLabel();
            lb_c_4 = new DarkLabel();
            lb_c_3 = new DarkLabel();
            lb_c_2 = new DarkLabel();
            lb_c_1 = new DarkLabel();
            lb_s_6 = new DarkLabel();
            lb_s_5 = new DarkLabel();
            lb_s_4 = new DarkLabel();
            lb_s_3 = new DarkLabel();
            lb_s_2 = new DarkLabel();
            lb_s_1 = new DarkLabel();
            lb_OXC_Locked = new DarkLabel();
            lb_OXC_Issued = new DarkLabel();
            lb_OXC_Name = new DarkLabel();
            lb_OXS_Locked = new DarkLabel();
            lb_OXS_Issued = new DarkLabel();
            lb_OXS_Name = new DarkLabel();
            lb_total_gas = new DarkLabel();
            pnlMain.SuspendLayout();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(18, 18);
            btnCancel.Click += btnCancel_Click;
            // 
            // btnClose
            // 
            btnClose.Location = new System.Drawing.Point(18, 18);
            btnClose.Click += btnClose_Click;
            // 
            // btnYes
            // 
            btnYes.Location = new System.Drawing.Point(18, 18);
            btnYes.Click += btnYes_Click;
            // 
            // btnNo
            // 
            btnNo.Location = new System.Drawing.Point(18, 18);
            btnNo.Click += btnNo_Click;
            // 
            // btnAbort
            // 
            btnAbort.Click += btnAbort_Click;
            // 
            // btnRetry
            // 
            btnRetry.Location = new System.Drawing.Point(708, 18);
            btnRetry.Click += btnRetry_Click;
            // 
            // btnIgnore
            // 
            btnIgnore.Location = new System.Drawing.Point(708, 18);
            btnIgnore.Click += btnIgnore_Click;
            // 
            // pnlMain
            // 
            pnlMain.Controls.Add(lb_total_gas);
            pnlMain.Controls.Add(lb_lq_oxc);
            pnlMain.Controls.Add(lb_lq_oxs);
            pnlMain.Controls.Add(lb_c_6);
            pnlMain.Controls.Add(lb_c_5);
            pnlMain.Controls.Add(lb_c_4);
            pnlMain.Controls.Add(lb_c_3);
            pnlMain.Controls.Add(lb_c_2);
            pnlMain.Controls.Add(lb_c_1);
            pnlMain.Controls.Add(lb_s_6);
            pnlMain.Controls.Add(lb_s_5);
            pnlMain.Controls.Add(lb_s_4);
            pnlMain.Controls.Add(lb_s_3);
            pnlMain.Controls.Add(lb_s_2);
            pnlMain.Controls.Add(lb_s_1);
            pnlMain.Controls.Add(lb_OXC_Locked);
            pnlMain.Controls.Add(lb_OXC_Issued);
            pnlMain.Controls.Add(lb_OXC_Name);
            pnlMain.Controls.Add(lb_OXS_Locked);
            pnlMain.Controls.Add(lb_OXS_Issued);
            pnlMain.Controls.Add(lb_OXS_Name);
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.Location = new System.Drawing.Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new System.Windows.Forms.Padding(15, 15, 15, 5);
            pnlMain.Size = new System.Drawing.Size(1524, 532);
            pnlMain.TabIndex = 2;
            // 
            // lb_lq_oxc
            // 
            lb_lq_oxc.AutoSize = true;
            lb_lq_oxc.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_lq_oxc.Location = new System.Drawing.Point(767, 152);
            lb_lq_oxc.Name = "lb_lq_oxc";
            lb_lq_oxc.Size = new System.Drawing.Size(98, 25);
            lb_lq_oxc.TabIndex = 19;
            lb_lq_oxc.Text = "darkLabel3";
            // 
            // lb_lq_oxs
            // 
            lb_lq_oxs.AutoSize = true;
            lb_lq_oxs.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_lq_oxs.Location = new System.Drawing.Point(40, 152);
            lb_lq_oxs.Name = "lb_lq_oxs";
            lb_lq_oxs.Size = new System.Drawing.Size(98, 25);
            lb_lq_oxs.TabIndex = 18;
            lb_lq_oxs.Text = "darkLabel3";
            // 
            // lb_c_6
            // 
            lb_c_6.AutoSize = true;
            lb_c_6.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_c_6.Location = new System.Drawing.Point(767, 459);
            lb_c_6.Name = "lb_c_6";
            lb_c_6.Size = new System.Drawing.Size(98, 25);
            lb_c_6.TabIndex = 17;
            lb_c_6.Text = "darkLabel3";
            // 
            // lb_c_5
            // 
            lb_c_5.AutoSize = true;
            lb_c_5.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_c_5.Location = new System.Drawing.Point(767, 405);
            lb_c_5.Name = "lb_c_5";
            lb_c_5.Size = new System.Drawing.Size(98, 25);
            lb_c_5.TabIndex = 16;
            lb_c_5.Text = "darkLabel3";
            // 
            // lb_c_4
            // 
            lb_c_4.AutoSize = true;
            lb_c_4.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_c_4.Location = new System.Drawing.Point(767, 349);
            lb_c_4.Name = "lb_c_4";
            lb_c_4.Size = new System.Drawing.Size(98, 25);
            lb_c_4.TabIndex = 15;
            lb_c_4.Text = "darkLabel3";
            // 
            // lb_c_3
            // 
            lb_c_3.AutoSize = true;
            lb_c_3.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_c_3.Location = new System.Drawing.Point(767, 296);
            lb_c_3.Name = "lb_c_3";
            lb_c_3.Size = new System.Drawing.Size(108, 25);
            lb_c_3.TabIndex = 14;
            lb_c_3.Text = "darkLabel10";
            // 
            // lb_c_2
            // 
            lb_c_2.AutoSize = true;
            lb_c_2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_c_2.Location = new System.Drawing.Point(767, 246);
            lb_c_2.Name = "lb_c_2";
            lb_c_2.Size = new System.Drawing.Size(98, 25);
            lb_c_2.TabIndex = 13;
            lb_c_2.Text = "darkLabel3";
            // 
            // lb_c_1
            // 
            lb_c_1.AutoSize = true;
            lb_c_1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_c_1.Location = new System.Drawing.Point(767, 196);
            lb_c_1.Name = "lb_c_1";
            lb_c_1.Size = new System.Drawing.Size(98, 25);
            lb_c_1.TabIndex = 12;
            lb_c_1.Text = "darkLabel3";
            // 
            // lb_s_6
            // 
            lb_s_6.AutoSize = true;
            lb_s_6.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_s_6.Location = new System.Drawing.Point(40, 459);
            lb_s_6.Name = "lb_s_6";
            lb_s_6.Size = new System.Drawing.Size(98, 25);
            lb_s_6.TabIndex = 11;
            lb_s_6.Text = "darkLabel3";
            // 
            // lb_s_5
            // 
            lb_s_5.AutoSize = true;
            lb_s_5.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_s_5.Location = new System.Drawing.Point(40, 405);
            lb_s_5.Name = "lb_s_5";
            lb_s_5.Size = new System.Drawing.Size(98, 25);
            lb_s_5.TabIndex = 10;
            lb_s_5.Text = "darkLabel3";
            // 
            // lb_s_4
            // 
            lb_s_4.AutoSize = true;
            lb_s_4.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_s_4.Location = new System.Drawing.Point(40, 349);
            lb_s_4.Name = "lb_s_4";
            lb_s_4.Size = new System.Drawing.Size(98, 25);
            lb_s_4.TabIndex = 9;
            lb_s_4.Text = "darkLabel3";
            // 
            // lb_s_3
            // 
            lb_s_3.AutoSize = true;
            lb_s_3.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_s_3.Location = new System.Drawing.Point(40, 296);
            lb_s_3.Name = "lb_s_3";
            lb_s_3.Size = new System.Drawing.Size(98, 25);
            lb_s_3.TabIndex = 8;
            lb_s_3.Text = "darkLabel3";
            // 
            // lb_s_2
            // 
            lb_s_2.AutoSize = true;
            lb_s_2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_s_2.Location = new System.Drawing.Point(40, 246);
            lb_s_2.Name = "lb_s_2";
            lb_s_2.Size = new System.Drawing.Size(98, 25);
            lb_s_2.TabIndex = 7;
            lb_s_2.Text = "darkLabel3";
            // 
            // lb_s_1
            // 
            lb_s_1.AutoSize = true;
            lb_s_1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_s_1.Location = new System.Drawing.Point(40, 196);
            lb_s_1.Name = "lb_s_1";
            lb_s_1.Size = new System.Drawing.Size(98, 25);
            lb_s_1.TabIndex = 6;
            lb_s_1.Text = "darkLabel3";
            // 
            // lb_OXC_Locked
            // 
            lb_OXC_Locked.AutoSize = true;
            lb_OXC_Locked.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_OXC_Locked.Location = new System.Drawing.Point(767, 111);
            lb_OXC_Locked.Name = "lb_OXC_Locked";
            lb_OXC_Locked.Size = new System.Drawing.Size(98, 25);
            lb_OXC_Locked.TabIndex = 5;
            lb_OXC_Locked.Text = "darkLabel4";
            // 
            // lb_OXC_Issued
            // 
            lb_OXC_Issued.AutoSize = true;
            lb_OXC_Issued.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_OXC_Issued.Location = new System.Drawing.Point(767, 67);
            lb_OXC_Issued.Name = "lb_OXC_Issued";
            lb_OXC_Issued.Size = new System.Drawing.Size(98, 25);
            lb_OXC_Issued.TabIndex = 4;
            lb_OXC_Issued.Text = "darkLabel5";
            // 
            // lb_OXC_Name
            // 
            lb_OXC_Name.AutoSize = true;
            lb_OXC_Name.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_OXC_Name.Location = new System.Drawing.Point(767, 28);
            lb_OXC_Name.Name = "lb_OXC_Name";
            lb_OXC_Name.Size = new System.Drawing.Size(98, 25);
            lb_OXC_Name.TabIndex = 3;
            lb_OXC_Name.Text = "darkLabel6";
            // 
            // lb_OXS_Locked
            // 
            lb_OXS_Locked.AutoSize = true;
            lb_OXS_Locked.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_OXS_Locked.Location = new System.Drawing.Point(40, 111);
            lb_OXS_Locked.Name = "lb_OXS_Locked";
            lb_OXS_Locked.Size = new System.Drawing.Size(98, 25);
            lb_OXS_Locked.TabIndex = 2;
            lb_OXS_Locked.Text = "darkLabel3";
            // 
            // lb_OXS_Issued
            // 
            lb_OXS_Issued.AutoSize = true;
            lb_OXS_Issued.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_OXS_Issued.Location = new System.Drawing.Point(40, 67);
            lb_OXS_Issued.Name = "lb_OXS_Issued";
            lb_OXS_Issued.Size = new System.Drawing.Size(98, 25);
            lb_OXS_Issued.TabIndex = 1;
            lb_OXS_Issued.Text = "darkLabel2";
            // 
            // lb_OXS_Name
            // 
            lb_OXS_Name.AutoSize = true;
            lb_OXS_Name.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_OXS_Name.Location = new System.Drawing.Point(40, 28);
            lb_OXS_Name.Name = "lb_OXS_Name";
            lb_OXS_Name.Size = new System.Drawing.Size(98, 25);
            lb_OXS_Name.TabIndex = 0;
            lb_OXS_Name.Text = "darkLabel1";
            // 
            // lb_total_gas
            // 
            lb_total_gas.AutoSize = true;
            lb_total_gas.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_total_gas.Location = new System.Drawing.Point(1155, 111);
            lb_total_gas.Name = "lb_total_gas";
            lb_total_gas.Size = new System.Drawing.Size(98, 25);
            lb_total_gas.TabIndex = 20;
            lb_total_gas.Text = "darkLabel4";
            // 
            // DialogNativeAsset
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1524, 615);
            Controls.Add(pnlMain);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DialogNativeAsset";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "About";
            Load += DialogNativeAsset_Load;
            Controls.SetChildIndex(pnlMain, 0);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private DarkLabel lb_OXC_Locked;
        private DarkLabel lb_OXC_Issued;
        private DarkLabel lb_OXC_Name;
        private DarkLabel lb_OXS_Locked;
        private DarkLabel lb_OXS_Issued;
        private DarkLabel lb_OXS_Name;
        private DarkLabel lb_c_6;
        private DarkLabel lb_c_5;
        private DarkLabel lb_c_4;
        private DarkLabel lb_c_3;
        private DarkLabel lb_c_2;
        private DarkLabel lb_c_1;
        private DarkLabel lb_s_6;
        private DarkLabel lb_s_5;
        private DarkLabel lb_s_4;
        private DarkLabel lb_s_3;
        private DarkLabel lb_s_2;
        private DarkLabel lb_s_1;
        private DarkLabel lb_lq_oxc;
        private DarkLabel lb_lq_oxs;
        private DarkLabel lb_total_gas;
    }
}