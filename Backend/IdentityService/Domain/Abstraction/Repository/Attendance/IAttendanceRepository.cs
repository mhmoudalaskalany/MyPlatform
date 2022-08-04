using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.DTO.Hr.Attendance;

namespace Domain.Abstraction.Repository.Attendance
{
    public interface IAttendanceRepository
    {

        /// <summary>
        /// Get Employee Info New View
        /// </summary>
        /// <param name="employeeNumber"></param>
        /// <returns></returns>
        Task<List<LeaveInfoDto>> GetAttendanceAsync(string employeeNumber);
        /// <summary>
        /// Get Mawred Attendance
        /// </summary>
        /// <returns></returns>
        Task<List<LeaveInfoDto>> GetMawredAttendanceAsync();
        /// <summary>
        /// Insert Mawred Leave
        /// </summary>
        /// <returns></returns>
        Task<bool>  InsertMawredAttendanceAsync();
    }
}
