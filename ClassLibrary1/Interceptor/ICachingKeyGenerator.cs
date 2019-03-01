using System.Reflection;

namespace Comm.InterceptorCaching.Interceptor
{
    public interface ICachingKeyGenerator
    {
        string GetCacheKey(MethodInfo serviceMethod, object[] invocationArguments, CachingInterceptorAttribute cachingInterceptorAttribute);
 
    }
}
 
