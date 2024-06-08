using OX.Ledger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OX.Wallets
{

    public class BoundedQueue<T>
    {
        private readonly ReaderWriterLockSlim _txRwLock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
        private Queue<T> queue;
        private int maxCapacity;

        public BoundedQueue(int capacity)
        {
            queue = new Queue<T>();
            maxCapacity = capacity;
        }
        public IEnumerable<T> Query(Func<T, bool> predicate)
        {
            _txRwLock.EnterReadLock();
            try
            {
                return queue.Where(predicate);
            }
            finally
            {
                _txRwLock.ExitReadLock();
            }
           
        }
        public T FirstOrDefault(Func<T, bool> predicate)
        {
            _txRwLock.EnterReadLock();
            try
            {
                return queue.FirstOrDefault(predicate);
            }
            finally
            {
                _txRwLock.ExitReadLock();
            }
           
        }
        public void Enqueue(T item)
        {
            _txRwLock.EnterReadLock();
            try
            {
                if (queue.Count >= maxCapacity)
                {
                    queue.Dequeue();
                }
                queue.Enqueue(item);
            }
            finally
            {
                _txRwLock.ExitReadLock();
            }
        }

        public T Dequeue()
        {
            _txRwLock.EnterReadLock();
            try
            {
                return queue.Dequeue();
            }
            finally
            {
                _txRwLock.ExitReadLock();
            }
        }
        public bool Contains(T item)
        {
            _txRwLock.EnterReadLock();
            try
            {
                return queue.Contains(item);
            }
            finally
            {
                _txRwLock.ExitReadLock();
            }
        }
        public int Count => queue.Count;

        public bool IsFull => queue.Count >= maxCapacity;
    }

}
