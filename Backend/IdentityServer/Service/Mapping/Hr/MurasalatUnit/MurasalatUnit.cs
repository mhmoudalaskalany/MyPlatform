using Domain.DTO.Hr.MurasalatUnit;
using Entities.Entities.Hr;
using Entities.Entities.Views.Murasalat;

//ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapMurasalatUnit()
        {


            CreateMap<OrganizationHierarchyView, MurasalatUnit>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.ChildId))

                .ForMember(dest => dest.ParentId, opt => opt.MapFrom(src => src.ParentId))

                .ForMember(dest => dest.ParentCode, opt =>
                    opt.MapFrom(src => src.ParentCode))

                .ForMember(dest => dest.Code, opt =>
                    opt.MapFrom(src => src.ChildCode))

                .ForMember(dest => dest.NameEn, opt =>
                    opt.MapFrom(src => src.EnName))

                .ForMember(dest => dest.NameAr, opt =>
                    opt.MapFrom(src => src.ArName))


                .ForMember(dest => dest.UnitType, opt =>
                    opt.MapFrom(src => src.Level))

                .ReverseMap();

            CreateMap<MurasalatUnit, AddMurasalatUnitDto>()
                .ReverseMap();

            CreateMap<Unit, MurasalatUnitDto>();


            CreateMap<Team, MurasalatUnitDto>();

            CreateMap<dynamic, MurasalatUnitDto>();

        }
    }
}