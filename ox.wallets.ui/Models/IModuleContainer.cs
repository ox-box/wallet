using OX.Wallets.UI.Controls;
using OX.Wallets.UI.Docking;
using System.Collections.Generic;
using System.Windows.Forms;


namespace OX.Wallets
{
    public interface IModuleContainer
    {
        DarkMenuStrip TopMenus { get; }
        ToolStripStatusLabel ModuleStatusLabel { get; }
        DarkDockPanel DockPanel { get; }
        List<DarkDockContent> ToolWindows { get; }
        DockPanelState DockPanelState { get; }
        string WebApiUrl { get; set; }
    }
}
