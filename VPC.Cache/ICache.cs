using System;

namespace VPC.Cache
{
    public interface ICache
    {
        T Get<T>(string key, Func<T> retriever = null, TimeSpan? expireAfter = null);

        void Remove<T>(string key);

        void Set(string key, object item, TimeSpan? expireAfter = null);

        bool Contains<T>(string key); 
    }
}