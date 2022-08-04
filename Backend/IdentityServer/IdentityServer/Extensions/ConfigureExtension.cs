using System;
using Data.Context.Identity;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace AuthServer.Extensions
{
    /// <summary>
    /// Pipeline Extensions
    /// </summary>
    public static class ConfigureExtension
    {
        /// <summary>
        /// General Configuration Method
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IApplicationBuilder Configure(this IApplicationBuilder app, IWebHostEnvironment env, IConfiguration configuration)
        {
            app.CreateDatabase();
            app.ConfigureCors();
            app.SwaggerConfig(configuration);
            return app;
        }
        /// <summary>
        /// Configure Cors
        /// </summary>
        /// <param name="app"></param>
        public static void ConfigureCors(this IApplicationBuilder app)
        {
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
        }
        /// <summary>
        /// Create Database From Migration
        /// </summary>
        /// <param name="app"></param>
        public static void CreateDatabase(this IApplicationBuilder app)
        {
            try
            {
                using var scope =
                    app.ApplicationServices.CreateScope();
                using var context = scope.ServiceProvider.GetService<IdentityServerDbContext>();
                context.Database.Migrate();
                using var context2 = scope.ServiceProvider.GetService<PersistedGrantDbContext>();
                context2.Database.Migrate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }

        /// <summary>
        /// User Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configuration"></param>
        private static void SwaggerConfig(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                string endPoint = configuration["SwaggerConfig:EndPoint"];
                string title = configuration["SwaggerConfig:Title"];
                c.SwaggerEndpoint(endPoint, title);
                c.DocumentTitle = $"{title} Documentation";
                c.DocExpansion(DocExpansion.None);
            });
        }
    }
}
