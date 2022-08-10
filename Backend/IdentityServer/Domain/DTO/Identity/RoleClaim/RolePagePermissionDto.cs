using System;
using System.Collections.Generic;
using Domain.Core;

namespace Domain.DTO.Identity.RoleClaim
{
    public class RolePagePermissionDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public ICollection<PermissionPageDto> PagePermissions { get; set; } = new List<PermissionPageDto>();
        public bool AllSelected { get; set; } = false;

    }

    public class PermissionPageDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool? IsSelected { get; set; } = false;
    }
}
