using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.FullUnit;
using Domain.DTO.Hr.FullUnit.Parameters;
using Domain.Extensions;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Service.Services.Base;

namespace Service.Services.Hr.FullUnit
{
    public class FullUnitService : BaseService<Entities.Entities.Hr.FullUnit, AddFullUnitDto, FullUnitDto, string>, IFullUnitService
    {
        public FullUnitService(IServiceBaseParameter<Entities.Entities.Hr.FullUnit> parameters) : base(parameters)
        {

        }

        #region Public Method

        public async Task<IFinalResult> GetUnitsCountAsync()
        {
            var count = await UnitOfWork.Repository.Count(x => x.IsDeleted == false);
            return ResponseResult.PostResult(count, HttpStatusCode.OK);
        }

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<FullUnitFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.FullUnit>, IEnumerable<FullUnitDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }
       

        public async Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var predicate = DropDownPredicateBuilderFunction(filter.Filter);
            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: predicate
                , skip: offset, take: limit, Utilities.GetOrderByList("Type" , "Asc"), include: src => src.Include(s => s.Parent).ThenInclude(p => p.Parent).ThenInclude(p => p.Parent));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.FullUnit>, IEnumerable<FullUnitDto>>(query.Item2);
            foreach (var item in data)
            {
                var unit = query.Item2.First(x => x.Id == item.Id);
                item.NameAr = item.NameAr;
                if (unit.Parent != null)
                {
                    item.NameAr = unit.NameAr + " - " + unit.Parent.NameAr;
                }
                if (unit.Parent?.Parent != null)
                {
                    item.NameAr = unit.NameAr + " - " + unit.Parent.NameAr + " - " + unit.Parent.Parent.NameAr;
                }
                if (unit.Parent?.Parent?.Parent != null)
                {
                    item.NameAr = unit.NameAr + " - " + unit.Parent.NameAr + " - " + unit.Parent.Parent.NameAr + " - " + unit.Parent.Parent.Parent.NameAr;
                }
            }
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }

       
        public async Task TransformAsync()
        {
            var query = (await UnitOfWork.Repository.GetAllAsync(disableTracking: false,
                include: src => src.Include(p => p.Parent))).ToList();
            
            foreach (var item in query)
            {
                var unit = query.First(x => x.Id == item.Id);
                item.FullNameEn = item.NameAr;
                item.FullNameAr = item.NameAr;
                if (unit.Parent != null)
                {
                    item.FullNameAr = unit.NameAr + " - " + unit.Parent.NameAr;
                }
                if (unit.Parent?.Parent != null)
                {
                    item.FullNameAr = unit.NameAr + " - " + unit.Parent.NameAr + " - " + unit.Parent.Parent.NameAr;
                }
                if (unit.Parent?.Parent?.Parent != null)
                {
                    item.FullNameAr = unit.NameAr + " - " + unit.Parent.NameAr + " - " + unit.Parent.Parent.NameAr + " - " + unit.Parent.Parent.Parent.NameAr;
                }
            }

            UnitOfWork.Repository.UpdateRange(query);
            await UnitOfWork.SaveChanges();
        }

        #endregion

        #region Private Methods

        static Expression<Func<Entities.Entities.Hr.FullUnit, bool>> PredicateBuilderFunction(FullUnitFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.FullUnit>(true);
            if (!string.IsNullOrWhiteSpace(filter?.Id))
            {
                predicate = predicate.And(b => b.Id.Contains(filter.Id));
            }
            if (!string.IsNullOrWhiteSpace(filter?.NameAr))
            {
                predicate = predicate.And(b => b.NameAr.ToLower().Contains(filter.NameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter?.NameEn))
            {
                predicate = predicate.And(b => b.NameEn.ToLower().Contains(filter.NameEn.ToLower()));
            }
            return predicate;
        }

        static Expression<Func<Entities.Entities.Hr.FullUnit, bool>> DropDownPredicateBuilderFunction(SearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.FullUnit>(true);
            if (!string.IsNullOrWhiteSpace(filter.SearchCriteria))
            {
                predicate = predicate.And(b => b.NameAr.ToLower().Contains(filter.SearchCriteria.ToLower()));
            }
            return predicate;
        }
        #endregion




    }
}
