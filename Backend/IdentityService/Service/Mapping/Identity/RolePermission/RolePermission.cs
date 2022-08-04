using Domain.DTO.Identity.RolePermission;
using Entities.Entities.Identity;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapRolePermission()
        {
            CreateMap<RolePermission, RolePermissionDto>()
                .ReverseMap();
            CreateMap<RolePermission, RolePermissionDto>()
                .ReverseMap();
        }
    }
}
