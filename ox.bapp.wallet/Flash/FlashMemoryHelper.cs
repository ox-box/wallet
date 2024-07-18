using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OX.Wallets.Flash
{
    class RangeCacheItem<T> where T : class
    {
        public T Data;
        public uint Timestamp;
    }
    public class RangeCache<T> where T : class
    {
        Dictionary<uint, RangeCacheItem<T>> cache = new Dictionary<uint, RangeCacheItem<T>>();
        public T Get(uint range, Func<uint, T> func)
        {
            T data = default;
            var ts = DateTime.Now.ToTimestamp();
            if (!cache.TryGetValue(range, out var value) || value.Timestamp + 30 < ts)
            {
                var f = func(range);
                if (f.IsNotNull())
                {
                    data = f;
                    cache[range] = new RangeCacheItem<T> { Timestamp = ts, Data = f };
                }
            }
            else
            {
                data = value.Data;
            }
            return data;
        }
    }
    public static class RangeCacheHelper<T> where T : class
    {
        static Dictionary<string, RangeCache<T>> CachePool = new Dictionary<string, RangeCache<T>>();
        public static RangeCache<T> GetCachePool(string poolName)
        {
            if (!CachePool.TryGetValue(poolName, out var value))
            {
                value = new RangeCache<T>();
                CachePool[poolName] = value;
            }
            return value;
        }
    }
    public static class FlashMemoryHelper
    {
        public const uint RangeSize = 10;
        public static uint LastQueueTimeStamp;
        public static BoundedQueue<UInt256> FlashHashs = new BoundedQueue<UInt256>(10000);
        public static BoundedQueue<Tuple<TalkLineKey, FlashUnicastRecord>> UnicastQueue = new BoundedQueue<Tuple<TalkLineKey, FlashUnicastRecord>>(10000);
        public static BoundedQueue<Tuple<TalkLineKey, FlashMulticastRecord>> MulticastQueue = new BoundedQueue<Tuple<TalkLineKey, FlashMulticastRecord>>(10000);
    }
}
