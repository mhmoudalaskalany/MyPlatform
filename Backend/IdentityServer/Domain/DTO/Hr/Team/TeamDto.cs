using System;
using Domain.Core;
using Domain.DTO.Hr.Unit;

namespace Domain.DTO.Hr.Team
{
    public class TeamDto : IPrimaryKeyField<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
        public UnitDto Unit { get; set; }
        public Guid UnitId { get; set; }
    }
}
