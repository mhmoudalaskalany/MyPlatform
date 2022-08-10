using System.Threading.Tasks;
using Domain.Core;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Identity.UserClaim;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Identity
{
    /// <summary>
    /// User Claims Controller
    /// </summary>
    public class UserClaimsController : BaseController
    {
        private readonly IUserClaimService _userClaimService;
        /// <summary>
        /// Constructor
        /// </summary>
        public UserClaimsController(IUserClaimService userClaimService)
        {
            _userClaimService = userClaimService;
        }

        /// <summary>
        /// Get By User Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByUserIdAsync/{userId}/{authenticationMethod}/{appCode}")]
        public async Task<IFinalResult> GetByUserIdAsync(long userId, string authenticationMethod, string appCode)
        {
            var result = await _userClaimService.GeyUserClaimsAsync(userId, authenticationMethod, appCode);
            return result;
        }
    }
}
