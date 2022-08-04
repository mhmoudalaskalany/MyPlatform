using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context.Murasalat;
using Domain.Abstraction.Repository.MurasalatEmployee;
using Entities.Entities.Views.Murasalat;
using Microsoft.EntityFrameworkCore;
using MoreLinq.Extensions;

namespace Data.Repository.MurasalatEmployee
{
    public class MurasalatEmployeeRepository : IMurasalatEmployeeRepository
    {
        private readonly MurasalatDbContext _dbContext;
        public MurasalatEmployeeRepository(MurasalatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #region Public Methods
        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<MurasalatEmployeeView> GetByIdAsync(Guid id)
        {
            var entity = await _dbContext.MurasalatEmployees.FirstOrDefaultAsync(x => x.Id == id);
            return entity;
        }
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        public async Task<List<MurasalatEmployeeView>> GetAllAsync()
        {
            var entities = await _dbContext.MurasalatEmployees.ToListAsync();
            return entities;
        }

        /// <summary>
        /// Get All No Duplication
        /// </summary>
        /// <returns></returns>
        public async Task<List<MurasalatEmployeeView>> GetNonDuplicateAllAsync()
        {
            var entities = await _dbContext.MurasalatEmployees.Where(e => e.EnPositiontype != "Resine").ToListAsync();
            return entities.DistinctBy(e => e.Id).ToList();
        }


        #endregion

    }
}
