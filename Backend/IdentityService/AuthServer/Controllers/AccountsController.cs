using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using Domain.DTO.Identity.User;
using Entities.Entities.Identity;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Service.Services.Identity.Account;

namespace AuthServer.Controllers
{
    /// <summary>
    /// Accounts Controller
    /// </summary>
    public class AccountsController : Controller
    {
        private readonly IIdentityServerInteractionService _interaction;
        private readonly UserManager<User> _userManager;
        private readonly IEventService _events;
        private readonly SignInManager<User> _signInManager;
        private readonly IAccountService _accountService;
        private readonly IConfiguration _configuration;
        private readonly IAuthenticationSchemeProvider _schemeProvider;
        private readonly IClientStore _clientStore;


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="interaction"></param>
        /// <param name="userManager"></param>
        /// <param name="events"></param>
        /// <param name="signInManager"></param>
        /// <param name="accountService"></param>
        /// <param name="configuration"></param>
        /// <param name="schemeProvider"></param>
        /// <param name="clientStore"></param>
        public AccountsController(
            IIdentityServerInteractionService interaction,
            UserManager<User> userManager,
            IEventService events, SignInManager<User> signInManager,
            IAccountService accountService,
            IConfiguration configuration,
            IAuthenticationSchemeProvider schemeProvider,
            IClientStore clientStore
            )
        {
            _interaction = interaction;
            _userManager = userManager;
            _events = events;
            _signInManager = signInManager;
            _accountService = accountService;
            _configuration = configuration;
            _schemeProvider = schemeProvider;
            _clientStore = clientStore;
        }

        #region Public EndPoints

        /// <summary>
        /// Entry point into the login workflow
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            var vm = await BuildLoginViewModelAsync(returnUrl);
            if (vm.IsExternalLoginOnly)
            {
                return await ExternalLogin(vm.ExternalLoginScheme, returnUrl);
            }
            return View(vm);
        }


