using System;

namespace Comm.InterceptorCaching
{
    public interface ICachingProvider
    {
        object Get(string cacheKey, Type type);

        void Remove(string cacheKey);
        void Set<T>(string cacheKey, T returnValue, TimeSpan fromSeconds);
    }


}