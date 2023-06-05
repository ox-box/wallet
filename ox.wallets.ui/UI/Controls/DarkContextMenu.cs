using OX.Wallets.UI.Renderers;
using System.Windows.Forms;

namespace OX.Wallets.UI.Controls
{
    public class DarkContextMenu : ContextMenuStrip
    {
        #region Constructor Region

        public DarkContextMenu()
        {
            Renderer = new DarkMenuRenderer();
        }

        #endregion
    }
}
