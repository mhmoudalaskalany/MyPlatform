using System.Threading.Tasks;
using Domain.Core;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Identity.App.Integration;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.IntegrationControllers.Identity
{
    /// <summary>
    /// App Controller
    /// </summary>
    public class ExternalAppsController : BaseController
    {
        private readonly IExternalAppService _appService;

        /// <summary>
        /// Constructor
        /// </summary>
        public ExternalAppsController(IExternalAppService appService)
        {
            _appService = appService;
        }
        /// <summary>
        /// Get Current Logged In User Apps
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetLoggedUserApps")]
        public async Task<IResult> GetLoggedUserAppsAsync()
        {
            var result = await _appService.GetLoggedUserAppsAsync();
            return result;
        }
       
    }
}
