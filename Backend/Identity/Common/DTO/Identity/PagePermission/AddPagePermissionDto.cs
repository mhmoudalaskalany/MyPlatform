using System.Collections.Generic;
using Common.Core;

namespace Common.DTO.Identity.PagePermission
{
    public class AddPagePermissionDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public long PageId { get; set; }
        public ICollection<long> PermissionIds { get; set; }

    }
}
