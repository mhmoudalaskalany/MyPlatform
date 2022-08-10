using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Employee;
using Domain.DTO.Hr.Employee.Parameters;
using Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.Employee;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    ///  Employees Controller
    /// </summary>
    public class EmployeesController : BaseController
    {
        private readonly IEmployeeService _employeeService;
        /// <summary>
        /// Constructor
        /// </summary>
        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IFinalResult> GetAsync(long id)
        {
            var result = await _employeeService.GetByIdAsync(id);
            return result;
        }
        /// <summary>
        /// Get Employee Info From Oracle DB
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeeInfoAsync/{nationalId}")]
        public async Task<IFinalResult> GetUserInfoAsync(string nationalId)
        {
            var result = await _employeeService.GetEmployeeInfoAsync(nationalId);
            return result;
        }
        /// <summary>
        /// Get Employee Info From Oracle DB
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetEmployeeInfoFromOracle/{nationalId}")]
        public async Task<IFinalResult> GetEmployeeInfoAsync(string nationalId)
        {
            var result = await _employeeService.GetEmployeeInfoNewViewAsync(nationalId);
            return result;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeeCount")]
        public async Task<IFinalResult> GetEmployeeCountAsync()
        {
            return await _employeeService.GetEmployeeCountAsync();
        }
        /// <summary>
        /// Get By Id For Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEdit/{id}")]
        public async Task<IFinalResult> GetEditAsync(long id)
        {
            return await _employeeService.GetByIdForEditAsync(id);
        }

        /// <summary>
        /// Get By Id For View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetView/{id}")]
        public async Task<IFinalResult> GetViewAsync(long id)
        {
            return await _employeeService.GetByIdForViewAsync(id);
        }
        /// <summary>
        /// Check If National Id Is Available Or Not
        /// </summary>
        /// <param name="nationalId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckNationalIdAsync/{nationalId}/{employeeId}")]
        public async Task<IFinalResult> CheckNationalIdAsync(string nationalId, long employeeId)
        {
            var result = await _employeeService.CheckNationalIdAsync(nationalId, employeeId);
            return result;
        }

        /// <summary>
        /// Check If Email Is Available Or Not
        /// </summary>
        /// <param name="email"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckEmailAsync/{email}/{employeeId}")]
        public async Task<IFinalResult> CheckEmailAsync(string email, long employeeId)
        {
            var result = await _employeeService.CheckEmailAsync(email, employeeId);
            return result;
        }
        /// <summary>
        /// Get employee Ids by Unit Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeeIdsByUnitId/{unitId}")]
        public async Task<IFinalResult> GetEmployeeIdsByUnitIdAsync(long unitId)
        {
            var result = await _employeeService.GetEmployeeIdsByUnitIdAsync(unitId);
            return result;
        }
        

        /// <summary>
        /// Get employee Ids by Unit Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUnitManager/{unitId}/{unitType}")]
        public async Task<IFinalResult> GetUnitManagerAsync(long unitId ,  UnitType? unitType)
        {
            var result = await _employeeService.GetUnitManagerAsync(unitId , unitType);
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
            var result = await _employeeService.GetAllAsync();
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
        public async Task<DataPaging> GetPaged([FromBody] BaseParam<EmployeeFilter> filter)
        {
            return await _employeeService.GetAllPagedAsync(filter);
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
            return await _employeeService.GetDropDownAsync(filter);
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IFinalResult> PostAsync([FromBody] AddEmployeeDto dto)
        {
            var result = await _employeeService.AddAsync(dto);
            return result;
        }


        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IFinalResult> Update(AddEmployeeDto model)
        {

            return await _employeeService.UpdateAsync(model);
        }

        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateEmployeeUnit")]
        public async Task<IFinalResult> UpdateEmployeeUnit(EmployeeUnitDto model)
        {
            return await _employeeService.UpdateEmployeeUnit(model);
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
            return await _employeeService.DeleteAsync(id);
        }
    }
}
