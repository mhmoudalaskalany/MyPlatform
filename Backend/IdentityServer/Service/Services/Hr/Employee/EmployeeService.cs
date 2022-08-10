using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Abstraction.UnitOfWork;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Employee;
using Domain.DTO.Hr.Employee.Parameters;
using Domain.Helper.HttpClient;
using Entities.Enum;
using Integration.FileRepository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using Service.Services.Base;

namespace Service.Services.Hr.Employee
{
    public class EmployeeService : BaseService<Entities.Entities.Hr.Employee, AddEmployeeDto, EmployeeDto, Guid?>, IEmployeeService
    {
        private readonly MicroServicesUrls _urls;
        private readonly IFileRepository _fileRepository;
        private readonly IUnitOfWork<Entities.Entities.Hr.Unit> _unitOfWork;

        public EmployeeService(IServiceBaseParameter<Entities.Entities.Hr.Employee> parameters, IUnitOfWork<Entities.Entities.Hr.Unit> unitOfWork, IFileRepository fileRepository, MicroServicesUrls urls) : base(parameters)
        {
            _unitOfWork = unitOfWork;
            _fileRepository = fileRepository;
            _urls = urls;
        }

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> GetEmployeeCountAsync()
        {
            var employees = await UnitOfWork.Repository.Count();
            return ResponseResult.PostResult(employees, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());
        }


        /// <summary>
        /// Get Unit Manager
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetUnitManagerAsync(string unitId, UnitType? unitType)
        {
            Entities.Entities.Hr.Employee employee;

            employee =
                await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.UnitId == Guid.Parse(unitId) && x.IsManager == true);

            var data = Mapper.Map<Entities.Entities.Hr.Employee, EmployeeDto>(employee);
            return new ResponseResult(data, HttpStatusCode.OK);
        }
        /// <summary>
        /// Get By Id For View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetByIdForViewAsync(Guid id)
        {
            var entity =
                await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == id,
                    include: src => src.Include(u => u.Unit)
                     .ThenInclude(p => p.Parent).ThenInclude(gp => gp.Parent));

            var data = Mapper.Map<Entities.Entities.Hr.Employee, EmployeeDto>(entity);
            if (data.ManagerId != null)
            {
                var manager = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == data.ManagerId);
                data.Manager.Email = manager.Email;
            }
            return new ResponseResult(data, HttpStatusCode.OK);
        }


        public async Task<IFinalResult> GetEmployeeIdsByUnitIdAsync(string unitId)
        {

            var ids = new List<Guid> { Guid.Parse(unitId) };

            var unit = await _unitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == Guid.Parse(unitId));
            //  ,include: src => src.Include(sb => sb.SubUnits));
            await GetChildren(unit, ids);
            var employees = await UnitOfWork.Repository.FindAsync(x => ids.Contains(x.UnitId));

            var employeeIds = employees.Select(x => x.Id).ToList();
            return ResponseResult.PostResult(employeeIds, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<EmployeeFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Employee>, IEnumerable<EmployeeDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }

        public async Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var predicate = DropDownPredicateBuilderFunction(filter.Filter);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: predicate
                , skip: offset, take: limit, filter.OrderByValue,
                include: src =>
                    src.Include(sec => sec.Unit)
                        .ThenInclude(p => p.Parent)
                        .ThenInclude(pr => pr.Parent)
                        .ThenInclude(pt => pt.Parent));

            var data = Mapper.Map<IEnumerable<EmployeeDto>>(query.Item2);
            foreach (var employee in data)
            {
                var unit = query.Item2.First(x => x.Id == employee.Id).Unit;
                if (unit != null)
                {
                    employee.Unit.NameAr = unit.NameAr;
                    if (unit.Parent != null)
                    {
                        employee.Unit.NameAr = unit.NameAr + " - " + unit.Parent.NameAr;
                    }
                    if (unit.Parent?.Parent != null)
                    {
                        employee.Unit.NameAr = unit.NameAr + " - " + unit.Parent.NameAr + " - " + unit.Parent.Parent.NameAr;
                    }
                    if (unit.Parent?.Parent?.Parent != null)
                    {
                        employee.Unit.NameAr = unit.NameAr + " - " + unit.Parent.NameAr + " - " + unit.Parent.Parent.NameAr + " - " + unit.Parent.Parent.Parent.NameAr;
                    }
                }

            }
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }


        public async Task<DataPaging> GetDropDownForHrAsync(BaseParam<SearchCriteriaFilter> filter)
        {
            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: DropDownPredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Employee>, IEnumerable<EmployeeDto>>(query.Item2);
            data.ForEach(e =>
            {
                if (e.PhotoId != null)
                {
                    e.PhotoUrl = _urls.DownloadWithId + "/" + e.PhotoId + "?appCode=" + Configuration["AppCodes:HR"];
                }
            });
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));
        }






        public override async Task<IFinalResult> AddAsync(AddEmployeeDto model)
        {
            var entity = Mapper.Map<AddEmployeeDto, Entities.Entities.Hr.Employee>(model);
            entity.Id = Guid.NewGuid();
            UnitOfWork.Repository.Add(entity);
            await UnitOfWork.SaveChanges();
            return ResponseResult.PostResult(true, HttpStatusCode.Created, null, "AddSuccess");

        }

        #endregion



        #region Private Methods



        static Expression<Func<Entities.Entities.Hr.Employee, bool>> PredicateBuilderFunction(EmployeeFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Employee>(true);

            if (filter.Id != null)
            {
                predicate = predicate.And(b => b.Id.ToString().Contains(filter.Id.ToString()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FullNameAr))
            {
                predicate = predicate.And(b => b.FullNameAr.ToLower().Contains(filter.FullNameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FullNameEn))
            {
                predicate = predicate.And(b => b.FullNameEn.ToLower().Contains(filter.FullNameEn.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.NationalId))
            {
                predicate = predicate.And(b => b.CivilNumber.Contains(filter.NationalId));
            }
            if (!string.IsNullOrWhiteSpace(filter.FileNumber))
            {
                predicate = predicate.And(b => b.FileNumber.Contains(filter.FileNumber));
            }
            if (!string.IsNullOrWhiteSpace(filter.Phone))
            {
                predicate = predicate.And(b => b.Phone.Contains(filter.Phone));
            }
            return predicate;
        }

        static Expression<Func<Entities.Entities.Hr.Employee, bool>> DropDownPredicateBuilderFunction(SearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Employee>(true);
            if (!string.IsNullOrWhiteSpace(filter.SearchCriteria))
            {
                predicate = predicate.Or(b => b.FullNameAr.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.CivilNumber.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.Phone.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.FullNameEn.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.Email.Contains(filter.SearchCriteria));
            }
            return predicate;
        }

        async Task GetChildren(Entities.Entities.Hr.Unit current, List<Guid> ids)
        {
            if (current == null)
            {
                return;
            }

            current = await _unitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == current.Id,
                include: src => src.Include(sb => sb.Children));
            ids.AddRange(current.Children.Select(x => x.Id));
            foreach (var child in current.Children)
            {
                await GetChildren(child, ids);
            }

        }

        #endregion

    }
}
