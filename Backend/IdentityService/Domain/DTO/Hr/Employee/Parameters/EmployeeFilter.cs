using Domain.DTO.Base;

namespace Domain.DTO.Hr.Employee.Parameters
{
    public class EmployeeFilter : MainFilter
    {
        public long? Id { get; set; }
        public string FullnameEn { get; set; }
        public string FullnameAr { get; set; }
        public string NationalId { get; set; }
        public string PhoneNumber { get; set; }
    }
}
