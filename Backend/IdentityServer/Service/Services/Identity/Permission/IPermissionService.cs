using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.Permission;
using Domain.DTO.Identity.Permission.Parameters;
using Service.Services.Base;

namespace Service.Services.Identity.Permission
{
    public interface IPermissionService : IBaseService<Entities.Entities.Identity.Permission ,AddPermissionDto ,PermissionDto , Guid?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<PermissionFilter> filter);
    }
}
