using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entities.Entities.Base;

namespace Entities.Entities.Hr
{
    public class Team : BaseEntity
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
        public Guid UnitId { get; set; }
        public virtual Unit Unit { get; set; }
        public virtual ICollection<EmployeeTeam> EmployeeTeam { get; set; } = new Collection<EmployeeTeam>();
    }
}
