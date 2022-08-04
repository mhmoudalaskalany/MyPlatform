using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.Page;
using Domain.DTO.Identity.Page.Parameters;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Service.Services.Base;

namespace Service.Services.Identity.Page
{
    public class PageService : BaseService<Entities.Entities.Identity.Page, AddPageDto, PageDto, long?>, IPageService
    {
        public PageService(IServiceBaseParameter<Entities.Entities.Identity.Page> parameters) : base(parameters)
        {

        }

        #region Public Methods

        public async Task<IResult> GetPagesCountAsync()
        {
            var pages = await UnitOfWork.Repository.Count();
            return ResponseResult.PostResult(pages, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());
        }
        public async Task<IResult> GetByAppId(long appId)
        {

            var predicate = PredicateBuilderFunction(appId);
            var pages = await UnitOfWork.Repository.FindAsync(predicate, include: source =>
                source.Include(c => c.PagePermissions)
                    .ThenInclude(p => p.Permission));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.Page>, IEnumerable<PageDto>>(pages);
            //ConstructPageWithPermission(pages , data);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<PageFilter> filter)
        {

            int limit = filter.PageSize;
            int offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue,
                include: src => src.Include(a => a.App));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.Page>, IEnumerable<PageDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }

        #endregion

        #region Private Methods

        static Expression<Func<Entities.Entities.Identity.Page, bool>> PredicateBuilderFunction(PageFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Identity.Page>(true);

            if (!string.IsNullOrWhiteSpace(filter.NameAr))
            {
                predicate = predicate.And(b => b.NameAr.ToLower().Contains(filter.NameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.NameEn))
            {
                predicate = predicate.And(b => b.NameEn.ToLower().Contains(filter.NameEn.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.AppNameEn))
            {
                predicate = predicate.And(b => b.App.NameEn.ToLower().Contains(filter.AppNameEn.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.AppNameAr))
            {
                predicate = predicate.And(b => b.App.NameAr.ToLower().Contains(filter.AppNameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.Url))
            {
                predicate = predicate.And(b => b.Url.ToLower().Contains(filter.Url.ToLower()));
            }
            return predicate;
        }

        static Expression<Func<Entities.Entities.Identity.Page, bool>> PredicateBuilderFunction(long appId)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Identity.Page>(true);
            if (!string.IsNullOrWhiteSpace(appId.ToString()))
            {
                predicate = predicate.And(b => b.AppId == appId);
            }
            return predicate;
        }

        //static void ConstructPageWithPermission(IEnumerable<Entities.Entities.Page> data , IEnumerable<PageDto> dataDto)
        //{

        //    foreach (var page in data)
        //    {
        //        foreach (var permission in page.PagePermissions)
        //        {
        //            var permissionDto = new PermissionDto
        //            {
        //                Id = permission.PermissionId,
        //                NameEn = permission.Permission.NameEn,
        //                NameAr = permission.Permission.NameAr
        //            };

        //        }
        //    }
        //}

        #endregion


    }
}
