using Common.DTO.Identity.Permission;
using Entities.Entities.Identity;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapPermission()
        {
            CreateMap<Permission, PermissionDto>()
                .ReverseMap();

            CreateMap<Permission, AddPermissionDto>()
                .ReverseMap();
        }
    }
}
