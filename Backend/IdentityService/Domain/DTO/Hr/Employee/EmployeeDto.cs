using System;
using Domain.Core;
using Domain.DTO.Hr.Unit;
using Entities.Enum;

namespace Domain.DTO.Hr.Employee
{
    public class EmployeeDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string PhoneNumber { get; set; }
        public string IpPhone { get; set; }
        public string Position { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string NationalId { get; set; }
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public long? UnitId { get; set; }
        public long? ManagerId { get; set; }
        public long? EmployeeTypeId { get; set; }
        public bool? IsGovernmental { get; set; }
        public bool IsManager { get; set; }
        public bool IsRetired { get; set; }
        public bool IsTeamManager { get; set; }
        public long? TeamId { get; set; }
        public EmployeeDto Manager { get; set; }
        public UnitDto Unit { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public long? GradeId { get; set; }
        public long? NationalityId { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool IsUpdated { get; set; }
    }
}
