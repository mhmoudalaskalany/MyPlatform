using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.Role;
using Domain.DTO.Identity.Role.Parameters;
using Service.Services.Base;

namespace Service.Services.Identity.Role
{
    public interface IRoleService : IBaseService<Entities.Entities.Identity.Role, AddRoleDto, RoleDto, Guid?>
    {
        Task<IFinalResult> GetByAppIdAsync(Guid appId);
        Task<IFinalResult> GetUnassignedByAppIdAsync(Guid appId, Guid useId);
        Task<IFinalResult> GetAssignedByAppIdAsync(Guid appId, Guid useId);
        Task<IFinalResult> GetRoleByIdAsync(Guid id);
        Task<IFinalResult> AddRoleAsync(AddRoleDto dto);
        Task<DataPaging> GetAllPagedAsync(BaseParam<RoleFilter> filter);
    }
}
