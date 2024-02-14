using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OX.Network.P2P.Payloads;

namespace OX.Wallets
{
    public class MixUTXO
    {
        public UInt160 Owner;
        public UInt256 AssetId;
        public Fixed8 Amount;
        public bool IsLockCoin;
        public Coin UnlockCoin;
        public KeyValuePair<CoinReference, MyLockAssetMerge> LockCoin;
    }
    public class UTXO
    {
        public long Value { get; set; }
        public UInt160 Address { get; set; }
        public ushort N { get; set; }
        public UInt256 TxId { get; set; }
        public string ToKey()
        {
            return $"{TxId.ToString()}-{N}";
        }
    }
    public class AssetTrustUTXO : UTXO
    {
        public AssetTrustOutput AssetTrustOutput;
    }
    public class EthMapUTXO : UTXO
    {
        public string EthAddress { get; set; }
        public uint LockExpirationIndex { get; set; }
    }
    public static class UTXOHelper
    {
        /// <summary>
        /// 限制UTXO输入的最大项目数，避免因输入项太多导致交易太大而验证失败
        /// </summary>
        public const int MAXTRANSACTIONCOUNT = 20;
        public static List<List<T>> ShardRange<T>(this List<T> list, int rangeLength) where T : UTXO
        {
            var count = list.Count();
            var r = count / rangeLength;
            var y = count % rangeLength;
            List<List<T>> lists = new List<List<T>>();
            for (int i = 0; i < r; i++)
            {
                lists.Add(list.GetRange(i * rangeLength, rangeLength));
            }
            if (y > 0)
                lists.Add(list.GetRange(r * rangeLength, y));
            return lists;
        }
        public static bool SortSearch<T>(this IEnumerable<T> items, long amount, List<string> excludedUtxoKeys, out T[] selectedutxos, out long remainder) where T : UTXO
        {
            List<T> result = new List<T>();
            var utxos = items.Where(m => !excludedUtxoKeys.Contains(m.ToKey())).DistinctBy(m => m.ToKey()).OrderBy(m => m.Value);
            int C = utxos.Count();
            IOrderedEnumerable<T> range = default;
            if (C <= MAXTRANSACTIONCOUNT)
            {
                range = utxos;
            }
            else
            {
                for (int i = 0; i <= C - MAXTRANSACTIONCOUNT; i++)
                {
                    range = utxos.Take(new Range(new Index(i), new Index(i + MAXTRANSACTIONCOUNT))).OrderBy(m => m.Value);
                    if (range.Sum(m => m.Value) >= amount) break;
                }
            }
            if (range.IsNotNullAndEmpty())
            {
                var total = range.Sum(m => m.Value);
                var surplus = amount;
                if (total >= amount)
                {
                    foreach (var item in range.OrderBy(m => m.Value))
                    {
                        if (surplus > 0)
                        {
                            result.Add(item);
                            surplus -= item.Value;
                        }
                        else break;
                    }
                }
                if (surplus <= 0)
                {
                    selectedutxos = result.ToArray();
                    remainder = selectedutxos.Sum(m => m.Value) - amount;
                    excludedUtxoKeys.AddRange(selectedutxos.Select(m => m.ToKey()));
                    return true;
                }
            }
            selectedutxos = default;
            remainder = 0;
            return false;
        }
    }
}
