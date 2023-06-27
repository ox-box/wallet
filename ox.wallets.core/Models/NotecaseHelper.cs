using OX.Ledger;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace OX.Wallets
{
    public static class NotecaseHelper
    {
        public static bool TryGetBalance(this INotecase notecase,UInt160 account, UInt256 assetId, out Fixed8 balance)
        {
            balance = Fixed8.Zero;
         
            if (notecase.IsNull()) return false;
            if (notecase.Wallet.IsNull()) return false;
            var coins = notecase.Wallet.GetCoins().Where(p => !p.State.HasFlag(CoinState.Spent)) ?? Enumerable.Empty<Coin>();
            var balances = coins.Where(p => p.Output.AssetId.Equals(assetId)).GroupBy(p => p.Output.ScriptHash).ToDictionary(p => p.Key, p => p.Sum(i => i.Output.Value));
            var amount = balances.ContainsKey(account) ? balances[account] : Fixed8.Zero;
            return balances.TryGetValue(account, out balance);
        }
    }
}
