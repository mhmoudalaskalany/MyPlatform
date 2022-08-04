using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Entities.Entities.Views.Murasalat;

namespace Domain.Abstraction.Repository.MurasalatUnit
{
    public interface IMurasalatUnitRepository
    {
        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<OrganizationHierarchyView> GetByIdAsync(Guid id);
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>

        Task<List<OrganizationHierarchyView>> GetAllAsync();
        /// <summary>
        /// Get All No Duplication
        /// </summary>
        /// <returns></returns>
        Task<List<OrganizationHierarchyView>> GetNonDuplicateAllAsync();

    }
}
