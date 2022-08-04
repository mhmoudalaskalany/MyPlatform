using System.Collections.Generic;
using Domain.DTO.Base;

namespace Domain.DTO.Hr.FullEmployee.Parameters
{
    public class EmployeeVaccinationReportFilter : MainFilter
    {
        public long? Id { get; set; }
        public string NationalId { get; set; }
        public string UnitId { get; set; }
        public string PhoneNumber { get; set; }
        public List<string> DoesStatus { get; set; } = new List<string>();

    }
}
