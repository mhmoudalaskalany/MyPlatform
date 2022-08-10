using System;
using Domain.Core;

namespace Domain.DTO.Hr.Team
{
    public class AddTeamDto : IPrimaryKeyField<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
        public Guid UnitId { get; set; }
    }
}
