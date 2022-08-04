using Domain.DTO.Hr.Team;
using Entities.Entities.Hr;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapTeam()
        {
            CreateMap<Team, TeamDto>()
                .ReverseMap();
            CreateMap<Team, AddTeamDto>()
                .ReverseMap();
        }
    }
}