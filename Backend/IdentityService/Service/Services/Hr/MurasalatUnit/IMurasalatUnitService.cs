using System;
using System.Threading.Tasks;
using Domain.Core;

namespace Service.Services.Hr.MurasalatUnit
{
    public interface IMurasalatUnitServiceData
    {
        /// <summary>
        /// Get By Id From View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> GetByIdFromViewAsync(Guid id);
        /// <summary>
        /// Get By Id From Our Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IResult> GetByIdAsync(Guid id);
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetAllAsync();
        /// <summary>
        /// Get All No Duplication
        /// </summary>
        /// <returns></returns>
        Task<IResult> GetNonDuplicateAllAsync();
        /// <summary>
        /// Update Current Employees
        /// </summary>
        /// <returns></returns>
        Task<IResult> UpdateNonDuplicateAllAsync();
    }
}
