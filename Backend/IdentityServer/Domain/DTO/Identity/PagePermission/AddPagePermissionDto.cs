using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Core;

namespace Domain.DTO.Identity.PagePermission
{
    public class AddPagePermissionDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public Guid PageId { get; set; }
        public ICollection<Guid> PermissionIds { get; set; } = new Collection<Guid>();

    }
}
