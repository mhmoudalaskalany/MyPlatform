using Domain.Core;
using Domain.DTO.Identity.App;

namespace Domain.DTO.Identity.PagePermission
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
