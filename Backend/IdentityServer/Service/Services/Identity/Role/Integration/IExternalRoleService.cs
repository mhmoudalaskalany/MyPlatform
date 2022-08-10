using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Identity.Role;
using Service.Services.Base;

namespace Service.Services.Identity.Role.Integration
{
   public interface IExternalRoleService : IBaseService<Entities.Entities.Identity.Role, AddRoleDto, RoleDto , long?>
    {

        Task<IFinalResult> GetByAppIdAsync(long appId);
    }
}
