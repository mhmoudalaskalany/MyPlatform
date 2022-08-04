using System.Threading.Tasks;
using Domain.Core;

namespace Service.Services.Hr.Attendance
{
    public interface IAttendanceServiceData 
    {
        /// <summary>
        /// Get Employees Count
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetEmployeeLeaveAsync(string employeeNumber);
        /// <summary>
        /// Get Mawred Leaves
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetMawredLeaveAsync();
        /// <summary>
        /// Insert Mawred Leaves
        /// </summary>
        /// <returns></returns>
        Task<IResult> InsertMawredLeaveAsync();


    }
}
