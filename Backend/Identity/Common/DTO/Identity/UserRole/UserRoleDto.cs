using Common.Core;

namespace Common.DTO.Identity.UserRole
{
    public class UserRoleDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public long RoleId { get; set; }
        public long UserId { get; set; }
        public long AppId { get; set; }
        public string RoleNameEn { get; set; }
        public string RoleNameAr { get; set; }
        public string AppNameEn { get; set; }
        public string AppNameAr { get; set; }
    }
}
