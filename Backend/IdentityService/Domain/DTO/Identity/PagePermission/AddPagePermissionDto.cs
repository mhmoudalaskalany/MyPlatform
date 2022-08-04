using System.Collections.Generic;
using Domain.Core;

namespace Domain.DTO.Identity.PagePermission
{
    public class AddPagePermissionDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public long PageId { get; set; }
        public ICollection<long> PermissionIds { get; set; }

    }
}
