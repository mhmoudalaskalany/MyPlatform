using Domain.DTO.Hr.FullUnit;
using Domain.DTO.Hr.Unit;
using Entities.Entities.Hr;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapFullUnit()
        {
            CreateMap<Unit, AddUnitDto>()
                .ReverseMap();

            CreateMap<Unit, UnitDto>();


            CreateMap<Team, UnitDto>();

            CreateMap<dynamic, UnitDto>();
        }
    }
}