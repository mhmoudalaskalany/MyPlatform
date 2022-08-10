using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Abstraction.UnitOfWork;
using Domain.Core;
using Domain.DTO.Identity.App;
using Domain.DTO.Identity.UserApp;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Service.Services.Base;

namespace Service.Services.Identity.UserApp
{
    public class UserAppService : BaseService<Entities.Entities.Identity.UserApp, AddUserAppDto, UserAppDto, Guid?>, IUserAppService
    {
        private readonly IUnitOfWork<Entities.Entities.Identity.App> _appUnitOfWork;
        public UserAppService(IServiceBaseParameter<Entities.Entities.Identity.UserApp> businessBaseParameter, IUnitOfWork<Entities.Entities.Identity.App> appUnitOfWork) : base(
            businessBaseParameter)
        {
            _appUnitOfWork = appUnitOfWork;
        }

        #region Public Methods
        /// <summary>
        /// Add App List
        /// </summary>
        /// <param name="ids"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        public virtual async Task<bool> AddAppListAsync(List<Guid> ids, Guid userId)
        {

            var dtoList = new List<AddUserAppDto>();
            foreach (var id in ids)
            {
                var dto = new AddUserAppDto
                {
                    UserId = userId,
                    AppId = id
                };
                dtoList.Add(dto);
            }
            await base.AddListAsync(dtoList);
            return true;


        }

        /// <summary>
        /// Get User Apps
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetUserApps(Guid userId)
        {

            var predicate = PredicateBuilderFunction(userId);
            var userApps = await UnitOfWork.Repository.FindAsync(predicate, include: source => source.Include(c => c.App));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.UserApp>, IEnumerable<UserAppDto>>(userApps).ToList();
            await GetAppUsersCount(data);
            var publicApps = (await _appUnitOfWork.Repository.FindAsync(x => x.IsPublic == true)).ToList();
            foreach (var app in publicApps)
            {
                var userAppDto = new UserAppDto
                {
                    App = Mapper.Map<AppDto>(app)
                };
                data.Add(userAppDto);
            }
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        /// <summary>
        /// Add Users To App
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public async Task<IFinalResult> AddUsersToAppAsync(List<AddUserAppDto> dtos)
        {

            var entities = Mapper.Map<IEnumerable<AddUserAppDto>, IEnumerable<Entities.Entities.Identity.UserApp>>(dtos);
            UnitOfWork.Repository.AddRange(entities);
            var affectedRows = await UnitOfWork.SaveChanges();
            if (affectedRows > 0)
            {
                Result = new ResponseResult(result: null, status: HttpStatusCode.Created,
                    message: "Data Inserted Successfully");
            }

            return Result;

        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Get App Users Count
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        async Task GetAppUsersCount(IEnumerable<UserAppDto> data)
        {
            foreach (var app in data)
            {
                var list = await UnitOfWork.Repository.FindAsync(x => x.AppId == app.AppId);
                var count = list.Select(x => x.UserId).Count();
                app.App.UsersCount = count;
            }
        }
        /// <summary>
        /// Predicate Builder
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Identity.UserApp, bool>> PredicateBuilderFunction(Guid userId)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Identity.UserApp>(true);
            if (!string.IsNullOrWhiteSpace(userId.ToString()))
            {
                predicate = predicate.And(b => b.UserId == userId && b.App.IsPublic == false);
            }

            return predicate;
        }

        #endregion


    }
}
