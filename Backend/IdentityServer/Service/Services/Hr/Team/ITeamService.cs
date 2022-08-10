using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Team;
using Domain.DTO.Hr.Team.Parameters;
using Service.Services.Base;

namespace Service.Services.Hr.Team
{
    public interface ITeamService : IBaseService<Entities.Entities.Hr.Team, AddTeamDto, TeamDto, Guid?>
    {
        Task<DataPaging> GetAllPagedAsync(BaseParam<TeamFilter> filter);
        Task<IFinalResult> GetTeamsByUnitIdAsync(string unitId);
        Task<IFinalResult> GetEmployeesByTeamIdAsync(Guid teamId);
        Task<IFinalResult> DeleteEmployeeTeamAsync(Guid employeeId, Guid teamId);
        Task<IFinalResult> AddEmployeeTeamAsync(TeamEmployeeDto dto);
        Task<IFinalResult> GetByIdAsync(Guid id);
    }
}
