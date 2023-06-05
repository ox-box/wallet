
namespace OX.Wallets.Base
{
    partial class MyNFTDetails
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
            lb_nfthash = new UI.Controls.DarkLabel();
            tb_nfthash = new UI.Controls.DarkTextBox();
            bt_copyNftHash = new UI.Controls.DarkButton();
            tb_author = new UI.Controls.DarkTextBox();
            lb_author = new UI.Controls.DarkLabel();
            lb_nftMsg = new UI.Controls.DarkLabel();
            bt_showDonates = new UI.Controls.DarkButton();
            tb_mark = new UI.Controls.DarkTextBox();
            bt_issue = new UI.Controls.DarkButton();
            panel1 = new System.Windows.Forms.Panel();
            bt_preview = new UI.Controls.DarkButton();
            bt_nodepreview = new UI.Controls.DarkButton();
            SuspendLayout();
            // 
            // btnOk
            // 
            btnOk.Click += btnOk_Click;
            // 
            // btnCancel
            // 
            btnCancel.Location = new System.Drawing.Point(18, 18);
            // 
            // btnClose
            // 
            btnClose.Location = new System.Drawing.Point(18, 18);
            // 
            // btnYes
            // 
            btnYes.Location = new System.Drawing.Point(18, 18);
            // 
            // btnNo
            // 
            btnNo.Location = new System.Drawing.Point(18, 18);
            // 
            // btnRetry
            // 
            btnRetry.Location = new System.Drawing.Point(708, 18);
            // 
            // btnIgnore
            // 
            btnIgnore.Location = new System.Drawing.Point(708, 18);
            // 
            // lb_nfthash
            // 
            lb_nfthash.AutoSize = true;
            lb_nfthash.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_nfthash.Location = new System.Drawing.Point(28, 22);
            lb_nfthash.Name = "lb_nfthash";
            lb_nfthash.Size = new System.Drawing.Size(106, 24);
            lb_nfthash.TabIndex = 2;
            lb_nfthash.Text = "darkLabel1";
            // 
            // tb_nfthash
            // 
            tb_nfthash.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_nfthash.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_nfthash.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_nfthash.Location = new System.Drawing.Point(177, 20);
            tb_nfthash.Name = "tb_nfthash";
            tb_nfthash.ReadOnly = true;
            tb_nfthash.Size = new System.Drawing.Size(1026, 30);
            tb_nfthash.TabIndex = 3;
            // 
            // bt_copyNftHash
            // 
            bt_copyNftHash.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            bt_copyNftHash.Location = new System.Drawing.Point(1232, 19);
            bt_copyNftHash.Name = "bt_copyNftHash";
            bt_copyNftHash.Padding = new System.Windows.Forms.Padding(5);
            bt_copyNftHash.Size = new System.Drawing.Size(155, 34);
            bt_copyNftHash.SpecialBorderColor = null;
            bt_copyNftHash.SpecialFillColor = null;
            bt_copyNftHash.SpecialTextColor = null;
            bt_copyNftHash.TabIndex = 28;
            bt_copyNftHash.Text = "darkButton1";
            bt_copyNftHash.Click += bt_copyNftHash_Click;
            // 
            // tb_author
            // 
            tb_author.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_author.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_author.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_author.Location = new System.Drawing.Point(177, 76);
            tb_author.Name = "tb_author";
            tb_author.ReadOnly = true;
            tb_author.Size = new System.Drawing.Size(1026, 30);
            tb_author.TabIndex = 30;
            // 
            // lb_author
            // 
            lb_author.AutoSize = true;
            lb_author.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_author.Location = new System.Drawing.Point(28, 78);
            lb_author.Name = "lb_author";
            lb_author.Size = new System.Drawing.Size(106, 24);
            lb_author.TabIndex = 29;
            lb_author.Text = "darkLabel1";
            // 
            // lb_nftMsg
            // 
            lb_nftMsg.AutoSize = true;
            lb_nftMsg.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            lb_nftMsg.Location = new System.Drawing.Point(177, 133);
            lb_nftMsg.Name = "lb_nftMsg";
            lb_nftMsg.Size = new System.Drawing.Size(106, 24);
            lb_nftMsg.TabIndex = 32;
            lb_nftMsg.Text = "darkLabel1";
            // 
            // bt_showDonates
            // 
            bt_showDonates.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            bt_showDonates.Location = new System.Drawing.Point(1232, 128);
            bt_showDonates.Name = "bt_showDonates";
            bt_showDonates.Padding = new System.Windows.Forms.Padding(5);
            bt_showDonates.Size = new System.Drawing.Size(155, 34);
            bt_showDonates.SpecialBorderColor = null;
            bt_showDonates.SpecialFillColor = null;
            bt_showDonates.SpecialTextColor = null;
            bt_showDonates.TabIndex = 33;
            bt_showDonates.Text = "darkButton1";
            bt_showDonates.Click += bt_showDonates_Click;
            // 
            // tb_mark
            // 
            tb_mark.BackColor = System.Drawing.Color.FromArgb(69, 73, 74);
            tb_mark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            tb_mark.ForeColor = System.Drawing.Color.FromArgb(220, 220, 220);
            tb_mark.Location = new System.Drawing.Point(177, 185);
            tb_mark.Margin = new System.Windows.Forms.Padding(6);
            tb_mark.MaxLength = 1000;
            tb_mark.Multiline = true;
            tb_mark.Name = "tb_mark";
            tb_mark.ReadOnly = true;
            tb_mark.Size = new System.Drawing.Size(1026, 144);
            tb_mark.TabIndex = 42;
            // 
            // bt_issue
            // 
            bt_issue.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            bt_issue.Location = new System.Drawing.Point(1232, 182);
            bt_issue.Name = "bt_issue";
            bt_issue.Padding = new System.Windows.Forms.Padding(5);
            bt_issue.Size = new System.Drawing.Size(155, 34);
            bt_issue.SpecialBorderColor = null;
            bt_issue.SpecialFillColor = null;
            bt_issue.SpecialTextColor = null;
            bt_issue.TabIndex = 43;
            bt_issue.Text = "darkButton1";
            bt_issue.Click += bt_issue_Click;
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.Location = new System.Drawing.Point(0, 358);
            panel1.Name = "panel1";
            panel1.Size = new System.Drawing.Size(1439, 573);
            panel1.TabIndex = 44;
            // 
            // bt_preview
            // 
            bt_preview.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            bt_preview.Location = new System.Drawing.Point(1232, 232);
            bt_preview.Name = "bt_preview";
            bt_preview.Padding = new System.Windows.Forms.Padding(5);
            bt_preview.Size = new System.Drawing.Size(155, 34);
            bt_preview.SpecialBorderColor = null;
            bt_preview.SpecialFillColor = null;
            bt_preview.SpecialTextColor = null;
            bt_preview.TabIndex = 45;
            bt_preview.Text = "darkButton1";
            bt_preview.Click += bt_preview_Click;
            // 
            // bt_nodepreview
            // 
            bt_nodepreview.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            bt_nodepreview.Location = new System.Drawing.Point(1232, 285);
            bt_nodepreview.Name = "bt_nodepreview";
            bt_nodepreview.Padding = new System.Windows.Forms.Padding(5);
            bt_nodepreview.Size = new System.Drawing.Size(155, 34);
            bt_nodepreview.SpecialBorderColor = null;
            bt_nodepreview.SpecialFillColor = null;
            bt_nodepreview.SpecialTextColor = null;
            bt_nodepreview.TabIndex = 46;
            bt_nodepreview.Text = "darkButton1";
            bt_nodepreview.Click += bt_nodepreview_Click;
            // 
            // MyNFTDetails
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1439, 1023);
            Controls.Add(bt_nodepreview);
            Controls.Add(bt_preview);
            Controls.Add(panel1);
            Controls.Add(bt_issue);
            Controls.Add(tb_mark);
            Controls.Add(bt_showDonates);
            Controls.Add(lb_nftMsg);
            Controls.Add(tb_author);
            Controls.Add(lb_author);
            Controls.Add(bt_copyNftHash);
            Controls.Add(tb_nfthash);
            Controls.Add(lb_nfthash);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "MyNFTDetails";
            Text = "FindTransactionForm";
            Load += NFTDetails_Load;
            Controls.SetChildIndex(lb_nfthash, 0);
            Controls.SetChildIndex(tb_nfthash, 0);
            Controls.SetChildIndex(bt_copyNftHash, 0);
            Controls.SetChildIndex(lb_author, 0);
            Controls.SetChildIndex(tb_author, 0);
            Controls.SetChildIndex(lb_nftMsg, 0);
            Controls.SetChildIndex(bt_showDonates, 0);
            Controls.SetChildIndex(tb_mark, 0);
            Controls.SetChildIndex(bt_issue, 0);
            Controls.SetChildIndex(panel1, 0);
            Controls.SetChildIndex(bt_preview, 0);
            Controls.SetChildIndex(bt_nodepreview, 0);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private UI.Controls.DarkLabel lb_nfthash;
        private UI.Controls.DarkTextBox tb_nfthash;
        private UI.Controls.DarkButton bt_copyNftHash;
        private UI.Controls.DarkTextBox tb_author;
        private UI.Controls.DarkLabel lb_author;
        private UI.Controls.DarkLabel lb_nftMsg;
        private UI.Controls.DarkButton bt_showDonates;
        private UI.Controls.DarkTextBox tb_mark;
        private UI.Controls.DarkButton bt_issue;
        private System.Windows.Forms.Panel panel1;
        private UI.Controls.DarkButton bt_preview;
        private UI.Controls.DarkButton bt_nodepreview;
    }
}