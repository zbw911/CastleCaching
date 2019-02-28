using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CacheRedisImple;
using Castle.Facilities.AspNetCore;
using Castle.Windsor;
using Castle.Windsor.Installer;

using CastleTEST.Controllers;
using ClassLibrary1;
using ClassLibrary1.Interceptor;
using EasyCaching.Demo.Interceptors.dao;
using EasyCaching.Demo.Interceptors.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Component = Castle.MicroKernel.Registration.Component;

namespace CastleTEST
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //services.AddWindsor(container, opts =>
            //{
            //    opts.RegisterControllers(typeof(HomeController).Assembly, LifestyleType.Transient);

            //});

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            var container = new WindsorContainer();
            container.Register(Component.For<CachingInterceptor>());
            container.Register(Component.For<ICachingKeyGenerator>().ImplementedBy<DefaultCachingKeyGenerator>());
            container.Register(Component.For<ICachingProvider>().ImplementedBy<RedisCachingProvider>());


            container.Register(Component.For<IPerson>().ImplementedBy<Person>().Interceptors<CachingInterceptor>());
            container.Register(Component.For<IAspectCoreService>().ImplementedBy<AspectCoreService>().Interceptors<CachingInterceptor>());
            container.Register(Component.For<IDatas>().ImplementedBy<Datas>().Interceptors<CachingInterceptor>());
            container.Register(Component.For<HomeController>().Interceptors<CachingInterceptor>());




            // Custom application component registrations, ordering is important here
            RegisterApplicationComponents(services);

            // Castle Windsor integration, controllers, tag helpers and view components, this should always come after RegisterApplicationComponents
            return services.AddWindsor(container,
                opts => opts.UseEntryAssembly(typeof(HomeController).Assembly), // <- Recommended
                () => services.BuildServiceProvider(validateScopes: false)); // <- Optional


        }


        private void RegisterApplicationComponents(IServiceCollection services)
        {
            //// Application components
            //Container.Register(Component.For<IHttpContextAccessor>().ImplementedBy<HttpContextAccessor>());
            //Container.Register(Component.For<IScopedDisposableService>().ImplementedBy<ScopedDisposableService>().LifestyleScoped().IsDefault());
            //Container.Register(Component.For<ITransientDisposableService>().ImplementedBy<TransientDisposableService>().LifestyleTransient().IsDefault());
            //Container.Register(Component.For<ISingletonDisposableService>().ImplementedBy<SingletonDisposableService>().LifestyleSingleton().IsDefault());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }


}
