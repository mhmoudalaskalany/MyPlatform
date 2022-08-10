using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entities.Entities.Base;
using Entities.Enum;

namespace Entities.Entities.Hr
{
    public class Unit : BaseEntity
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public Guid? ParentId { get; set; }
        public virtual Unit Parent { get; set; }
        public UnitType? UnitType { get; set; }
        public virtual ICollection<Unit> Children { get; set; } = new Collection<Unit>();
        public virtual ICollection<Employee> Employees { get; set; } = new Collection<Employee>();
    }
}
