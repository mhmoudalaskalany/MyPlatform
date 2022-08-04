using Entities.Entities.Base;

namespace Entities.Entities.Identity
{
    public class RolePermission : BaseEntity
    {
        public long RoleId { get; set; }
        public Role Role { get; set; }
        public long PageId { get; set; }
        public Page Page { get; set; }
        public long PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
