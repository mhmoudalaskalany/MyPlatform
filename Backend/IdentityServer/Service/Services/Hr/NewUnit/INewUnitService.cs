using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Unit;
using Domain.DTO.Hr.Unit.Parameters;
using Entities.Enum;
using Service.Services.Base;

namespace Service.Services.Hr.NewUnit
{
    public interface IUnitService : IBaseService<Entities.Entities.Hr.Unit, AddUnitDto, UnitDto, Guid>
    {

        /// <summary>
        /// Transform Name To Full Name
        /// </summary>
        /// <returns></returns>
        Task TransformAsync();
        /// <summary>
        /// Get Count For Dashboard
        /// </summary>
        /// <returns></returns>
        Task<IFinalResult> GetUnitsCountAsync();
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetAllPagedAsync(BaseParam<UnitFilter> filter);
        /// <summary>
        /// Get Drop Down
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);
        /// <summary>
        /// Get With Children
        /// </summary>
        /// <returns></returns>
        Task<IFinalResult> GetUnitsWithChildren();
        /// <summary>
        /// Get By Employee Section Id
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        Task<IFinalResult> GetSectionsByEmployeeSectionIdAsync(Guid sectionId);
        /// <summary>
        /// Get Unit Or Team By Id And Type
        /// </summary>
        /// <param name="id"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        Task<IFinalResult> GetUnitOrTeamAsync(Guid id, UnitType unitType);
    }
}
