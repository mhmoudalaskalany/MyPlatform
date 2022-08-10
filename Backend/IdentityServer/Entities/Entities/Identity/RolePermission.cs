using System;
using Entities.Entities.Base;

namespace Entities.Entities.Identity
{
    public class RolePermission : BaseEntity
    {
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Guid PageId { get; set; }
        public Page Page { get; set; }
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
