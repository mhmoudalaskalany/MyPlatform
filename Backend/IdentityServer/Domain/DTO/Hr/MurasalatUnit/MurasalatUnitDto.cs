using Domain.Core;
using Entities.Enum;

namespace Domain.DTO.Hr.MurasalatUnit
{
    public class MurasalatUnitDto : IPrimaryKeyField<string>
    {
        public string Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string ParentId { get; set; }
        public virtual MurasalatUnitDto Parent { get; set; }
        public UnitType? UnitType { get; set; }
    }
}
