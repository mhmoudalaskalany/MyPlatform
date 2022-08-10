using Domain.DTO.Hr.Unit;
using Entities.Entities.Hr;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapUnit()
        {
            CreateMap<Unit, AddUnitDto>()
                .ReverseMap();

            CreateMap<Unit, UnitDto>();

            CreateMap<dynamic, UnitDto>();
        }
    }
}