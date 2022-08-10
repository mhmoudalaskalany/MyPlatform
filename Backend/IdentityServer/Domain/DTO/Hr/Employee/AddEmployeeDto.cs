using System;
using Domain.Core;
using Entities.Enum;

namespace Domain.DTO.Hr.Employee
{
    public class AddEmployeeDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string FileNumber { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string NationalId { get; set; }
        public string Email { get; set; }
        public Guid? UnitId { get; set; }
        public Guid? ManagerId { get; set; }
        public bool IsManager { get; set; }
        public Gender? Gender { get; set; }
        public MaritalStatus? MaritalStatus { get; set; }
        public Guid? NationalityId { get; set; }
        public DateTime? HireDate { get; set; }
        public DateTime? BirthDate { get; set; }
        public Guid? PhotoId { get; set; }
        public string PhotoUrl { get; set; }
    }
}
