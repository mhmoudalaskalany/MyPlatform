using System;
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities.Identity
{
    public class UserRole : IdentityUserRole<long>
    {
        public long? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public long? ModifiedById { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public long AppId { get; set; }
        public App App { get; set; }
    }
}
