using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Hr.Employee;
using Service.Services.Base;

namespace Service.Services.Validators.Services.Employee
{
    public interface IEmployeeValidationService : IBaseService<Entities.Entities.Hr.Employee , AddEmployeeDto , EmployeeDto , Guid?>
    {
        /// <summary>
        /// Check National Id
        /// </summary>
        /// <param name="nationalId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<IFinalResult> CheckNationalIdAsync(string nationalId, Guid employeeId);
        /// <summary>
        /// Check Email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<IFinalResult> CheckEmailAsync(string email, Guid employeeId);
        /// <summary>
        /// Check File Number Existing
        /// </summary>
        /// <param name="fileNumber"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        Task<IFinalResult> CheckFileNumberAsync(string fileNumber, Guid employeeId);
    }
}
