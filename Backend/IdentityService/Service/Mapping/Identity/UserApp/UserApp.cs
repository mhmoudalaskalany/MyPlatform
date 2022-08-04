using Domain.DTO.Identity.UserApp;
using Entities.Entities.Identity;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapUserApp()
        {
            CreateMap<UserApp, UserAppDto>()
                .ReverseMap();

            CreateMap<UserApp, AddUserAppDto>()
                .ReverseMap();
        }
    }
}
