using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.Role;
using Domain.DTO.Identity.Role.Parameters;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Identity.Role;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Identity
{
    /// <summary>
    /// Role Controller
    /// </summary>
    public class RolesController : BaseController
    {
        private readonly IRoleService _roleService;
        /// <summary>
        /// Constructor
        /// </summary>
        public RolesController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IResult> GetAsync(long id)
        {
            var result = await _roleService.GetRoleByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get By Id For Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEdit/{id}")]
        public async Task<IResult> GetEdit(long id)
        {
            return await _roleService.GetByIdForEditAsync(id);
        }

        /// <summary>
        /// Get By App Id
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByAppIdAsync/{appId}")]
        public async Task<IResult> GetByAppIdAsync(long appId)
        {
            var result = await _roleService.GetByAppIdAsync(appId);
            return result;
        }

        /// <summary>
        /// Get Unassigned By App Id And User Id
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUnassignedByAppIdAsync/{appId}/{userId}")]
        public async Task<IResult> GetUnassignedByAppIdAsync(long appId, long userId)
        {
            var result = await _roleService.GetUnassignedByAppIdAsync(appId, userId);
            return result;
        }


        /// <summary>
        /// Get Assigned By App Id And User Id
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAssignedByAppIdAsync/{appId}/{userId}")]
        public async Task<IResult> GetAssignedByAppIdAsync(long appId, long userId)
        {
            var result = await _roleService.GetAssignedByAppIdAsync(appId, userId);
            return result;
        }

        /// <summary>
        /// Get All Roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IResult> GetAllAsync()
        {
            var result = await _roleService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// GetAll Data paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        //[Authorize(policy: Permission.Roles.View)]
        [Route("GetPaged")]
        public async Task<DataPaging> GetPaged([FromBody] BaseParam<RoleFilter> filter)
        {
            return await _roleService.GetAllPagedAsync(filter);
        }
        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IResult> Post([FromBody] AddRoleDto dto)
        {
            var result = await _roleService.AddRoleAsync(dto);
            return result;
        }

        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IResult> Update(AddRoleDto model)
        {

            return await _roleService.UpdateAsync(model);
        }
        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("Delete/{id}")]
        public async Task<IResult> Remove(long id)
        {
            return await _roleService.DeleteAsync(id);
        }

        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("DeleteSoft/{id}")]
        public async Task<IResult> DeleteSoftAsync(long id)
        {
            return await _roleService.DeleteSoftAsync(id);
        }
    }
}
