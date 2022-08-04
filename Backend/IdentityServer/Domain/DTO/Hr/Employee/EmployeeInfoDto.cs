using System;

namespace Domain.DTO.Hr.Employee
{
    public class EmployeeInfoDto
    {
        public string EmployeeNumber { get; set; }
        public string NationalId { get; set; }
        public string EmployeeId { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
        public string Nationality { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string MaritalStatus { get; set; }
        public DateTime? HireDate { get; set; }
        public string Grade { get; set; }
        public long? JobTitleId { get; set; }
        public string JobTitle { get; set; }
        public long? JobLevelId { get; set; }
        public string JobLevel { get; set; }
        public string Department { get; set; }
        public string Sector { get; set; }
        public string Section { get; set; }
    }
}
