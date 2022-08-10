using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Nationality;
using Domain.DTO.Hr.Nationality.Parameters;
using LinqKit;
using Service.Services.Base;

namespace Service.Services.Hr.Nationality
{
    public class NationalityService : BaseService<Entities.Entities.Hr.Nationality, AddNationalityDto, NationalityDto, Guid?>, INationalityService
    {
        public NationalityService(IServiceBaseParameter<Entities.Entities.Hr.Nationality> parameters) : base(parameters)
        {

        }

        #region Public Methods

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<NationalityFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Nationality>, IEnumerable<NationalityDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }

        #endregion



        #region Private Methods

        static Expression<Func<Entities.Entities.Hr.Nationality, bool>> PredicateBuilderFunction(NationalityFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Nationality>(true);

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
