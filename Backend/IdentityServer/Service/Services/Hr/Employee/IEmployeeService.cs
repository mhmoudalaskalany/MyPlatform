using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Employee;
using Domain.DTO.Hr.Employee.Parameters;
using Entities.Enum;
using Service.Services.Base;

namespace Service.Services.Hr.Employee
{
    public interface IEmployeeService : IBaseService<Entities.Entities.Hr.Employee, AddEmployeeDto, EmployeeDto, Guid?>
    {
        
        /// <summary>
        /// Get Employees Count
        /// </summary>
        /// <returns></returns>
        Task<IFinalResult> GetEmployeeCountAsync();
        /// <summary>
        /// Get Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetAllPagedAsync(BaseParam<EmployeeFilter> filter);
        /// <summary>
        /// Get Drop Down
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);

        /// <summary>
        /// Get Drop Down
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetDropDownForHrAsync(BaseParam<SearchCriteriaFilter> filter);

        /// <summary>
        /// Get Employee Ids By Unit
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        Task<IFinalResult> GetEmployeeIdsByUnitIdAsync(string unitId);
        /// <summary>
        /// Get Unit Manager
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        Task<IFinalResult> GetUnitManagerAsync(string unitId, UnitType? unitType);
        /// <summary>
        /// Get By Id For View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IFinalResult> GetByIdForViewAsync(Guid id);
    }
}
