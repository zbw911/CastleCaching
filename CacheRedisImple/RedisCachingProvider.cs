using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary1;
using News.Comm.RedisHelper;

namespace CacheRedisImple
{
    public class RedisCachingProvider : ICachingProvider
    {

        public RedisCachingProvider()
        {
            RedisConfig.ConfigRedis(new[] { "127.0.0.1:6379" }, null, 100, 100, 1000);
        }


        public T Get<T>(string cacheKey)
        {
            return RedisClientManager.Get<T>(cacheKey);



        }

        public void Remove(string cacheKey)
        {
            RedisClientManager.Remove(cacheKey);
        }

        public void Set<T>(string cacheKey, T returnValue, TimeSpan fromSeconds)
        {
            RedisClientManager.Set<T>(cacheKey, returnValue, fromSeconds);
        }
    }
}
