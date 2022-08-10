using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Employee;
using Domain.DTO.Integration.ItHelpDesk.Ticket;
using Entities.Entities.Hr;
using Entities.Enum;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Service.Services.Base;

namespace Service.Services.Hr.Employee.Integration
{
    public class ExternalEmployeeService : BaseService<Entities.Entities.Hr.Employee, AddEmployeeDto, EmployeeDto, Guid?>, IExternalEmployeeService
    {

        public ExternalEmployeeService(IServiceBaseParameter<Entities.Entities.Hr.Employee> parameters) : base(parameters)
        {
        }



        #region Public Methods
        /// <summary>
        /// Get By Id With Unit Name Combined
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<IFinalResult> GetByIdAsync(object id)
        {
            var entity = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == (Guid)id, include: src => src.Include(x => x.Unit)
                .ThenInclude(p => p.Parent)
                .ThenInclude(pr => pr.Parent)
                .ThenInclude(pt => pt.Parent));

            var data = Mapper.Map<Entities.Entities.Hr.Employee>(entity);
            if (entity.Unit != null)
            {
                data.Unit.NameAr = entity.Unit.NameAr;
                if (entity.Unit.Parent != null)
                {
                    data.Unit.NameAr = entity.Unit.NameAr + " - " + entity.Unit.Parent.NameAr;
                }
                if (entity.Unit.Parent?.Parent != null)
                {
                    data.Unit.NameAr = entity.Unit.NameAr + " - " + entity.Unit.Parent.NameAr + " - " + entity.Unit.Parent.Parent.NameAr;
                }
                if (entity.Unit.Parent?.Parent?.Parent != null)
                {
                    data.Unit.NameAr = entity.Unit.NameAr + " - " + entity.Unit.Parent.NameAr + " - " + entity.Unit.Parent.Parent.NameAr + " - " + entity.Unit.Parent.Parent.Parent.NameAr;
                }
            }

            return ResponseResult.PostResult(data, HttpStatusCode.OK);

        }
        /// <summary>
        /// Get All (Used In Self Services)
        /// </summary>
        /// <param name="disableTracking"></param>
        /// <returns></returns>
        public override async Task<IFinalResult> GetAllAsync(bool disableTracking = false)
        {
            var entities = (await UnitOfWork.Repository.GetAllAsync()).ToList();

            var data = Mapper.Map<List<EmployeeDto>>(entities);

            return ResponseResult.PostResult(data, HttpStatusCode.OK);

        }

