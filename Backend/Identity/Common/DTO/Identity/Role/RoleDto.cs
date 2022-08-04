using Common.Core;

namespace Common.DTO.Identity.Role
{
    public class RoleDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string AppNameEn { get; set; }
        public string AppNameAr { get; set; }
        public string AppCode { get; set; }
        public string Code { get; set; }
    }
}
