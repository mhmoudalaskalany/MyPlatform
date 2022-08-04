using Common.Core;

namespace Common.DTO.Identity.UserApp
{
    public class AddUserAppDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public long UserId { get; set; }
        public long AppId { get; set; }
    }
}
