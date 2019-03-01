using System.Reflection;
using Caching.Core.Interceptor;

namespace ClassLibrary1.Interceptor
{
    public interface ICachingKeyGenerator
    {
        string GetCacheKey(MethodInfo serviceMethod, object[] invocationArguments, CachingInterceptorAttribute cachingInterceptorAttribute);
        string GetCacheKeyPrefix(MethodInfo serviceMethod, CachingInterceptorAttribute cachingInterceptorAttribute);
    }
}
 
