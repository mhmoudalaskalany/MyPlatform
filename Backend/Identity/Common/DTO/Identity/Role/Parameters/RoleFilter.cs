using Common.DTO.Base;

namespace Common.DTO.Identity.Role.Parameters
{
    public class RoleFilter : MainFilter
    {
        public long? Id { get; set; }
        public long? AppId { get; set; }
        public string AppNameEn { get; set; }
        public string AppNameAr { get; set; }
    }
}
