using System;
using Entities.Entities.Base;

namespace Entities.Entities.Hr
{
    public class EmployeeType : BaseEntity
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}
