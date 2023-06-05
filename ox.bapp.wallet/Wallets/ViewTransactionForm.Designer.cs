
namespace OX.Wallets.Base
{
    partial class ViewTransactionForm
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
            this.darkTreeView1 = new OX.Wallets.UI.Controls.DarkTreeView();
            this.SuspendLayout();
            // 
            // darkTreeView1
            // 
            this.darkTreeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.darkTreeView1.Location = new System.Drawing.Point(0, 0);
            this.darkTreeView1.MaxDragChange = 20;
            this.darkTreeView1.Name = "darkTreeView1";
            this.darkTreeView1.Size = new System.Drawing.Size(603, 616);
            this.darkTreeView1.TabIndex = 0;
            this.darkTreeView1.Text = "darkTreeView1";
            // 
            // ViewTransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 616);
            this.Controls.Add(this.darkTreeView1);
            this.Name = "ViewTransactionForm";
            this.Text = "ViewTransactionForm";
            this.Load += new System.EventHandler(this.ViewTransactionForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private UI.Controls.DarkTreeView darkTreeView1;
    }
}