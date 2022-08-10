using System;

namespace Domain.DTO.Hr.Employee
{
    public class EmployeeUnitDto
    {
        public string EmployeeNumber { get; set; }
        public Guid UnitId { get; set; }
        public bool IsManager { get; set; }
        public bool? Retired { get; set; }
    }
}
