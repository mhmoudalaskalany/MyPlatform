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
        Task<IFinalResult> GetByAppIdAsync(long appId);
        Task<IFinalResult> GetUnassignedByAppIdAsync(long appId, long useId);
        Task<IFinalResult> GetAssignedByAppIdAsync(long appId, long useId);
        Task<IFinalResult> GetRoleByIdAsync(long id);
        Task<IFinalResult> AddRoleAsync(AddRoleDto dto);
        Task<DataPaging> GetAllPagedAsync(BaseParam<RoleFilter> filter);
    }
}
