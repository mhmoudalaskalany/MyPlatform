using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entities.Entities.Base;
using Entities.Enum;

namespace Entities.Entities.Hr
{
    public class Employee : BaseEntity
    {
        public Guid Id { get; set; }
        public string CivilNumber { get; set; }
        public string Position { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string IpPhone { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public bool? IsManager { get; set; }
        public bool? IsTeamManager { get; set; }
        public bool? Retired { get; set; }
        public Guid? NationalityId { get; set; }
        public virtual Nationality Nationality { get; set; }
        public Guid? EmployeeTypeId { get; set; }
        public virtual EmployeeType EmployeeType { get; set; }
        public Guid? ManagerId { get; set; }
        public virtual Employee Manager { get; set; }
        public virtual ICollection<Employee> ManagerEmployees { get; set; } = new Collection<Employee>();
        public Guid UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public Guid? TeamId { get; set; }
        public virtual Team Team { get; set; }

    }
}