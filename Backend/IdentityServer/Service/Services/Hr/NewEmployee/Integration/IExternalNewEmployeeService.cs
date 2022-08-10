using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.FullEmployee;
using Domain.DTO.Integration.ItHelpDesk.Ticket;
using Service.Services.Base;

namespace Service.Services.Hr.NewEmployee.Integration
{
    public interface IExternalNewEmployeeService : IBaseService<Entities.Entities.Hr.FullEmployee, AddMurasalatEmployeeDto, MurasalatEmployeeDto, Guid?>
    {
        /// <summary>
        /// Get Manager Email By UnitId 
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        Task<IFinalResult> GetManagerEmailByUnitIdAsync(string unitId);
        /// <summary>
        /// Get Employees by App Code ( Used In Stock )
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        Task<IFinalResult> GetByAppCodeAsync(string appCode);

        /// <summary>
        /// Get Employee Phones By List Of Ids from e-services service
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        Task<IFinalResult> GetEmployeePhonesByIdsAsync(List<TicketSmsDto> dtos);
        /// <summary>
        /// Get Unit Managers By Unit Or Team Ids  from e-services service
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        Task<IFinalResult> GetUnitManagersPhonesByUnitIdsAsync(List<TicketSmsDto> dtos);
        /// <summary>
        /// Get Employees By Unit Or Team Id from e-services service
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>

        Task<IFinalResult> GetEmployeesByUnitOrTeamIdAsync(string unitId);
        /// <summary>
        /// Get the team manager phone Id
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        Task<IFinalResult> GetTeamManagerPhone(long teamId);
        /// <summary>
        /// Get Employees Phones By Role Code (Used By Legal Affairs)
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        Task<IFinalResult> GetEmployeesPhonesByRoleCodeAsync(string roleCode);
        /// <summary>
        /// Get Employee Phone By Id (Used By Legal Affairs)
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<IFinalResult> GetEmployeePhoneByIdAsync(string employeeId);
        /// <summary>
        /// Get By Role Code (Used In Legal Affairs)
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        Task<IFinalResult> GetByRoleCodeAsync(string roleCode);
        /// <summary>
        /// Get Employee Ids By Unit (Used By Stock System To Get Unit Report)
        /// if option is false it will get the unit employees only
        /// if option is true it will employees of unit and its children unit
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        Task<IFinalResult> GetEmployeeIdsByUnitIdAsync(string unitId, bool option);
        /// <summary>
        /// Get Murasalat Employees For Self Service
        /// </summary>
        /// <returns></returns>
        Task<IFinalResult> GetAllMurasalatAsync();

        /// <summary>
        /// /
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);

    }
}
