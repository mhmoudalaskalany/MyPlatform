using Domain.Core;
using Domain.DTO.Identity.PagePermission;
using Domain.DTO.Identity.Role;

namespace Domain.DTO.Identity.RolePermission
{
    public class RolePermissionDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public RoleDto Role { get; set; }
        public PagePermissionDto Permission { get; set; }

    }
}
