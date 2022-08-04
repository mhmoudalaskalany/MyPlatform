using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Core;
using Domain.DTO.Identity.PagePermission;
using Domain.DTO.Identity.RoleClaim;

namespace Domain.DTO.Identity.Role
{
    public class AddRoleDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public long AppId { get; set; }
        public string Code { get; set; }
        // for adding new role with permission ids for each page
        public ICollection<AddPagePermissionDto> PagePermissions { get; set; } = new Collection<AddPagePermissionDto>();
        // to get the required data in format suitable for editing
        public ICollection<RolePagePermissionDto> RolePagePermissions { get; set; } = new Collection<RolePagePermissionDto>();
    }
}
