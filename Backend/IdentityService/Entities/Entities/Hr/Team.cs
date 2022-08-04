using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entities.Entities.Base;

namespace Entities.Entities.Hr
{
    public class Team : BaseEntity
    {
        public long Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
        public string UnitId { get; set; }
        public virtual FullUnit Unit { get; set; }
        public virtual ICollection<EmployeeTeam> EmployeeTeam { get; set; } = new Collection<EmployeeTeam>();
    }
}
