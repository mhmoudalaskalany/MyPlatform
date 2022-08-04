using Domain.DTO.Base;

namespace Domain.DTO.Hr.FullEmployee.Parameters
{
    public class NewEmployeeFilter : MainFilter
    {
        public long? Id { get; set; }
        public string NationalId { get; set; }
        public string UnitId { get; set; }
        public string PhoneNumber { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string FileNumber { get; set; }
    }
}
