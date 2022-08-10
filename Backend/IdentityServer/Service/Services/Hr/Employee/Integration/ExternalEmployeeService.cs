using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Domain.Abstraction.UnitOfWork;
using Domain.Core;
using Domain.DTO.Hr.Employee;
using Domain.DTO.Integration.ItHelpDesk.Employee;
using Domain.DTO.Integration.ItHelpDesk.Ticket;
using Entities.Enum;
using Microsoft.EntityFrameworkCore;
using Service.Services.Base;

namespace Service.Services.Hr.Employee.Integration
{
    public class ExternalEmployeeService : BaseService<Entities.Entities.Hr.Employee, AddEmployeeDto, EmployeeDto, long?>, IExternalEmployeeService
    {

        private readonly IUnitOfWork<Entities.Entities.Hr.Team> _teamUnitOfWork;
        private readonly IUnitOfWork<Entities.Entities.Hr.Unit> _unitUnitOfWork;
        public ExternalEmployeeService(IServiceBaseParameter<Entities.Entities.Hr.Employee> parameters, IUnitOfWork<Entities.Entities.Hr.Team> teamUnitOfWork, IUnitOfWork<Entities.Entities.Hr.Unit> unitUnitOfWork) : base(parameters)
        {
            _teamUnitOfWork = teamUnitOfWork;
            _unitUnitOfWork = unitUnitOfWork;
        }



        #region Public Methods
        /// <summary>
        /// Get By Id With Unit Name Combined
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public override async Task<IFinalResult> GetByIdAsync(object id)
        {
            var entity = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == Convert.ToInt64(id),
                include: src => src.Include(u => u.Unit).ThenInclude(p => p.Parent).ThenInclude(p => p.Parent).ThenInclude(p => p.Parent));

            var data = Mapper.Map<EmployeeDto>(entity);
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
       
        ///// <summary>
        ///// Ger Employees By App Code ( Used In Stock )
        ///// </summary>
        ///// <param name="appCode"></param>
        ///// <returns></returns>
        //public async Task<IResult> GetByAppCodeAsync(string appCode)
        //{
        //    var appUsers = await UnitOfWork.GetRepository<Entities.Entities.Identity.UserApp>()
        //        .FindAsync(x => x.App.Code == appCode);

        //    var userIds = appUsers.Select(x => x.UserId).ToList();

        //    var employeeIds = (await UnitOfWork.GetRepository<Entities.Entities.Identity.User>()
        //        .FindSelectAsync(x => new { x.UserTypeId }, x => userIds.Contains(x.Id))).ToList();
        //    var ids = employeeIds.Select(x => x.UserTypeId).ToList();
        //    var employees = await UnitOfWork.Repository.FindAsync(x => ids.Contains(x.Id));
        //    var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Employee>, List<EmployeeDto>>(employees);

        //    return ResponseResult.PostResult(data, HttpStatusCode.OK);

        //}
        public async Task<IFinalResult> GetManagerEmailByUnitIdAsync(long unitId)
        {
            var entity = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.UnitId == unitId && x.IsManager == true);

            var data = Mapper.Map<EmployeeDto>(entity);
           
            

