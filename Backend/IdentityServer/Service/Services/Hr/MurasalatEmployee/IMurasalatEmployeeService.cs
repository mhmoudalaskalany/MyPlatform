using System;
using System.Threading.Tasks;
using Domain.Core;

namespace Service.Services.Hr.MurasalatEmployee
{
    public interface IMurasalatEmployeeServiceData
    {
        /// <summary>
        /// Get By Id From View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IFinalResult> GetByIdFromViewAsync(Guid id);
        /// <summary>
        /// Get By Id From Our Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IFinalResult> GetByIdAsync(Guid id);
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        Task<IFinalResult> GetAllAsync();
        /// <summary>
        /// Get All No Duplication
        /// </summary>
        /// <returns></returns>
        Task<IFinalResult> GetNonDuplicateAllAsync();
        /// <summary>
        /// Update Current Employees
        /// </summary>
        /// <returns></returns>
        Task<IFinalResult> UpdateNonDuplicateAllAsync();
    }
}
