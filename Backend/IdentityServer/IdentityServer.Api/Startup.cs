using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AuthServer.Extensions;
using Domain.Exceptions;
using IdentityServer.Api.Extensions;
using Service.DependencyExtension;

namespace AuthServer
{
    /// <summary>
    /// Start Up
    /// </summary>
    public class Startup
    {
        private readonly Shell _shell;
        /// <summary>
        /// Public Property for configuration
        /// </summary>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Public Property for environment
        /// </summary>
        public IWebHostEnvironment Environment { get; }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="environment"></param>
        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
            _shell = (Shell)Activator.CreateInstance(typeof(Shell)) ?? new Shell();

        }

        /// <summary>
        /// Configure Services
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.RegisterServices(Configuration);
            services.AddControllersWithViews();
        }

       /// <summary>
       /// Configure Pipeline
       /// </summary>
       /// <param name="app"></param>
       /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _shell.ConfigureHttp(app, env);

            Shell.Start(_shell);

            app.ConfigureCustomExceptionMiddleware();

            app.Configure(env, configuration: Configuration);
            
            app.UseRouting();

            app.UseCookiePolicy(new CookiePolicyOptions { MinimumSameSitePolicy = SameSiteMode.Lax });

            app.UseStaticFiles();

            app.UseCors(options => options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

            app.UseIdentityServer();

            //if (env.IsDevelopment())
            //{
            //    app.UseCookiePolicy(new CookiePolicyOptions()
            //    {
            //        MinimumSameSitePolicy = SameSiteMode.Lax
            //    });
            //}

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
