using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Common.Extensions;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Serilog;

namespace Api.Config
{
    /// <summary>
    /// Identity Server Config Class
    /// </summary>
    public static class Config
    {
        /// <summary>
        /// Get Clients According To Environment
        /// </summary>
        /// <param name="env"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients(string env, IConfiguration configuration)
        {

            switch (env)
            {
                case "Dev":
                    return GetDevClients(configuration.GetValue("AppSettings:Address", ""));

                case "Prod":
                    return GetProdClients(configuration.GetValue("AppSettings:Address", ""));

                case "Stage":
                    return GetStageClients(configuration.GetValue("AppSettings:Address", ""));
                default:
                    return GetDevClients(configuration.GetValue("AppSettings:Address", ""));

            }
        }
        /// <summary>
        /// Get Identity Resources
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Email(),
                new IdentityResources.Profile(),
            };
        }
        /// <summary>
        /// Define Api Scopes To be Used In Api Resources
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            return new List<ApiScope>
            {
                new ApiScope("UserManagementApi"),
                new ApiScope("SheiokhApi"),
                new ApiScope("IPPhoneApi"),
                new ApiScope("StockManagementApi"),
                new ApiScope("ItHelpDeskApi"),
                new ApiScope("OmsgdProjectsApi"),
                new ApiScope("LegalAffairsApi"),
                new ApiScope("FileManagerApi"),
                new ApiScope("BackendCoreApi"),
                new ApiScope("ContentApi"),
                new ApiScope("SelfServicesApi"),
                new ApiScope("CabinetManagementApi"),
                new ApiScope("GatewayManagementApi"),
                new ApiScope("FinancialApi")
            };
        }
        /// <summary>
        /// Get Api Resources
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource("UserManagementApi", "UserManagement API")
                {
                    Scopes = { "UserManagementApi" }
                },
                new ApiResource("SheiokhApi" , "Sheiokh API")
                {
                    Scopes = { "SheiokhApi" }
                },
                new ApiResource("IPPhoneApi" , "IPPhone API")
                {
                    Scopes = { "IPPhoneApi" }
                },
                new ApiResource("StockManagementApi" , "Stock Management API")
                {
                    Scopes = { "StockManagementApi" }
                },
                new ApiResource("ItHelpDeskApi" , "Help Desk API")
                {
                    Scopes = { "ItHelpDeskApi" }
                },
                new ApiResource("OmsgdProjectsApi" , "Omsgd Projects Management API")
                {
                    Scopes = { "OmsgdProjectsApi" }
                },
                new ApiResource("LegalAffairsApi" , "Legal Affairs API")
                {
                    Scopes = { "LegalAffairsApi" }
                },
                new ApiResource("FileManagerApi" , "File Manager API")
                {
                    Scopes = { "FileManagerApi" }
                },
                new ApiResource("ContentApi" , "Content Management API")
                {
                    Scopes = { "ContentApi" }
                },
                new ApiResource("BackendCoreApi" , "Backend Core API")
                {
                    Scopes = { "BackendCoreApi" }
                },
                new ApiResource("SelfServicesApi" , "Self Services API")
                {
                Scopes = { "SelfServicesApi" }
                },
                new ApiResource("CabinetManagementApi" , "Cabinet Management Api")
                {
                Scopes = { "CabinetManagementApi" }
                },
                new ApiResource("GatewayManagementApi" , "Gateway Management Api")
                {
                Scopes = { "GatewayManagementApi" }
                },
                new ApiResource("FinancialApi" , "Financial System Api")
                {
                    Scopes = { "FinancialApi" }
                }
            };
        }
        /// <summary>
        /// Get Clients Development
        /// </summary>
        /// <param name="devHost"></param>
        /// <returns></returns>
        public static IEnumerable<Client> GetDevClients(string devHost = "")
        {
            return new[]
            {
                new Client {
                    RequireConsent = false,
                    ClientId = "USERS",
                    ClientName = "User Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" },
                    RedirectUris = {"https://localhost:4200/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4200/"},
                    AllowedCorsOrigins = new List<string> { "https://localhost:4200" , "https://localhost:4400 ", "https://127.0.0.1:4200" , "https://127.0.0.1:4400"},
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "HR",
                    ClientName = "HR Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "FileManagerApi" },
                    RedirectUris = {"https://localhost:4203/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4203/"},
                    AllowedCorsOrigins = new List<string> { "https://localhost:4203" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "PORTAL",
                    ClientName = "Portal Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "ContentApi"},
                    RedirectUris = {"https://localhost:4300/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4300/"},
                    AllowedCorsOrigins = {"https://localhost:4300" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "SHEIOKH",
                    ClientName = "Sheiokh System Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "SheiokhApi" ,"UserManagementApi"},
                    RedirectUris = {"https://localhost:4400/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4400/"},
                    AllowedCorsOrigins = {"https://localhost:4400" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "IPPHONE",
                    ClientName = "IPPhone System Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "IPPhoneApi", "UserManagementApi"},
                    RedirectUris = {"https://localhost:4500/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4500/"},
                    AllowedCorsOrigins = {"https://localhost:4500" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "STOCK-MANAGEMENT",
                    ClientName = "Stock Management System Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "StockManagementApi", "UserManagementApi"},
                    RedirectUris = {"https://localhost:4600/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4600/"},
                    AllowedCorsOrigins = {"https://localhost:4600" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    RequireConsent = false,
                    ClientId = "OMSGD-SERVICES",
                    ClientName = "Omsgd Help Desk Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "StockManagementApi", "ItHelpDeskApi" },
                    RedirectUris = {"https://localhost:4700/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4700/"},
                    AllowedCorsOrigins = {"https://localhost:4700" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    RequireConsent = false,
                    ClientId = "LEGAL-AFFAIRS",
                    ClientName = "Legal Affairs Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "LegalAffairsApi" },
                    RedirectUris = {"https://localhost:4800/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4800/"},
                    AllowedCorsOrigins = {"https://localhost:4800" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    RequireConsent = false,
                    ClientId = "PROJECT-MANAGEMENT",
                    ClientName = "Project Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "OmsgdProjectsApi" },
                    RedirectUris = {"https://localhost:4201/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4201/"},
                    AllowedCorsOrigins = {"https://localhost:4201" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    RequireConsent = false,
                    ClientId = "SELF-SERVICES",
                    ClientName = "Omsgd Self Services Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "SelfServicesApi" },
                    RedirectUris = {"https://localhost:4900/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4900/"},
                    AllowedCorsOrigins = {"https://localhost:4900" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    RequireConsent = false,
                    ClientId = "CABINET",
                    ClientName = "Cabinet Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "CabinetManagementApi" , "FileManagerApi"},
                    RedirectUris = {"https://localhost:4204/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4204/"},
                    AllowedCorsOrigins = {"https://localhost:4204" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    RequireConsent = false,
                    ClientId = "GATEWAY-MANAGEMENT",
                    ClientName = "Gateway Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "GatewayManagementApi" , "FileManagerApi"},
                    RedirectUris = {"https://localhost:4205/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4205/"},
                    AllowedCorsOrigins = {"https://localhost:4205" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    RequireConsent = false,
                    ClientId = "FINANCIAL-SYSTEM",
                    ClientName = "Financial System Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "FinancialApi", "FileManagerApi"},
                    RedirectUris = {"https://localhost:4206/account/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://localhost:4206/"},
                    AllowedCorsOrigins = {"https://localhost:4206" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                }
            };
        }
        /// <summary>
        /// Get Clients Production For Same Port
        /// </summary>
        /// <param name="devHost"></param>
        /// <returns></returns>
        public static IEnumerable<Client> GetProdClients(string devHost = "")
        {
            return new[]
            {
                new Client {
                    RequireConsent = false,
                    ClientId = "USERS",
                    ClientName = "User Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" },
                    RedirectUris = {"https://apps.omsgd.local:448/User/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/User/"},
                    AllowedCorsOrigins = new List<string> { "https://apps.omsgd.local:448"},
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "HR",
                    ClientName = "HR Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "FileManagerApi" },
                    RedirectUris = {"https://apps.omsgd.local:448/hr/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/hr/"},
                    AllowedCorsOrigins = new List<string> { "https://apps.omsgd.local:448" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "PORTAL",
                    ClientName = "Portal Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "ContentApi"},
                    RedirectUris = {"https://apps.omsgd.local:448/Portal/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/Portal/"},
                    AllowedCorsOrigins = { "https://apps.omsgd.local:448" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "SHEIOKH",
                    ClientName = "Sheiokh System Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "SheiokhApi" ,"UserManagementApi"},
                    RedirectUris = {"https://apps.omsgd.local:448/Sheiokh/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/Sheiokh/"},
                    AllowedCorsOrigins = {"http://localhost:4300" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "IPPHONE",
                    ClientName = "IPPhone System Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "IPPhoneApi", "UserManagementApi"},
                    RedirectUris = {"https://apps.omsgd.local:448/IPPhone/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/IPPhone/"},
                    AllowedCorsOrigins = {"https://apps.omsgd.local:448"},
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                }
                ,
                new Client {
                    RequireConsent = false,
                    ClientId = "STOCK-MANAGEMENT",
                    ClientName = "Stock Management System Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "StockManagementApi", "UserManagementApi"},
                    RedirectUris = {"https://apps.omsgd.local:448/Stock/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/Stock/"},
                    AllowedCorsOrigins = {"https://apps.omsgd.local:448"},
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "OMSGD-SERVICES",
                    ClientName = "Omsgd Help Desk Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "StockManagementApi", "ItHelpDeskApi" },
                    RedirectUris = {"https://apps.omsgd.local:448/helpdesk/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/helpdesk/"},
                    AllowedCorsOrigins = {"https://apps.omsgd.local:448" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "LEGAL-AFFAIRS",
                    ClientName = "Legal Affairs Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" ,  "LegalAffairsApi" },
                    RedirectUris = {"https://apps.omsgd.local:448/LegalAffairs/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/LegalAffairs/"},
                    AllowedCorsOrigins = {"https://apps.omsgd.local:448" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "PROJECT-MANAGEMENT",
                    ClientName = "Project Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" ,  "OmsgdProjectsApi" },
                    RedirectUris = {"https://apps.omsgd.local:448/ptm/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/ptm/"},
                    AllowedCorsOrigins = {"https://apps.omsgd.local:448" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "SELF-SERVICES",
                    ClientName = "Omsgd Self Services Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "SelfServicesApi"  },
                    RedirectUris = {"https://apps.omsgd.local:448/SelfServices/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/SelfServices/"},
                    AllowedCorsOrigins = {"https://apps.omsgd.local:448" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "CABINET",
                    ClientName = "Cabinet Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" ,  "CabinetManagementApi" ,"FileManagerApi" },
                    RedirectUris = {"https://apps.omsgd.local:448/cabinet/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/cabinet/"},
                    AllowedCorsOrigins = {"https://apps.omsgd.local:448" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "GATEWAY-MANAGEMENT",
                    ClientName = "Gateway Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" ,  "GatewayManagementApi" ,"FileManagerApi" },
                    RedirectUris = {"https://apps.omsgd.local:448/gateways/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/gateways/"},
                    AllowedCorsOrigins = {"https://apps.omsgd.local:448" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    RequireConsent = false,
                    ClientId = "FINANCIAL-SYSTEM",
                    ClientName = "Financial System Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "FinancialApi", "FileManagerApi"},
                    RedirectUris = {"https://apps.omsgd.local:448/financial/account/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://apps.omsgd.local:448/financial"},
                    AllowedCorsOrigins = {"https://apps.omsgd.local:448" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                }
            };
        }
        /// <summary>
        /// Get Clients Production For Different Ports
        /// </summary>
        /// <param name="devHost"></param>
        /// <returns></returns>
        public static IEnumerable<Client> GetStageClients(string devHost = "")
        {
            return new[]
            {
                new Client {
                    RequireConsent = false,
                    ClientId = "USERS",
                    ClientName = "User Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" },
                    RedirectUris = {"https://stageapps.dhofar.local/User/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/User/"},
                    AllowedCorsOrigins = new List<string> { "https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "HR",
                    ClientName = "HR Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "FileManagerApi" },
                    RedirectUris = {"https://stageapps.dhofar.local/hr/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/hr"},
                    AllowedCorsOrigins = new List<string> { "https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "PORTAL",
                    ClientName = "Portal Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "ContentApi"},
                    RedirectUris = {"https://stageapps.dhofar.local/Portal/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/Portal/"},
                    AllowedCorsOrigins = { "https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "SHEIOKH",
                    ClientName = "Sheiokh System Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "SheiokhApi" ,"UserManagementApi"},
                    RedirectUris = {"https://stageapps.dhofar.local/Sheiokh/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/Sheiokh/"},
                    AllowedCorsOrigins = { "https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "IPPHONE",
                    ClientName = "IPPhone System Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "IPPhoneApi", "UserManagementApi"},
                    RedirectUris = {"https://stageapps.dhofar.local/IPPhone/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/IPPhone/"},
                    AllowedCorsOrigins = { "https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds
                }
                ,
                new Client {
                    RequireConsent = false,
                    ClientId = "STOCK-MANAGEMENT",
                    ClientName = "Stock Management System Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "StockManagementApi", "UserManagementApi"},
                    RedirectUris = {"https://stageapps.dhofar.local/Stock/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/Stock/"},
                    AllowedCorsOrigins = { "https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "OMSGD-SERVICES",
                    ClientName = "Omsgd Services Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "StockManagementApi", "ItHelpDeskApi" },
                    RedirectUris = {"https://stageapps.dhofar.local/helpdesk/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/helpdesk/"},
                    AllowedCorsOrigins = { "https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "LEGAL-AFFAIRS",
                    ClientName = "Legal Affairs Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "LegalAffairsApi" },
                    RedirectUris = {"https://stageapps.dhofar.local/LegalAffairs/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/LegalAffairs/"},
                    AllowedCorsOrigins = { "https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "PROJECT-MANAGEMENT",
                    ClientName = "Project Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "OmsgdProjectsApi" },
                    RedirectUris = {"https://stageapps.dhofar.local/ptm/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/ptm/"},
                    AllowedCorsOrigins = { "https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "SELF-SERVICES",
                    ClientName = "Omsgd Self Services Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "SelfServicesApi" },
                    RedirectUris = {"https://stageapps.dhofar.local/SelfServices/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/SelfServices/"},
                    AllowedCorsOrigins = { "https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "CABINET",
                    ClientName = "Cabinet Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "CabinetManagementApi" ,"FileManagerApi" },
                    RedirectUris = {"https://stageapps.dhofar.local/cabinet/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/cabinet/"},
                    AllowedCorsOrigins = { "https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client {
                    RequireConsent = false,
                    ClientId = "GATEWAY-MANAGEMENT",
                    ClientName = "Gateway Management Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "GatewayManagementApi", "FileManagerApi" },
                    RedirectUris = {"https://stageapps.dhofar.local/Gateway/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/Gateway/"},
                    AllowedCorsOrigins = { "https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                },
                new Client
                {
                    RequireConsent = false,
                    ClientId = "FINANCIAL-SYSTEM",
                    ClientName = "Financial System Client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowedScopes = { "openid", "profile", "email", "UserManagementApi" , "FinancialApi", "FileManagerApi"},
                    RedirectUris = {"https://stageapps.omsgd.dhofar/financial/account/auth-callback"},
                    PostLogoutRedirectUris = new List<string> {"https://stageapps.dhofar.local/financial"},
                    AllowedCorsOrigins = {"https://stageapps.dhofar.local" },
                    AccessTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    IdentityTokenLifetime = (int)TimeSpan.FromMinutes(120).TotalSeconds,
                    AllowAccessTokensViaBrowser = true
                }
            };
        }


        /// <summary>
        /// Get Certificate
        /// </summary>
        /// <returns></returns>
        public static IIdentityServerBuilder GetIdentityServerCertificate(this IIdentityServerBuilder builder, string path, string password, IConfiguration configuration)
        {
            try
            {
                if (configuration["Environment"] == "Dev")
                {
                    builder.AddDeveloperSigningCredential();
                }
                if (configuration["Environment"] == "Prod" || configuration["Environment"] == "Stage")
                {
                    var jsonSerializerSettings = new JsonSerializerSettings
                    {
                        ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    };
                    var certificatePath = path.ToApplicationPath();
                    var certificate = new X509Certificate2(certificatePath, password);
                    Log.Information($"INFO-CERTIFICATE: Reading Certificate : {JsonConvert.SerializeObject(certificate, jsonSerializerSettings)}");
                    builder.AddSigningCredential(certificate);
                }

                return builder;
            }
            catch (Exception e)
            {
                var jsonSerializerSettings = new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                };
                Log.Error($"ERROR-CERTIFICATE: Error Reading Certificate : {JsonConvert.SerializeObject(e, jsonSerializerSettings)}");
                throw;
            }

        }



    }
}
