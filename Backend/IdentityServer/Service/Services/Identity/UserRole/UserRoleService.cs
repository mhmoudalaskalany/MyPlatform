using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Abstraction.UnitOfWork;
using Domain.Core;
using Domain.DTO.Identity.UserRole;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Service.Services.Base;
using Service.Services.Identity.Role;

namespace Service.Services.Identity.UserRole
{
    public class UserRoleService : BaseService<Entities.Entities.Identity.UserRole, AddUserRoleDto, UserRoleDto, long?>, IUserRoleService
    {
        private readonly IUnitOfWork<Entities.Entities.Identity.Role> _roleUnitOfWork;
        private readonly IUnitOfWork<Entities.Entities.Identity.App> _appUnitOfWork;
        private readonly IRoleService _roleService;
        private readonly IConfiguration _configuration;
        public UserRoleService(
            IServiceBaseParameter<Entities.Entities.Identity.UserRole> businessBaseParameter, IUnitOfWork<Entities.Entities.Identity.Role> roleUnitOfWork, IConfiguration configuration, IUnitOfWork<Entities.Entities.Identity.App> appUnitOfWork , IRoleService roleService) : base(
            businessBaseParameter)
        {
            _roleUnitOfWork = roleUnitOfWork;
            _configuration = configuration;
            _appUnitOfWork = appUnitOfWork;
            _roleService = roleService;
        }

        #region Public Methods
        /// <summary>
        /// Get User Roles By User Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<IFinalResult> GetByIdAsync(object id)
        {

            var userRoles = await UnitOfWork.Repository.FindAsync(x => x.UserId == Convert.ToInt64(id)
                                                                       && (x.IsDeleted != false || x.IsDeleted != null), include: src => src.Include(e => e.App));

            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.UserRole>, IEnumerable<UserRoleDto>>(userRoles);
            foreach (var userRole in data)
            {
                var app = await _appUnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == userRole.AppId);
                var role = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                userRole.AppNameEn = app.NameEn;
                userRole.AppNameAr = app.NameAr;
                userRole.RoleNameEn = role.NameEn;
                userRole.RoleNameAr = role.NameAr;

            }
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        /// <summary>
        ///  For Other Systems don't return the portal role
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetByUserIdAsync(long userId, long appId)
        {

            var userRole =
                await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.UserId == userId && x.AppId == appId);
            // means user have no role on that app and its adding mode so return 
            var data = Mapper.Map<Entities.Entities.Identity.UserRole, AddUserRoleDto>(userRole);
            if (data == null)
            {
                data = new AddUserRoleDto
                {
                    AppId = appId,
                    UserId = userId,
                    RoleId = null
                };
            }
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        /// <summary>
        /// Update User Roles
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<IFinalResult> UpdateAsync(AddUserRoleDto model)
        {

            var portalCode = _configuration["Roles:Portal"];
            var portalRole = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(x => x.Code == portalCode);
            var userRoles = await UnitOfWork.Repository.FindAsync(PredicateBuilderFunction(model, portalRole.Id));
            UnitOfWork.Repository.RemoveRange(userRoles);


            if (model.RoleId != portalRole.Id)
            {
                await base.AddAsync(model);
            }


            Result = new ResponseResult(result: null, status: HttpStatusCode.Created,
                message: "Data Updated Successfully");
            return Result;
        }


        /// <summary>
        /// Add Multiple User Roles
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IFinalResult> AddMultipleRolesAsync(AddMultipleRolesDto model)
        {
            var assignedRoles =  await UnitOfWork.Repository.FindAsync(x => x.UserId == model.UserId && x.AppId == model.AppId);

            if(model.RoleIds.Count == 0)
            {
                UnitOfWork.Repository.RemoveRange(assignedRoles);

                await UnitOfWork.SaveChanges();

                return Result = new ResponseResult(result: null, status: HttpStatusCode.Accepted,
                    message: "Data Updated Successfully");

            }

            foreach (var role in assignedRoles)
            {
                   
                  if(!model.RoleIds.Contains(role.RoleId))
                  {
                      UnitOfWork.Repository.Remove(role);
                      await UnitOfWork.SaveChanges();
                  }
            }

            foreach (var id in model.RoleIds)
            {

                    var role = assignedRoles.FirstOrDefault(x => x.RoleId == id);

                    if (role == null)
                    {
                        var newRole = new AddUserRoleDto
                        {
                            AppId = model.AppId,
                            UserId = model.UserId,
                            RoleId = id
                        };
                        await base.AddAsync(newRole);
                    }
            }


            Result = new ResponseResult(result: null, status: HttpStatusCode.Accepted,
                message: "Data Updated Successfully");


            return Result;
        }



        /// <summary>
        /// Delete User Role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IFinalResult> DeleteUserRoleAsync(AddUserRoleDto dto)
        {

            var portalCode = _configuration["Roles:Portal"];
            var portalRole = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(x => x.Code == portalCode);
            var entity = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.UserId == dto.UserId
             && x.AppId == dto.AppId && x.RoleId == dto.RoleId && (x.IsDeleted != false || x.IsDeleted != null));
            if (entity.RoleId != portalRole.Id)
            {
                UnitOfWork.Repository.Remove(entity);
                await UnitOfWork.SaveChanges();
                Result = new ResponseResult(result: null, status: HttpStatusCode.NoContent,
                    message: "Data Deleted Successfully");
            }
            else
            {
                Result = new ResponseResult(result: null, status: HttpStatusCode.BadRequest,
                    message: "ErrorDeletePortal");
            }
            return Result;

        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Predicate Builder
        /// </summary>
        /// <param name="model"></param>
        /// <param name="portalRoleId"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Identity.UserRole, bool>> PredicateBuilderFunction(AddUserRoleDto model, long portalRoleId)
        {

            var predicate = PredicateBuilder.New<Entities.Entities.Identity.UserRole>(true);

            predicate = predicate.And(b => b.AppId == model.AppId && b.UserId == model.UserId && b.RoleId != portalRoleId);

            return predicate;
        }

        #endregion

    }
}
