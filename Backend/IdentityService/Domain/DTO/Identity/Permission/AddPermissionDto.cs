using Domain.Core;

namespace Domain.DTO.Identity.Permission
{
    public class AddPermissionDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
    }
}
