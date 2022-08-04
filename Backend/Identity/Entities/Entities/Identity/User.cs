using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.AspNetCore.Identity;

namespace Entities.Entities.Identity
{
    public class User :  IdentityUser<long> 
    {
        public string FullNameAr { get; set; }
        public string FullNameEn { get; set; }
        public string NationalId { get; set; }
        public string Position { get; set; }
        public long? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } 
        public long? ModifiedById { get; set; }
        public bool? PasswordChanged { get; set; }
        public string ProfileImageUrl { get; set; }
        public DateTime? PasswordChangedDate { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; }
        public string UserTypeId { get; set; } // map to employee id or any other different user type
        public string PersonId { get; set; }
        public long SessionDuration { get; set; }
        public virtual ICollection<UserApp> UserApps { get; set; } = new Collection<UserApp>();
    }
}
