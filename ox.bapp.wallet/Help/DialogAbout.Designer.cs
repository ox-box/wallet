﻿using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;

namespace OX.Wallets.Base
{
    partial class DialogAbout
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogAbout));
            this.pnlMain = new System.Windows.Forms.Panel();
            this.lblVersion = new OX.Wallets.UI.Controls.DarkLabel();
            this.darkLabel2 = new OX.Wallets.UI.Controls.DarkLabel();
            this.darkLabel1 = new OX.Wallets.UI.Controls.DarkLabel();
            this.lblHeader = new OX.Wallets.UI.Controls.DarkLabel();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(18, 18);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(18, 18);
            // 
            // btnYes
            // 
            this.btnYes.Location = new System.Drawing.Point(18, 18);
            // 
            // btnNo
            // 
            this.btnNo.Location = new System.Drawing.Point(18, 18);
            // 
            // btnRetry
            // 
            this.btnRetry.Location = new System.Drawing.Point(708, 18);
            // 
            // btnIgnore
            // 
            this.btnIgnore.Location = new System.Drawing.Point(708, 18);
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblVersion);
            this.pnlMain.Controls.Add(this.darkLabel2);
            this.pnlMain.Controls.Add(this.darkLabel1);
            this.pnlMain.Controls.Add(this.lblHeader);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(15, 15, 15, 5);
            this.pnlMain.Size = new System.Drawing.Size(622, 231);
            this.pnlMain.TabIndex = 2;
            // 
            // lblVersion
            // 
            this.lblVersion.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblVersion.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblVersion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblVersion.Location = new System.Drawing.Point(15, 187);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(592, 36);
            this.lblVersion.TabIndex = 7;
            this.lblVersion.Text = "Version: [version]";
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // darkLabel2
            // 
            this.darkLabel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.darkLabel2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.darkLabel2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel2.Location = new System.Drawing.Point(15, 136);
            this.darkLabel2.Name = "darkLabel2";
            this.darkLabel2.Size = new System.Drawing.Size(592, 51);
            this.darkLabel2.TabIndex = 5;
            this.darkLabel2.Text = "Created because of all the little annoyances and issues with the default .NET con" +
    "trol library.";
            this.darkLabel2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // darkLabel1
            // 
            this.darkLabel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.darkLabel1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.darkLabel1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.darkLabel1.Location = new System.Drawing.Point(15, 82);
            this.darkLabel1.Name = "darkLabel1";
            this.darkLabel1.Size = new System.Drawing.Size(592, 54);
            this.darkLabel1.TabIndex = 4;
            this.darkLabel1.Text = "Dark themed control and docking library for .NET WinForms.";
            this.darkLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblHeader
            // 
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.lblHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.lblHeader.Location = new System.Drawing.Point(15, 15);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(592, 67);
            this.lblHeader.TabIndex = 3;
            this.lblHeader.Text = "OX-ECO";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DialogAbout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(622, 314);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogAbout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.Controls.SetChildIndex(this.pnlMain, 0);
            this.pnlMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private DarkLabel lblHeader;
        private DarkLabel darkLabel1;
        private DarkLabel darkLabel2;
        private DarkLabel lblVersion;
    }
}