        /// <summary>
        /// Handle post-back from username/password login
        /// </summary>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model)
        {
            // check if we are in the context of an authorization request
            var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            if (ModelState.IsValid)
            {
                
                // validate username/password
                var user = await _userManager.FindByNameAsync(model.Username);

                if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
                {
                     await _accountService.AddLoginHistory(user.Id);
                    // first see if this is the first login for the user and force him to change password
                    var isFirstLogin = _accountService.CheckUserFirstLogin(user);
                    if (!isFirstLogin)
                    {
                        // user this in change password
                        TempData["userId"] = user.Id.ToString();
                        return RedirectToAction("ChangePassword", "Accounts");
                    }

                    await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id.ToString(), user.FullNameEn));

                    // only set explicit expiration here if user chooses "remember me". 
                    // otherwise we rely upon expiration configured in cookie middleware.
                    AuthenticationProperties props = null;
                    if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(TimeSpan.FromMinutes(user.SessionDuration))
                        };
                    }

                    var issuer = new IdentityServerUser(user.Id.ToString())
                    {
                        DisplayName = user.UserName
                    };
                    // issue authentication cookie with subject ID and username

                    await HttpContext.SignInAsync(issuer, props);

                    if (context != null)
                    {

                        await _accountService.CacheUserAsync(user);
                        return Redirect(model.ReturnUrl);
                    }

                    // request for a local page
                    if (Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return Redirect(model.ReturnUrl);
                    }

                    if (string.IsNullOrEmpty(model.ReturnUrl))
                    {
                        return Redirect("~/");
                    }

                    // user might have clicked on a malicious link - should be logged
                    throw new Exception("invalid return URL");
                }

                await _events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials"));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
                var providers = await GetExternalProviders();
                // add external  providers here because if they are null it will crash
                var viewModel = new LoginViewModel
                {
                    ExternalProviders = providers
                };
                return View(viewModel);
            }

            // something went wrong, show form with error 
            var vm = new LoginViewModel
            {
                Username = model.Username,
                RememberLogin = model.RememberLogin
            };
            return View(vm);
        }
        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl)
        {
            return await ProcessWindowsLoginAsync(returnUrl);
        }
        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback()
        {
            // read external identity from the temporary cookie
            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            if (result?.Succeeded != true)
            {
                await _events.RaiseAsync(new UserLoginFailureEvent("No User Name", AccountOptions.NotPartOfDomain));
                throw new Exception(AccountOptions.NotPartOfDomain);
            }
            // lookup our user and external provider info
            var (user, provider, providerUserId, claims) = await FindUserFromExternalProviderAsync(result);


            // issue authentication cookie for user
            // we must issue the cookie manually, and can't use the SignInManager because
            // it doesn't expose an API to issue additional claims from the login workflow
            // var principal = await _signInManager.CreateUserPrincipalAsync(user);
            var name = claims.FirstOrDefault(x => x.Type == "name")?.Value ?? user.Id.ToString();

            var issuer = new IdentityServerUser(name)
            {
                DisplayName = name,
                IdentityProvider = provider,
                AdditionalClaims = claims.ToList()
            };
            //await HttpContext.SignInAsync(providerUserId, name, provider);
            await HttpContext.SignInAsync(issuer);
            // delete temporary cookie used during external authentication
            await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);
            await _events.RaiseAsync(new UserLoginSuccessEvent(provider, providerUserId, providerUserId, name));
            // validate return URL and redirect back to authorization endpoint or a local page
            var returnUrl = result.Properties.Items["returnUrl"];
            if (_interaction.IsValidReturnUrl(returnUrl) || Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return Redirect("~/");
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <param name="logoutId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            if (User.Identity.IsAuthenticated)
            {
                await _signInManager.SignOutAsync();
                await HttpContext.SignOutAsync();
                // delete local authentication cookie
                await HttpContext.SignOutAsync(IdentityServerConstants.DefaultCookieAuthenticationScheme);
                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);
                // Clear the existing external cookie to ensure a clean login process
                await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
                // issue event of successful logout
                await _events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));
            }

            //get logout context to get the redirect logout url
            var context = await _interaction.GetLogoutContextAsync(logoutId);
            // redirect to the logout url defined in the client configurations
            return Redirect(context.PostLogoutRedirectUri);
        }
        /// <summary>
        /// Change Password View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ChangePassword()
        {
            return View();
        }
        /// <summary>
        /// Change Password Submit
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            model.UserId = TempData["userId"];
            var result = await _accountService.ChangePasswordAsync(model);
            if (result)
            {
                var url = _configuration["Urls:Portal"];
                return Redirect(url);
            }
            ModelState.AddModelError(string.Empty, "Error Updating Password");
            return View();
        }
        /// <summary>
        /// Reset Password View
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ResetPassword()
        {
            return View();
        }

        /// <summary>
        /// Reset Password Submit
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> ResetPassword(ResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _accountService.ResetPassword(model);
            if (result.Status == HttpStatusCode.OK)
            {
                return View("CompleteResetPassword");
            }
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        /// <summary>
        /// Reset Password Submit
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IActionResult> CompleteResetPassword(CompleteResetPasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await _accountService.CompleteResetPassword(model);
            if (result.Status == HttpStatusCode.OK)
            {
                var url = _configuration["Urls:Portal"];
                return Redirect(url);
            }
            ModelState.AddModelError(string.Empty, result.Message);
            return View();
        }

        [HttpGet]
        public IActionResult ElectronicGuide(string provider, string returnUrl)
        {
            var ipPhoneUrl = _configuration["Urls:IPPhone"];
            return Redirect(ipPhoneUrl);
        }

        #endregion


        #region Private Methods

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            var providers = await GetExternalProviders();

            var allowLocal = true;
            if (context?.Client.ClientId != null)
            {
                var client = await _clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers
            };
        }
        private string GetDisplayName(AuthenticationScheme authenticationScheme)
        {
            if (authenticationScheme.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName,
                StringComparison.OrdinalIgnoreCase))
            {
                return AccountOptions.WindowsAuthenticationSchemeName;
            }

            return authenticationScheme.DisplayName;
        }

        private async Task<List<ExternalProvider>> GetExternalProviders()
        {
            var schemes = await _schemeProvider.GetAllSchemesAsync();

            var providers = schemes
                .Where(x => x.DisplayName != null ||
                            (x.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName,
                                StringComparison.OrdinalIgnoreCase))
                )
                .Select(x => new ExternalProvider
                {
                    DisplayName = GetDisplayName(x),
                    AuthenticationScheme = x.Name
                }).ToList();
            return providers;
        }
        private async Task<IActionResult> ProcessWindowsLoginAsync(string returnUrl)
        {
            // see if windows auth has already been requested and succeeded
            var result = await HttpContext.AuthenticateAsync(AccountOptions.WindowsAuthenticationSchemeName);
            if (result?.Principal is WindowsPrincipal wp)
            {
                // we will issue the external cookie and then redirect the
                // user back to the external callback, in essence, tresting windows
                // auth the same as any other external authentication mechanism
                var props = new AuthenticationProperties()
                {
                    RedirectUri = Url.Action("ExternalLoginCallback"),
                    Items =
                    {
                        { "returnUrl", returnUrl },
                        { "LoginProvider", AccountOptions.WindowsAuthenticationSchemeName },
                    }
                };
                var id = new ClaimsIdentity(AccountOptions.WindowsAuthenticationSchemeName);
                id.AddClaim(new Claim(JwtClaimTypes.Subject, wp.Identity.Name));
                id.AddClaim(new Claim(JwtClaimTypes.Name, wp.Identity.Name));
                await HttpContext.SignInAsync(IdentityConstants.ExternalScheme, new ClaimsPrincipal(id), props);
                return Redirect(props.RedirectUri);
            }
            else
            {
                // trigger windows auth
                // since windows auth don't support the redirect uri,
                // this URL is re-triggered when we call challenge
                return Challenge(AccountOptions.WindowsAuthenticationSchemeName);
            }
        }

        private async Task<(User user, string provider, string providerUserId, IEnumerable<Claim> claims)>
            FindUserFromExternalProviderAsync(AuthenticateResult result)
        {
            var externalUser = result.Principal;

            // try to determine the unique id of the external user (issued by the provider)
            // the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used
            var userIdClaim = externalUser.FindFirst(JwtClaimTypes.Subject) ??
                              externalUser.FindFirst(ClaimTypes.NameIdentifier) ??
                              throw new Exception("Unknown userid");

            // remove the user id claim so we don't include it as an extra claim if/when we provision the user
            var claims = externalUser.Claims.ToList();
            claims.Remove(userIdClaim);

            var provider = result.Properties.Items["LoginProvider"];
            var providerUserId = userIdClaim.Value;

            // find external user
            var user = await _userManager.FindByLoginAsync(provider, providerUserId);
            return (user, provider, providerUserId, claims);
        }

        #endregion





    }
}

