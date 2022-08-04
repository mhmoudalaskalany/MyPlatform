using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.User;
using Domain.DTO.Identity.User.Parameters;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Identity.User;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Identity
{
    /// <summary>
    /// Users Controller
    /// </summary>
    public class UsersController : BaseController
    {
        private readonly IUserService _userService;
        /// <summary>
        /// Constructor
        /// </summary>
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }
        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IResult> GetByIdAsync(long id)
        {
            var result = await _userService.GetByIdAsync(id);
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
            return await _userService.GetByIdForEditAsync(id);
        }


        /// <summary>
        /// Get By App Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByAppIdAsync/{id}")]
        public async Task<IResult> GetByAppIdAsync(long id)
        {
            var result = await _userService.GetByAppIdAsync(id);
            return result;
        }


        /// <summary>
        /// Get Logged In User Profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetProfileAsync/{id}")]
        public async Task<IResult> GetProfileAsync(long id)
        {
            var result = await _userService.GetUserProfileAsync(id);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUserCount")]
        public async Task<IResult> GetUserCountAsync()
        {
            var result = await _userService.GetUserCountAsync();
            return result;
        }
        /// <summary>
        /// Get All Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IResult> GetAllAsync()
        {
            var result = await _userService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Check If National Id Is Available Or Not
        /// </summary>
        /// <param name="nationalId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckNationalId/{nationalId}/{userId}")]
        public async Task<IResult> CheckNationalIdAsync(string nationalId, long userId)
        {
            var result = await _userService.CheckNationalIdAsync(nationalId, userId);
            return result;
        }

        /// <summary>
        /// Check If Email Is Available Or Not
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckEmail/{email}/{userId}")]
        public async Task<IResult> CheckEmailAsync(string email, long userId)
        {
            var result = await _userService.CheckEmailAsync(email, userId);
            return result;
        }
        /// <summary>
        /// GetAll Data paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPaged")]
        //[Authorize(policy: Permission.Users.View)]
        public async Task<DataPaging> GetPaged([FromBody] BaseParam<UserFilter> filter)
        {
            return await _userService.GetAllPagedAsync(filter);
        }

        /// <summary>
        /// GetAll Data paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetByAppIdPaged")]
        //[Authorize(policy: Permission.Users.View)]
        public async Task<DataPaging> GetByAppIdPaged([FromBody] BaseParam<UserSearchCriteriaFilter> filter)
        {
            return await _userService.GetByAppIdPagedAsync(filter);
        }

        /// <summary>
        /// Upload Image
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("UploadProfileImageAsync")]
        public async Task<IResult> Post([FromBody] UploadProfileImageDto dto)
        {
            var result = await _userService.UploadProfileImageAsync(dto);
            return result;
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IResult> Post([FromBody] AddUserDto model)
        {
            var result = await _userService.AddAsync(model);
            return result;
        }
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("ChangePasswordAsync")]
        public async Task<IResult> ChangePasswordAsync([FromBody] ChangePasswordDto model)
        {
            var result = await _userService.ChangePasswordAsync(model);
            return result;
        }
        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IResult> Update(AddUserDto model)
        {
            return await _userService.UpdateAsync(model);
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
            return await _userService.DeleteAsync(id);
        }

        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("DeleteSoft/{id}")]
        public async Task<IResult> DeleteSoftAsync(long id)
        {
            return await _userService.DeleteSoftAsync(id);
        }

        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <param name="appId"></param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("DeleteByUserAppId/{id}/{appId}")]
        public async Task<IResult> RemoveByUserAppId(long id, long appId)
        {
            return await _userService.DeleteByUserAppId(id, appId);
        }

    }
}
