using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AntDesign;
using AntDesign.ProLayout;
using OX.Bapps;

namespace OX.Wallets
{
    public class WebBoxBuilder: WebBoxBuilderBase
    {
        public static MenuDataItem[] Menus { get { return WebBox.Boxes.OrderBy(m => m.BoxIndex).SelectMany(m => (m as WebBoxBlazor).GetMemus()).ToArray(); } }
       
    }
}
