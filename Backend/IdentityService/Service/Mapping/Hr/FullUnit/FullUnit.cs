using Domain.DTO.Hr.FullUnit;
using Entities.Entities.Hr;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapFullUnit()
        {
            CreateMap<FullUnit, AddFullUnitDto>()
                .ReverseMap();

            CreateMap<FullUnit, FullUnitDto>();


            CreateMap<Team, FullUnitDto>();

            CreateMap<dynamic, FullUnitDto>();
        }
    }
}