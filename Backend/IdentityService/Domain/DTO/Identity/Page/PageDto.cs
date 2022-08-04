using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain.Core;
using Domain.DTO.Identity.App;
using Domain.DTO.Identity.Permission;

namespace Domain.DTO.Identity.Page
{
    public class PageDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string AppNameEn { get; set; }
        public string AppNameAr { get; set; }
        public string Code { get; set; }
        public long AppId { get; set; }
        public AppDto  App { get; set; }
        public ICollection<PermissionDto> PagePermissions { get; set; } 

        public PageDto()
        {
            PagePermissions  = new Collection<PermissionDto>();
        }
    }
}
