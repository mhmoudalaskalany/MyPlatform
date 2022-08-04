using System.Threading.Tasks;
using Domain.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.Attendance;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    ///  Leaves Controller
    /// </summary>
    public class LeavesController : BaseController
    {
        private readonly IAttendanceServiceData _attendanceService;
        /// <summary>
        /// Constructor
        /// </summary>
        public LeavesController(IAttendanceServiceData attendanceService)
        {
            _attendanceService = attendanceService;
        }

        /// <summary>
        /// Get Employee Leave From Oracle DB
        /// </summary>
        /// <param name="employeeNumber"></param>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetEmployeeLeave/{employeeNumber}")]
        public async Task<IResult> GetEmployeeLeaveAsync(string employeeNumber)
        {
            var result = await _attendanceService.GetEmployeeLeaveAsync(employeeNumber);
            return result;
        }

        /// <summary>
        /// Get Mawred Leave From Oracle DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetMawredLeave")]
        public async Task<IResult> GetMawredLeaveAsync()
        {
            var result = await _attendanceService.GetMawredLeaveAsync();
            return result;
        }

        /// <summary>
        /// Get Mawred Leave From Oracle DB
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("InsertMawredLeave")]
        public async Task<IResult> InsertMawredLeaveAsync()
        {
            var result = await _attendanceService.InsertMawredLeaveAsync();
            return result;
        }

    }
}
