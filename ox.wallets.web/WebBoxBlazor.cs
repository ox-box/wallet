using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AntDesign.ProLayout;
namespace OX.Wallets
{
    public abstract class WebBoxBlazor : WebBox
    {
        public abstract MenuDataItem[] GetMemus();
        public abstract MenuDataItem[] GetMobileMemus();
    }
}
