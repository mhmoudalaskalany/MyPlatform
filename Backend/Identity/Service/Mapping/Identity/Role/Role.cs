using Common.DTO.Identity.Role;
using Entities.Entities.Identity;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapRole()
        {
            CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.AppNameEn , 
                    opt => opt.MapFrom(src => src.App.NameEn))
                .ForMember(dest => dest.AppNameAr,
                    opt => opt.MapFrom(src => src.App.NameAr))
                .ForMember(dest => dest.AppCode,
                    opt => opt.MapFrom(src => src.App.Code))
                .ReverseMap();

            CreateMap<Role, AddRoleDto>()
                .ForMember(dest => dest.NameEn , opt => opt.MapFrom(src => src.NameEn))
                .ReverseMap();

            CreateMap<AddRoleDto, Role>()
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(dest => dest.NormalizedName, opt => opt.MapFrom(src => src.NameEn));
        }
    }
}
