using Domain.Core;
using Domain.DTO.Hr.FullUnit;

namespace Domain.DTO.Hr.Team
{
    public class TeamDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
        public FullUnitDto Unit { get; set; }
        public string UnitId { get; set; }
    }
}
