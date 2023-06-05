using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.Wallets
{
    public class TransferRequest
    {
        public UInt160 From { get; set; }
        public UInt160 To { get; set; }
        public UInt256 Asset { get; set; }
        public Fixed8 Amount { get; set; }
    }
}
