using System;
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities.Identity
{
    public class UserToken : IdentityUserToken<Guid>
    {
        public Guid? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public Guid? ModifiedById { get; set; }
        public bool? IsDeleted { get; set; } = false;
    }
}
