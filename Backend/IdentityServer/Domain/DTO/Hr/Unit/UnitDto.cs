using System;
using Domain.Core;
using Entities.Enum;

namespace Domain.DTO.Hr.Unit
{
    public class UnitDto : IPrimaryKeyField<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public Guid? ParentId { get; set; }
        public UnitDto Parent { get; set; }
        public UnitType? UnitType { get; set; }
    }
}
