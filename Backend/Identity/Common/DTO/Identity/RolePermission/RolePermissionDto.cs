using Common.Core;
using Common.DTO.Identity.PagePermission;
using Common.DTO.Identity.Role;

namespace Common.DTO.Identity.RolePermission
{
    public class RolePermissionDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public RoleDto Role { get; set; }
        public PagePermissionDto Permission { get; set; }

    }
}
