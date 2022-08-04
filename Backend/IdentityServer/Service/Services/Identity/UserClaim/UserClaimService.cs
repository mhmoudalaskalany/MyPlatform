using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Abstraction.UnitOfWork;
using Domain.Core;
using Domain.DTO.Identity.Permission;
using LinqKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Service.Services.Identity.UserClaim
{
    public class UserClaimService : IUserClaimService
    {
        private readonly RoleManager<Entities.Entities.Identity.Role> _roleManager;
        private readonly IUnitOfWork<Entities.Entities.Identity.UserRole> _userRoleUnitOfWork;
        private readonly IUnitOfWork<Entities.Entities.Identity.Role> _roleUnitOfWork;

        public UserClaimService(
            IUnitOfWork<Entities.Entities.Identity.UserRole> userRoleUnitOfWork,
            IUnitOfWork<Entities.Entities.Identity.Role> roleUnitOfWork,
            RoleManager<Entities.Entities.Identity.Role> roleManager
        )
        {
            _userRoleUnitOfWork = userRoleUnitOfWork;
            _roleUnitOfWork = roleUnitOfWork;
            _roleManager = roleManager;
        }

        #region Public Methods

        public async Task<IResult> GeyUserClaimsAsync(long userId, string authMethod, string appCode)
        {
            try
            {
                var claimDtos = new List<ClaimDto>();
                if (authMethod.ToLower() == "external")
                {
                    claimDtos = AddExternalProviderUserClaims(appCode);
                    return new ResponseResult(claimDtos, status: HttpStatusCode.OK,
                        message: HttpStatusCode.OK.ToString());
                }
                // get all user roles and include app with each role
                var userRolePredicate = PredicateBuilderFunction(userId, false, appCode);
                var userRoles = await _userRoleUnitOfWork.Repository.FindAsync(userRolePredicate,
                    include: source => source.Include(x => x.App));
                //var dic = new Dictionary<string, Dictionary<string, List<string>>>();

                // loop on roles to get each role claims
                foreach (var userRole in userRoles)
                {
                    var role = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    var claimDto = new ClaimDto
                    {
                        AppName = userRole.App.NameEn,
                        AppCode = userRole.App.Code,
                        AppId = userRole.AppId,
                        RoleName = role.Name,
                        RoleNameAr = role.NameAr,
                        RoleId = role.Id,
                        RoleCode = role.Code
                    };
                    claimDto.Permissions.AddRange(roleClaims.Select(x => x.Value).ToList());
                    claimDtos.Add(claimDto);

                }


                return new ResponseResult(claimDtos, status: HttpStatusCode.OK,
                    message: HttpStatusCode.OK.ToString());
            }
            catch (Exception e)
            {
                var result = new Result {Message = e.InnerException != null ? e.InnerException.Message : e.Message};
                result = new ResponseResult(null, status: HttpStatusCode.InternalServerError, exception: e,
                    message: result.Message);
                return result;
            }
        }

        #endregion

        #region Private Methods

        private List<ClaimDto> AddExternalProviderUserClaims(string appCode)
        {
            var claimDtos = new List<ClaimDto>();
            var claimDto = new ClaimDto();
            if (appCode == "PORTAL")
            {
                claimDto.AppCode = "PORTAL";
                claimDto.AppName = "Portal";
                claimDto.AppId = 2;
                claimDto.RoleName = "Portal User";
                claimDto.RoleNameAr = "مستخدم بوابة";
                claimDto.RoleId = 2;
                claimDto.Permissions.Add("Permission.Applications.View");
                claimDtos.Add(claimDto);
            }
            else if (appCode == "IPPHONE")
            {
                claimDto.AppCode = "IPPHONE";
                claimDto.AppName = "IPPhone";
                claimDto.AppId = 7;
                claimDto.RoleName = "Portal User";
                claimDto.RoleNameAr = "مستخدم بوابة";
                claimDto.RoleId = 2;
                claimDto.Permissions.Add("Permission.Employees.View");
                claimDto.Permissions.Add("Permission.Employees.Add");
                claimDto.Permissions.Add("Permission.Employees.Edit");
                claimDto.Permissions.Add("Permission.Employees.Delete");
                claimDtos.Add(claimDto);
            }
            return claimDtos;
        }

        static Expression<Func<Entities.Entities.Identity.UserRole, bool>> PredicateBuilderFunction(long userId, bool isDeleted, string appCode)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Identity.UserRole>(true);
            if (!string.IsNullOrWhiteSpace(userId.ToString()))
            {
                predicate = predicate.And(b => b.UserId == userId && b.App.Code == appCode);
            }

            predicate = predicate.And(b => b.IsDeleted == isDeleted);
            return predicate;
        }

        #endregion

    }
}
