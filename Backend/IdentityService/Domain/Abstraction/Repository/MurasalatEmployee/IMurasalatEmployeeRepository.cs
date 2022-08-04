using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Entities.Views.Murasalat;

namespace Domain.Abstraction.Repository.MurasalatEmployee
{
    public interface IMurasalatEmployeeRepository
    {
        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<MurasalatEmployeeView> GetByIdAsync(Guid id);
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>

        Task<List<MurasalatEmployeeView>> GetAllAsync();
        /// <summary>
        /// Get All No Duplication
        /// </summary>
        /// <returns></returns>
        Task<List<MurasalatEmployeeView>> GetNonDuplicateAllAsync();

    }
}
