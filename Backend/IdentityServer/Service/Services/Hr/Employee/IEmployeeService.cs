using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Employee;
using Domain.DTO.Hr.Employee.Parameters;
using Domain.DTO.Hr.FullEmployee;
using Entities.Enum;
using Service.Services.Base;

namespace Service.Services.Hr.Employee
{
    public interface IEmployeeService : IBaseService<Entities.Entities.Hr.Employee, AddEmployeeDto, EmployeeDto , long?>
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
        /// Update Employee Unit
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IFinalResult> UpdateEmployeeUnit(EmployeeUnitDto dto);
        /// <summary>
        /// Get Employee Info From Oracle
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        Task<IFinalResult> GetEmployeeInfoAsync(string nationalId);
        /// <summary>
        /// Get Employee Info From Oracle
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        Task<IFinalResult> GetEmployeeInfoNewViewAsync(string nationalId);
        /// <summary>
        /// Check National Id
        /// </summary>
        /// <param name="nationalId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<IFinalResult> CheckNationalIdAsync(string nationalId, long employeeId);
        /// <summary>
        /// Check Email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<IFinalResult> CheckEmailAsync(string email, long employeeId);
        /// <summary>
        /// Get Employee Ids By Unit
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        Task<IFinalResult> GetEmployeeIdsByUnitIdAsync(long unitId);
        /// <summary>
        /// Get Unit Manager
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        Task<IFinalResult> GetUnitManagerAsync(long unitId, UnitType? unitType);
        /// <summary>
        /// Get By Id For View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IFinalResult> GetByIdForViewAsync(long id);
        /// <summary>
        /// Update Employee Image For Card (Update FullEmployee Entity For Now)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IFinalResult> UpdateEmployeeImageAsync(UpdateEmployeeImageDto dto);
    }
}
