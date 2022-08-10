using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.FullUnit;
using Domain.DTO.Hr.Unit;
using Domain.DTO.Hr.Unit.Parameters;
using Service.Services.Base;

namespace Service.Services.Hr.Unit
{
    public interface IFullUnitService : IBaseService<Entities.Entities.Hr.Unit, AddUnitDto, UnitDto, string>
    {
        /// <summary>
        /// Get Count For Dashboard
        /// </summary>
        /// <returns></returns>
        Task<IFinalResult> GetUnitsCountAsync();
        /// <summary>
        /// Transform Name To Full Name
        /// </summary>
        /// <returns></returns>
        Task TransformAsync();
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetAllPagedAsync(BaseParam<UnitFilter> filter);
        /// <summary>
        /// Get All Paged For Drop Down
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);
    }
}
