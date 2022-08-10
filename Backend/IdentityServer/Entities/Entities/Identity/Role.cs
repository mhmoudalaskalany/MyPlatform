using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities.Identity
{
    public class Role : IdentityRole<Guid>
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public Guid? ModifiedById { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public Guid? AppId { get; set; }
        public App App { get; set; }
        public string Code { get; set; }
        public virtual  ICollection<RolePermission> RolePermissions { get; set; } = new Collection<RolePermission>();

    }
}
