using System;
using Domain.Core;

namespace Domain.DTO.Identity.Permission
{
    public class PermissionDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public bool? IsSelected { get; set; }
    }
}
