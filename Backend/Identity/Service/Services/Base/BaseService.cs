using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Common.Abstraction.UnitOfWork;
using Common.Core;
using Common.DTO.Identity.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Service.Services.Base
{
    public class BaseService<T, TDto, TGetDto, TKeyDto> : IBaseService<T, TDto, TGetDto, TKeyDto>
        where T : class
        where TDto : IPrimaryKeyField<TKeyDto>
    {
        #region Properties

        protected readonly IUnitOfWork<T> UnitOfWork;
        protected readonly IMapper Mapper;
        protected readonly IResponseResult ResponseResult;
        protected IFinalResult Result;
        protected IHttpContextAccessor HttpContextAccessor;
        protected IConfiguration Configuration;
        protected TokenClaimDto ClaimData { get; set; }

        #endregion

        #region Constructors

        protected internal BaseService(IServiceBaseParameter<T> businessBaseParameter)
        {
            UnitOfWork = businessBaseParameter.UnitOfWork;
            ResponseResult = businessBaseParameter.ResponseResult;
            Mapper = businessBaseParameter.Mapper;
            HttpContextAccessor = businessBaseParameter.HttpContextAccessor;
            Configuration = businessBaseParameter.Configuration;
            var claims = HttpContextAccessor.HttpContext.User;
            ClaimData = new TokenClaimDto()
            {
                UserId = claims?.FindFirst(t => t.Type == "UserId")?.Value,
                TeamId = claims?.FindFirst(t => t.Type == "TeamId")?.Value,
                UnitId = claims?.FindFirst(t => t.Type == "UnitId")?.Value,
                Email = claims?.FindFirst(t => t.Type == "Email")?.Value,
                EmployeeId = claims?.FindFirst(t => t.Type == "EmployeeId")?.Value,
                EmployeeEn = claims?.FindFirst(t => t.Type == "EmployeeEn")?.Value,
                EmployeeAr = claims?.FindFirst(t => t.Type == "EmployeeAr")?.Value,
                IpAddress = HttpContextAccessor.HttpContext?.Connection?.RemoteIpAddress?.ToString()
            };
        }

        #endregion


        #region Public Methods

        /// <summary>
        /// Get By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<IFinalResult> GetByIdAsync(object id)
        {
            T query = await UnitOfWork.Repository.GetAsync(id);
            var data = Mapper.Map<T, TGetDto>(query);
            return ResponseResult.PostResult(result: data, status: HttpStatusCode.OK, null, "Data Retrieved Successfully");
        }
        /// <summary>
        /// Get For Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<IFinalResult> GetByIdForEditAsync(object id)
        {
            var query = await UnitOfWork.Repository.GetAsync(id);
            var data = Mapper.Map<T, TDto>(query);
            return ResponseResult.PostResult(result: data, status: HttpStatusCode.OK,
                message: "Data Retrieved Successfully");
        }

        /// <summary>
        /// Get All
        /// </summary>
        /// <param name="disableTracking"></param>
        /// <returns></returns>

        public virtual async Task<IFinalResult> GetAllAsync(bool disableTracking = false)
        {
            var query = await UnitOfWork.Repository.GetAllAsync(disableTracking: disableTracking);
            var data = Mapper.Map<IEnumerable<T>, IEnumerable<TGetDto>>(query);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());
        }

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<IFinalResult> AddAsync(TDto model)
        {

            T entity = Mapper.Map<TDto, T>(model);
            SetEntityCreatedBaseProperties(entity);
            UnitOfWork.Repository.Add(entity);
            var affectedRows = await UnitOfWork.SaveChanges();
            if (affectedRows > 0)
            {
                Result = new ResponseResult(result: null, status: HttpStatusCode.Created,
                    message: "AddSuccess");
            }

            Result.Data = model;
            return Result;

        }
        /// <summary>
        /// Add List
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<IFinalResult> AddListAsync(List<TDto> model)
        {

            List<T> entities = Mapper.Map<List<TDto>, List<T>>(model);
            UnitOfWork.Repository.AddRange(entities);
            var affectedRows = await UnitOfWork.SaveChanges();
            if (affectedRows > 0)
            {
                Result = new ResponseResult(result: null, status: HttpStatusCode.Created,
                    message: "AddSuccess");
            }

            Result.Data = model;
            return Result;

        }
        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public virtual async Task<IFinalResult> UpdateAsync(TDto model)
        {

            T entityToUpdate = await UnitOfWork.Repository.GetAsync(model.Id);
            var newEntity = Mapper.Map(model, entityToUpdate);
            SetEntityModifiedBaseProperties(newEntity);
            UnitOfWork.Repository.Update(entityToUpdate, newEntity);
            var affectedRows = await UnitOfWork.SaveChanges();
            if (affectedRows > 0)
            {
                Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                    message: "UpdateSuccess");
            }

            return Result;

        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<IFinalResult> DeleteAsync(object id)
        {

            var entityToDelete = await UnitOfWork.Repository.GetAsync(id);
            UnitOfWork.Repository.Remove(entityToDelete);
            var affectedRows = await UnitOfWork.SaveChanges();
            if (affectedRows > 0)
            {
                Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                    message: "DeleteSuccess");
            }

            return Result;

        }
        /// <summary>
        /// Delete Soft
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<IFinalResult> DeleteSoftAsync(object id)
        {
            var entityToDelete = await UnitOfWork.Repository.GetAsync(id);
            SetEntityModifiedBaseProperties(entityToDelete);
            UnitOfWork.Repository.RemoveLogical(entityToDelete);
            var affectedRows = await UnitOfWork.SaveChanges();
            if (affectedRows > 0)
            {
                Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                    message: "DeleteSuccess");
            }
            return Result;
        }


        /// <summary>
        /// Set Creation Audit
        /// </summary>
        /// <param name="entity"></param>
        protected void SetEntityCreatedBaseProperties(T entity)
        {
            var type = entity.GetType();
            var createdByProperty = type.GetProperty("CreatedById");
            if (createdByProperty != null) createdByProperty.SetValue(entity, long.Parse(ClaimData.UserId));

            var createdDateProperty = type.GetProperty("CreatedDate");
            if (createdDateProperty != null) createdDateProperty.SetValue(entity, DateTime.Now);

            var createdByEmployeeIdProperty = type.GetProperty("CreatedByEmployeeId");
            if (createdByEmployeeIdProperty != null) createdByEmployeeIdProperty.SetValue(entity, ClaimData.EmployeeId);

            var createdByEmployeeEn = type.GetProperty("CreatedByEmployeeEn");
            if (createdByEmployeeEn != null) createdByEmployeeEn.SetValue(entity, ClaimData.EmployeeEn);

            var createdByEmployeeAr = type.GetProperty("CreatedByEmployeeAr");
            if (createdByEmployeeAr != null) createdByEmployeeAr.SetValue(entity, ClaimData.EmployeeAr);

            var ipAddress = type.GetProperty("IpAddress");
            if (ipAddress != null) ipAddress.SetValue(entity, ClaimData.IpAddress);


        }
        /// <summary>
        /// Set Modified Audit
        /// </summary>
        /// <param name="entity"></param>
        protected void SetEntityModifiedBaseProperties(T entity)
        {
            if (entity != null)
            {
                var type = entity.GetType();
                var modifiedById = type.GetProperty("ModifiedById");
                if (modifiedById != null) modifiedById.SetValue(entity, long.Parse(ClaimData.UserId));

                var modifiedDate = type.GetProperty("ModifiedDate");
                if (modifiedDate != null) modifiedDate.SetValue(entity, DateTime.Now);

                var modifiedByEmployeeId = type.GetProperty("ModifiedByEmployeeId");
                if (modifiedByEmployeeId != null) modifiedByEmployeeId.SetValue(entity, ClaimData.EmployeeId);

                var modifiedByEmployeeEn = type.GetProperty("ModifiedByEmployeeEn");
                if (modifiedByEmployeeEn != null) modifiedByEmployeeEn.SetValue(entity, ClaimData.EmployeeEn);

                var modifiedByEmployeeAr = type.GetProperty("ModifiedByEmployeeAr");
                if (modifiedByEmployeeAr != null) modifiedByEmployeeAr.SetValue(entity, ClaimData.EmployeeAr);

                var ipAddress = type.GetProperty("IpAddress");
                if (ipAddress != null) ipAddress.SetValue(entity, ClaimData.IpAddress);
            }


        }

        #endregion



    }
}
