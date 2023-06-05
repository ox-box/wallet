using OX.Cryptography.ECC;
using OX.IO.Json;
using OX.Ledger;
using OX.Network.P2P.Payloads;
using OX.Persistence;
using OX.VM;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using OX.SmartContract;
using OX.Wallets;
using OX.Wallets.NEP6;

namespace OX.Wallets.Base
{

    public static class OXSHelper
    {
        public static Fixed8 CalculateBonusSpend(IEnumerable<LockOXS> unclaimed)
        {
            Fixed8 amount_claimed = Fixed8.Zero;
            foreach (var group in unclaimed.GroupBy(p => new { p.Index, p.SpendIndex }))
            {
                long amount = 0;
                long ustart = group.Key.Index / Blockchain.DecrementInterval;
                if (ustart < Blockchain.GenerationBonusAmount.Length)
                {
                    long istart = group.Key.Index % Blockchain.DecrementInterval;
                    long uend = group.Key.SpendIndex / Blockchain.DecrementInterval;
                    long iend = group.Key.SpendIndex % Blockchain.DecrementInterval;
                    if (uend >= Blockchain.GenerationBonusAmount.Length)
                    {
                        uend = (uint)Blockchain.GenerationBonusAmount.Length;
                        iend = 0;
                    }
                    if (iend == 0)
                    {
                        uend--;
                        iend = Blockchain.DecrementInterval;
                    }
                    while (ustart < uend)
                    {
                        amount += (Blockchain.DecrementInterval - istart) * Blockchain.Singleton.CurrentSnapshot.GetGenerationAmount((uint)ustart);
                        ustart++;
                        istart = 0;
                    }
                    amount += (iend - istart) * Blockchain.Singleton.CurrentSnapshot.GetGenerationAmount((uint)ustart);
                }
                var hash = Blockchain.Singleton.GetBlockHash(group.Key.SpendIndex - 1);
                var blockstate = Blockchain.Singleton.CurrentSnapshot.Blocks.TryGet(hash);
                long f2 = 0;
                if (group.Key.Index != 0)
                {
                    var hash2 = Blockchain.Singleton.GetBlockHash(group.Key.Index - 1);
                    var blockstate2 = Blockchain.Singleton.CurrentSnapshot.Blocks.TryGet(hash2);
                    f2 = blockstate2.SystemFeeAmount;
                }
                amount += (uint)(blockstate.SystemFeeAmount - f2);
                amount_claimed += group.Sum(p => p.Output.Value) / 100000000 * amount;
            }
            return amount_claimed;
        }
        public static Fixed8 CalculateBonusUnspend(IEnumerable<LockOXS> unclaimed, long Height)
        {
            Fixed8 amount_claimed = Fixed8.Zero;
            foreach (var group in unclaimed.GroupBy(p => p.Index))
            {
                long amount = 0;
                long ustart = group.Key / Blockchain.DecrementInterval;
                if (ustart < Blockchain.GenerationBonusAmount.Length)
                {
                    long istart = group.Key % Blockchain.DecrementInterval;
                    long uend = Height / Blockchain.DecrementInterval;
                    long iend = Height % Blockchain.DecrementInterval;
                    if (uend >= Blockchain.GenerationBonusAmount.Length)
                    {
                        uend = (uint)Blockchain.GenerationBonusAmount.Length;
                        iend = 0;
                    }
                    if (iend == 0)
                    {
                        uend--;
                        iend = Blockchain.DecrementInterval;
                    }
                    while (ustart < uend)
                    {
                        amount += (Blockchain.DecrementInterval - istart) * Blockchain.Singleton.CurrentSnapshot.GetGenerationAmount((uint)ustart);
                        ustart++;
                        istart = 0;
                    }
                    amount += (iend - istart) * Blockchain.Singleton.CurrentSnapshot.GetGenerationAmount((uint)ustart);
                }
                var hash = Blockchain.Singleton.GetBlockHash((uint)Height - 1);
                var blockstate = Blockchain.Singleton.CurrentSnapshot.Blocks.TryGet(hash);
                long f2 = 0;
                if (group.Key != 0)
                {
                    var hash2 = Blockchain.Singleton.GetBlockHash((uint)group.Key - 1);
                    var blockstate2 = Blockchain.Singleton.CurrentSnapshot.Blocks.TryGet(hash2);
                    f2 = blockstate2.SystemFeeAmount;
                }
                amount += (uint)(blockstate.SystemFeeAmount - f2);
                amount_claimed += group.Sum(p => p.Output.Value) / 100000000 * amount;
            }
            return amount_claimed;
        }
    }
}
