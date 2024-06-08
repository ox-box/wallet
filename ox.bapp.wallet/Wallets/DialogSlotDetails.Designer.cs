using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Forms;

namespace OX.Wallets.Base
{
    partial class DialogSlotDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DialogSlotDetails));
            pnlMain = new System.Windows.Forms.Panel();
            bt_doVote = new DarkButton();
            darkLabel2 = new DarkLabel();
            darkTextBox1 = new DarkTextBox();
            darkLabel1 = new DarkLabel();
            lv_votes = new DarkListView();
            lv_slots = new DarkListView();
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
            pnlMain.Controls.Add(bt_doVote);
            pnlMain.Controls.Add(darkLabel2);
            pnlMain.Controls.Add(darkTextBox1);
            pnlMain.Controls.Add(darkLabel1);
            pnlMain.Controls.Add(lv_votes);
            pnlMain.Controls.Add(lv_slots);
            pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            pnlMain.Location = new System.Drawing.Point(0, 0);
            pnlMain.Name = "pnlMain";
            pnlMain.Padding = new System.Windows.Forms.Padding(15, 15, 15, 5);
            pnlMain.Size = new System.Drawing.Size(1197, 620);
            pnlMain.TabIndex = 2;
            // 
            // bt_doVote
            // 
            bt_doVote.Location = new System.Drawing.Point(1051, 570);
            bt_doVote.Name = "bt_doVote";
            bt_doVote.Padding = new System.Windows.Forms.Padding(5);
            bt_doVote.Size = new System.Drawing.Size(112, 34);
            bt_doVote.SpecialBorderColor = null;
            bt_doVote.SpecialFillColor = null;
            bt_doVote.SpecialTextColor = null;
            bt_doVote.TabIndex = 6;
            bt_doVote.Text = "darkButton1";
            bt_doVote.Click += bt_doVote_Click;
            // 
            // darkLabel2
            // 
            darkLabel2.AutoSize = true;
            darkLabel2.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel2.Location = new System.Drawing.Point(649, 572);
            darkLabel2.Name = "darkLabel2";
            darkLabel2.Size = new System.Drawing.Size(98, 25);
            darkLabel2.TabIndex = 5;
            darkLabel2.Text = "darkLabel2";
            // 
            // darkTextBox1
            // 
            darkTextBox1.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            darkTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            darkTextBox1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkTextBox1.Location = new System.Drawing.Point(778, 570);
            darkTextBox1.Name = "darkTextBox1";
            darkTextBox1.Size = new System.Drawing.Size(246, 31);
            darkTextBox1.TabIndex = 4;
            darkTextBox1.TextChanged += darkTextBox1_TextChanged;
            // 
            // darkLabel1
            // 
            darkLabel1.AutoSize = true;
            darkLabel1.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            darkLabel1.Location = new System.Drawing.Point(649, 15);
            darkLabel1.Name = "darkLabel1";
            darkLabel1.Size = new System.Drawing.Size(98, 25);
            darkLabel1.TabIndex = 3;
            darkLabel1.Text = "darkLabel1";
            // 
            // lv_votes
            // 
            lv_votes.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lv_votes.Location = new System.Drawing.Point(630, 59);
            lv_votes.Name = "lv_votes";
            lv_votes.Size = new System.Drawing.Size(549, 480);
            lv_votes.TabIndex = 2;
            lv_votes.Text = "darkListView1";
            lv_votes.SelectedIndicesChanged += lv_votes_SelectedIndicesChanged;
            // 
            // lv_slots
            // 
            lv_slots.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lv_slots.Location = new System.Drawing.Point(3, 4);
            lv_slots.Name = "lv_slots";
            lv_slots.Size = new System.Drawing.Size(615, 612);
            lv_slots.TabIndex = 1;
            lv_slots.Text = "darkListView1";
            lv_slots.SelectedIndicesChanged += lv_slots_SelectedIndicesChanged;
            lv_slots.TabIndexChanged += lv_slots_TabIndexChanged;
            lv_slots.Click += lv_slots_Click;
            // 
            // DialogSlotDetails
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(10F, 25F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1197, 703);
            Controls.Add(pnlMain);
            Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(9, 10, 9, 10);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "DialogSlotDetails";
            StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            Text = "DialogSlotDetails";
            Load += DialogNativeAsset_Load;
            Controls.SetChildIndex(pnlMain, 0);
            pnlMain.ResumeLayout(false);
            pnlMain.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel pnlMain;
        private DarkListView lv_slots;
        private DarkListView lv_votes;
        private DarkLabel darkLabel1;
        private DarkLabel darkLabel2;
        private DarkTextBox darkTextBox1;
        private DarkButton bt_doVote;
    }
}