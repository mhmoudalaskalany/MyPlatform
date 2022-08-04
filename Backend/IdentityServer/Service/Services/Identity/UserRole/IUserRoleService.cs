using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Identity.UserRole;
using Service.Services.Base;

namespace Service.Services.Identity.UserRole
{
    public interface IUserRoleService : IBaseService<Entities.Entities.Identity.UserRole, AddUserRoleDto, UserRoleDto, long?>
    {
        Task<IResult> GetByUserIdAsync(long userId, long appId);
        Task<IResult> DeleteUserRoleAsync(AddUserRoleDto dto);
        Task<IResult> AddMultipleRolesAsync(AddMultipleRolesDto model);
    }

}
