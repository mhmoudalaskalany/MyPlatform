using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entities.Entities.Base;
using Entities.Enum;

namespace Entities.Entities.Hr
{
    public class FullUnit : BaseEntity
    {
        public string Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public int Type { get; set; }
        public string ParentId { get; set; }
        public virtual FullUnit Parent { get; set; }
        public long? OldUnitId { get; set; }
        public UnitType? UnitType { get; set; }
        public virtual ICollection<FullUnit> Children { get; set; } = new Collection<FullUnit>();
        public virtual ICollection<FullEmployee> Employees { get; set; } = new Collection<FullEmployee>();
        public virtual ICollection<Team> Teams { get; set; } = new Collection<Team>();
    }
}
