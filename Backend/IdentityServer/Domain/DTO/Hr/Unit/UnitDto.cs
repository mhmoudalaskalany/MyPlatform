using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Core;
using Entities.Enum;

namespace Domain.DTO.Hr.Unit
{
    public class UnitDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public long? ParentId { get; set; }
        public UnitDto Parent { get; set; }
        public UnitType? UnitType { get; set; }
        public ICollection<UnitDto> SubUnits { get; set; } = new Collection<UnitDto>();
    }
}
