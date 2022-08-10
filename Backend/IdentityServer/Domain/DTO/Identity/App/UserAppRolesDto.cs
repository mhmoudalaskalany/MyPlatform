using System;
using System.Collections.Generic;

namespace Domain.DTO.Identity.App
{
    public class UserAppRolesDto
    {
        public Guid UserId { get; set; }
        public List<Guid> AppIds { get; set; } = new List<Guid>();
    }
}
