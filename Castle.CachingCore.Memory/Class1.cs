using System;
using System.Runtime.Caching;
using Microsoft.Extensions.Caching.Memory;
using MemoryCache = System.Runtime.Caching.MemoryCache;


namespace Castle.CachingCore.Memory
{
    public class MemoryCachingProvider : Comm.InterceptorCaching.ICachingProvider
    {
        private readonly MemoryCache _cache = MemoryCache.Default;


        public object Get(string cacheKey, Type type)
        {
            //进程内，没有类型转换的问题
            return _cache.Get(cacheKey);
        }

        public void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public void Set<T>(string cacheKey, T returnValue, TimeSpan fromSeconds)
        {
            var policy = new CacheItemPolicy { SlidingExpiration = fromSeconds };

            _cache.Add(cacheKey, returnValue, policy);
        }
    }
     
}
