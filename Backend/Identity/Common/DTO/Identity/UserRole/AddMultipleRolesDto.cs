﻿using Common.Core;
using System.Collections.Generic;


namespace Common.DTO.Identity.UserRole
{

    public class AddMultipleRolesDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public long UserId { get; set; }
        public List<long> RoleIds { get; set; } = new List<long>();
        public long AppId { get; set; }

    }
}

