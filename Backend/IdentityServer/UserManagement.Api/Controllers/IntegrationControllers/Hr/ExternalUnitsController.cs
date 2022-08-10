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
        public async Task<IFinalResult> GetUnitParentAsync(string childId)
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
        [Route("GetDepartmentDropDown")]
        public async Task<DataPaging> GetDropDownForDepartmentAsync([FromBody] BaseParam<SearchCriteriaFilter> filter)
        {
            return await _externalUnitService.GetDropDownForDepartmentAsync(filter);
        }

        /// <summary>
        /// Get Drop Down Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDropDown")]
        public async Task<DataPaging> GetDropDownAsync([FromBody] BaseParam<SearchCriteriaFilter> filter)
        {
            return await _externalUnitService.GetDropDownAsync(filter);
        }

        /// <summary>
        /// Get Units By Ids (Used In Stock)
        /// </summary>
        /// <param name="unitIds"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetUnitsByIds")]
        public async Task<IFinalResult> GetUnitsByIdsAsync([FromBody] List<string> unitIds)
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
