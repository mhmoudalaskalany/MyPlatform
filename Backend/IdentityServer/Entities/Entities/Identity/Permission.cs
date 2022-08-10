using System;
using System.Collections.Generic;
using Entities.Entities.Base;

namespace Entities.Entities.Identity
{
    public class Permission : BaseEntity
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
        public virtual ICollection<PagePermission> PagePermissions { get; set; } = new List<PagePermission>();
        public virtual ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    }
}
