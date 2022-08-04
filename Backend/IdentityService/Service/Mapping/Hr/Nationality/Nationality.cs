using Domain.DTO.Hr.Nationality;
using Entities.Entities.Hr;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapNationality()
        {
            CreateMap<Nationality, NationalityDto>()
                .ReverseMap();
            CreateMap<Nationality, AddNationalityDto>()
                .ReverseMap();
        }
    }
}