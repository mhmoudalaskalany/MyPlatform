using System.Reflection;
using AuthServer.Config;
using AuthServer.Extensions;
using AutoMapper;
using Data.Context.Identity;
using Data.DataInitializer;
using Data.UnitOfWork;
using Domain.Abstraction.UnitOfWork;
using Domain.Extensions;
using Entities.Entities.Identity;
using IdentityServer4.Services;
using Integration.FileRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetCore.AutoRegisterDi;
using Service.Mapping;
using Service.Services.Base;
using Service.Services.Identity.User;
using Service.Services.Identity.UserApp;
using Service.Services.Validators.Base;

namespace IdentityServer.Api.Extensions
{
    /// <summary>
    /// Dependency Extensions
    /// </summary>
    public static class ConfigureServiceExtension
    {
        private const string ConnectionStringName = "Default";
        /// <summary>
        /// Register Extensions
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.RegisterDbContext(configuration);
            services.RegisterCores();
            services.ConfigureNonBreakingSameSiteCookies();
            services.RegisterIdentity();
            services.ConfigureIIsOptions();
            services.RegisterIdentityServer(configuration);
            services.RegisterIntegrationRepositories();
            services.RegisterAutoMapper();
            services.RegisterValidators();
            services.RegisterCommonServices(configuration);
            return services;
        }

        /// <summary>
        /// Add DbContext
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void RegisterDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<IdentityServerDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString(ConnectionStringName));
            });
            services.AddScoped<DbContext, IdentityServerDbContext>();
            services.AddSingleton<IDataInitializer, DataInitializer>();
        }


        private static void RegisterIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(config =>
                {
                    config.Password.RequiredLength = 4;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<IdentityServerDbContext>();
        }


        private static void ConfigureDatabase(DbContextOptionsBuilder builder, IConfiguration configuration)
        {

            builder.UseSqlServer(configuration.GetConnectionString(ConnectionStringName));

        }
        /// <summary>
        /// Configure IIS Options For Windows Authentication
        /// </summary>
        /// <param name="services"></param>
        private static void ConfigureIIsOptions(this IServiceCollection services)
        {
            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = true;
            });
        }
        private static void RegisterIdentityServer(this IServiceCollection services, IConfiguration configuration)
        {
            
            var env = configuration["Environment"];
            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    
                    options.ConfigureDbContext = config =>
                    {
                        ConfigureDatabase(config, configuration);
                    };
                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 180; // interval in seconds

                })
                .AddProfileService<IdentityClaimsProfileService>()
                .AddInMemoryApiScopes(AuthServer.Config.Config.GetApiScopes())
                .AddInMemoryIdentityResources(AuthServer.Config.Config.GetIdentityResources())
                .AddInMemoryApiResources(AuthServer.Config.Config.GetApiResources())
                .AddInMemoryClients(AuthServer.Config.Config.GetClients(env,configuration))
                .AddAspNetIdentity<User>()
                .GetIdentityServerCertificate(configuration["Certificates:Signing"],
                configuration["Certificates:Password"] , configuration);

            services.ConfigureApplicationCookie((obj) =>
            {
                obj.LoginPath = "/Accounts/Login";
                obj.LogoutPath = "/Accounts/Logout";
            });

            services.AddTransient<IProfileService, IdentityClaimsProfileService>();
        }

        /// <summary>
        /// register auto-mapper
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(MappingService));

        }
      
        /// <summary>
        /// register Integration Repositories
        /// </summary>
        /// <param name="services"></param>
        private static void RegisterIntegrationRepositories(this IServiceCollection services)
        {
            services.AddTransient<IFileRepository, FileRepository>();
        }
        /// <summary>
        /// Register Business Validators
        /// </summary>
        /// <param name="services"></param>
        public static void RegisterValidators(this IServiceCollection services)
        {
            services.AddTransient(typeof(IValidator<>), typeof(Validator<>));
        }

        /// <summary>
        /// Register Main Core
        /// </summary>
        /// <param name="services"></param>

        private static void RegisterCores(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseService<,,,>), typeof(BaseService<,,,>));
            services.AddTransient(typeof(IServiceBaseParameter<>), typeof(ServiceBaseParameter<>));
            services.AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
            var servicesToScan = Assembly.GetAssembly(typeof(UserAppService)); //..or whatever assembly you need
            services.RegisterAssemblyPublicNonGenericClasses(servicesToScan)
                .Where(c => c.Name.EndsWith("Service"))
                .AsPublicImplementedInterfaces();
            services.RegisterAssemblyPublicNonGenericClasses(servicesToScan)
                .Where(c => c.Name.EndsWith("Validator"))
                .AsPublicImplementedInterfaces();
        }
    }
}
