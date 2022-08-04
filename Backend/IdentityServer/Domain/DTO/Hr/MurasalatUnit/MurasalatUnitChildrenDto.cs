using System.Collections.Generic;

namespace Domain.DTO.Hr.MurasalatUnit
{
    public class MurasalatUnitChildrenDto
    {
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public List<string> ChildrenIds { get; set; }
    }
}
