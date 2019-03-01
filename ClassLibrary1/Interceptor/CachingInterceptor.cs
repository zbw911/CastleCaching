using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Caching.Core.Interceptor;
using Castle.DynamicProxy;
using EasyCaching.Interceptor.Castle;

namespace ClassLibrary1.Interceptor
{
    /// <summary>
    /// caching interceptor.
    /// </summary>
    public class CachingInterceptor : IInterceptor
    {
        /// <summary>
        /// The cache provider.
        /// </summary>
        private readonly ICachingProvider _cacheProvider;

        /// <summary>
        /// The key generator.
        /// </summary>
        private readonly ICachingKeyGenerator _keyGenerator;

        /// <summary>
        /// The typeof task result method.
        /// </summary>
        private static readonly ConcurrentDictionary<Type, MethodInfo>
                    TypeofTaskResultMethod = new ConcurrentDictionary<Type, MethodInfo>();

        /// <summary>
        /// Initializes a new instance of the <see cref="T:ClassLibrary1.Interceptor.CachingInterceptor"/> class.
        /// </summary>
        /// <param name="cacheProvider">Cache provider.</param>
        /// <param name="keyGenerator">Key generator.</param>
        public CachingInterceptor(ICachingProvider cacheProvider, ICachingKeyGenerator keyGenerator)
        {
            _cacheProvider = cacheProvider;
            _keyGenerator = keyGenerator;
        }

        /// <summary>
        /// Intercept the specified invocation.
        /// </summary>
        /// <returns>The intercept.</returns>
        /// <param name="invocation">Invocation.</param>
        public void Intercept(IInvocation invocation)
        {
            //Process any early evictions 
            ProcessEvict(invocation, true);

            //Process any cache interceptor 
            ProceedAble(invocation);

            // Process any put requests
            ProcessPut(invocation);

            // Process any late evictions
            ProcessEvict(invocation, false);
        }

        /// <summary>
        /// Proceeds the able.
        /// </summary>
        /// <param name="invocation">Invocation.</param>
        private void ProceedAble(IInvocation invocation)
        {
            var serviceMethod = invocation.Method ?? invocation.MethodInvocationTarget;

            if (serviceMethod.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(CachingAbleAttribute)) is CachingAbleAttribute attribute)
            {
                var returnType = serviceMethod.IsReturnTask()
                        ? serviceMethod.ReturnType.GetGenericArguments().First()
                        : serviceMethod.ReturnType;

                var cacheKey = _keyGenerator.GetCacheKey(serviceMethod, invocation.Arguments, attribute.CacheKeyPrefix);

                var d1 = typeof(CacheValue<>);
                Type[] typeArgs = { returnType };
                var cachevaluetype = d1.MakeGenericType(typeArgs);

                var cacheValue = (_cacheProvider.Get(cacheKey,   cachevaluetype));

                //typeof(CacheValue).GetMethods()
            
               


                if (cacheValue != null)
                {
                    

                    PropertyInfo prop = cachevaluetype.GetProperty("Value");

                    object value = prop.GetValue(cacheValue);

                    if (serviceMethod.IsReturnTask())
                    {
                        invocation.ReturnValue =
                            TypeofTaskResultMethod.GetOrAdd(returnType,
                                    t => typeof(Task).GetMethods()
                                    .First(p => p.Name == "FromResult" && p.ContainsGenericParameters)
                                    .MakeGenericMethod(returnType)
                                    ).Invoke(null, new object[] { value });

                    }
                    else
                    {
                        invocation.ReturnValue = value;
                    }
                }
                else
                {
                    // Invoke the method if we don't have a cache hit                    
                    invocation.Proceed();

                    if (!string.IsNullOrWhiteSpace(cacheKey) && invocation.ReturnValue != null)
                    {
                        if (serviceMethod.IsReturnTask())
                        {
                            //get the result
                            var returnValue = invocation.UnwrapAsyncReturnValue().Result;
                            var newcacheValue = new CacheValue<object>(returnValue, returnValue != null, TimeSpan.FromSeconds(attribute.Expiration));
                            _cacheProvider.Set(cacheKey, newcacheValue, TimeSpan.FromSeconds(attribute.Expiration));
                        }
                        else
                        {
                            var returnValue = invocation.ReturnValue;
                            var newcacheValue = new CacheValue<object>(returnValue, returnValue != null, TimeSpan.FromSeconds(attribute.Expiration));
                            _cacheProvider.Set(cacheKey, newcacheValue, TimeSpan.FromSeconds(attribute.Expiration));
                        }
                    }

                }
            }
            else
            {
                // Invoke the method if we don't have CachingAbleAttribute
                invocation.Proceed();
            }
        }

        /// <summary>
        /// Processes the put.
        /// </summary>
        /// <param name="invocation">Invocation.</param>
        private void ProcessPut(IInvocation invocation)
        {
            var serviceMethod = invocation.Method ?? invocation.MethodInvocationTarget;

            if (serviceMethod.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(CachingPutAttribute)) is CachingPutAttribute attribute && invocation.ReturnValue != null)
            {
                var cacheKey = _keyGenerator.GetCacheKey(serviceMethod, invocation.Arguments, attribute.CacheKeyPrefix);

                if (serviceMethod.IsReturnTask())
                {
                    //get the result
                    var returnValue = invocation.UnwrapAsyncReturnValue().Result;

                    var cacheValue = new CacheValue<object>(returnValue, returnValue != null, TimeSpan.FromSeconds(attribute.Expiration));

                    _cacheProvider.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(attribute.Expiration));
                }
                else
                {
                    var returnValue = invocation.ReturnValue;
                    var cacheValue = new CacheValue<object>(returnValue, returnValue != null, TimeSpan.FromSeconds(attribute.Expiration));
                    _cacheProvider.Set(cacheKey, cacheValue, TimeSpan.FromSeconds(attribute.Expiration));
                }
            }
        }

        /// <summary>
        /// Processes the evict.
        /// </summary>
        /// <param name="invocation">Invocation.</param>
        /// <param name="isBefore">If set to <c>true</c> is before.</param>
        private void ProcessEvict(IInvocation invocation, bool isBefore)
        {
            var serviceMethod = invocation.Method ?? invocation.MethodInvocationTarget;

            if (serviceMethod.GetCustomAttributes(true).FirstOrDefault(x => x.GetType() == typeof(CachingEvictAttribute)) is CachingEvictAttribute attribute && attribute.IsBefore == isBefore)
            {

                //If not all , just remove the cached item by its cachekey.
                var cacheKey = _keyGenerator.GetCacheKey(serviceMethod, invocation.Arguments, attribute.CacheKeyPrefix);

                _cacheProvider.Remove(cacheKey);

            }
        }
    }
}
