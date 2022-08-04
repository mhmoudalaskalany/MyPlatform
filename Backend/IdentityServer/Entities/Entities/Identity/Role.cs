using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities.Identity
{
    public class Role : IdentityRole<long>
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public long? ModifiedById { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public long? AppId { get; set; }
        public App App { get; set; }
        public string Code { get; set; }
        public virtual  ICollection<RolePermission> RolePermissions { get; set; } = new Collection<RolePermission>();

    }
}
