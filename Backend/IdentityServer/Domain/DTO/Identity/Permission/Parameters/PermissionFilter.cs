using System;
using Domain.DTO.Base;

namespace Domain.DTO.Identity.Permission.Parameters
{
    public class PermissionFilter : MainFilter
    {
        public Guid? Id { get; set; }
    }
}
