using OX.Wallets.UI.Docking;
using System.Drawing;

namespace OX.Wallets.UI
{
    public partial class DockDocument : DarkDocument
    {
        #region Constructor Region

        public DockDocument()
        {
            InitializeComponent();
        }

        public DockDocument(string text, Image icon)
            : this()
        {
            DockText = text;
            Icon = icon;
        }

        #endregion

        #region Event Handler Region

        public override void Close()
        {
            //var result = DarkMessageBox.ShowWarning(@"You will lose any unsaved changes. Continue?", @"Close document", DarkDialogButton.YesNo);
            //if (result == DialogResult.No)
            //    return;

            base.Close();
        }

        #endregion
    }
}
