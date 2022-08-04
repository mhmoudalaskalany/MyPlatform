using Domain.Core;
using Domain.DTO.Identity.App;
using Domain.DTO.Identity.User;

namespace Domain.DTO.Identity.UserApp
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
