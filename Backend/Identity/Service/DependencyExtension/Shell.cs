using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace Service.DependencyExtension
{
    public class Shell : IDisposable
    {
        private IServiceProvider _rootProvider;
        protected static Shell App;

        public static IServiceProvider RootInjector => App.RootProvider;

        public Shell()
        {

        }
        /// <summary>
        /// Configure App Services
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public virtual void ConfigureHttp(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _rootProvider = app.ApplicationServices;
        }
        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
            // dispose instances here
        }

        /// <summary>
        /// Get DI Instance
        /// </summary>
        protected virtual IServiceProvider RootProvider
        {
            get { return _rootProvider ??= MakeProvider(); }
        }

        /// <summary>
        /// Create DI Container
        /// </summary>
        /// <returns></returns>
        private static IServiceProvider MakeProvider()
        {
            ServiceCollection collection = new ServiceCollection();
            // register services here
            return collection.BuildServiceProvider();
        }

        /// <summary>
        /// Create Scope
        /// </summary>
        /// <returns></returns>
        public static IServiceScope GetScope()
        {
            var sc = RootInjector.CreateScope();
            return sc;
        }
        /// <summary>
        /// Start Shell Class
        /// </summary>
        /// <param name="shell"></param>
        public static void Start(Shell shell)
        {
            App = shell;
        }
    }
}
