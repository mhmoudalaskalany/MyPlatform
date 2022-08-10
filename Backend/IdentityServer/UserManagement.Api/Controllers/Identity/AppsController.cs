using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.App;
using Domain.DTO.Identity.App.Parameters;
using Domain.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Identity.App;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Identity
{
    /// <summary>
    /// App Controller
    /// </summary>
    public class AppsController : BaseController
    {
        private readonly IAppService _appService;

        /// <summary>
        /// Constructor
        /// </summary>
        public AppsController(IAppService appService)
        {
            _appService = appService;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAppsCount")]
        public async Task<IFinalResult> GetAppsCountAsync()
        {
            var result = await _appService.GetAppsCountAsync();
            return result;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IFinalResult> GetAsync(long id)
        {
            var result = await _appService.GetByIdAsync(id);
            return result;
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
            return await _appService.GetByIdForEditAsync(id);
        }

        /// <summary>
        /// Get By User Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByUserIdAsync/{userId}")]
        public async Task<IFinalResult> GetByUserIdAsync(long userId)
        {
            var result = await _appService.GetByUserIdAsync(userId);
            return result;
        }
        /// <summary>
        /// Get By User And App Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByUserAppIdAsync/{userId}/{appId}")]
        public async Task<IFinalResult> GetByUserAppIdAsync(long userId, long appId)
        {
            var result = await _appService.GetByUserAppIdAsync(userId, appId);
            return result;
        }

        /// <summary>
        /// Get By User And App Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPublicAppsAsync")]
        public async Task<IFinalResult> GetPublicAppsAsync()
        {
            var result = await _appService.GetPublicAppsAsync();
            return result;
        }

        /// <summary>
        /// Get All Apps
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IFinalResult> GetAllAsync()
        {
            var result = await _appService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// GetAll Data paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Authorize(policy: Permission.Apps.View)]
        [Route("GetPaged")]
        public async Task<DataPaging> GetPaged([FromBody] BaseParam<AppFilter> filter)
        {
            return await _appService.GetAllPagedAsync(filter);
        }

        /// <summary>
        /// GetAll Data paged
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserAppsWithNoRoles")]
        public async Task<IFinalResult> GetUserAppsWithNoRoles(UserAppRolesDto dto)
        {
            return await _appService.GetUserAppsWithNoRoles(dto);
        }

        /// <summary>
        /// GetAll Data paged
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUserAppsWithRoles")]
        public async Task<IFinalResult> GetUserAppsWithRoles(UserAppRolesDto dto)
        {
            return await _appService.GetUserAppsWithRoles(dto);
        }

        /// <summary>
        /// Add App
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IFinalResult> PostAsync([FromBody] AddAppDto dto)
        {
            var result = await _appService.AddAsync(dto);
            return result;
        }
        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IFinalResult> Update(AddAppDto model)
        {
            return await _appService.UpdateAsync(model);
        }

        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("Delete/{id}")]
        public async Task<IFinalResult> Remove(long id)
        {
            return await _appService.DeleteAsync(id);
        }

        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("DeleteSoft/{id}")]
        public async Task<IFinalResult> DeleteSoftAsync(long id)
        {
            return await _appService.DeleteSoftAsync(id);
        }
    }
}
