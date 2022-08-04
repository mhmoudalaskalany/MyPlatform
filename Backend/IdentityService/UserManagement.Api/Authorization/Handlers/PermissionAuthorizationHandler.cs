﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Entities.Entities;
using Entities.Entities.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using UserManagement.Api.Authorization.Requirements;
using Permission = Domain.Extensions.Permission;

namespace UserManagement.Api.Authorization.Handlers
{
    internal class PermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
    {
        readonly UserManager<User> _userManager;
        readonly RoleManager<Role> _roleManager;

        public PermissionAuthorizationHandler(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            try
            {
                if (context.User == null)
                {
                    return;
                }

                // Get all the roles the user belongs to and check if any of the roles has the permission required
                // for the authorization to succeed.
                var user = await _userManager.GetUserAsync(context.User);
                var userRoleNames = await _userManager.GetRolesAsync(user);
                var userRoles = _roleManager.Roles.Where(x => userRoleNames.Contains(x.Name)).ToList();

                foreach (var role in userRoles)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(role);
                    var permissions = roleClaims.Where(x => x.Type == Permission.CustomClaimTypes.Permission &&
                                                            x.Value == requirement.Permission &&
                                                            x.Issuer == "LOCAL AUTHORITY")
                        .Select(x => x.Value);

                    if (permissions.Any())
                    {
                        context.Succeed(requirement);
                        return;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
