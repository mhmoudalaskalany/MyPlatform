using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Team;
using Domain.DTO.Hr.Team.Parameters;
using Service.Services.Base;

namespace Service.Services.Hr.Team
{
    public interface ITeamService : IBaseService<Entities.Entities.Hr.Team, AddTeamDto, TeamDto, long?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<TeamFilter> filter);
        Task<IResult> GetTeamsByUnitIdAsync(string unitId);
        Task<IResult> GetEmployeesByTeamIdAsync(long teamId);
        Task<IResult> DeleteEmployeeTeamAsync(Guid employeeId, long teamId);
        Task<IResult> AddEmployeeTeamAsync(TeamEmployeeDto dto);
        Task<IResult> GetByIdAsync(long id);
    }
}
