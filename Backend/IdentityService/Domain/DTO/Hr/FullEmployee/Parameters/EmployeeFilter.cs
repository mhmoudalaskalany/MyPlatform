using Domain.DTO.Base;

namespace Domain.DTO.Hr.FullEmployee.Parameters
{
    public class FullEmployeeFilter : MainFilter
    {
        public long? Id { get; set; }
        public string NationalId { get; set; }
        public string UnitId { get; set; }
        public string PhoneNumber { get; set; }

    }
}
