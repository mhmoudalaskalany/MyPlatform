using Domain.Core;

namespace Domain.DTO.Hr.Grade
{
    public class GradeDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
    }
}
