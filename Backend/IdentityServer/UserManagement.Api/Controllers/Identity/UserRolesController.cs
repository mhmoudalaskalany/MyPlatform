using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Identity.UserRole;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Identity.UserRole;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Identity
{
    /// <summary>
    ///  User Roles Controller
    /// </summary>
    public class UserRolesController : BaseController
    {
        private readonly IUserRoleService _userRoleService;
        /// <summary>
        /// Constructor
        /// </summary>
        public UserRolesController(IUserRoleService userRoleService)
        {
            _userRoleService = userRoleService;
        }

        /// <summary>
        /// Get By User Id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{userId}")]
        public async Task<IFinalResult> GetByIdAsync(long userId)
        {
            var result = await _userRoleService.GetByIdAsync(userId);
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
            return await _userRoleService.GetByIdForEditAsync(id);
        }


        /// <summary>
        /// Get By User Id For Other Systems
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByUserIdAsync/{userId}/{appId}")]
        public async Task<IFinalResult> GetByUserIdAsync(long userId, long appId)
        {
            var result = await _userRoleService.GetByUserIdAsync(userId, appId);
            return result;
        }
        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IFinalResult> GetAllAsync()
        {
            var result = await _userRoleService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IFinalResult> Post([FromBody] AddUserRoleDto dto)
        {
            var result = await _userRoleService.AddAsync(dto);
            return result;
        }


        /// <summary>
        /// Add Multible Roles
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddMulitpleRoles")]
        public async Task<IFinalResult> AddMulitpleRolesAsync([FromBody] AddMultipleRolesDto dto)
        {
            var result = await _userRoleService.AddMultipleRolesAsync(dto);
            return result;
        }

        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IFinalResult> Update(AddUserRoleDto model)
        {

            return await _userRoleService.UpdateAsync(model);
        }
        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IFinalResult> Remove(long id)
        {
            return await _userRoleService.DeleteAsync(id);
        }

        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("DeleteUserRole")]
        public async Task<IFinalResult> DeleteUserRole(AddUserRoleDto dto)
        {
            return await _userRoleService.DeleteUserRoleAsync(dto);
        }


    }
}
