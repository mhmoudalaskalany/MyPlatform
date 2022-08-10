using System;
using Domain.Core;
using System.Collections.Generic;


namespace Domain.DTO.Identity.UserRole
{

    public class AddMultipleRolesDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public Guid UserId { get; set; }
        public List<Guid> RoleIds { get; set; } = new List<Guid>();
        public Guid AppId { get; set; }

    }
}

