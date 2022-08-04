using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.Role;
using Domain.DTO.Identity.Role.Parameters;
using Service.Services.Base;

namespace Service.Services.Identity.Role
{
    public interface IRoleService : IBaseService<Entities.Entities.Identity.Role, AddRoleDto, RoleDto, long?>
    {
        Task<IResult> GetByAppIdAsync(long appId);
        Task<IResult> GetUnassignedByAppIdAsync(long appId, long useId);
        Task<IResult> GetAssignedByAppIdAsync(long appId, long useId);
        Task<IResult> GetRoleByIdAsync(long id);
        Task<IResult> AddRoleAsync(AddRoleDto dto);
        Task<DataPaging> GetAllPagedAsync(BaseParam<RoleFilter> filter);
    }
}
