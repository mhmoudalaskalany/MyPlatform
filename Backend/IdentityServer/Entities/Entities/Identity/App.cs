using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entities.Entities.Base;

namespace Entities.Entities.Identity
{
    public class App : BaseEntity
    {
        public long Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string AppUrl { get; set; }
        public string AppIcon { get; set; }
        public string AppColor { get; set; }
        public string Code { get; set; }
        public bool? IsPublic { get; set; } = false;
        public virtual ICollection<Role> Roles { get; set; } = new Collection<Role>();
        public virtual ICollection<UserApp> UserApps { get; set; } = new Collection<UserApp>();
        public virtual ICollection<Page> Pages { get; set; } = new Collection<Page>();
        public virtual ICollection<UserRole> UserRoles { get; set; } = new Collection<UserRole>();

    }
}
