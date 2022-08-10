using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Identity.UserApp;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Identity.UserApp;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Identity
{
    /// <summary>
    /// User Apps Controller
    /// </summary>
    public class UserAppsController : BaseController
    {
        private readonly IUserAppService _userAppService;
        /// <summary>
        /// Constructor
        /// </summary>
        public UserAppsController(IUserAppService userAppService)
        {
            _userAppService = userAppService;
        }
        /// <summary>
        /// Get User Apps By  Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByIdAsync")]
        public async Task<IFinalResult> GetByIdAsync(long id)
        {
            return await _userAppService.GetByIdAsync(id);
        }

        /// <summary>
        /// Get By Id For Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEdit/{id}")]
        public async Task<IFinalResult> GetEdit(long id)
        {
            return await _userAppService.GetByIdForEditAsync(id);
        }

        /// <summary>
        /// Get User Apps By User Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByUserIdAsync/{id}")]
        public async Task<IFinalResult> GetByUserIdAsync(long id)
        {
            return await _userAppService.GetUserApps(id);
        }

        /// <summary>
        /// Add List Of Users To App
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddUsersToAppAsync")]
        public async Task<IFinalResult> AddUsersToAppAsync(List<AddUserAppDto> dtos)
        {
            var result = await _userAppService.AddUsersToAppAsync(dtos);
            return result;
        }
    }
}
