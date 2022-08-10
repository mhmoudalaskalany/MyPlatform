using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstraction.Repository.MurasalatUnit;
using Domain.Abstraction.UnitOfWork;
using Domain.Core;
using Entities.Entities.Views.Murasalat;

namespace Service.Services.Hr.MurasalatUnit
{
    public class MurasalatUnitServiceData : IMurasalatUnitServiceData
    {
        private readonly IMapper _mapper;
        private readonly IMurasalatUnitRepository _murasalatUnitRepository;
        private readonly IUnitOfWork<Entities.Entities.Hr.MurasalatUnit> _uow;
        public MurasalatUnitServiceData(IMurasalatUnitRepository murasalatUnitRepository, IMapper mapper, IUnitOfWork<Entities.Entities.Hr.MurasalatUnit> uow)
        {
            _murasalatUnitRepository = murasalatUnitRepository;
            _mapper = mapper;
            _uow = uow;
        }

        #region Public Methods
        /// <summary>
        /// Get By Id From View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetByIdFromViewAsync(Guid id)
        {
            var entity = await _murasalatUnitRepository.GetByIdAsync(id);
            return new ResponseResult(entity, HttpStatusCode.OK);
        }
        /// <summary>
        /// Get By Id From Our Database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetByIdAsync(Guid id)
        {
            var entity = await _uow.Repository.GetAsync(id);
            return new ResponseResult(entity, HttpStatusCode.OK);
        }
        /// <summary>
        /// Get All
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> GetAllAsync()
        {
            var entities = await _murasalatUnitRepository.GetAllAsync();
            return new ResponseResult(entities, HttpStatusCode.OK);
        }
        /// <summary>
        /// Get All Distinct
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> GetNonDuplicateAllAsync()
        {
            var entities = await _murasalatUnitRepository.GetNonDuplicateAllAsync();
            var mappedEntities = _mapper.Map<List<Entities.Entities.Hr.MurasalatUnit>>(entities);
            _uow.Repository.AddRange(mappedEntities);
            await _uow.SaveChanges();
            return new ResponseResult(entities.Count, HttpStatusCode.OK);
        }

        /// <summary>
        /// Get All Distinct
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> UpdateNonDuplicateAllAsync()
        {
            try
            {
                var currentEntities = (await _uow.Repository.GetAllAsync()).ToList();
                var updatedEntities = new List<Entities.Entities.Hr.MurasalatUnit>();
                var newEntities = new List<Entities.Entities.Hr.MurasalatUnit>();
                var entities = await _murasalatUnitRepository.GetNonDuplicateAllAsync();
                foreach (var entity in entities)
                {
                    var originalEntity = currentEntities.FirstOrDefault(x => x.Id == entity.ChildId);
                    if (originalEntity != null  && originalEntity.Id == Guid.Parse("b23194c5-bdd5-48d6-b650-f85bb34c125c"))
                    {
                        continue;
                    }
                    if (originalEntity != null)
                    {
                        var updatedEntity = _mapper.Map(entity, originalEntity);
                        updatedEntities.Add(updatedEntity);
                    }
                    else
                    {
                        var newEntity = _mapper.Map<OrganizationHierarchyView, Entities.Entities.Hr.MurasalatUnit>(entity);
                        newEntities.Add(newEntity);
                    }

                }
                _uow.Repository.UpdateRange(updatedEntities);
                _uow.Repository.AddRange(newEntities);
                var affected = await _uow.SaveChanges();
                return new ResponseResult(new
                {
                    Updated = updatedEntities.Count,
                    New = newEntities.Count
                }, HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
           
        }

        #endregion

        #region Private Methods



        #endregion


    }
}