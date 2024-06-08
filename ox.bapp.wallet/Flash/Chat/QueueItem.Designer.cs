
namespace OX.Wallets.Flash.Chat
{
	partial class QueueItem
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            bodyPanel = new System.Windows.Forms.Panel();
            bodyTextBox = new System.Windows.Forms.TextBox();
            bodyPanel.SuspendLayout();
            SuspendLayout();
            // 
            // bodyPanel
            // 
            bodyPanel.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            bodyPanel.BackColor = System.Drawing.Color.RoyalBlue;
            bodyPanel.Controls.Add(bodyTextBox);
            bodyPanel.Location = new System.Drawing.Point(19, 10);
            bodyPanel.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            bodyPanel.Name = "bodyPanel";
            bodyPanel.Padding = new System.Windows.Forms.Padding(3);
            bodyPanel.Size = new System.Drawing.Size(1067, 121);
            bodyPanel.TabIndex = 9;
            // 
            // bodyTextBox
            // 
            bodyTextBox.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            bodyTextBox.BackColor = System.Drawing.Color.RoyalBlue;
            bodyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            bodyTextBox.Font = new System.Drawing.Font("Segoe UI Emoji", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            bodyTextBox.ForeColor = System.Drawing.Color.White;
            bodyTextBox.Location = new System.Drawing.Point(9, 10);
            bodyTextBox.Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            bodyTextBox.Multiline = true;
            bodyTextBox.Name = "bodyTextBox";
            bodyTextBox.ReadOnly = true;
            bodyTextBox.Size = new System.Drawing.Size(1049, 103);
            bodyTextBox.TabIndex = 4;
            bodyTextBox.Text = "Hello there. This is a test for the longer word usage.";
            // 
            // QueueItem
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.SystemColors.ControlLightLight;
            Controls.Add(bodyPanel);
            Margin = new System.Windows.Forms.Padding(6, 5, 6, 5);
            Name = "QueueItem";
            Padding = new System.Windows.Forms.Padding(19, 10, 19, 10);
            Size = new System.Drawing.Size(1111, 141);
            Load += ChatItem_Load;
            bodyPanel.ResumeLayout(false);
            bodyPanel.PerformLayout();
            ResumeLayout(false);
        }

        #endregion
        private System.Windows.Forms.Panel bodyPanel;
		private System.Windows.Forms.TextBox bodyTextBox;
	}
}
