using System;
using Entities.Entities.Base;

namespace Entities.Entities.Hr
{
    public class Card : BaseEntity
    {
        public long Id { get; set; }
        public Guid EmployeeId { get; set; }
        public virtual FullEmployee Employee { get; set; }
        public string NationalId { get; set; }
        public bool IsActive { get; set; }
        public string CardNumber { get; set; }
        public Guid FileId { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
