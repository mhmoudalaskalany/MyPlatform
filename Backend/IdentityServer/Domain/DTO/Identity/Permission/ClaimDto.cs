﻿using System;
using System.Collections.Generic;

namespace Domain.DTO.Identity.Permission
{
    public class ClaimDto
    {
        public string AppName { get; set; }
        public Guid AppId { get; set; }
        public string RoleName { get; set; }
        public string RoleNameAr { get; set; }
        public string RoleCode { get; set; }
        public string AppCode { get; set; }
        public Guid RoleId { get; set; }
        public List<string> Permissions { get; set; } = new List<string>();
    }
}
