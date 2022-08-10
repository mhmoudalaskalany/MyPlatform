using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Domain.Abstraction.UnitOfWork;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.Role;
using Domain.DTO.Identity.Role.Parameters;
using Domain.DTO.Identity.RoleClaim;
using Entities.Entities.Identity;
using LinqKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Service.Services.Base;

namespace Service.Services.Identity.Role
{
    public class RoleService : BaseService<Entities.Entities.Identity.Role, AddRoleDto, RoleDto, long?>, IRoleService
    {
        private readonly RoleManager<Entities.Entities.Identity.Role> _roleManager;
        private readonly IUnitOfWork<Entities.Entities.Identity.Page> _pageUnitOfWork;
        private readonly IUnitOfWork<Entities.Entities.Identity.Permission> _permissionUnitOfWork;
        private readonly IUnitOfWork<RolePermission> _rolePermissionOfWork;
        public RoleService(
            IServiceBaseParameter<Entities.Entities.Identity.Role> parameters,
            RoleManager<Entities.Entities.Identity.Role> roleManager,
            IUnitOfWork<Entities.Entities.Identity.Page> pageUnitOfWork,
            IUnitOfWork<Entities.Entities.Identity.Permission> permissionUnitOfWork,
            IUnitOfWork<RolePermission> rolePermissionOfWork
            ) : base(parameters)
        {
            _roleManager = roleManager;
            _pageUnitOfWork = pageUnitOfWork;
            _permissionUnitOfWork = permissionUnitOfWork;
            _rolePermissionOfWork = rolePermissionOfWork;
        }

        #region Public Methods

        public async Task<IFinalResult> GetRoleByIdAsync(long id)
        {

            var pagesList = new List<RolePagePermissionDto>();
            // get role permissions
            var rolePermissions = await _rolePermissionOfWork.Repository
                .FindAsync(x => x.RoleId == id);

            var permissions = rolePermissions.ToList();

            // get all page ids
            var pageIds = permissions.Select(x => x.PageId).Distinct().ToList();

            foreach (var pageId in pageIds)
            {
                // get permissions of each page
                var pagePermissionsSelectedIds = permissions.Where(r => r.RoleId == id && r.PageId == pageId)
                    .Select(x => x.PermissionId).ToList();

                // get all page permissions from lookup and include it
                var pageWithPermissions = await _pageUnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == pageId,
                    include: src => src.Include(per => per.PagePermissions)
                        .ThenInclude(p => p.Permission));

                var pagePermissionsList = new List<PermissionPageDto>();
                foreach (var pagePermission in pageWithPermissions.PagePermissions)
                {
                    var permissionDto = new PermissionPageDto
                    {
                        Id = pagePermission.PermissionId,
                        NameEn = pagePermission.Permission.NameEn,
                        NameAr = pagePermission.Permission.NameAr
                    };
                    if (pagePermissionsSelectedIds.Contains(pagePermission.PermissionId))
                    {
                        permissionDto.IsSelected = true;
                    }
                    pagePermissionsList.Add(permissionDto);
                }

                var pagePermissionDto = new RolePagePermissionDto
                {
                    Id = pageWithPermissions.Id,
                    NameEn = pageWithPermissions.NameEn,
                    NameAr = pageWithPermissions.NameAr,
                    PagePermissions = pagePermissionsList
                };
                // if the permissions selected are the same for  the page in lookup make AllSelected True
                if (pagePermissionsSelectedIds.Count == pageWithPermissions.PagePermissions.Count)
                {
                    pagePermissionDto.AllSelected = true;
                }
                pagesList.Add(pagePermissionDto);

            }



            var entity = await UnitOfWork.Repository.FirstOrDefaultAsync(r => r.Id == id);
            var data = Mapper.Map<Entities.Entities.Identity.Role, AddRoleDto>(entity);
            pagesList = await GetRemainingPages(pagesList, pageIds, entity.AppId);
            data.RolePagePermissions = pagesList;
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }

