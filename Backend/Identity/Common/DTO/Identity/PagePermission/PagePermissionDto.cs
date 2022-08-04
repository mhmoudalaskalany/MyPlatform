using Common.Core;
using Common.DTO.Identity.App;

namespace Common.DTO.Identity.PagePermission
{
    public class PagePermissionDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Url { get; set; }
        public AppDto  App { get; set; }
    }
}
