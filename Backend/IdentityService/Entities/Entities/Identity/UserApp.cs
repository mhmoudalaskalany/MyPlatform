using Entities.Entities.Base;

namespace Entities.Entities.Identity
{
    public class UserApp : BaseEntity
    {
        public long UserId { get; set; }
        public User User { get; set; }
        public long AppId { get; set; }
        public App App { get; set; }
    }
}
