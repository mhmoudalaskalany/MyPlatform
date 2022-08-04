using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.FullEmployee;
using Domain.DTO.Hr.FullEmployee.Parameters;
using Entities.Enum;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.Employee;
using Service.Services.Hr.FullEmployee;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    /// Full Employees Controller
    /// </summary>
    public class FullEmployeesController : BaseController
    {
        private readonly IEmployeeService _currentEmployeeService;
        private readonly IFullEmployeeService _employeeService;
        /// <summary>
        /// Constructor
        /// </summary>
        public FullEmployeesController(IFullEmployeeService employeeService, IEmployeeService currentEmployeeService)
        {
            _employeeService = employeeService;
            _currentEmployeeService = currentEmployeeService;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        [Route("Get/{id:guid}")]
        public async Task<IResult> GetAsync(Guid id)
        {
            var result = await _employeeService.GetByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get By NationalId 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmPhoneNumber/{nationalId}/{phone}/{doesStatus}")]
        public async Task<IResult> ConfirmPhoneNumberAsync(string nationalId , string phone , DoseStatus doesStatus)
        {
            var result = await _employeeService.ConfirmPhoneNumber(nationalId , phone , doesStatus);
            return result;
        }

        /// <summary>
        /// Get By NationalId 
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmOtp/{otp}/{phone}/{nationalId}/{doesStatus}")]
        public async Task<IResult> ConfirmOtpAsync(string otp , string phone , string nationalId , DoseStatus doesStatus)
        {
            var result = await _employeeService.ConfirmOtp(otp , phone , nationalId , doesStatus);
            return result;
        }

        /// <summary>
        ///GetLawsuitStatusCount
        /// /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("GetEmployeesStatusCount")]
        public async Task<IResult> GetEmployeesStatusCountAsync()
        {
            var result = await _employeeService.GetEmployeesStatusCountAsync();
            return result;
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IResult> GetAllAsync()
        {
            var result = await _employeeService.GetAllAsync();
            return result;
        }
        /// <summary>
        /// Delete  
        /// </summary>
        /// <param name="id">Object content</param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteCertificate/{id:guid}")]
        public async Task<IResult> DeleteCertificateAsync(Guid id)
        {
            return await _employeeService.DeleteCertificate(id);
        }

        /// <summary>
        /// Get Employee Details By File Id
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeeDetailsByFileId/{fileId:guid}")]
        public async Task<IResult> GetEmployeeDetailsByFileIdAsync(Guid fileId)
        {
            return await _employeeService.GetEmployeeDetailsByFileIdAsync(fileId);
        }
        /// <summary>
        /// Delete By Attachment Id  
        /// </summary>
        /// <param name="id">Object content</param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteCertificateByAttachmentId/{id:guid}")]
        public async Task<IResult> DeleteCertificateByAttachmentIdAsync(Guid id)
        {
            return await _employeeService.DeleteCertificateByAttachmentIdAsync(id);
        }
        /// <summary>
        /// Get Unit Tasks 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("GetEmployeeVaccinationReport")]
        public async Task<IResult> GetEmployeeVaccinationReportAsync([FromBody] EmployeeVaccinationReportFilter parameters)
        {
            var result = await _employeeService.GetEmployeeVaccinationReportAsync(parameters);
            return result;
        }
        /// <summary>
        /// Get All Data paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPaged")]
        public async Task<DataPaging> GetPagedAsync([FromBody] BaseParam<SearchCriteriaFilter> filter)
        {
            return await _employeeService.GetAllPagedAsync(filter);
        }

        /// <summary>
        /// Get All Data paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetDropDown")]
        public async Task<DataPaging> GetDropDownAsync([FromBody] BaseParam<SearchCriteriaFilter> filter)
        {
            return await _employeeService.GetDropDownAsync(filter);
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpPost]
        [Route("Add")]
        public async Task<IResult> AddAsync([FromBody] AddFullEmployeeDto dto)
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
        public async Task<IResult> UpdateAsync(AddFullEmployeeDto model)
        {
            return await _employeeService.UpdateAsync(model);
        }
        /// <summary>
        /// Update Employee Image
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("UpdateEmployeeImage")]
        public async Task<IResult> UpdateEmployeeImageAsync([FromBody] UpdateEmployeeImageDto dto)
        {
            return await _currentEmployeeService.UpdateEmployeeImageAsync(dto);
        }
        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("AddException")]
        public async Task<IResult> AddExceptionAsync(AddFullEmployeeDto model)
        {

            return await _employeeService.AddException(model);
        }
        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("Delete/{id:guid}")]
        public async Task<IResult> DeleteAsync(Guid id)
        {
            return await _employeeService.DeleteAsync(id);
        }

        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteSoft/{id:guid}")]
        public async Task<IResult> DeleteSoftAsync(Guid id)
        {
            return await _employeeService.DeleteSoftAsync(id);
        }


    }
}
