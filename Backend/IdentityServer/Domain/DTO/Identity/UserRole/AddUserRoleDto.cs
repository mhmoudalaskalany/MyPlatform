using System;
using Domain.Core;

namespace Domain.DTO.Identity.UserRole
{
    public class AddUserRoleDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? RoleId { get; set; }
        public Guid? AppId { get; set; }

    }
}
