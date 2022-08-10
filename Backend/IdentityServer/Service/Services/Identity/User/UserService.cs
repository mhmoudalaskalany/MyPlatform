using Domain.Abstraction.UnitOfWork;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.User;
using Domain.DTO.Identity.User.Parameters;
using Domain.Helper.MediaUploader;
using LinqKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Services.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;

namespace Service.Services.Identity.User
{
    public class UserService : BaseService<Entities.Entities.Identity.User, AddUserDto, UserDto, long?>, IUserService
    {
        private readonly UserManager<Entities.Entities.Identity.User> _userManager;
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork<Entities.Entities.Identity.App> _appUnitOfWork;
        private readonly IUnitOfWork<Entities.Entities.Identity.UserRole> _userRoleUnitOfWork;
        private readonly IUnitOfWork<Entities.Entities.Identity.Role> _roleUnitOfWork;
        private readonly IUnitOfWork<Entities.Entities.Identity.UserApp> _userAppUnitOfWork;
        private readonly IUploaderConfiguration _uploader;
        private const string FolderName = "Files/ProfileImages";
        public UserService(IServiceBaseParameter<Entities.Entities.Identity.User> parameters,
            UserManager<Entities.Entities.Identity.User> userManager,
            IConfiguration configuration,
            IUnitOfWork<Entities.Entities.Identity.App> appUnitOfWork,
            IUnitOfWork<Entities.Entities.Identity.UserRole> userRoleUnitOfWork,
            IUnitOfWork<Entities.Entities.Identity.Role> roleUnitOfWork,
            IUnitOfWork<Entities.Entities.Identity.UserApp> userAppUnitOfWork,
            IUploaderConfiguration uploader) : base(parameters)
        {
            _userManager = userManager;
            _configuration = configuration;
            _appUnitOfWork = appUnitOfWork;
            _userRoleUnitOfWork = userRoleUnitOfWork;
            _roleUnitOfWork = roleUnitOfWork;
            _userAppUnitOfWork = userAppUnitOfWork;
            _uploader = uploader;
        }

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> GetUserCountAsync()
        {
            var users = await UnitOfWork.Repository.Count();
            return ResponseResult.PostResult(users, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());
        }
        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<IFinalResult> GetByIdAsync(object id)
        {
            var entity = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == Convert.ToInt64(id),
                include: src => src.Include(x => x.UserApps));
            var data = Mapper.Map<Entities.Entities.Identity.User, AddUserDto>(entity);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());
        }
        /// <summary>
        /// Get User Profile
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetUserProfileAsync(long id)
        {
            var entity = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == id,
                   include: src => src.Include(x => x.UserApps).ThenInclude(a => a.App));
            var imageSrc = _configuration["DefaultImagePath"];
            if (!string.IsNullOrEmpty(entity.ProfileImageUrl) && entity.ProfileImageUrl != null)
            {
                imageSrc = await _uploader.ConvertToBase64String(entity.ProfileImageUrl, FolderName);
            }
            var data = Mapper.Map<Entities.Entities.Identity.User, UserDto>(entity);
            data.Base64 = imageSrc;
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        /// <summary>
        /// Get Users that does not have this app id so we can add them to this app
        /// </summary>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetByAppIdAsync(long appId)
        {

            var entity = await UnitOfWork.Repository
                .FindAsync(x => !x.UserApps.Select(userApp => userApp.AppId).Contains(appId));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.User>, IEnumerable<UserDto>>(entity);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }


        /// <summary>
        /// Get Paged Users that does not have this app id so we can add them to this app
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<DataPaging> GetByAppIdPagedAsync(BaseParam<UserSearchCriteriaFilter> filter)
        {
            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter)
                , skip: offset, take: limit, filter.OrderByValue, include: src => src.Include(u => u.UserApps));

            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.User>, IEnumerable<UserDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));


        }
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<DataPaging> GetAllPagedAsync(BaseParam<UserFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue, include: src => src.Include(u => u.UserApps));
            if (filter.Filter.AppId != 0)
            {
                query.Item2 = query.Item2.Where(x => x.UserApps.Select(userApp => userApp.AppId).Contains(filter.Filter.AppId));
            }
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.User>, IEnumerable<UserDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }

        /// <summary>
        /// Check National Id
        /// </summary>
        /// <param name="nationalId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IFinalResult> CheckNationalIdAsync(string nationalId, long userId)
        {

            if (userId != 0)
            {
                Result = await CheckForNationalIdAtEditMode(nationalId, userId);
            }
            else
            {
                Result = await CheckForNationalIdAtAddMode(nationalId);

            }
            return Result;

        }
        /// <summary>
        /// Check Email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IFinalResult> CheckEmailAsync(string email, long userId)
        {

            if (userId != 0)
            {
                Result = await CheckForEmailAtEditMode(email, userId);
            }
            else
            {
                Result = await CheckForEmailAtAddMode(email);

            }

            return Result;

        }
        /// <summary>
        /// Upload Profile Image
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IFinalResult> UploadProfileImageAsync(UploadProfileImageDto dto)
        {
            var entity = await _userManager.FindByIdAsync(dto.UserId.ToString());

            entity.ProfileImageUrl = _uploader.SaveBase64(dto.Base64, entity.UserName, FolderName);
            try
            {



                var result = await _userManager.UpdateAsync(entity);
                if (result.Succeeded)
                {
                    Result = new ResponseResult(result: null, status: HttpStatusCode.Created,
                        message: "SuccessUploadingImage");
                }
                else
                {
                    Result = new ResponseResult(result: null, status: HttpStatusCode.BadRequest,
                        message: "ErrorUploadingImage");
                }
                return Result;
            }
            catch (Exception e)
            {
                _uploader.RemoveFile(entity.ProfileImageUrl, FolderName);
                _uploader.RemoveFile(entity.ProfileImageUrl, FolderName);
                Result.Message = e.InnerException != null ? e.InnerException.Message : e.Message;
                Result = new ResponseResult(null, HttpStatusCode.InternalServerError, e, Result.Message);
                return Result;
            }
        }

        /// <summary>
        /// Add User
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<IFinalResult> AddAsync(AddUserDto model)
        {
            model.SessionDuration = 30;         // session duration set to 30s
            var user = Mapper.Map<AddUserDto, Entities.Entities.Identity.User>(model);
            
            // add portal as default app for employee
            await CheckForPortalApp(user);
            // check for employee contract type
            // CheckIsGovernmental(user, model);
            // creating user by default password as national id
            var result = await _userManager.CreateAsync(user, user.NationalId);

            if (!result.Succeeded)
            {
                return Result = new ResponseResult(null, HttpStatusCode.InternalServerError, null, result.Errors.First().ToString());
            }

            await AddPortalRole(user);
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("userName", user.UserName));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("name", user.FullNameEn));
            await _userManager.AddClaimAsync(user, new System.Security.Claims.Claim("email", user.Email));
            var registerResult = new ResponseResult(null, status: HttpStatusCode.Created,
                message: "Data Inserted Successfully");
            return registerResult;

        }
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IFinalResult> ChangePasswordAsync(ChangePasswordDto model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user != null)
            {
                var passwordResult = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                if (passwordResult.Succeeded)
                {
                    user.PasswordChanged = true;
                    user.PasswordChangedDate = DateTime.Now;
                    var userResult = await _userManager.UpdateAsync(user);
                    if (userResult.Succeeded)
                    {
                        Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                            message: "PasswordChangedSuccess");
                    }
                    else
                    {
                        Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.BadRequest,
                            message: "UpdatingUserErrorAfterChangePassword");
                    }

                }
                else
                {
                    Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.BadRequest,
                        message: "CurrentPasswordIncorrect");
                }
            }

            return Result;

        }
        //async Task CheckForPortalApp(Entity.User user, AddUserDto model)
        //{
        //    try
        //    {
        //        var appCode = _configuration["Apps:Portal"];
        //        var portalApp = await _appUnitOfWork.Repository.FirstOrDefaultAsync(a => a.Code == appCode);
        //        if (!model.Apps.Contains(portalApp.Id))
        //        {
        //            var userAppEntity = new Entity.UserApp
        //            {
        //                AppId = portalApp.Id
        //            };
        //            user.UserApps.Add(userAppEntity);
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        throw;
        //    }

        //}



        /// <summary>
        /// Delete By User App Id
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<IFinalResult> DeleteByUserAppId(long userId, long appId)
        {

            var userApp =
                await _userAppUnitOfWork.Repository.FirstOrDefaultAsync(x => x.AppId == appId && x.UserId == userId);
            _userAppUnitOfWork.Repository.Remove(userApp);
            var userRoles =
                await _userRoleUnitOfWork.Repository.FindAsync(
                    x => x.UserId == userId && x.AppId == appId);
            if (userRoles != null)
            {
                _userRoleUnitOfWork.Repository.RemoveRange(userRoles);
            }

            var affectedRows = await _userAppUnitOfWork.SaveChanges();
            if (affectedRows > 0)
            {
                Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                    message: "Data Deleted Successfully");
            }
            return Result;


        }

        #endregion




        #region Private Methods
        /// <summary>
        /// Check For Portal App
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        async Task CheckForPortalApp(Entities.Entities.Identity.User user)
        {

            var appCode = _configuration["Apps:Portal"];
            var portalApp = await _appUnitOfWork.Repository.FirstOrDefaultAsync(a => a.Code == appCode);
            var userAppEntity = new Entities.Entities.Identity.UserApp
            {
                AppId = portalApp.Id
            };
            user.UserApps.Add(userAppEntity);

        }
        /// <summary>
        /// Add Portal Role On Add
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        async Task AddPortalRole(Entities.Entities.Identity.User user)
        {

            var portalRoleCode = _configuration["Roles:Portal"];
            var portalRole = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(x => x.Code == portalRoleCode);
            var userRoles = await _userRoleUnitOfWork.Repository.FindAsync(u => u.UserId == user.Id);
            if (!userRoles.Select(x => x.RoleId).Contains(portalRole.Id))
            {
                if (portalRole.AppId != null)
                {
                    var userRole = new Entities.Entities.Identity.UserRole
                    {
                        UserId = user.Id,
                        RoleId = portalRole.Id,
                        AppId = (long)portalRole.AppId
                    };
                    _userRoleUnitOfWork.Repository.Add(userRole);
                }

                await _userRoleUnitOfWork.SaveChanges();
            }


        }
        /// <summary>
        /// Check For National Id At Add
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        async Task<IFinalResult> CheckForNationalIdAtAddMode(string nationalId)
        {
            var data = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.NationalId == nationalId);
            if (data != null)
            {
                Result = ResponseResult.PostResult(true, status: HttpStatusCode.OK,
                    message: HttpStatusCode.OK.ToString());
            }
            else
            {
                Result = ResponseResult.PostResult(false, status: HttpStatusCode.OK,
                    message: HttpStatusCode.OK.ToString());
            }

            return Result;
        }
        /// <summary>
        /// Check For Email On Add
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        async Task<IFinalResult> CheckForEmailAtAddMode(string email)
        {
            var data = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Email == email);
            if (data != null)
            {
                Result = ResponseResult.PostResult(true, status: HttpStatusCode.OK,
                    message: HttpStatusCode.OK.ToString());
            }
            else
            {
                Result = ResponseResult.PostResult(false, status: HttpStatusCode.OK,
                    message: HttpStatusCode.OK.ToString());
            }

            return Result;
        }
        /// <summary>
        /// Check For National Id On Edit
        /// </summary>
        /// <param name="nationalId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        async Task<IFinalResult> CheckForNationalIdAtEditMode(string nationalId, long userId)
        {
            var user = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == userId);
            var data = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.NationalId == nationalId);
            if (data != null)
            {
                // mean the same user name for the same user so return false as the name is available for him
                if (data.Id == user.Id)
                {
                    Result = ResponseResult.PostResult(false, status: HttpStatusCode.OK,
                        message: HttpStatusCode.OK.ToString());
                }
                else
                {
                    // means this username already taken by another user so return true as its not available for him
                    Result = ResponseResult.PostResult(true, status: HttpStatusCode.OK,
                        message: HttpStatusCode.OK.ToString());
                }

            }
            else
            {
                Result = ResponseResult.PostResult(false, status: HttpStatusCode.OK,
                    message: HttpStatusCode.OK.ToString());
            }

            return Result;
        }
        /// <summary>
        /// Check For Email On Edit
        /// </summary>
        /// <param name="email"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        async Task<IFinalResult> CheckForEmailAtEditMode(string email, long userId)
        {
            var user = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == userId);
            var data = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Email == email);
            if (data != null)
            {
                // mean the same user name for the same user so return false as the name is available for him
                if (data.Id == user.Id)
                {
                    Result = ResponseResult.PostResult(false, status: HttpStatusCode.OK,
                        message: HttpStatusCode.OK.ToString());
                }
                else
                {
                    // means this username already taken by another user so return true as its not available for him
                    Result = ResponseResult.PostResult(true, status: HttpStatusCode.OK,
                        message: HttpStatusCode.OK.ToString());
                }

            }
            else
            {
                Result = ResponseResult.PostResult(false, status: HttpStatusCode.OK,
                    message: HttpStatusCode.OK.ToString());
            }

            return Result;
        }
        /// <summary>
        /// Predicate Builder
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Identity.User, bool>> PredicateBuilderFunction(UserFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Identity.User>(x => !x.IsDeleted);


            if (filter.AppId != 0)
            {
                predicate = predicate.And(b => b.UserApps.Select(a => a.AppId).Contains(filter.AppId));
            }

            if (!string.IsNullOrWhiteSpace(filter.UserName))
            {
                predicate = predicate.And(b => b.UserName.ToLower().Contains(filter.UserName.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FullNameEn))
            {
                predicate = predicate.And(b => b.FullNameEn.ToLower().Contains(filter.FullNameEn.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FullNameAr))
            {
                predicate = predicate.And(b => b.FullNameAr.ToLower().Contains(filter.FullNameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.Email))
            {
                predicate = predicate.And(b => b.Email.ToLower().Contains(filter.Email.ToLower()));
            }
            return predicate;
        }

        static Expression<Func<Entities.Entities.Identity.User, bool>> PredicateBuilderFunction(UserSearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Identity.User>(true);
            if (!string.IsNullOrWhiteSpace(filter.SearchCriteria))
            {
                predicate = predicate.Or(b => b.FullNameAr.ToLower().Contains(filter.SearchCriteria.ToLower()));
                predicate = predicate.Or(b => b.Id.ToString().Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.NationalId.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.UserName.ToLower().Contains(filter.SearchCriteria.ToLower()));
            }
            if (filter.AppId != 0)
            {
                predicate = predicate.And(x => !x.UserApps.Select(userApp => userApp.AppId).Contains(filter.AppId));
            }
            return predicate;
        }

        #endregion
    }
}
