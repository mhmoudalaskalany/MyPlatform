using System;
using Entities.Entities.Base;

namespace Entities.Entities.Identity
{
    public class PagePermission : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid PageId { get; set; }
        public Page Page { get; set; }
        public Guid PermissionId { get; set; }
        public Permission Permission { get; set; }
    }
}
