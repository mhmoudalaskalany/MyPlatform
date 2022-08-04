using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.Unit.Integration;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.IntegrationControllers.Hr
{
    /// <summary>
    /// External Units Controller Used By Other Services
    /// </summary>
    public class ExternalUnitsController : BaseController
    {
        private readonly IExternalUnitService _externalUnitService;
        /// <summary>
        /// constructor
        /// </summary>
        public ExternalUnitsController(IExternalUnitService externalUnitService)
        {
            _externalUnitService = externalUnitService;
        }
        /// <summary>
        /// Get Unit Parent
        /// Used In Stock Management
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetParent/{childId}")]
        public async Task<IResult> GetUnitParentAsync(long childId)
        {
            var result = await _externalUnitService.GetUnitParentAsync(childId);
            return result;
        }

        /// <summary>
        /// Get (Departments )Drop Down Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDropDown")]
        public async Task<DataPaging> GetDropDown([FromBody] BaseParam<SearchCriteriaFilter> filter)
        {
            return await _externalUnitService.GetDropDownForDepartmentAsync(filter);
        }

        /// <summary>
        /// Get Units By Ids (Used In Stock)
        /// </summary>
        /// <param name="unitIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUnitsByIds")]
        public async Task<IResult> GetUnitsByIdsAsync([FromBody] List<long?> unitIds)
        {
            return await _externalUnitService.GetUnitsByIdsAsync(unitIds);
        }
        /// <summary>
        /// Ge All Units
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDropDownForEdit")]
        public async Task<DataPaging> GetDropDownForEdit([FromBody] BaseParam<SearchCriteriaFilter> filter)
        {
            return await _externalUnitService.GetDropDownAsync(filter);
        }
    }
}
