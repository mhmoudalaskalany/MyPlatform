using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Integration.ItHelpDesk.Employee;
using Domain.DTO.Integration.ItHelpDesk.Ticket;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.Employee.Integration;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.IntegrationControllers.Hr
{
    /// <summary>
    /// External Employees Controller Used By Other Services
    /// </summary>
    public class ExternalEmployeesController : BaseController
    {
        private readonly IExternalEmployeeService _externalEmployeeService;
        /// <summary>
        /// constructor
        /// </summary>
        public ExternalEmployeesController(IExternalEmployeeService externalEmployeeService)
        {
            _externalEmployeeService = externalEmployeeService;
        }

        /// <summary>
        /// Get Employees By Id ( used For Omsgd Services )
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IFinalResult> GetAsync(long id)
        {
            var result = await _externalEmployeeService.GetByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get Employees By Id ( used For Stock )
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetById/{id}")]
        public async Task<IFinalResult> GetByIdAsync(long id)
        {
            var result = await _externalEmployeeService.GetEmployeeByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get All ( used For Self Services )
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        [AllowAnonymous]
        public async Task<IFinalResult> GetAllAsync(long id)
        {
            var result = await _externalEmployeeService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Get employee Ids by Unit Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeeIdsByUnitId/{unitId}/{option}")]
        public async Task<IFinalResult> GetEmployeeIdsByUnitIdAsync(long unitId, bool option)
        {
            var result = await _externalEmployeeService.GetEmployeeIdsByUnitIdAsync(unitId, option);
            return result;
        }

        ///// <summary>
        ///// Get Employees By App Code ( used For Stock )
        ///// </summary>
        ///// <param name="appCode"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("GetByAppCode/{appCode}")]
        //public async Task<IResult> GetByAppCodeAsync(string appCode)
        //{
        //    var result = await _externalEmployeeService.GetByAppCodeAsync(appCode);
        //    return result;
        //}

        /// <summary>
        /// Update  Employee (Unit , Phone , IPPhone ) ( Used From Omsgd Services ) 
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateEmployeeFromServices")]
        public async Task<IFinalResult> UpdateEmployeeFromServices(UpdateEmployeeFromServicesDto model)
        {
            return await _externalEmployeeService.UpdateEmployeeFromServices(model);
        }
        /// <summary>
        /// Get Employee Phone Numbers Using List Of Ids ( Used In Omsgd Services )
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetEmployeePhonesByIds")]
        public async Task<IFinalResult> GetEmployeePhonesByIdsAsync([FromBody] List<TicketSmsDto> dtos)
        {
            var result = await _externalEmployeeService.GetEmployeePhonesByIdsAsync(dtos);
            return result;
        }

        /// <summary>
        /// Get Unit Managers Phones Numbers Using List Of Unit Or Team Ids ( Used In Omsgd Services )
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetUnitManagersPhonesByUnitIds")]
        public async Task<IFinalResult> GetUnitManagersPhonesByUnitIdsAsync([FromBody] List<TicketSmsDto> dtos)
        {
            var result = await _externalEmployeeService.GetUnitManagersPhonesByUnitIdsAsync(dtos);
            return result;
        }
        /// <summary>
        /// Get Unit Managers Phones Numbers Using List Of Unit Or Team Ids ( Used In Omsgd Services )
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [Route("GetTeamManagerPhone")]
        public async Task<IFinalResult> GetTeamManagerPhoneAsync([FromBody] long teamId)
        {
            var result = await _externalEmployeeService.GetTeamManagerPhone(teamId);
            return result;
        }

        ///// <summary>
        ///// Get Employees By UnitId Or Team Id ( used For Omsgd Services )
        ///// </summary>
        ///// <param name="unitId"></param>
        ///// <returns></returns>
        //[HttpGet]
        //[Route("GetEmployeesByUnitOrTeamId/{unitId}")]
        //public async Task<IResult> GetEmployeesByUnitIdAsync(long unitId)
        //{
        //    var result = await _externalEmployeeService.GetEmployeesByUnitOrTeamIdAsync(unitId);
        //    return result;
        //}

        /// <summary>
        /// Get Employees Phones By Role Code( used For Legal Affairs)
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeesPhonesByRoleCode/{roleCode}")]
        public async Task<IFinalResult> GetEmployeesPhonesByRoleCodeAsync(string roleCode)
        {
            var result = await _externalEmployeeService.GetEmployeesPhonesByRoleCodeAsync(roleCode);
            return result;
        }

        /// <summary>
        /// Get Manager Email by unit id( used For OMSGD Projects)
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetUnitManagerEmail/{unitId}")]
        public async Task<IFinalResult> GetUnitManagerEmail(long unitId)
        {
            var result = await _externalEmployeeService.GetManagerEmailByUnitIdAsync(unitId);
            return result;
        }

        /// <summary>
        /// Get Employee Phone By Id( used For Legal Affairs)
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetEmployeePhoneById/{employeeId}")]
        public async Task<IFinalResult> GetEmployeePhoneByIdAsync(long employeeId)
        {
            var result = await _externalEmployeeService.GetEmployeePhoneByIdAsync(employeeId);
            return result;
        }

        /// <summary>
        /// Get Employees  By Role Code( used For Legal Affairs)
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByRoleCode/{roleCode}")]
        public async Task<IFinalResult> GetByRoleCodeAsync(string roleCode)
        {
            var result = await _externalEmployeeService.GetByRoleCodeAsync(roleCode);
            return result;
        }

    }
}
