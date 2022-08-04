using System;
using Entities.Entities.Base;

namespace Entities.Entities.Hr
{
    public class EmployeeTeam : BaseEntity
    {
        public Guid EmployeeId { get; set; }
        public virtual FullEmployee Employee { get; set; }
        public long TeamId { get; set; }
        public virtual Team Team { get; set; }
        public bool IsTeamManager { get; set; }
    }
}
