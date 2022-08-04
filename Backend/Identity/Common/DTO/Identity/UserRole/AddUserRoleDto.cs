using Common.Core;
using System.Collections.Generic;

namespace Common.DTO.Identity.UserRole
{
    public class AddUserRoleDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public long UserId { get; set; }
        public long? RoleId { get; set; }
        public long? AppId { get; set; }

    }
}
