using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.Wallets
{
    public enum WalletBappEventType : int
    {
        EventTransactionEvent = 1 << 0,
        CollectionBoardEvent = 1 << 1
    }
}
