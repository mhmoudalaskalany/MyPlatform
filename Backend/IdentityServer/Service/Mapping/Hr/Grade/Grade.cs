using Domain.DTO.Hr.Grade;
using Entities.Entities;
using Entities.Entities.Hr;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapGradeProfile()
        {
            CreateMap<Grade, GradeDto>()
                .ReverseMap();
            CreateMap<Grade, AddGradeDto>()
                .ReverseMap();
        }
    }
}