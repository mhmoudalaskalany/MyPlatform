﻿using System;
using System.Collections.Generic;
using Domain.Core;
using Domain.DTO.Identity.Role;

namespace Domain.DTO.Hr.Employee
{
    public class EmployeeProfileDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string FileNumber { get; set; }
        public string Phone { get; set; }
        public string IpPhone { get; set; }
        public string Position { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string NationalId { get; set; }
        public string Email { get; set; }
        public Guid UnitId { get; set; }
        public Guid? ManagerId { get; set; }
        public Guid? EmployeeTypeId { get; set; }
        public bool? IsGovernmental { get; set; }
        public bool IsManager { get; set; }
        public bool IsRetired { get; set; }
        public bool IsTeamManager { get; set; }
        public Guid? PhotoId { get; set; }
        public Guid? TeamId { get; set; }
        public EmployeeProfileDto Manager { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? LastCacheUpdate { get; set; }
        public List<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }
}