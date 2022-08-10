using System.Reflection;
using AutoMapper;
using Data.Context.Identity;
using Data.DataInitializer;
using Data.UnitOfWork;
using Domain.Abstraction.UnitOfWork;
using Domain.Extensions;
using Entities.Entities.Identity;
using Integration.FileRepository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using NetCore.AutoRegisterDi;
using Service.Mapping;
using Service.Services.Base;
using Service.Services.Identity.UserApp;
using Service.Services.Validators.Base;
using UserManagement.Api.Authorization.Handlers;
using UserManagement.Api.Authorization.Providers;

namespace UserManagement.Api.Extensions
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
            services.RegisterIdentity();
            services.RegisterCores();
            services.RegisterAutoMapper();
            services.RegisterCommonServices(configuration);
            services.RegisterIntegrationRepositories();
            services.ConfigureAuthentication(configuration);
            services.ConfigureAuthorization();
            services.RegisterValidators();
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
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
            services
                .AddIdentity<User, Role>(config =>
                {
                    config.Password.RequiredLength = 4;
                    config.Password.RequireDigit = false;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireLowercase = false;
                })
                .AddEntityFrameworkStores<IdentityServerDbContext>();
            // instead of registering new validator replace old one
            services.Replace(ServiceDescriptor.Scoped<IRoleValidator<Role>, Service.Services.Identity.Role.RoleValidator<Role>>());
        }

        /// <summary>
        /// Configure Authentication With Identity Server
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //services.AddAuthentication("Bearer")
            //    .AddIdentityServerAuthentication(options =>
            //    {
            //        options.Authority = configuration["Authority"];
            //        options.ApiName =
            //            "UserManagementApi";
            //        options.RequireHttpsMetadata = false;
            //    });
            //services.AddAuthentication("BasicAuthentication")
            //    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

                })
                .AddJwtBearer("Bearer", config =>
                {
                    config.Authority = configuration["Authority"];// the url of identity server
                    config.Audience = "UserManagementApi"; // name of the api that is defined in the identity server api resource
                    config.RequireHttpsMetadata = false;

                });


        }
        /// <summary>
        /// Configure Authorization 
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            //register permission requirement handler
            services.AddScoped<IAuthorizationHandler, PermissionAuthorizationHandler>();
            //register generic policy provider
            services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();
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
            //services.AddTransient<IProfileService, IdentityClaimsProfileService>();
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
