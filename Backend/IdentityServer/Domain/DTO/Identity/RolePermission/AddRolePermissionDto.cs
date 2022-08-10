using System;
using Domain.Core;

namespace Domain.DTO.Identity.RolePermission
{
    class AddRolePermissionDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public Guid RoleId { get; set; }
        public Guid PagePermissionId { get; set; }
    }
}