        public async Task<IFinalResult> GetByAppIdAsync(long appId)
        {

            var entities = await UnitOfWork.Repository.FindAsync(r => r.AppId == appId);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.Role>, IEnumerable<RoleDto>>(entities);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        public async Task<IFinalResult> GetUnassignedByAppIdAsync(long appId, long userId)
        {


            var userRoles = await UnitOfWork.GetRepository<Entities.Entities.Identity.UserRole>().FindAsync(x => x.UserId == userId);
            var userRoleIds = userRoles.Select(x => x.RoleId);

            var entities = await UnitOfWork.Repository.FindAsync(r => r.AppId == appId && !userRoleIds.Contains(r.Id));


            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.Role>, IEnumerable<RoleDto>>(entities);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }

        public async Task<IFinalResult> GetAssignedByAppIdAsync(long appId, long userId)
        {


            var userRoles = await UnitOfWork.GetRepository<Entities.Entities.Identity.UserRole>().FindAsync(x => x.UserId == userId);
            var userRoleIds = userRoles.Select(x => x.RoleId);

            var entities = await UnitOfWork.Repository.FindAsync(r => r.AppId == appId && userRoleIds.Contains(r.Id));


            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.Role>, IEnumerable<RoleDto>>(entities);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        public async Task<DataPaging> GetAllPagedAsync(BaseParam<RoleFilter> filter)
        {

            int limit = filter.PageSize;
            int offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter),
                  skip: offset, take: limit, filter.OrderByValue, src => src.Include(a => a.App));

            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.Role>, IEnumerable<RoleDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }
        /// <summary>
        /// Add Role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IFinalResult> AddRoleAsync(AddRoleDto dto)
        {

            var newRole = Mapper.Map<AddRoleDto, Entities.Entities.Identity.Role>(dto);
            var result = await _roleManager.CreateAsync(newRole);
            if (result.Succeeded)
            {
                var role = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == newRole.NameEn && x.AppId == dto.AppId);
                await AddRoleClaimsAsync(role, dto);
            }

