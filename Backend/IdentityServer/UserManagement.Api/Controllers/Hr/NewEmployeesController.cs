using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.FullEmployee;
using Domain.DTO.Hr.FullEmployee.Parameters;
using Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.NewEmployee;
using Service.Services.Validators.Services.Employee;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    ///  Employees Controller
    /// </summary>
    public class NewEmployeesController : BaseController
    {
        private readonly INewEmployeeService _employeeService;
        private readonly IEmployeeValidationService _employeeValidationService;
        /// <summary>
        /// Constructor
        /// </summary>
        public NewEmployeesController(INewEmployeeService employeeService, IEmployeeValidationService employeeValidationService)
        {
            _employeeService = employeeService;
            _employeeValidationService = employeeValidationService;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IFinalResult> GetAsync(Guid id)
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
        [Route("GetEmployeeInfo/{nationalId}")]
        public async Task<IFinalResult> GetEmployeeInfoAsync(string nationalId)
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
        public async Task<IFinalResult> GetEmployeeInfoFromOracleAsync(string nationalId)
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
        public async Task<IFinalResult> GetEditAsync(Guid id)
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
        public async Task<IFinalResult> GetViewAsync(Guid id)
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
        [Route("CheckNationalId/{nationalId}/{employeeId}")]
        public async Task<IFinalResult> CheckNationalIdAsync(string nationalId, Guid employeeId)
        {
            var result = await _employeeValidationService.CheckNationalIdAsync(nationalId, employeeId);
            return result;
        }

        /// <summary>
        /// Check If File Number Is Available Or Not
        /// </summary>
        /// <param name="fileNumber"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckFileNumber/{fileNumber}/{employeeId}")]
        public async Task<IFinalResult> CheckFileNumberAsync(string fileNumber, Guid employeeId)
        {
            var result = await _employeeValidationService.CheckFileNumberAsync(fileNumber, employeeId);
            return result;
        }

        /// <summary>
        /// Check If Email Is Available Or Not
        /// </summary>
        /// <param name="email"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("CheckEmail/{email}/{employeeId}")]
        public async Task<IFinalResult> CheckEmailAsync(string email, Guid employeeId)
        {
            var result = await _employeeValidationService.CheckEmailAsync(email, employeeId);
            return result;
        }

        /// <summary>
        /// Get employee Ids by Unit Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeeIdsByUnitId/{unitId}")]
        public async Task<IFinalResult> GetEmployeeIdsByUnitIdAsync(string unitId)
        {
            var result = await _employeeService.GetEmployeeIdsByUnitIdAsync(unitId);
            return result;
        }

        /// <summary>
        /// Update Employee Image
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateEmployeeImage")]
        public async Task<IFinalResult> UpdateEmployeeImageAsync([FromBody] UpdateEmployeeImageDto dto)
        {
            return await _employeeService.UpdateEmployeeImageAsync(dto);
        }


        /// <summary>
        /// Get employee Ids by Unit Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUnitManager/{unitId}/{unitType}")]
        public async Task<IFinalResult> GetUnitManagerAsync(string unitId, UnitType? unitType)
        {
            var result = await _employeeService.GetUnitManagerAsync(unitId, unitType);
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
        public async Task<DataPaging> GetPaged([FromBody] BaseParam<NewEmployeeFilter> filter)
        {
            return await _employeeService.GetAllPagedAsync(filter);
        }

        /// <summary>
        /// For Search
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
        /// Get Drop Down For Vaccination And Hr Systems
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDropDownForHr")]
        public async Task<DataPaging> GetDropDownForHrAsync([FromBody] BaseParam<SearchCriteriaFilter> filter)
        {
            return await _employeeService.GetDropDownForHrAsync(filter);
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IFinalResult> PostAsync([FromBody] AddMurasalatEmployeeDto dto)
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
        public async Task<IFinalResult> Update(AddMurasalatEmployeeDto model)
        {
            return await _employeeService.UpdateAsync(model);
        }

    }
}
