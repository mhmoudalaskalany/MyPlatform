using Domain.DTO.Hr.Unit;
using Entities.Entities.Hr;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapUnit()
        {
            CreateMap<Unit, UnitDto>()
                .ReverseMap();
            CreateMap<Unit, AddUnitDto>()
                .ReverseMap();
            // used in omsgd services app to show teams in unit drop down and manager can use it
            CreateMap<Team, UnitDto>();

            CreateMap<dynamic, UnitDto>();
        }
    }
}