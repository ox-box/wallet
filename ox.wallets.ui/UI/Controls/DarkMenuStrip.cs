﻿using OX.Wallets.UI.Renderers;
using System.Windows.Forms;

namespace OX.Wallets.UI.Controls
{
    public class DarkMenuStrip : MenuStrip
    {
        #region Constructor Region

        public DarkMenuStrip()
        {
            Renderer = new DarkMenuRenderer();
            Padding = new Padding(3, 2, 0, 2);
        }

        #endregion
    }
}
