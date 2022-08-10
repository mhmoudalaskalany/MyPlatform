using System;
using Domain.Core;
using Domain.DTO.Identity.App;

namespace Domain.DTO.Identity.PagePermission
{
    public class PagePermissionDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Url { get; set; }
        public AppDto  App { get; set; }
    }
}
