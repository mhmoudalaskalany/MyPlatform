using Common.Core;
using Common.DTO.Identity.App;
using Common.DTO.Identity.User;

namespace Common.DTO.Identity.UserApp
{
    public class UserAppDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public long UserId { get; set; }
        public long AppId { get; set; }
        public AppDto App { get; set; }
        public UserDto User { get; set; }
    }
}
