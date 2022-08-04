using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Abstraction.UnitOfWork;
using Common.DTO.Identity.Permission;
using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using LinqKit;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace Service.Services.Identity.User
{
    public class IdentityClaimsProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<Entities.Entities.Identity.User> _claimsFactory;
        private readonly UserManager<Entities.Entities.Identity.User> _userManager;
        private readonly IUnitOfWork<Entities.Entities.Identity.UserRole> _userRoleUnitOfWork;
        private readonly IUnitOfWork<Entities.Entities.Identity.Role> _roleUnitOfWork;


        public IdentityClaimsProfileService(
            UserManager<Entities.Entities.Identity.User> userManager,
            IUserClaimsPrincipalFactory<Entities.Entities.Identity.User> claimsFactory,
            IUnitOfWork<Entities.Entities.Identity.UserRole> userRoleUnitOfWork,
            IUnitOfWork<Entities.Entities.Identity.Role> roleUnitOfWork)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _userRoleUnitOfWork = userRoleUnitOfWork;
            _roleUnitOfWork = roleUnitOfWork;
        }

       
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var authMethod = context.Subject.Identity.GetAuthenticationMethod();
            // if external provider authentication
            if (authMethod.ToLower() == "external")
            {
                AddExternalProviderUserClaims(context, sub);
            }
            else
            {
                var user = await _userManager.FindByIdAsync(sub);
                var principal = await _claimsFactory.CreateAsync(user);

                var claims = principal.Claims.ToList();
                claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
                claims.Add(new Claim(JwtClaimTypes.GivenName, string.IsNullOrEmpty(user.FullNameEn) ? "" : user.FullNameEn));
                claims.Add(new Claim("arabicName", user.FullNameAr));
                claims.Add(new Claim("UserId", sub));
                claims.Add(new Claim("appCode", context.Client.ClientId));
                claims.Add(new Claim("NationalId", user.NationalId));
                //claims.Add(new Claim(IdentityServerConstants.StandardScopes.Email, string.IsNullOrEmpty(user.Email) ? "" : user.Email));
                await AddEmployeeClaims(claims, user);
                await AddUserClaims(claims, sub, context.Client.ClientId);
                // note: to dynamically add roles (ie. for users other than consumers - simply look them up by sub id            
                //claims.Add(new Claim(ClaimTypes.Role, principal.Claims.First(c => c.Type == "role").Value)); // need this for role-based authorization - https://stackoverflow.com/questions/40844310/role-based-authorization-with-identityserver4

                context.IssuedClaims = claims;
            }

        }
        
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var authMethod = context.Subject.Identity.GetAuthenticationMethod();
            // if external provider authentication
            if (authMethod.ToLower() != "external")
            {
                var sub = context.Subject.GetSubjectId();
                var user = await _userManager.FindByIdAsync(sub);
                context.IsActive = user != null;
            }
        }

     

       
        private async Task AddEmployeeClaims(List<Claim> claims, Entities.Entities.Identity.User user)
        {
            if (user != null)
            {
                try
                {


                    if (!string.IsNullOrWhiteSpace(user.FullNameEn))
                    {
                        claims.Add(new Claim("EmployeeEn", user.FullNameEn));
                    }
                    if (!string.IsNullOrWhiteSpace(user.FullNameEn))
                    {
                        claims.Add(new Claim("Email", user.Email));
                    }

                    if (!string.IsNullOrWhiteSpace(user.FullNameAr))
                    {
                        claims.Add(new Claim("EmployeeAr", user.FullNameAr));
                    }

                    claims.Add(new Claim("EmployeeId", string.IsNullOrEmpty(user.PersonId) ? "" : user.PersonId));

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }



            }
        }

     
        private async Task AddUserClaims(List<Claim> claims, string userId, string clientId)
        {
            try
            {
                // get all user roles and include app with each role
                var userRolePredicate = PredicateBuilderFunction(long.Parse(userId), false, clientId);
                var userRoles = await _userRoleUnitOfWork.Repository.FindAsync(userRolePredicate,
                    include: source => source.Include(x => x.App));
                //var dic = new Dictionary<string, Dictionary<string, List<string>>>();
                var claimDtos = new List<ClaimDto>();
                // loop on roles to get each role claims
                foreach (var userRole in userRoles)
                {
                    var role = await _roleUnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == userRole.RoleId);
                    // var roleClaims = await _roleManager.GetClaimsAsync(role);
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
                    // claimDto.Permissions.AddRange(roleClaims.Select(x => x.Value).ToList());
                    claimDtos.Add(claimDto);

                }
                claims.Add(new Claim("roles", JsonConvert.SerializeObject(claimDtos)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
        
        private void AddExternalProviderUserClaims(ProfileDataRequestContext context, string sub)
        {
            var clientId = context.Client.ClientId;
            var claims = context.Subject.Identities.FirstOrDefault()?.Claims?.ToList();
            claims?.Add(new Claim(JwtClaimTypes.GivenName, context.Subject.Identity.Name));
            claims?.Add(new Claim("arabicName", context.Subject.Identity.Name));
            claims?.Add(new Claim("UserId", sub));
            claims?.Add(new Claim("appCode", context.Client.ClientId));
            var claimDtos = new List<ClaimDto>();
            var claimDto = new ClaimDto();
            if (clientId == "PORTAL")
            {
                claimDto.AppCode = "PORTAL";
                claimDto.AppName = "Portal";
                claimDto.AppId = 2;
                claimDto.RoleName = "Portal User";
                claimDto.RoleNameAr = "مستخدم بوابة";
                claimDto.RoleId = 2;
                claimDto.Permissions.Add("Permission.PORTAL-APPLICATIONS.View");
                claimDtos.Add(claimDto);
            }
            else if (clientId == "IPPHONE")
            {
                claimDto.AppCode = "IPPHONE";
                claimDto.AppName = "IPPhone";
                claimDto.AppId = 7;
                claimDto.RoleName = "Portal User";
                claimDto.RoleNameAr = "مستخدم بوابة";
                claimDto.RoleId = 2;
                claimDto.Permissions.Add("Permission.IPPHONE-EMPLOYEE.View");
                claimDto.Permissions.Add("Permission.IPPHONE-EMPLOYEE.Add");
                claimDto.Permissions.Add("Permission.IPPHONE-EMPLOYEE.Edit");
                claimDto.Permissions.Add("Permission.IPPHONE-EMPLOYEE.Delete");
                claimDtos.Add(claimDto);
            }

            claims?.Add(new Claim("roles", JsonConvert.SerializeObject(claimDtos)));
            context.IssuedClaims = claims;
        }
        
        static Expression<Func<Entities.Entities.Identity.UserRole, bool>> PredicateBuilderFunction(long userId, bool isDeleted, string clientId)
        {
            try
            {
                var predicate = PredicateBuilder.New<Entities.Entities.Identity.UserRole>(true);
                if (!string.IsNullOrWhiteSpace(userId.ToString()))
                {
                    predicate = predicate.And(b => b.UserId == userId && b.App.Code == clientId);
                }

                predicate = predicate.And(b => b.IsDeleted == isDeleted);
                return predicate;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }


    }
}
