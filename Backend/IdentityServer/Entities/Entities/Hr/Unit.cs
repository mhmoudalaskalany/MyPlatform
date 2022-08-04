using System.Collections.Generic;
using Entities.Entities.Base;
using Entities.Enum;

namespace Entities.Entities.Hr
{
    public class Unit : BaseEntity
    {
        public long Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public UnitType? UnitType { get; set; }
        public string Code { get; set; }
        public long? BudgetId { get; set; }
        public virtual Budget Budget { get; set; }
        public long? ParentId  { get; set; }
        public virtual Unit Parent { get; set; }
        public virtual ICollection<Unit> SubUnits { get; set; }
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
