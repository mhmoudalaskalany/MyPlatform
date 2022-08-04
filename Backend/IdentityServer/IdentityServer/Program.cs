using System;
using System.IO;
using Domain.Extensions;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace AuthServer
{
    /// <summary>
    /// Kick Off
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Configuration Properties
        /// </summary>
        public static IConfiguration Configuration { get; } = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production"}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        /// <summary>
        /// Entry Point
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            Log.Logger = BaseLoggerConfiguration
                .CreateLoggerConfiguration(Configuration["ApplicationName"] , Configuration["Environment"])
                .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Error)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
                .WriteToSql(Configuration["LoggingDbConnectionString"])
                .CreateLogger();

            try
            {
                Log.Information("-----Starting web host at Identity Server------");
                CreateHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Fatal(e, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
        /// <summary>
        /// Build Web Host
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        //public static IHostBuilder CreateHostBuilder(string[] args) =>
        //    Host.CreateDefaultBuilder(args)
        //        .ConfigureWebHostDefaults(webBuilder =>
        //        {
        //            webBuilder
        //                .UseHttpSysOrIisIntegration()
        //            .UseStartup<Startup>();
        //        });

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseSerilog();
                    webBuilder
                        .UseStartup<Startup>();
                });
    }

    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// For Self Hosting
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static IWebHostBuilder UseHttpSysOrIisIntegration(this IWebHostBuilder builder)
        {
            if (builder.GetSetting("UseIISIntegration") == null)
            {
                // Self hosted
                builder.UseHttpSys(options =>
                {
                    options.Authentication.Schemes = AuthenticationSchemes.None | AuthenticationSchemes.NTLM |
                                                     AuthenticationSchemes.Negotiate;
                    options.Authentication.AllowAnonymous = true;
                    options.MaxConnections = 100;
                    options.MaxRequestBodySize = 30000000;
                    options.UrlPrefixes.Add("https://localhost:5001");
                    options.UrlPrefixes.Add("http://localhost:5000");
                });
            }

            return builder;
        }
    }
}
