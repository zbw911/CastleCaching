using System;
using System.Collections.Generic;

using System.Text;
using Caching.Interceptor.Castle;
using Castle.Core;
using Castle.MicroKernel.Registration;
using Castle.Windsor;

namespace ClassLibrary1
{
    public class testWinsor
    {
        public static void initWinsor()
        {
            // Registering
            var container = new WindsorContainer();
            container.Register(Component.For<CachingInterceptor>());
            container.Register(Component.For<IPerson>().ImplementedBy<Person>().Interceptors<CachingInterceptor>());

            
            // Resolving
            var logger = container.Resolve<IPerson>();
            Console.WriteLine(logger.Go(1));
        }
    }


    public interface ILogger
    {
        void Log(string message);
    }

    
    public class Logger : ILogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