            return ResponseResult.PostResult(data, HttpStatusCode.OK);

        }
        /// <summary>
        /// Update Employee Phone And IPPhone
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IFinalResult> UpdateEmployeeFromServices(UpdateEmployeeFromServicesDto dto)
        {
            var employee = await UnitOfWork.Repository.GetAsync(dto.Id);
            if (employee != null)
            {
                employee.PhoneNumber = string.IsNullOrEmpty(dto.PhoneNumber) ? employee.PhoneNumber : dto.PhoneNumber;
                employee.IpPhone = string.IsNullOrEmpty(dto.IpPhone) ? employee.IpPhone : dto.IpPhone;
                employee.IsUpdated = true;
                await UnitOfWork.Repository.UpdateAsync(employee.Id, employee);
                await UnitOfWork.SaveChanges();
                return ResponseResult.PostResult(true, HttpStatusCode.Accepted, null, "EmployeeUpdated");

            }
            return ResponseResult.PostResult(false, HttpStatusCode.BadRequest, null, "NoThingChanged");
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
                dto.PhoneNumber = employee.PhoneNumber;
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
                    var employee = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.TeamId == dto.UnitId && x.IsTeamManager == true);
                    dto.PhoneNumber = employee?.PhoneNumber;
                }
                else
                {
                    var employee = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.UnitId == dto.UnitId && x.Unit.UnitType == dto.UnitType && x.IsManager == true, include: src => src.Include(u => u.Unit));
                    dto.PhoneNumber = employee?.PhoneNumber;
                }


            }
            return ResponseResult.PostResult(dtos, HttpStatusCode.OK, null, "Data Retrieved Successfully");
        }
        ///// <summary>
        ///// Get Employees By Unit Id Or Team Id
        ///// </summary>
        ///// <param name="unitId"></param>
        ///// <returns></returns>
        //public async Task<IResult> GetEmployeesByUnitOrTeamIdAsync(string unitId)
        //{
        //    var unit = await _unitUnitOfWork.Repository.GetAsync(unitId);
        //    List<Entities.Entities.Hr.Employee> entities;
        //    if (!string.IsNullOrEmpty(ClaimData.TeamId))
        //    {
        //        entities = (await UnitOfWork.Repository.FindAsync(x =>
        //            x.TeamId == long.Parse(ClaimData.TeamId) && x.IsTeamManager == false && x.IsTeamManager != null)).ToList();
        //    }
        //    else
        //    {
        //        // get unit teams and exclude all employees in these teams from response
        //        var unitTeams = await _teamUnitOfWork.Repository.FindAsync(x => x.UnitId == unitId);
        //        var teamIds = unitTeams.Select(x => x.Id).ToList();
        //        var teamEmployees = await UnitOfWork.Repository.FindAsync(x => teamIds.Contains(x.TeamId.Value));
        //        var excludedEmployeeIds = teamEmployees.Select(x => x.Id).ToList();
        //        entities = (await UnitOfWork.Repository.FindAsync(x => x.UnitId == unitId && !excludedEmployeeIds.Contains(x.Id))).ToList();
        //        var hos = await GetHeadOfSections(unit);
        //        entities.AddRange(hos);
        //    }
        //    var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Employee>, IEnumerable<EmployeeDto>>(entities);
        //    return new ResponseResult(data, HttpStatusCode.OK, null, "Data Retrieved Successfully");
        //}
        /// <summary>
        /// Get Team Manager Phone
        /// </summary>
        /// <param name="teamId"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetTeamManagerPhone(long teamId)
        {
            var workShopManager =
                await UnitOfWork.Repository.FirstOrDefaultAsync(t => t.TeamId == teamId && t.IsTeamManager == true);

            var data = Mapper.Map<Entities.Entities.Hr.Employee, EmployeeDto>(workShopManager);
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

        public async Task<IFinalResult> GetEmployeePhoneByIdAsync(long employeeId)
        {
            var employee = await UnitOfWork.GetRepository<Entities.Entities.Hr.Employee>().FirstOrDefaultAsync(x => x.Id == employeeId);
            return ResponseResult.PostResult(employee.PhoneNumber, HttpStatusCode.OK);
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
                x.UserTypeId
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
        public async Task<IFinalResult> GetEmployeeIdsByUnitIdAsync(long unitId, bool option)
        {
            if (option == false)
            {
                var managementUnitEmployees = await UnitOfWork.Repository.FindAsync(x => x.UnitId == unitId);

                var managementIds = managementUnitEmployees.Select(x => x.Id).ToList();
                return ResponseResult.PostResult(managementIds, status: HttpStatusCode.OK,
                    message: HttpStatusCode.OK.ToString());
            }

            var ids = new List<long> { unitId };

            var unit = await UnitOfWork.GetRepository<Entities.Entities.Hr.Unit>().FirstOrDefaultAsync(x => x.Id == unitId,
                include: src => src.Include(sb => sb.SubUnits));
            await GetChildren(unit, ids);
            var employees = await UnitOfWork.Repository.FindAsync(x => ids.Contains(x.UnitId.Value));

            var employeeIds = employees.Select(x => x.Id).ToList();
            return ResponseResult.PostResult(employeeIds, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }

        public async Task<IFinalResult> GetEmployeeByIdAsync(long employeeId)
        {
            var entity = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == employeeId,
                include: src => src.Include(u => u.Unit).ThenInclude(p => p.Parent).ThenInclude(p => p.Parent).ThenInclude(p => p.Parent));
            var data = Mapper.Map<EmployeeDto>(entity);
            return ResponseResult.PostResult(data, HttpStatusCode.OK);
        }
        #endregion

        #region Private Methods
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
        async Task GetChildren(Entities.Entities.Hr.Unit current, List<long> ids)
        {
            if (current == null)
            {
                return;
            }

            current = await UnitOfWork.GetRepository<Entities.Entities.Hr.Unit>().FirstOrDefaultAsync(x => x.Id == current.Id,
                include: src => src.Include(sb => sb.SubUnits));
            ids.AddRange(current.SubUnits.Select(x => x.Id));
            foreach (var child in current.SubUnits)
            {
                await GetChildren(child, ids);
            }

        }
        #endregion

    }
}
