using System;
using System.Collections.Generic;
using System.Text;
using ClassLibrary1;
using News.Comm.RedisHelper;

namespace CacheRedisImple
{

    public class RedisCachingProvider : ICachingProvider
    {
        private readonly ICachingSerializer _cachingSerializer;

        public RedisCachingProvider(ICachingSerializer cachingSerializer)
        {
            _cachingSerializer = cachingSerializer;
            RedisConfig.ConfigRedis(new[] { "127.0.0.1:6379" }, null, 100, 100, 1000);
        }

        public RedisCachingProvider() : this(new JsonCachingSerializer())
        {

        }


        public object Get(string cacheKey, Type type)
        {

            var jsonstr = RedisClientManager.Get<String>(cacheKey);

            if (jsonstr == null)
            {
                return null;
            }

            return _cachingSerializer.Deserialize(jsonstr, type);
        }

        public void Remove(string cacheKey)
        {
            RedisClientManager.Remove(cacheKey);
        }

        public void Set<T>(string cacheKey, T returnValue, TimeSpan fromSeconds)
        { 
            RedisClientManager.Set(cacheKey, _cachingSerializer.Serialize(returnValue), fromSeconds);
        }
    }
}
