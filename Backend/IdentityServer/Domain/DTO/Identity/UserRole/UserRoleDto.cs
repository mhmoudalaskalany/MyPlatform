using System;
using Domain.Core;

namespace Domain.DTO.Identity.UserRole
{
    public class UserRoleDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid UserId { get; set; }
        public Guid AppId { get; set; }
        public string RoleNameEn { get; set; }
        public string RoleNameAr { get; set; }
        public string AppNameEn { get; set; }
        public string AppNameAr { get; set; }
    }
}
