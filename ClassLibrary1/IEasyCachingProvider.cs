using System;
using System.Collections.Generic;
using System.Text;

namespace ClassLibrary1
{
    public interface ICachingProvider
    {
        object Get(string cacheKey);

        void Remove(string cacheKey);
        void Set(string cacheKey, object returnValue, TimeSpan fromSeconds);
    }
}