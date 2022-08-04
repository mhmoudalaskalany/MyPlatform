using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Service.Services.Identity.Role
{
    public class RoleValidator<TRole> : IRoleValidator<TRole>
        where TRole : Entities.Entities.Identity.Role
    {
        #region Public Methods
        /// <summary>
        /// Validate Role 
        /// </summary>
        /// <param name="manager"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<IdentityResult> ValidateAsync(RoleManager<TRole> manager,
            TRole role)
        {
            // means its update so pass this validation
            if (role.Id != 0)
            {
                return IdentityResult.Success;
            }
            var owner = await manager.Roles.FirstOrDefaultAsync(x => x.AppId == role.AppId && x.Name == role.Name);

            if (owner == null)
            {
                return IdentityResult.Success;
            }

            return
                IdentityResult.Failed(new IdentityError
                {
                    Code = "DuplicatedRole",
                    Description = "Role Already Added On This Tenant"
                });
        }

        #endregion

    }
}
