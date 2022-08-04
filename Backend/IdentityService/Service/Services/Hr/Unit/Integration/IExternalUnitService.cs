using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Unit;
using Service.Services.Base;

namespace Service.Services.Hr.Unit.Integration
{
    public interface IExternalUnitService : IBaseService<Entities.Entities.Hr.Unit, AddUnitDto, UnitDto , long?>
    {
        /// <summary>
        /// Get Unit Parent ( Used In Stock )
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        Task<IResult> GetUnitParentAsync(long childId);
        /// <summary>
        /// Get All Units
        /// </summary>
        /// <param ></param>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);
        /// <summary>
        /// Get Units By Ids (Used In Stock)
        /// </summary>
        /// <param name="unitIds"></param>
        /// <returns></returns>
        Task<IResult> GetUnitsByIdsAsync(List<long?> unitIds);
        /// <summary>
        /// Get All Departments
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetDropDownForDepartmentAsync(BaseParam<SearchCriteriaFilter> filter);
    }
}

