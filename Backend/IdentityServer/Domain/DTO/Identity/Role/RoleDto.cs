using System;
using Domain.Core;

namespace Domain.DTO.Identity.Role
{
    public class RoleDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string AppNameEn { get; set; }
        public string AppNameAr { get; set; }
        public string AppCode { get; set; }
        public string Code { get; set; }
    }
}
