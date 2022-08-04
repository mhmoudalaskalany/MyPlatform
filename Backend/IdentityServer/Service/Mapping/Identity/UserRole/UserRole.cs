using Domain.DTO.Identity.UserRole;
using Entities.Entities.Identity;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapUserRole()
        {
            CreateMap<UserRole, UserRoleDto>()
                .ReverseMap();

            CreateMap<UserRole, AddUserRoleDto>()
                .ReverseMap();
        }
    }
}
