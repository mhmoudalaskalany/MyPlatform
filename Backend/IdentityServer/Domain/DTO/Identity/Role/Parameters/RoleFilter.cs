using System;
using Domain.DTO.Base;

namespace Domain.DTO.Identity.Role.Parameters
{
    public class RoleFilter : MainFilter
    {
        public Guid? Id { get; set; }
        public Guid? AppId { get; set; }
        public string AppNameEn { get; set; }
        public string AppNameAr { get; set; }
    }
}
