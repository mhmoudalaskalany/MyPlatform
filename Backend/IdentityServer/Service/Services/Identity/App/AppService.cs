using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Abstraction.UnitOfWork;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.App;
using Domain.DTO.Identity.App.Parameters;
using LinqKit;
using Service.Services.Base;

namespace Service.Services.Identity.App
{
    public class AppService : BaseService<Entities.Entities.Identity.App, AddAppDto, AppDto, Guid?>, IAppService
    {
        private readonly IUnitOfWork<Entities.Entities.Identity.UserApp> _userAppsUnitOfWork;
        public AppService(IServiceBaseParameter<Entities.Entities.Identity.App> businessBaseParameter, IUnitOfWork<Entities.Entities.Identity.UserApp> userAppsUnitOfWork) : base(
            businessBaseParameter)
        {
            _userAppsUnitOfWork = userAppsUnitOfWork;
        }

        #region Public Methods

        public async Task<IFinalResult> GetAppsCountAsync()
        {
            var apps = await UnitOfWork.Repository.Count();
            return ResponseResult.PostResult(apps, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        public async Task<DataPaging> GetAllPagedAsync(BaseParam<AppFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.App>, IEnumerable<AppDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }

        public async Task<IFinalResult> GetByUserIdAsync(Guid userId)
        {

            var userApps = await UnitOfWork.Repository
                .FindAsync(x => x.UserApps.Select(userApp => userApp.UserId).Contains(userId));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.App>, IEnumerable<AppDto>>(userApps);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        public async Task<IFinalResult> GetUserAppsWithNoRoles(UserAppRolesDto dto)
        {

            var userApps = await _userAppsUnitOfWork.Repository.FindAsync(
                x => x.UserId == dto.UserId && (x.IsDeleted != false || x.IsDeleted != null)
                && !dto.AppIds.Contains(x.AppId)
                );
            var appIds = userApps.Select(x => x.AppId).ToList();
            var apps = await UnitOfWork.Repository.FindAsync(x => appIds.Contains(x.Id));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.App>, IEnumerable<AppDto>>(apps);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }

        public async Task<IFinalResult> GetUserAppsWithRoles(UserAppRolesDto dto)
        {

            var userApps = await _userAppsUnitOfWork.Repository.FindAsync(
                x => x.UserId == dto.UserId && (x.IsDeleted != false || x.IsDeleted != null)
                );
            var appIds = userApps.Select(x => x.AppId).ToList();
            var apps = await UnitOfWork.Repository.FindAsync(x => appIds.Contains(x.Id));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.App>, IEnumerable<AppDto>>(apps);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        // user this to get only the user app on the sub system that request user apps and system is not user management
        public async Task<IFinalResult> GetByUserAppIdAsync(Guid userId, Guid appId)
        {

            var userApps = await UnitOfWork.Repository.FindAsync(app =>
                app.Id == appId && app.UserApps.Select(x => x.UserId).Contains(userId));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.App>, IEnumerable<AppDto>>(userApps);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        /// <summary>
        /// Get Apps For Non Registered Users
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> GetPublicAppsAsync()
        {

            var userApps = await UnitOfWork.Repository.FindAsync(app => app.IsPublic == true && app.IsDeleted == false);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.App>, IEnumerable<AppDto>>(userApps);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }

        #endregion


        #region Private Methods

        static Expression<Func<Entities.Entities.Identity.App, bool>> PredicateBuilderFunction(AppFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Identity.App>(true);

            if (!string.IsNullOrWhiteSpace(filter.NameAr))
            {
                predicate = predicate.And(b => b.NameAr.ToLower().Contains(filter.NameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.NameEn))
            {
                predicate = predicate.And(b => b.NameEn.ToLower().Contains(filter.NameEn.ToLower()));
            }
            return predicate;
        }

        #endregion


    }
}
