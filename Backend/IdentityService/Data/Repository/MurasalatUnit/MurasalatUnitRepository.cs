using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context.Murasalat;
using Domain.Abstraction.Repository.MurasalatUnit;
using Entities.Entities.Views.Murasalat;
using Microsoft.EntityFrameworkCore;
using MoreLinq.Extensions;

namespace Data.Repository.MurasalatUnit
{
    public class MurasalatUnitRepository : IMurasalatUnitRepository
    {
        private readonly MurasalatDbContext _dbContext;
        public MurasalatUnitRepository(MurasalatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Public Methods
        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<OrganizationHierarchyView> GetByIdAsync(Guid id)
        {
            var entity = await _dbContext.MurasalatUnits.FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizationHierarchyView>> GetAllAsync()
        {
            var entities = await _dbContext.MurasalatUnits.ToListAsync();
            return entities;
        }

        /// <summary>
        /// Get All No Duplication
        /// </summary>
        /// <returns></returns>
        public async Task<List<OrganizationHierarchyView>> GetNonDuplicateAllAsync()
        {
            var entities = await _dbContext.MurasalatUnits.ToListAsync();
            return entities.DistinctBy(e => e.Id).ToList();
        }


        #endregion

    }
}
