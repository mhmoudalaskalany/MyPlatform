using System;
using System.IO;
using Domain.Core;
using Domain.Helper.EmailHelper;
using Domain.Helper.HttpClient;
using Domain.Helper.HttpClient.RestSharp;
using Domain.Helper.MediaUploader;
using Domain.Helper.SmsHelper;
using Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Domain.Extensions
{
    public static class ConfigureCommonExtension
    {
        public static IServiceCollection RegisterCommonServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors();
            services.RegisterMainCore();
            services.AddApiDocumentationServices(configuration);
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.RegisterNotificationMetadata(configuration);
            return services;
        }

        private static void RegisterMainCore(this IServiceCollection services)
        {
            services.AddSingleton<MicroServicesUrls>();
            services.AddTransient<IResponseResult, ResponseResult>();
            services.AddTransient<IFinalResult, Result>();
            services.AddSingleton<IUploaderConfiguration, UploaderConfiguration>();
            services.AddTransient<ISmsService, SmsService>();
            services.AddSingleton<ISendMail, SendMail>();
            services.AddTransient<IRestSharpContainer, RestSharpContainer>();
            services.AddScoped<ISessionStorage, SessionStorage>();
        }

        /// <summary>
        /// Register Notification Meta Data
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        private static void RegisterNotificationMetadata(this IServiceCollection services, IConfiguration configuration)
        {
            var notificationMetadata = configuration.GetSection("EmailMetadata").Get<EmailMetadata>();
            services.AddSingleton(notificationMetadata);
        }

        private static void AddApiDocumentationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                string title = configuration["SwaggerConfig:Title"];
                string version = configuration["SwaggerConfig:Version"];
                string docPath = configuration["SwaggerConfig:DocPath"];
                options.SwaggerDoc(version, new OpenApiInfo { Title = title, Version = version });
                var filePath = Path.Combine(AppContext.BaseDirectory, docPath);
                options.IncludeXmlComments(filePath);
                var security = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }

                    }
                };
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                options.AddSecurityRequirement(security);

            });
        }
    }
}
