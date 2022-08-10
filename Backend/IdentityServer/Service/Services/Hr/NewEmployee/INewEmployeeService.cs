using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.FullEmployee;
using Domain.DTO.Hr.FullEmployee.Parameters;
using Entities.Enum;
using Service.Services.Base;

namespace Service.Services.Hr.NewEmployee
{
    public interface INewEmployeeService : IBaseService<Entities.Entities.Hr.FullEmployee, AddMurasalatEmployeeDto, MurasalatEmployeeDto, Guid?>
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
        Task<DataPaging> GetAllPagedAsync(BaseParam<NewEmployeeFilter> filter);
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
        /// <summary>
        /// Update Employee Image For Card (Update FullEmployee Entity For Now)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<IFinalResult> UpdateEmployeeImageAsync(UpdateEmployeeImageDto dto);
    }
}
