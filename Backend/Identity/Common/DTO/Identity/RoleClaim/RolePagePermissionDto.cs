using System.Collections.Generic;
using Common.Core;

namespace Common.DTO.Identity.RoleClaim
{
    public class RolePagePermissionDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public ICollection<PermissionPageDto> PagePermissions { get; set; } = new List<PermissionPageDto>();
        public bool AllSelected { get; set; } = false;

    }

    public class PermissionPageDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool? IsSelected { get; set; } = false;
    }
}
