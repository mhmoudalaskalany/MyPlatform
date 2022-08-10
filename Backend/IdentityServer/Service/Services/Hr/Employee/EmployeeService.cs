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
using Domain.Helper.HttpClient;
using Entities.Entities.Hr;
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

        public EmployeeService(IServiceBaseParameter<Entities.Entities.Hr.Employee> parameters,  IUnitOfWork<Entities.Entities.Hr.Unit> unitOfWork, IFileRepository fileRepository, MicroServicesUrls urls) : base(parameters)
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
            // if user have a team id get the employee according to team if
            if (unitType == UnitType.Team)
            {
                var team = await UnitOfWork.GetRepository<EmployeeTeam>()
                    .FirstOrDefaultAsync(x => x.TeamId == Guid.Parse(unitId) && x.IsTeamManager);
                employee =
                    await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == team.EmployeeId);
            }
            else
            {
                employee =
                    await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.UnitId == Guid.Parse(unitId) && x.IsManager == true);
            }

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
                var manager = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == Guid.Parse(data.ManagerId));
                data.Manager.Email = manager.Email;
            }
            return new ResponseResult(data, HttpStatusCode.OK);
        }

        
        public async Task<IFinalResult> GetEmployeeIdsByUnitIdAsync(string unitId)
        {

            var ids = new List<Guid> { Guid.Parse(unitId) };

            var unit = await _unitOfWork.Repository.FirstOrDefaultAsync(x => x.Id ==Guid.Parse(unitId));
            //  ,include: src => src.Include(sb => sb.SubUnits));
            await GetChildren(unit, ids);
            var employees = await UnitOfWork.Repository.FindAsync(x => ids.Contains(x.UnitId));

            var employeeIds = employees.Select(x => x.Id).ToList();
            return ResponseResult.PostResult(employeeIds, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<NewEmployeeFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.FullEmployee>, IEnumerable<NewEmployeeDto>>(query.Item2);
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

            var data = Mapper.Map<IEnumerable<NewEmployeeDto>>(query.Item2);
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
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.FullEmployee>, IEnumerable<MurasalatEmployeeDto>>(query.Item2);
            data.ForEach(e =>
            {
                if (e.PhotoId != null)
                {
                    e.PhotoUrl = _urls.DownloadWithId + "/" + e.PhotoId + "?appCode=" + Configuration["AppCodes:HR"];
                }
            });
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));
        }



        public async Task<IFinalResult> UpdateEmployeeImageAsync(UpdateEmployeeImageDto dto)
        {
            var entity = await _unitOfWork.GetRepository<Entities.Entities.Hr.FullEmployee>().GetAsync(dto.EmployeeId);
            entity.PhotoId = dto.NewPhotoId;
            await UnitOfWork.GetRepository<Entities.Entities.Hr.FullEmployee>().UpdateAsync(entity.Id, entity);
            var affectedRows = await UnitOfWork.SaveChanges();
            if (affectedRows > 0)
            {
                if (dto.OldPhotoId != null)
                {
                    await _fileRepository.DeleteFile(dto.OldPhotoId.Value);
                }

                return ResponseResult.PostResult(true, HttpStatusCode.Accepted, null, "UpdateSuccess");
            }
            return ResponseResult.PostResult(false, HttpStatusCode.BadRequest, null, "UpdateError");
        }


        public override async Task<IFinalResult> AddAsync(AddMurasalatEmployeeDto model)
        {
            var entity = Mapper.Map<AddMurasalatEmployeeDto, Entities.Entities.Hr.FullEmployee>(model);
            var unit = await UnitOfWork.GetRepository<Entities.Entities.Hr.Unit>().GetAsync(model.UnitId);
            if (model.ManagerId != Guid.Empty && model.ManagerId != Guid.NewGuid() && model.ManagerId != null)
            {
                var manager = await UnitOfWork.Repository.GetAsync(model.ManagerId);
                entity.DirectManagerCivilNumber = manager.CivilNumber.TrimStart('0');
                entity.DirectManagerEmail = manager.Email;
                entity.DirectManagerFileNumber = manager.FileNumber;
                entity.ArDirectManagerName = manager.ArFullName;
                entity.ArDirectManagerPosition = manager.ArPositiontype;
            }
            entity.Id = Guid.NewGuid();
            entity.Unit = null;
            entity.ArDepartmentName = unit.NameAr;
            entity.EnDepartmentName = unit.NameAr;
          //  entity.CivilNumber = entity.CivilNumber.TrimStart('0');

            UnitOfWork.Repository.Add(entity);
            await UnitOfWork.SaveChanges();
            return ResponseResult.PostResult(true, HttpStatusCode.Created,null , "AddSuccess");

        }

        #endregion



        #region Private Methods



        static Expression<Func<Entities.Entities.Hr.FullEmployee, bool>> PredicateBuilderFunction(NewEmployeeFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.FullEmployee>(true);

            if (filter.Id != 0 && filter.Id != null)
            {
                predicate = predicate.And(b => b.Id.ToString().Contains(filter.Id.ToString()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FullNameAr))
            {
                predicate = predicate.And(b => b.ArFullName.ToLower().Contains(filter.FullNameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FullNameEn))
            {
                predicate = predicate.And(b => b.EnFullName.ToLower().Contains(filter.FullNameEn.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.NationalId))
            {
                predicate = predicate.And(b => b.CivilNumber.Contains(filter.NationalId));
            }
            if (!string.IsNullOrWhiteSpace(filter.UnitId))
            {
                predicate = predicate.And(b => b.DepartmentCode.Contains(filter.UnitId));
            }
            if (!string.IsNullOrWhiteSpace(filter.FileNumber))
            {
                predicate = predicate.And(b => b.FileNumber.Contains(filter.FileNumber));
            }
            if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
            {
                predicate = predicate.And(b => b.Phone.Contains(filter.PhoneNumber));
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
                predicate = predicate.Or(b => b..Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.EnFullName.Contains(filter.SearchCriteria));
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
