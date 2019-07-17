using System;
using System.Runtime.Caching;
using NLog;

namespace VPC.Cache
{
    public class Cache : ICache
    {
        private static readonly Logger Log = LogManager.GetCurrentClassLogger();


        private readonly ObjectCache _backEndCache;

        private readonly TimeSpan? _expireAfter;

        public Cache(TimeSpan? expireAfter = null)
            : this(MemoryCache.Default, expireAfter)
        {
            
        }

        internal Cache(ObjectCache cacheImplementation, TimeSpan? expireAfter = null)
        {
            _backEndCache = cacheImplementation;

            _expireAfter = expireAfter;
        }

        #region ICache Members

        public T Get<T>(string key, Func<T> retriever = null, TimeSpan? expireAfter = null)
        {
            string realKey = RealKey(key, typeof (T));

            if (_backEndCache.Contains(realKey))
            {
                return (T) _backEndCache.Get(realKey);
            }

            if (retriever == null)
            {
                throw new ApplicationException(String.Format("Cache miss on key '{0}' and no value retriever given.",
                                                             key));
            }

            T item = retriever();

            Set(key, item, expireAfter);

            return item;
        }


        public void Set(string key, object item, TimeSpan? expireAfter = null)
        {
            // nullvalues are not cached.

            if (item == null)

                return;


            Log.Trace("Setting cache item {0} with key '{1}' into cache with expireTime = {2}. Stats: {3}", item, key,
                      expireAfter, GetCacheStats());


            var policy = new CacheItemPolicy

                             {
                                 RemovedCallback =
                                     arguments =>
                                     Log.Trace("Remove cache Item: {0}, Reason: {1}, Source: {2}",
                                               arguments.CacheItem.Key,
                                               arguments.RemovedReason, arguments.Source.Name)
                             };


            if (expireAfter.HasValue)
            {
                policy.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(expireAfter.Value.TotalMinutes);

                _backEndCache.Set(RealKey(key, item.GetType()), item, policy);
            }

            else if (_expireAfter.HasValue)
            {
                policy.AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(_expireAfter.Value.TotalMinutes);

                _backEndCache.Set(RealKey(key, item.GetType()), item, policy);
            }

            else
            {
                _backEndCache.Set(RealKey(key, item.GetType()), item, policy);
            }
        }


        public bool Contains<T>(string key)
        {
            return _backEndCache.Contains(RealKey(key, typeof (T)));
        }


        public void Remove<T>(string key)
        {
            _backEndCache.Remove(RealKey(key, typeof (T)));
        }

        #endregion

        public void RemoveAll(string token)
        {
            MemoryCache.Default.Trim(100);

            foreach (var element in _backEndCache)
            {
                if (element.Key.Contains(token))
                    MemoryCache.Default.Remove(element.Key);
            }
            //MemoryCache.Default.Dispose();
        }

        public void RemoveAll()
        {
            MemoryCache.Default.Dispose();
        }

        private string GetCacheStats()
        {
            var cache = _backEndCache as MemoryCache;


            if (cache == null)

                return "Cache is not MemoryCache";


            return string.Format("Count: {0}, CacheMemLimit: {1} MB, PhysMemLimit: {2}",
                                 cache.GetCount(), cache.CacheMemoryLimit, cache.PhysicalMemoryLimit);
        }

        private static string RealKey(string key, Type type)
        {
            return type.FullName + "_" + key;
        }
    }
}