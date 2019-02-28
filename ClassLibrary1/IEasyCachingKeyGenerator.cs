using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ClassLibrary1
{
    public interface ICachingKeyGenerator
    {
        string GetCacheKey(MethodInfo serviceMethod, object[] invocationArguments, string attributeCacheKeyPrefix);
        string GetCacheKeyPrefix(MethodInfo serviceMethod, string attributeCacheKeyPrefix);
    }
}