            Result = new ResponseResult(result: null, status: HttpStatusCode.Created,
                message: "Data Inserted Successfully");
            return Result;

        }
        /// <summary>
        /// Update Role
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public override async Task<IFinalResult> UpdateAsync(AddRoleDto dto)
        {

            var role = await _roleManager.Roles.FirstOrDefaultAsync(r => r.Id == dto.Id && r.AppId == dto.AppId);

            //role = Mapper.Map<AddRoleDto, Entity.Role>(dto);
            role.NameEn = dto.NameEn;
            role.NameAr = dto.NameAr;
            role.Name = dto.NameEn;
            role.NormalizedName = dto.NameEn;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                await AddRoleClaimsAsync(role, dto, false);
            }
            Result = new ResponseResult(result: null, status: HttpStatusCode.Accepted,
                message: "Data Updated Successfully");
            return Result;

        }

        #endregion


        #region Private Methods
        /// <summary>
        /// Add Role Claims
        /// </summary>
        /// <param name="role"></param>
        /// <param name="dto"></param>
        /// <param name="isNew"></param>
        /// <returns></returns>
        async Task AddRoleClaimsAsync(Entities.Entities.Identity.Role role, AddRoleDto dto, bool isNew = true)
        {
            // if updating role delete all old claims and add the new one
            if (!isNew)
            {
                await RemoveOldRole(role);
            }
            foreach (var page in dto.PagePermissions)
            {
                // If Page Has permissions selected from front end
                if (page.PermissionIds.Count > 0)
                {
                    var predicate = PredicateBuilderFunction(page.PageId);
                    var pageInDb = await _pageUnitOfWork.Repository.FirstOrDefaultAsync(predicate);
                    var pagePermissions =
                        await _permissionUnitOfWork.Repository.FindAsync(per => page.PermissionIds.Contains(per.Id));
                    foreach (var permission in pagePermissions)
                    {
                        // add role claims to role claims table
                        var claim = new Claim(Domain.Extensions.Permission.CustomClaimTypes.Permission,
                            "Permission." + pageInDb.Code + "." + permission.NameEn);
                        await _roleManager.AddClaimAsync(role, claim);
                        // add role and page and permission id to role permission table
                        var rolePermission = new RolePermission
                        {
                            PageId = page.PageId,
                            PermissionId = permission.Id,
                            RoleId = role.Id
                        };
                        _rolePermissionOfWork.Repository.Add(rolePermission);
                        await _rolePermissionOfWork.SaveChanges();
                    }
                }
            }
        }
        /// <summary>
        /// Remove Old Role With Claims And Permissions
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        async Task RemoveOldRole(Entities.Entities.Identity.Role role)
        {

            if (await _roleManager.RoleExistsAsync(role.Name))
            {
                var claimsList = await _roleManager.GetClaimsAsync(role);
                foreach (var claim in claimsList)
                {
                    await _roleManager.RemoveClaimAsync(role, claim);
                }

                var oldRolePermissions =
                    await _rolePermissionOfWork.Repository.FindAsync(x => x.RoleId == role.Id, disableTracking: true);
                _rolePermissionOfWork.Repository.RemoveRange(oldRolePermissions);
                await _rolePermissionOfWork.SaveChanges();

            }

        }
        /// <summary>
        /// Get Remaining Pages
        /// </summary>
        /// <param name="pagesList"></param>
        /// <param name="pageIds"></param>
        /// <param name="appId"></param>
        /// <returns></returns>
        async Task<List<RolePagePermissionDto>> GetRemainingPages(List<RolePagePermissionDto> pagesList, List<long> pageIds, long? appId)
        {
            var unSelectedPages = await _pageUnitOfWork.Repository
                .FindAsync(x => x.AppId == appId && !pageIds.Contains(x.Id),
                    include: src => src.Include(x => x.PagePermissions)
                         .ThenInclude(p => p.Permission));
            foreach (var page in unSelectedPages)
            {
                var pageDto = new RolePagePermissionDto
                {
                    Id = page.Id,
                    NameEn = page.NameEn,
                    NameAr = page.NameAr,
                    AllSelected = false
                };
                foreach (var permission in page.PagePermissions)
                {
                    var permissionDto = new PermissionPageDto
                    {
                        Id = permission.PermissionId,
                        NameEn = permission.Permission.NameEn,
                        NameAr = permission.Permission.NameAr,
                        IsSelected = false
                    };
                    pageDto.PagePermissions.Add(permissionDto);
                }
                pagesList.Add(pageDto);
            }

            var orderedList = pagesList.OrderBy(x => x.NameEn).ToList();
            return orderedList;
        }
        /// <summary>
        /// Role Predicate Builder
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Identity.Role, bool>> PredicateBuilderFunction(RoleFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Identity.Role>(true);

            if (!string.IsNullOrWhiteSpace(filter?.NameAr))
            {
                predicate = predicate.And(b => b.NameAr.ToLower().Contains(filter.NameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter?.NameEn))
            {
                predicate = predicate.And(b => b.NameEn.ToLower().Contains(filter.NameEn.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter?.AppNameAr))
            {
                predicate = predicate.And(b => b.App.NameAr.ToLower().Contains(filter.AppNameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter?.AppNameEn))
            {
                predicate = predicate.And(b => b.App.NameEn.ToLower().Contains(filter.AppNameEn.ToLower()));
            }

            if (filter?.AppId != 0 && filter?.AppId != null)
            {
                predicate = predicate.And(b => b.AppId == filter.AppId);
            }

            return predicate;
        }
        /// <summary>
        /// Page Predicate Builder
        /// </summary>
        /// <param name="pageId"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Identity.Page, bool>> PredicateBuilderFunction(long pageId)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Identity.Page>(true);
            if (!string.IsNullOrWhiteSpace(pageId.ToString()))
            {
                predicate = predicate.And(b => b.Id == pageId);
            }
            return predicate;
        }

        #endregion


    }
}
