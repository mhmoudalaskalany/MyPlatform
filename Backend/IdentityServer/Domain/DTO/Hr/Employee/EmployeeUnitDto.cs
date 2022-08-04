namespace Domain.DTO.Hr.Employee
{
    public class EmployeeUnitDto
    {
        public long EmployeeNumber { get; set; }
        public long UnitId { get; set; }
        public bool IsManager { get; set; }
        public bool? Retired { get; set; }
    }
}
