using System.Threading.Tasks;
using Domain.Core;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Identity.Role.Integration;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.IntegrationControllers.Identity
{
    /// <summary>
    /// External Roles Controller Used By Other Services
    /// </summary>
    public class ExternalRolesController : BaseController
    {
        private readonly IExternalRoleService _externalRoleService;
        /// <summary>
        /// constructor
        /// </summary>
        public ExternalRolesController(IExternalRoleService externalRoleService)
        {
            _externalRoleService = externalRoleService;
        }

        /// <summary>
        /// Get By App Id
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByAppId/{appId}")]
        public async Task<IResult> GetByAppIdAsync(long appId)
        {
            var result = await _externalRoleService.GetByAppIdAsync(appId);
            return result;
        }
    }
}
