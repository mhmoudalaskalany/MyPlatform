using Common.DTO.Identity.PagePermission;
using Entities.Entities.Identity;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapPagePermission()
        {
            CreateMap<PagePermission, PagePermissionDto>()
                .ReverseMap();
            CreateMap<PagePermission, AddPagePermissionDto>()
                .ReverseMap();
        }
    }
}
