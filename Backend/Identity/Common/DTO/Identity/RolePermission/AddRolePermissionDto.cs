using Common.Core;

namespace Common.DTO.Identity.RolePermission
{
    class AddRolePermissionDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public long RoleId { get; set; }
        public long PagePermissionId { get; set; }
    }
}
