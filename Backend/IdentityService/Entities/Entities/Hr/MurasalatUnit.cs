using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entities.Entities.Base;

namespace Entities.Entities.Hr
{
    public class MurasalatUnit : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string Code { get; set; }
        public string ParentCode { get; set; }
        public int UnitType { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string HierarchyPath { get; set; }
        public virtual MurasalatUnit Parent { get; set; }
        public virtual ICollection<MurasalatUnit> Children { get; set; } = new Collection<MurasalatUnit>();
    }
}
