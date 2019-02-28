using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public interface ICachingProvider
    {
        T Get<T>(string cacheKey);

        void Remove(string cacheKey);
        void Set<T>(string cacheKey, T returnValue, TimeSpan fromSeconds);
    }


}