namespace Caching.Core.Interceptor
{
    using System;

    /// <summary>
    /// caching interceptor attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingInterceptorAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets a value indicating whether is hybrid provider.
        /// </summary>
        /// <value><c>true</c> if is hybrid provider; otherwise, <c>false</c>.</value>
        public bool IsHybridProvider { get; set; } = false;

        /// <summary>
        /// Gets or sets the cache key prefix. 
        /// </summary>
        /// <value>The cache key prefix.</value>
        public string CacheKeyPrefix { get; set; } = string.Empty;
    }

    /// <summary>
    /// caching able attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingAbleAttribute : CachingInterceptorAttribute
    {
        /// <summary>
        /// Gets or sets the expiration. The default value is 30 second.
        /// </summary>
        /// <value>The expiration.</value>
        public int Expiration { get; set; } = 30;
    }

    /// <summary>
    /// caching put attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingPutAttribute : CachingInterceptorAttribute
    {
        /// <summary>
        /// Gets or sets the expiration. The default value is 30 second.
        /// </summary>
        /// <value>The expiration.</value>
        public int Expiration { get; set; } = 30;
    }

    /// <summary>
    /// caching evict attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = true)]
    public class CachingEvictAttribute : CachingInterceptorAttribute
    {        
        

        /// <summary>
        /// Gets or sets a value indicating whether is before.
        /// </summary>
        /// <value><c>true</c> if is before; otherwise, <c>false</c>.</value>
        public bool IsBefore { get; set; } = false;
    }
}
