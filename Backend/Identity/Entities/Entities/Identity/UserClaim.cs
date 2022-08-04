using System;
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities.Identity
{
    public class UserClaim : IdentityUserClaim<long>
    {
        public long? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public long? ModifiedById { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
