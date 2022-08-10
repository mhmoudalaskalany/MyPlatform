using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.FullUnit;
using Domain.DTO.Hr.FullUnit.Parameters;
using Domain.DTO.Hr.Unit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.Unit;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    /// Units Controller
    /// </summary>
    public class FullUnitsController : BaseController
    {
        private readonly IFullUnitService _unitService;
        /// <summary>
        /// Constructor
        /// </summary>
        public FullUnitsController(IFullUnitService unitService)
        {
            _unitService = unitService;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IFinalResult> GetAsync(string id)
        {
            var result = await _unitService.GetByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get Count For Dashboard 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUnitsCount")]
        public async Task<IFinalResult> GetUnitsCountAsync()
        {
            var result = await _unitService.GetUnitsCountAsync();
            return result;
        }

        /// <summary>
        /// Transform Name To Full Name
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("Transform")]
        public async Task TransformAsync()
        {
            await _unitService.TransformAsync();
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IFinalResult> GetAllAsync()
        {
            var result = await _unitService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// GetAll Data paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPaged")]
        public async Task<DataPaging> GetPaged([FromBody] BaseParam<UnitFilter> filter)
        {
            return await _unitService.GetAllPagedAsync(filter);
        }

        /// <summary>
        /// Get All Data paged For Drop Down
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDropDown")]
        public async Task<DataPaging> GetDropDownAsync([FromBody] BaseParam<SearchCriteriaFilter> filter)
        {
            return await _unitService.GetDropDownAsync(filter);
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IFinalResult> AddAsync([FromBody] AddUnitDto dto)
        {
            var result = await _unitService.AddAsync(dto);
            return result;
        }


        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IFinalResult> UpdateAsync(AddUnitDto model)
        {
            return await _unitService.UpdateAsync(model);
        }
        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id}")]
        public async Task<IFinalResult> DeleteAsync(string id)
        {
            return await _unitService.DeleteAsync(id);
        }

        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteSoft/{id}")]
        public async Task<IFinalResult> DeleteSoftAsync(string id)
        {
            return await _unitService.DeleteSoftAsync(id);
        }


    }
}
