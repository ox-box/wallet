using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.Wallets
{
    public interface IWebBox
    {
        string Name { get; }
        bool SupportMobile { get; }
    }
}
