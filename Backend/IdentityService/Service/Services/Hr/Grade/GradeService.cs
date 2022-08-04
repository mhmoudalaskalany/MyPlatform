using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Grade;
using Domain.DTO.Hr.Grade.Parameters;
using LinqKit;
using Service.Services.Base;

namespace Service.Services.Hr.Grade
{
    public class GradeService : BaseService<Entities.Entities.Hr.Grade, AddGradeDto, GradeDto, long?>, IGradeService
    {
        public GradeService(IServiceBaseParameter<Entities.Entities.Hr.Grade> parameters) : base(parameters)
        {

        }

        #region Public Methods

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<GradeFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Grade>, IEnumerable<GradeDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }

        #endregion

        #region Private Methods

        static Expression<Func<Entities.Entities.Hr.Grade, bool>> PredicateBuilderFunction(GradeFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Grade>(true);

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
