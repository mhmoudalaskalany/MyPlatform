using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.FullUnit;
using Domain.DTO.Hr.Unit.Parameters;
using Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.NewUnit;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    /// Units Controller
    /// </summary>
    public class NewUnitsController : BaseController
    {
        private readonly INewUnitService _unitService;
        /// <summary>
        /// Constructor
        /// </summary>
        public NewUnitsController(INewUnitService unitService)
        {
            _unitService = unitService;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IFinalResult> GetAsync(long id)
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
        [Route("Transform")]
        [AllowAnonymous]
        public async Task TransformAsync()
        {
            await _unitService.TransformAsync();
        }

        /// <summary>
        /// Get Unit Or Team By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUnitOrTeam/{id}/{unitType}")]
        public async Task<IFinalResult> GetUnitOrTeamAsync(string id , UnitType unitType)
        {
            var result = await _unitService.GetUnitOrTeamAsync(id , unitType);
            return result;
        }

        /// <summary>
        /// Get By Id For Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEdit/{id}")]
        public async Task<IFinalResult> GetEdit(long id)
        {
            return await _unitService.GetByIdForEditAsync(id);
        }

        /// <summary>
        /// Get Unit With Children 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUnitsWithChildren")]
        public async Task<IFinalResult> GetUnitsWithChildren()
        {
            var result = await _unitService.GetUnitsWithChildren();
            return result;
        }

        /// <summary>
        /// Get Sections In Department
        /// And Teams of Logged In Section Manager ( The Teams Under That Section )
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetSectionsByEmployeeSectionId/{sectionId}")]
        public async Task<IFinalResult> GetSectionsByEmployeeSectionIdAsync(string sectionId)
        {
            var result = await _unitService.GetSectionsByEmployeeSectionIdAsync(sectionId);
            return result;
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
        /// 
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDropDown")]
        public async Task<DataPaging> GetDropDown([FromBody] BaseParam<SearchCriteriaFilter> filter)
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
        public async Task<IFinalResult> PostAsync([FromBody] AddFullUnitDto dto)
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
        public async Task<IFinalResult> Update(AddFullUnitDto model)
        {

            return await _unitService.UpdateAsync(model);
        }
        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("Delete/{id}")]
        public async Task<IFinalResult> Remove(long id)
        {
            return await _unitService.DeleteAsync(id);
        }

        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IFinalResult> DeleteSoftAsync(long id)
        {
            return await _unitService.DeleteSoftAsync(id);
        }


    }
}
