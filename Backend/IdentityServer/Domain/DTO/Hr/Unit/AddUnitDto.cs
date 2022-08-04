using Domain.Core;
using Entities.Enum;

namespace Domain.DTO.Hr.Unit
{
    public class AddUnitDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public UnitType? UnitType { get; set; }
        public long? ParentId { get; set; }
    }
}
