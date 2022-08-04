using System.Threading.Tasks;
using Domain.DTO.Hr.Employee;

namespace Domain.Abstraction.Repository.Employee
{
    public interface IEmployeeRepository : IRepository<Entities.Entities.Hr.Employee>
    {
        /// <summary>
        /// Get Employee Info Old View
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        Task<EmployeeInfoDto> GetEmployeeInfoAsync(string nationalId);
        /// <summary>
        /// Get Employee Info New View
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        Task<EmployeeInfoDto> GetEmployeeInfoFromNewViewAsync(string nationalId);
    }
}
