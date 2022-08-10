using System;
using Domain.DTO.Base;

namespace Domain.DTO.Hr.Employee.Parameters
{
    public class EmployeeFilter : MainFilter
    {
        public Guid? Id { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string NationalId { get; set; }
        public string Phone { get; set; }
        public string FileNumber { get; set; }
    }
}
