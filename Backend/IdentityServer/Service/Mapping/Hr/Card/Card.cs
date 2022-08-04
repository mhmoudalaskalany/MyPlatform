using Domain.DTO.Hr.Card;
using Entities.Entities.Hr;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapCard()
        {
            CreateMap<Card, CardDto>()
                .ReverseMap();
            CreateMap<Card, AddCardDto>()
                .ReverseMap();
        }
    }
}