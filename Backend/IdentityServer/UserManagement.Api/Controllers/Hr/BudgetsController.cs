using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Budget;
using Domain.DTO.Hr.Budget.Parameters;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.Budget;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    /// Budgets Controller
    /// </summary>
    public class BudgetsController : BaseController
    {
        private readonly IBudgetService _budgetService;
        /// <summary>
        /// Constructor
        /// </summary>
        public BudgetsController(IBudgetService budgetService)
        {
            _budgetService = budgetService;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IFinalResult> GetAsync(long id)
        {
            var result = await _budgetService.GetByIdAsync(id);
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
            return await _budgetService.GetByIdForEditAsync(id);
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IFinalResult> GetAllAsync()
        {
            var result = await _budgetService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// GetAll Data paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPaged")]
        //[Authorize(policy: Permission.Permissions.View)]
        public async Task<DataPaging> GetPaged([FromBody] BaseParam<BudgetFilter> filter)
        {
            return await _budgetService.GetAllPagedAsync(filter);
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IFinalResult> PostAsync([FromBody] AddBudgetDto dto)
        {
            var result = await _budgetService.AddAsync(dto);
            return result;
        }


        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IFinalResult> Update(AddBudgetDto model)
        {

            return await _budgetService.UpdateAsync(model);
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
            return await _budgetService.DeleteAsync(id);
        }

        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IFinalResult> DeleteSoftAsync(long id)
        {
            return await _budgetService.DeleteSoftAsync(id);
        }


    }
}
