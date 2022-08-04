using System;
using Domain.Core;
using Domain.DTO.Hr.FullEmployee;

namespace Domain.DTO.Hr.Card
{
    public class CardDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public Guid EmployeeId { get; set; }
        public string NationalId { get; set; }
        public bool IsActive { get; set; }
        public Guid FileId { get; set; }
        public string CardUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CreatedByEmployeeEn { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string CreatedByEmployeeAr { get; set; }
        public string CardNumber { get; set; }
    }
}