        /// <summary>
        /// Ger Employees By App Code ( Used In Stock )
        /// </summary>
        /// <param name="appCode"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetByAppCodeAsync(string appCode)
        {
            var appUsers = await UnitOfWork.GetRepository<Entities.Entities.Identity.UserApp>()
                .FindAsync(x => x.App.Code == appCode);

            var userIds = appUsers.Select(x => x.UserId).ToList();

            var employeeIds = (await UnitOfWork.GetRepository<Entities.Entities.Identity.User>()
                .FindSelectAsync(x => new { x.PersonId }, x => userIds.Contains(x.Id) && x.PersonId != null)).ToList();

            var ids = employeeIds.Select(x => Guid.Parse(x.PersonId)).ToList();
            var employees = await UnitOfWork.Repository.FindAsync(x => ids.Contains(x.Id));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Employee>, List<EmployeeDto>>(employees);

            return ResponseResult.PostResult(data, HttpStatusCode.OK);

        }
        public async Task<IFinalResult> GetManagerEmailByUnitIdAsync(string unitId)
        {
            var entity = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.UnitId == Guid.Parse(unitId) && x.IsManager == true);
            var data = Mapper.Map<EmployeeDto>(entity);
            return ResponseResult.PostResult(data, HttpStatusCode.OK);

        }

        /// <summary>
        /// Get Employee Phones By Ids
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetEmployeePhonesByIdsAsync(List<TicketSmsDto> dtos)
        {
            foreach (var dto in dtos)
            {
                var employee = await UnitOfWork.Repository.GetAsync(dto.EmployeeId);
                dto.PhoneNumber = employee.Phone;
            }

            return ResponseResult.PostResult(dtos, HttpStatusCode.OK, null, "Data Retrieved Successfully");
        }
        /// <summary>
        /// Get Unit Manager Phones By Unit Id
        /// </summary>
        /// <param name="dtos"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetUnitManagersPhonesByUnitIdsAsync(List<TicketSmsDto> dtos)
        {
            foreach (var dto in dtos)
            {
                if (dto.UnitType == UnitType.Team)
                {
                    var employee = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.UnitId == dto.UnitId);
                    dto.PhoneNumber = employee?.Phone;
                }
                else
                {
                    //var employee = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.DepartmentCode == dto.UnitId.ToString() && x.Unit.UnitType == dto.UnitType && x.IsManager);
                    //dto.PhoneNumber = employee?.Phone;
                }


            }
            return ResponseResult.PostResult(dtos, HttpStatusCode.OK, null, "Data Retrieved Successfully");
        }
        /// <summary>
        /// Get Employees By Unit Id Or Team Id
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetEmployeesByUnitOrTeamIdAsync(string unitId)
        {
            var unit = await UnitOfWork.GetRepository<Entities.Entities.Hr.Unit>().GetAsync(unitId);
            List<Entities.Entities.Hr.Employee> entities;
            if (!string.IsNullOrEmpty(ClaimData.TeamId))
            {
                var team = await UnitOfWork.GetRepository<EmployeeTeam>()
                    .FindAsync(x => x.TeamId == Guid.Parse(ClaimData.TeamId) && x.IsTeamManager == false);

                var empIds = team.Select(x => x.EmployeeId).ToList();

                entities = (await UnitOfWork.Repository.FindAsync(x =>
                    empIds.Contains(x.Id))).ToList();
            }
            else
            {
                // get unit teams and exclude all employees in these teams from response
                var unitTeams = await UnitOfWork.GetRepository<Entities.Entities.Hr.Team>().FindAsync(x => x.UnitId == Guid.Parse(unitId));

                var teamIds = unitTeams.Select(x => x.Id).ToList();

                var teamEmployees = await UnitOfWork.GetRepository<EmployeeTeam>().FindAsync(x => teamIds.Contains(x.TeamId));

                var excludedEmployeeIds = teamEmployees.Select(x => x.EmployeeId).ToList();

                entities = (await UnitOfWork.Repository.FindAsync(x => x.UnitId == Guid.Parse(unitId) && !excludedEmployeeIds.Contains(x.Id))).ToList();

                var hos = await GetHeadOfSections(unit);

                entities.AddRange(hos);
            }
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Employee>, IEnumerable<EmployeeDto>>(entities);
            return new ResponseResult(data, HttpStatusCode.OK);
        }
        /// <summary>
        /// Get Team Manager Phone
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetTeamManagerPhone(Guid teamId)
        {
            var teamManager =
                await UnitOfWork.GetRepository<EmployeeTeam>().FirstOrDefaultAsync(t => t.TeamId ==  teamId && t.IsTeamManager);

            var manager = await UnitOfWork.Repository.GetAsync(teamManager.EmployeeId);

            var data = Mapper.Map<Entities.Entities.Hr.Employee, EmployeeDto>(manager);

            return new ResponseResult(data.PhoneNumber, HttpStatusCode.OK, null, "Data Retrieved Successfully");
        }
        /// <summary>
        /// Get Employees Phones By Role Code (Used By Legal Affairs)
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetEmployeesPhonesByRoleCodeAsync(string roleCode)
        {
            var role = await UnitOfWork.GetRepository<Entities.Entities.Identity.Role>()
                .FirstOrDefaultAsync(x => x.Code == roleCode);

            var users = (await UnitOfWork.GetRepository<Entities.Entities.Identity.UserRole>().FindSelectAsync(x => new
            {
                x.UserId
            }, x => x.RoleId == role.Id)).ToList();
            var userIds = users.Select(x => x.UserId).ToList();
            var phones = (await UnitOfWork.GetRepository<Entities.Entities.Identity.User>().FindSelectAsync(x => new
            {
                x.PhoneNumber
            }, x => userIds.Contains(x.Id))).ToList();

            return ResponseResult.PostResult(phones.Select(x => x.PhoneNumber).ToList(), HttpStatusCode.OK);
        }

        public async Task<IFinalResult> GetEmployeePhoneByIdAsync(string employeeId)
        {
            var employee = await UnitOfWork.GetRepository<Entities.Entities.Hr.Employee>().FirstOrDefaultAsync(x => x.Id == Guid.Parse(employeeId));
            return ResponseResult.PostResult(employee.Phone, HttpStatusCode.OK);
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
        /// <summary>
        /// Get Employees By Role Code (Used In Legal Affairs)
        /// </summary>
        /// <param name="roleCode"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetByRoleCodeAsync(string roleCode)
        {
            var role = await UnitOfWork.GetRepository<Entities.Entities.Identity.Role>()
                .FirstOrDefaultAsync(x => x.Code == roleCode);

            var users = (await UnitOfWork.GetRepository<Entities.Entities.Identity.UserRole>().FindSelectAsync(x => new
            {
                x.UserId
            }, x => x.RoleId == role.Id)).ToList();
            var userIds = users.Select(x => x.UserId).ToList();
            var usersData = (await UnitOfWork.GetRepository<Entities.Entities.Identity.User>().FindSelectAsync(x => new
            {
                x.Id,
                x.FullNameAr,
                x.FullNameEn,
                x.UserTypeId,
                x.PersonId
            }, x => userIds.Contains(x.Id))).ToList();

            return ResponseResult.PostResult(usersData, HttpStatusCode.OK);
        }

        /// <summary>
        /// Get Employee Ids By Unit (Used By Stock System To Get Unit Report)
        /// if option is false it will get the unit employees only
        /// if option is true it will employees of unit and its children unit
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="option"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetEmployeeIdsByUnitIdAsync(string unitId, bool option)
        {

            if (option == false)
            {
                var managementUnitEmployees = await UnitOfWork.Repository.FindAsync(x => x.UnitId == Guid.Parse(unitId));

                var managementIds = managementUnitEmployees.Select(x => x.Id).ToList();
                return ResponseResult.PostResult(managementIds, status: HttpStatusCode.OK,
                    message: HttpStatusCode.OK.ToString());
            }

            var ids = new List<Guid> { Guid.Parse(unitId)};

            var unit = await UnitOfWork.GetRepository<Entities.Entities.Hr.Unit>().FirstOrDefaultAsync(x => x.Id == Guid.Parse(unitId));
            //   , include: src => src.Include(sb => sb.SubUnits)
            await GetChildren(unit, ids);
            var employees = await UnitOfWork.Repository.FindAsync(x => ids.Contains(x.UnitId));

            var employeeIds = employees.Select(x => x.Id).ToList();
            return ResponseResult.PostResult(employeeIds, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        /// <summary>
        /// Get Murasalat Employees For Self Service
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> GetAllMurasalatAsync()
        {
            var entities = await UnitOfWork.Repository.GetAllAsync();
            return ResponseResult.PostResult(entities, HttpStatusCode.OK);
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Predicate For Get Drop Down
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Hr.Employee, bool>> PredicateBuilderFunction(SearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Employee>(true);
            if (!string.IsNullOrWhiteSpace(filter.SearchCriteria))
            {
                predicate = predicate.Or(b => b.FullNameAr.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.CivilNumber.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.FileNumber.Contains(filter.SearchCriteria));

            }
            return predicate;
        }
        /// <summary>
        /// Get Head Of Section
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private async Task<List<Entities.Entities.Hr.Employee>> GetHeadOfSections(Entities.Entities.Hr.Unit entity)
        {
            var hos = new List<Entities.Entities.Hr.Employee>();
            if (entity.UnitType == UnitType.Department)
            {
                hos = (await UnitOfWork.Repository.FindAsync(x =>
                   x.Unit.ParentId == entity.Id && x.IsManager == true)).ToList();
            }
            return hos;
        }

        /// <summary>
        /// Get Children Units
        /// </summary>
        /// <param name="current"></param>
        /// <param name="ids"></param>
        /// <returns></returns>
        async Task GetChildren(Entities.Entities.Hr.Unit current, List<Guid> ids)
        {
            if (current == null)
            {
                return;
            }

            current = await UnitOfWork.GetRepository<Entities.Entities.Hr.Unit>().FirstOrDefaultAsync(x => x.Id == current.Id,
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
