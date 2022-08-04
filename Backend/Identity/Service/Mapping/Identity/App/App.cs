using Common.DTO.Identity.App;
using Entities.Entities.Identity;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapApp()
        {
            CreateMap<App, AppDto>()
                .ReverseMap();
            CreateMap<App, AddAppDto>()
                .ReverseMap();
        }
    }
}
