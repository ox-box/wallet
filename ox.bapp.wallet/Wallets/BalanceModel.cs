using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.Wallets.Base.Wallets
{
    public class BalanceModel
    {
        public bool OnlyWatch;
        public string Address;
        public Fixed8 GTS;
        public Fixed8 GTC;
        public string Label;
    }
}
