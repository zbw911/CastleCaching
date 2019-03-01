using CacheRedisImple;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ClassLibrary1;
using Comm.InterceptorCaching;
using Comm.InterceptorCaching.Interceptor;
using EasyCaching.Demo.Interceptors.dao;
using EasyCaching.Demo.Interceptors.Services;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        private WindsorContainer container;
        [TestInitialize]

        public void Init()
        {
            this.container = new WindsorContainer();
            container.Register(Component.For<CachingInterceptor>());
            container.Register(Component.For<ICachingKeyGenerator>().ImplementedBy<DefaultCachingKeyGenerator>());
            container.Register(Component.For<ICachingProvider>().ImplementedBy<RedisCachingProvider>());


            container.Register(Component.For<IPerson>().ImplementedBy<Person>().Interceptors<CachingInterceptor>());
            container.Register(Component.For<IAspectCoreService>().ImplementedBy<AspectCoreService>().Interceptors<CachingInterceptor>());
            container.Register(Component.For<IDatas>().ImplementedBy<Datas>().Interceptors<CachingInterceptor>());
            //container.Register(Component.For<HomeController>().Interceptors<CachingInterceptor>());

        }

        [TestMethod]
        public void TestMethod1()
        {
            var k = container.Resolve<IAspectCoreService>();
            System.Console.WriteLine(k.GetCurrentUtcTime());
        }
    }
}
