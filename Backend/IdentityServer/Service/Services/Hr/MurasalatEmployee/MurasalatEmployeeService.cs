using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstraction.Repository.MurasalatEmployee;
using Domain.Abstraction.UnitOfWork;
using Domain.Core;
using Entities.Entities.Views.Murasalat;

namespace Service.Services.Hr.MurasalatEmployee
{
    public class MurasalatEmployeeServiceData : IMurasalatEmployeeServiceData
    {
        private readonly IMapper _mapper;
        private readonly IMurasalatEmployeeRepository _murasalatEmployeeRepository;
        private readonly IUnitOfWork<Entities.Entities.Hr.FullEmployee> _uow;
        public MurasalatEmployeeServiceData(IMurasalatEmployeeRepository murasalatEmployeeRepository, IMapper mapper, IUnitOfWork<Entities.Entities.Hr.FullEmployee> uow)
        {
            _murasalatEmployeeRepository = murasalatEmployeeRepository;
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
            var entity = await _murasalatEmployeeRepository.GetByIdAsync(id);
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
            var entities = await _murasalatEmployeeRepository.GetAllAsync();
            return new ResponseResult(entities, HttpStatusCode.OK);
        }
        /// <summary>
        /// Get All Distinct
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> GetNonDuplicateAllAsync()
        {
            var entities = await _murasalatEmployeeRepository.GetNonDuplicateAllAsync();
            var mappedEntities = _mapper.Map<List<Entities.Entities.Hr.FullEmployee>>(entities);
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
                var updatedEntities = new List<Entities.Entities.Hr.FullEmployee>();
                var newEntities = new List<Entities.Entities.Hr.FullEmployee>();
                var entities = await _murasalatEmployeeRepository.GetNonDuplicateAllAsync();
                foreach (var entity in entities)
                {
                    // ignore mohsen for now
                    var originalEntity = currentEntities.FirstOrDefault(x => x.Id == entity.Id && entity.CivilNumber != "6479515");
                    if (originalEntity != null)
                    {
                        var updatedEntity = _mapper.Map(entity, originalEntity);
                        updatedEntities.Add(updatedEntity);
                    }
                    else
                    {
                        if (entity.CivilNumber == "6479515")
                        {
                            continue;
                        }
                        var newEntity = _mapper.Map<MurasalatEmployeeView, Entities.Entities.Hr.FullEmployee>(entity);
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