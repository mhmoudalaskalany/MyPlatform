using Domain.Core;

namespace Domain.DTO.Hr.Team
{
    public class AddTeamDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
        public long UnitId { get; set; }
    }
}
