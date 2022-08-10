using System;
using Entities.Entities.Base;

namespace Entities.Entities.Identity
{
    public class UserApp : BaseEntity
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid AppId { get; set; }
        public App App { get; set; }
    }
}
