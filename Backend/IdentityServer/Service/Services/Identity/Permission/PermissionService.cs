using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.Permission;
using Domain.DTO.Identity.Permission.Parameters;
using LinqKit;
using Service.Services.Base;

namespace Service.Services.Identity.Permission
{
    public class PermissionService : BaseService<Entities.Entities.Identity.Permission, AddPermissionDto, PermissionDto, Guid?>, IPermissionService
    {
        public PermissionService(IServiceBaseParameter<Entities.Entities.Identity.Permission> parameters) : base(parameters)
        {

        }

        #region Public Methods

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<PermissionFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Identity.Permission>, IEnumerable<PermissionDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }

        #endregion

        #region Private Methods
        static Expression<Func<Entities.Entities.Identity.Permission, bool>> PredicateBuilderFunction(PermissionFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Identity.Permission>(true);

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
