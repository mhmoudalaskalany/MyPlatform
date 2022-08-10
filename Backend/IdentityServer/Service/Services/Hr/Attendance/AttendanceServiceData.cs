using System.Net;
using System.Threading.Tasks;
using Domain.Abstraction.Repository.Attendance;
using Domain.Core;

namespace Service.Services.Hr.Attendance
{
    public class AttendanceServiceData : IAttendanceServiceData
    {

        private readonly IAttendanceRepository _attendanceRepository;

        public AttendanceServiceData(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }

        #region Public Methods
        /// <summary>
        /// Get Employee Leaves
        /// </summary>
        /// <param name="employeeNumber"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetEmployeeLeaveAsync(string employeeNumber)
        {
            var data = await _attendanceRepository.GetAttendanceAsync(employeeNumber);
            return new ResponseResult(data, HttpStatusCode.OK);
        }

        /// <summary>
        /// Get Mawred Leaves
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> GetMawredLeaveAsync()
        {
            var data = await _attendanceRepository.GetMawredAttendanceAsync();
            return new ResponseResult(data, HttpStatusCode.OK);
        }

        /// <summary>
        /// Insert Mawred Leaves
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> InsertMawredLeaveAsync()
        {
            var data = await _attendanceRepository.InsertMawredAttendanceAsync();
            return new ResponseResult(data, HttpStatusCode.OK);
        }
        

        #endregion






    }
}
