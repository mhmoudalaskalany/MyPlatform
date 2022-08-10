using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Employee;
using Domain.DTO.Hr.Team;
using Domain.DTO.Hr.Team.Parameters;
using Entities.Entities.Hr;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Service.Services.Base;

namespace Service.Services.Hr.Team
{
    public class TeamService : BaseService<Entities.Entities.Hr.Team, AddTeamDto, TeamDto, Guid?>, ITeamService
    {


        public TeamService(IServiceBaseParameter<Entities.Entities.Hr.Team> parameters) : base(parameters)
        {
        }


        public async Task<IFinalResult> GetByIdAsync(Guid id)
        {
            var team = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == id && x.IsDeleted == false,
                include: src => src.Include(x => x.Unit));
            var data = Mapper.Map<Entities.Entities.Hr.Team, TeamDto>(team);
            return Result = ResponseResult.PostResult(data, HttpStatusCode.OK);
        }

        #region Public Methods
        public async Task<IFinalResult> GetTeamsByUnitIdAsync(string unitId)
        {
            var entities = await UnitOfWork.Repository.FindAsync(x => x.UnitId == Guid.Parse(unitId));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Team>, IEnumerable<TeamDto>>(entities);
            return Result = ResponseResult.PostResult(data, HttpStatusCode.OK);
        }

        public async Task<IFinalResult> GetEmployeesByTeamIdAsync(Guid teamId)
        {

            var employeeTeam = await UnitOfWork.GetRepository<EmployeeTeam>().FindAsync(x => x.TeamId == teamId);
            var employeeIds = employeeTeam.Select(x => x.EmployeeId).ToList();
            var employees = await UnitOfWork.GetRepository<Entities.Entities.Hr.Employee>()
                .FindAsync(x => employeeIds.Contains(x.Id));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Employee>, IEnumerable<EmployeeDto>>(employees);
            return Result = ResponseResult.PostResult(data, HttpStatusCode.OK);
        }

        public async Task<IFinalResult> DeleteEmployeeTeamAsync(Guid employeeId, Guid teamId)
        {

            var employeeTeam = await UnitOfWork.GetRepository<EmployeeTeam>().FirstOrDefaultAsync(x => x.EmployeeId == employeeId && x.TeamId == teamId);
            UnitOfWork.GetRepository<EmployeeTeam>().Remove(employeeTeam);
            await UnitOfWork.SaveChanges();

            return Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.OK,
                  message: "DeleteSuccess");



        }


        public async Task<IFinalResult> AddEmployeeTeamAsync(TeamEmployeeDto dto)
        {
            var employeeTeam = new EmployeeTeam
            {
                EmployeeId = dto.EmployeeId,
                TeamId = dto.TeamId,
            };
            UnitOfWork.GetRepository<EmployeeTeam>().Add(employeeTeam);
            await UnitOfWork.SaveChanges();
            return Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.OK,
                  message: "AddSuccess");



        }

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<TeamFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue, include: src => src.Include(t => t.Unit));
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Team>, IEnumerable<TeamDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }

        #endregion

        #region Private Methods

        static Expression<Func<Entities.Entities.Hr.Team, bool>> PredicateBuilderFunction(TeamFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Team>(true);

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

        #endregion






    }

}
