using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations.Schema;
using Entities.Entities.Base;

namespace Entities.Entities.Hr
{
    public class FullEmployee : BaseEntity
    {
        [Column(Order = 1)]
        public Guid Id { get; set; }
        [Column(Order = 2)]
        public string FileNumber { get; set; }
        [Column(Order = 3)]
        public string CivilNumber { get; set; }
        [Column(Order = 4)]
        public string ArCurrentFirstName { get; set; }
        [Column(Order = 5)]
        public string ArCurrentSecondName { get; set; }
        [Column(Order = 6)]
        public string ArCurrentMiddleName { get; set; }
        [Column(Order = 7)]
        public string ArCurrentLastName { get; set; }
        [Column(Order = 8)]
        public string ArFullName { get; set; }
        [Column(Order = 9)]
        public string EnCurrentFirstName { get; set; }
        [Column(Order = 10)]
        public string EnCurrentSecondNamee { get; set; }
        [Column(Order = 11)]
        public string EnCurrentMiddleName { get; set; }
        [Column(Order = 12)]
        public string EnCurrentLastName { get; set; }
        [Column(Order = 13)]
        public string EnFullName { get; set; }
        [Column(Order = 14)]
        public string Phone { get; set; }
        [Column(Order = 15)]
        public string ArPositiontype { get; set; }
        [Column(Order = 16)]
        public string EnPositiontype { get; set; }
        [Column(Order = 17)]
        public string ArDepartmentName { get; set; }
        [Column(Order = 18)]
        public string EnDepartmentName { get; set; }
        [Column(Order = 19)]
        public string DirectManagerFileNumber { get; set; }
        [Column(Order = 20)]
        public string DirectManagerCivilNumber { get; set; }
        [Column(Order = 21)]
        public string ArDirectManagerName { get; set; }
        [Column(Order = 22)]
        public string EnDirectManagerName { get; set; }
        [Column(Order = 23)]
        public string ParentDepartmentCode { get; set; }
        [Column(Order = 24)]
        public string ArParentDepartmentName { get; set; }
        [Column(Order = 25)]
        public string EnParentDepartmentName { get; set; }
        [Column(Order = 26)]
        public string GrandParentDepartmentCode { get; set; }
        [Column(Order = 27)]
        public string ArGrandParentDepartmentName { get; set; }
        [Column(Order = 28)]
        public string EnGrandParentDepartmentName { get; set; }
        [Column(Order = 29)]
        public string GrandDepartmentCode { get; set; }
        [Column(Order = 30)]
        public string ArGrandDepartmentName { get; set; }
        [Column(Order = 31)]
        public string EnGrandDepartmentName { get; set; }
        [Column(Order = 32)]
        public string Email { get; set; }
        [Column(Order = 33)]
        public string DirectManagerEmail { get; set; }
        [Column(Order = 34)]
        public string EnDirectManagerPosition { get; set; }
        [Column(Order = 35)]
        public string ArDirectManagerPosition { get; set; }
        [Column(Order = 36)]
        public bool IsManager { get; set; }
        [Column(Order = 37)]
        public string DepartmentCode { get; set; }
        public virtual FullUnit Unit { get; set; }
        [Column(Order = 38)]
        public string IpPhone { get; set; }
        [Column(Order = 39)]
        public bool IsVaccinated { get; set; }
        [Column(Order = 40)]
        public int? DoseStatus { get; set; }
        [Column(Order = 41)]
        public string Notes { get; set; }
        [Column(Order = 42)]
        public Guid? PhotoId { get; set; }
        [Column(Order = 43)]
        public DateTime? HiringDate { get; set; }
        [Column(Order = 44)]
        public Guid? AttachmentId { get; set; }

        public Guid? ManagerId { get; set; }
        public virtual Attachment Attachment { get; set; }

        public virtual ICollection<EmployeeTeam> EmployeeTeams { get; set; } = new List<EmployeeTeam>();
    }
}
