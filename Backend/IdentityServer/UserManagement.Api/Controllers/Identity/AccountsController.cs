using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Identity.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Identity.Account;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Identity
{
    /// <summary>
    /// Accounts Controller
    /// </summary>
    public class AccountsController : BaseController
    {
        private readonly IAccountService _accountService;
        /// <summary>
        /// Constructor
        /// </summary>
        public AccountsController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        /// <summary>
        /// Reset Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        public async Task<IFinalResult> ResetPassword([FromBody] ResetPasswordDto model)
        {
            var result = await _accountService.ResetPassword(model);
            return result;
        }
    }
}
