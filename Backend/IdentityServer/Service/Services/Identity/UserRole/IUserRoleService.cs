using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Identity.UserRole;
using Service.Services.Base;

namespace Service.Services.Identity.UserRole
{
    public interface IUserRoleService : IBaseService<Entities.Entities.Identity.UserRole, AddUserRoleDto, UserRoleDto, Guid?>
    {
        Task<IFinalResult> GetByUserIdAsync(Guid userId, Guid appId);
        Task<IFinalResult> DeleteUserRoleAsync(AddUserRoleDto dto);
        Task<IFinalResult> AddMultipleRolesAsync(AddMultipleRolesDto model);
    }

}
