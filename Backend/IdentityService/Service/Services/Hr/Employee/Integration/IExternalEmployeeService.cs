using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Hr.Employee;
using Domain.DTO.Integration.ItHelpDesk.Employee;
using Domain.DTO.Integration.ItHelpDesk.Ticket;
using Service.Services.Base;

namespace Service.Services.Hr.Employee.Integration
{
    public interface IExternalEmployeeService : IBaseService<Entities.Entities.Hr.Employee, AddEmployeeDto, EmployeeDto, long?>
    {
        /// <summary>
        /// Get Manager Email By UnitId 
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        Task<IResult> GetManagerEmailByUnitIdAsync(long unitId);
        ///// <summary>
        ///// Get Employees by App Code ( Used In Stock )
        ///// </summary>
        ///// <param name="appCode"></param>
        ///// <returns></returns>
        //Task<IResult> GetByAppCodeAsync(string appCode);
        /// <summary>
        /// this method update employee phone , ip phone and unit from e-services service
        /// </summary>
        /// <returns></returns>
        Task<IResult> UpdateEmployeeFromServices(UpdateEmployeeFromServicesDto dto);
        /// <summary>
        /// Get Employee Phones By List Of Ids from e-services service
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        Task<IResult> GetEmployeePhonesByIdsAsync(List<TicketSmsDto> dtos);
        /// <summary>
        /// Get Unit Managers By Unit Or Team Ids  from e-services service
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        Task<IResult> GetUnitManagersPhonesByUnitIdsAsync(List<TicketSmsDto> dtos);
        ///// <summary>
        ///// Get Employees By Unit Or Team Id from e-services service
        ///// </summary>
        ///// <param name="unitId"></param>
        ///// <returns></returns>

        //Task<IResult> GetEmployeesByUnitOrTeamIdAsync(long unitId);
        /// <summary>
        /// Get the team manager phone Id
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        Task<IResult> GetTeamManagerPhone(long teamId);
        /// <summary>
        /// Get Employees Phones By Role Code (Used By Legal Affairs)
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        Task<IResult> GetEmployeesPhonesByRoleCodeAsync(string roleCode);
        /// <summary>
        /// Get Employee Phone By Id (Used By Legal Affairs)
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<IResult> GetEmployeePhoneByIdAsync(long employeeId);
        /// <summary>
        /// Get By Role Code (Used In Legal Affairs)
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        Task<IResult> GetByRoleCodeAsync(string roleCode);
        /// <summary>
        /// Get Employee Ids By Unit (Used By Stock System To Get Unit Report)
        /// if option is false it will get the unit employees only
        /// if option is true it will employees of unit and its children unit
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        Task<IResult> GetEmployeeIdsByUnitIdAsync(long unitId, bool option);
        /// <summary>
        /// Get By Id (For Stock)
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<IResult> GetEmployeeByIdAsync(long employeeId);






    }
}
