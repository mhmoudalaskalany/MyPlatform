using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Unit;
using Entities.Enum;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Service.Services.Base;

namespace Service.Services.Hr.NewUnit.Integration
{
    public class ExternalNewFullUnitService : BaseService<Entities.Entities.Hr.Unit, AddUnitDto, UnitDto, Guid>, IExternalNewUnitService
    {

        public ExternalNewFullUnitService(IServiceBaseParameter<Entities.Entities.Hr.Unit> parameters) : base(parameters)
        {

        }

        #region Public Methods
        /// <summary>
        /// Get Unit Parent ( Used In Stock )
        /// </summary>
        /// <param name="childId"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetUnitParentAsync(Guid childId)
        {
            var entity = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == childId, include:
                src => src.Include(p => p.Parent));
            var parent = Mapper.Map<Entities.Entities.Hr.Unit, UnitDto>(entity.Parent);
            return ResponseResult.PostResult(parent, HttpStatusCode.OK);
        }
        /// <summary>
        /// Get All Departments
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<DataPaging> GetDropDownForDepartmentAsync(BaseParam<SearchCriteriaFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var predicate = PredicateBuilderFunctionForDepartment(filter.Filter);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: predicate
                , skip: offset, take: limit, filter.OrderByValue, include: src => src.Include(s => s.Parent).ThenInclude(p => p.Parent).ThenInclude(p => p.Parent));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Unit>, IEnumerable<UnitDto>>(query.Item2);

            foreach (var unit in data)
            {
                var parent = query.Item2.First(x => x.Id == unit.Id).Parent;
                if (parent != null)
                {
                    unit.Parent.NameAr = parent.NameAr;
                    if (parent.Parent != null)
                    {
                        unit.NameAr = parent.NameAr + " - " + unit.NameAr;
                    }

                }

            }
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }
        /// <summary>
        /// Get All Units Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var predicate = PredicateBuilderFunction(filter.Filter);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: predicate
                , skip: offset, take: limit, filter.OrderByValue, include: src => src.Include(s => s.Parent).ThenInclude(p => p.Parent).ThenInclude(p => p.Parent));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Unit>, IEnumerable<UnitDto>>(query.Item2);

            foreach (var unit in data)
            {
                var parent = query.Item2.First(x => x.Id == unit.Id).Parent;
                if (parent != null)
                {
                    unit.Parent.NameAr = parent.NameAr;
                    if (parent.Parent != null)
                    {
                        unit.NameAr = parent.NameAr + " - " + unit.NameAr;
                    }

                }

            }
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }

        /// <summary>
        /// Get Units By Ids (Used In Stock)
        /// </summary>
        /// <param name="unitIds"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetUnitsByIdsAsync(List<Guid> unitIds)
        {
            var entities = (await UnitOfWork.Repository.FindAsync(x => unitIds.Contains(x.Id), include:
                src => src.Include(p => p.Parent))).ToList();
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Unit>, List<UnitDto>>(entities);
            foreach (var unit in data)
            {
                var parent = entities.First(x => x.Id == unit.Id).Parent;
                if (parent != null)
                {
                    unit.NameAr = unit.NameAr + " - " + parent.NameAr;
                }
            }
            return ResponseResult.PostResult(data, HttpStatusCode.OK);
        }

        #endregion


        #region Private Methods
        /// <summary>
        /// Get All Departments Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Hr.Unit, bool>> PredicateBuilderFunctionForDepartment(SearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Unit>(true);
            if (!string.IsNullOrWhiteSpace(filter.SearchCriteria))
            {
                predicate = predicate.Or(b => b.NameAr.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.NameAr.Contains(filter.SearchCriteria));
                predicate = predicate.And(u => u.UnitType == UnitType.Department);


            }
            return predicate;
        }
        /// <summary>
        /// Get All Units Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Hr.Unit, bool>> PredicateBuilderFunction(SearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Unit>(true);
            if (!string.IsNullOrWhiteSpace(filter.SearchCriteria))
            {
                predicate = predicate.Or(b => b.NameAr.ToLower().Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.NameAr.Contains(filter.SearchCriteria));
                predicate = predicate.Or(u => u.Id.Equals(filter.SearchCriteria));
            }
            return predicate;
        }

        #endregion
    }
}
