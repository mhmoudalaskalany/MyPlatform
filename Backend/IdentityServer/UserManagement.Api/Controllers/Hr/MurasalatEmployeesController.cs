using System;
using System.Threading.Tasks;
using Domain.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.MurasalatEmployee;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    /// Full Employees Controller
    /// </summary>
    public class MurasalatEmployeesController : BaseController
    {
        private readonly IMurasalatEmployeeServiceData _employeeService;
        /// <summary>
        /// Constructor
        /// </summary>
        public MurasalatEmployeesController(IMurasalatEmployeeServiceData employeeService)
        {
            _employeeService = employeeService;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetFromView/{id:guid}")]
        public async Task<IFinalResult> GetFromViewAsync(Guid id)
        {
            var result = await _employeeService.GetByIdFromViewAsync(id);
            return result;
        }

        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("Get/{id:guid}")]
        public async Task<IFinalResult> GetAsync(Guid id)
        {
            var result = await _employeeService.GetByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAll")]
        public async Task<IFinalResult> GetAllAsync()
        {
            var result = await _employeeService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Get All No Duplication 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetAllDistinct")]
        public async Task<IFinalResult> GetNonDuplicateAllAsync()
        {
            var result = await _employeeService.GetNonDuplicateAllAsync();
            return result;
        }

        /// <summary>
        /// Get All No Duplication 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("UpdateAllDistinct")]
        public async Task<IFinalResult> UpdateNonDuplicateAllAsync()
        {
            var result = await _employeeService.UpdateNonDuplicateAllAsync();
            return result;
        }
    }
}
