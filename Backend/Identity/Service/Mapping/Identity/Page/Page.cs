using System.Linq;
using Common.DTO.Identity.Page;
using Common.DTO.Identity.Permission;
using Entities.Entities.Identity;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapPage()
        {
            CreateMap<Page, PageDto>()
                .ForMember(dest => dest.AppNameEn, 
                    opt => opt.MapFrom(src => src.App.NameEn))
                .ForMember(dest => dest.AppNameAr , 
                    opt => opt.MapFrom(src => src.App.NameAr))
                .ForMember(dest => dest.PagePermissions, opt => opt.MapFrom(src => src.PagePermissions.Select(p => new PermissionDto
                {
                    Id = p.PermissionId,
                    NameAr = p.Permission.NameAr,
                    NameEn = p.Permission.NameEn
                })))
                .ReverseMap();
            CreateMap<Page, AddPageDto>()
                .ReverseMap();
        }
    }
}
