using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Unit;
using Domain.DTO.Hr.Unit.Parameters;
using Entities.Enum;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Service.Services.Base;

namespace Service.Services.Hr.Unit
{
    public class UnitService : BaseService<Entities.Entities.Hr.Unit, AddUnitDto, UnitDto, Guid>, IUnitService
    {
        public UnitService(IServiceBaseParameter<Entities.Entities.Hr.Unit> parameters) : base(parameters)
        {
        }

        #region Public Methods
        /// <summary>
        /// Transform Name To Full Name
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// Get Units Count For Dashboard
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> GetUnitsCountAsync()
        {
            var count = await UnitOfWork.Repository.Count(x => x.IsDeleted == false);
            return ResponseResult.PostResult(count, HttpStatusCode.OK);
        }
        /// <summary>
        /// Get All 
        /// </summary>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        public override async Task<IFinalResult> GetAllAsync(bool disableTracking = false)
        {
            var query = (await UnitOfWork.Repository.GetAllAsync(disableTracking: disableTracking,
                include: src => src.Include(p => p.Parent))).ToList();

            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Unit>, IEnumerable<UnitDto>>(query);
            foreach (var item in data)
            {
                var unit = query.First(x => x.Id == item.Id);
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
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        /// <summary>
        /// Get Drop Down
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
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<UnitFilter> filter)
        {

            int limit = filter.PageSize;
            int offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Unit>, IEnumerable<UnitDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }
        /// <summary>
        /// Get Sections Of Same Department By Employee Section Id
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>

        public async Task<IFinalResult> GetSectionsByEmployeeSectionIdAsync(Guid sectionId)
        {
            var query = await UnitOfWork.Repository.FindAsync(await SectionsPredicateBuilderFunction(sectionId));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Unit>, List<UnitDto>>(query);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());
        }

        /// <summary>
        /// Get Unit Or Team
        /// </summary>
        /// <param name="id"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetUnitOrTeamAsync(Guid id, UnitType unitType)
        {

            var unit = await UnitOfWork.Repository.GetAsync(id);


            var data = Mapper.Map<Entities.Entities.Hr.Unit, UnitDto>(unit);

            return ResponseResult.PostResult(data, HttpStatusCode.OK);
        }
        /// <summary>
        /// Get Recursion Units
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> GetUnitsWithChildren()
        {
            var localDatabase = (await UnitOfWork.Repository.GetAllAsync()).ToList();
            var unitList = new List<UnitChildrenDto>();
            var list = new List<Guid>();
            // get unit type 3
            var units = localDatabase.Where(x => x.UnitType == UnitType.Department);


            foreach (var unit in units)
            {
                var ids = new List<Guid> { unit.Id };
                var unitDto = new UnitChildrenDto
                {
                    NameAr = unit.NameAr,
                    NameEn = unit.NameEn,

                };
                await GetChildren(unit, ids, localDatabase);
                unitDto.ChildrenIds = ids;
                unitList.Add(unitDto);
                list.AddRange(ids);
            }

            var sectorsAndDirectorates = localDatabase.Where(x =>
                x.UnitType == UnitType.Sector || x.UnitType == UnitType.Directorate);

            var sectorAndDireChildrenList = new List<UnitChildrenDto>();
            foreach (var item in sectorsAndDirectorates)
            {
                var sections = localDatabase.Where(x => x.ParentId == item.Id && x.UnitType == UnitType.Section);
                var unitDto = new UnitChildrenDto
                {
                    NameAr = item.NameAr,
                    NameEn = item.NameEn,
                    ChildrenIds = sections.Select(x => x.Id).ToList()
                };
                unitDto.ChildrenIds.Insert(0, item.Id);
                sectorAndDireChildrenList.Add(unitDto);
            }
            unitList.AddRange(sectorAndDireChildrenList);
            return ResponseResult.PostResult(unitList, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Predicate For Drop Down
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Hr.Unit, bool>> PredicateBuilderFunction(SearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Unit>(true);
            if (!string.IsNullOrWhiteSpace(filter.SearchCriteria))
            {
                predicate = predicate.Or(b => b.NameAr.Contains(filter.SearchCriteria.ToLower()));
                predicate = predicate.Or(b => b.NameAr.Contains(filter.SearchCriteria));
            }
            return predicate;
        }
        /// <summary>
        /// Predicate For Get Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Hr.Unit, bool>> PredicateBuilderFunction(UnitFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Unit>(true);
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
        /// <summary>
        /// Recursion Get Children
        /// </summary>
        /// <param name="current"></param>
        /// <param name="ids"></param>
        /// <param name="localDatabase"></param>
        /// <returns></returns>
        async Task GetChildren(Entities.Entities.Hr.Unit current, List<Guid> ids, List<Entities.Entities.Hr.Unit> localDatabase)
        {
            if (current == null)
            {
                return;
            }

            var subUnits = localDatabase.Where(x => x.ParentId == current.Id).ToList();
            //current = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == current.Id,
            //    include: src => src.Include(sb => sb.SubUnits));
            ids.AddRange(subUnits.Select(x => x.Id));
            foreach (var child in subUnits)
            {
                await GetChildren(child, ids, localDatabase);
            }

        }
        /// <summary>
        /// Sections Predicate
        /// </summary>
        /// <param name="sectionId"></param>
        /// <returns></returns>
        async Task<Expression<Func<Entities.Entities.Hr.Unit, bool>>> SectionsPredicateBuilderFunction(Guid sectionId)
        {
            // get employee parent unit ( department , directorate , any thing )
            // then get all children under it when its parent id = the id of the parent unit we got in first query
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Unit>(true);
            var departmentOfTheSection = await UnitOfWork.Repository.GetAsync(sectionId);
            predicate = predicate.And(x => x.ParentId == departmentOfTheSection.ParentId);
            return predicate;
        }
   

        #endregion










    }
}
