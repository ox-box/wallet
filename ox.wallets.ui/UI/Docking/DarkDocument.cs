using OX.Wallets.UI.Config;
using System.ComponentModel;

namespace OX.Wallets.UI.Docking
{
    [ToolboxItem(false)]
    public class DarkDocument : DarkDockContent
    {
        #region Property Region

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new DarkDockArea DefaultDockArea
        {
            get { return base.DefaultDockArea; }
        }

        #endregion

        #region Constructor Region

        public DarkDocument()
        {
            BackColor = Colors.GreyBackground;
            base.DefaultDockArea = DarkDockArea.Document;
        }

        #endregion
    }
}
