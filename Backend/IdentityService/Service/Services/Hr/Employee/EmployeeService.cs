using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Abstraction.Repository.Employee;
using Domain.Abstraction.UnitOfWork;
using Domain.Caching.Redis;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Employee;
using Domain.DTO.Hr.Employee.Parameters;
using Domain.DTO.Hr.FullEmployee;
using Domain.Mq.Events;
using Domain.Mq.Publishers;
using Entities.Enum;
using Integration.FileRepository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using Service.Services.Base;

namespace Service.Services.Hr.Employee
{
    public class EmployeeService : BaseService<Entities.Entities.Hr.Employee, AddEmployeeDto, EmployeeDto, long?>, IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IFileRepository _fileRepository;

        private readonly IUnitOfWork<Entities.Entities.Hr.Unit> _unitOfWork;

        public EmployeeService(IServiceBaseParameter<Entities.Entities.Hr.Employee> parameters, IEmployeeRepository employeeRepository, IUnitOfWork<Entities.Entities.Hr.Unit> unitOfWork, IFileRepository fileRepository) : base(parameters)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _fileRepository = fileRepository;
        }

        #region Public Methods
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<IResult> GetEmployeeCountAsync()
        {
            var employees = await UnitOfWork.Repository.Count();
            return ResponseResult.PostResult(employees, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());
        }
        /// <summary>
        /// Check National Id
        /// </summary>
        /// <param name="nationalId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<IResult> CheckNationalIdAsync(string nationalId, long employeeId)
        {

            if (employeeId != 0)
            {
                Result = await CheckForNationalIdAtEditMode(nationalId, employeeId);
            }
            else
            {
                Result = await CheckForNationalIdAtAddMode(nationalId);

            }
            return Result;


        }
        /// <summary>
        /// Check Email
        /// </summary>
        /// <param name="email"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<IResult> CheckEmailAsync(string email, long employeeId)
        {

            if (employeeId != 0)
            {
                Result = await CheckForEmailAtEditMode(email, employeeId);
            }
            else
            {
                Result = await CheckForEmailAtAddMode(email);

            }


            return Result;

        }
        /// <summary>
        /// Get Unit Manager
        /// </summary>
        /// <param name="unitId"></param>
        /// <param name="unitType"></param>
        /// <returns></returns>
        public async Task<IResult> GetUnitManagerAsync(long unitId, UnitType? unitType)
        {
            Entities.Entities.Hr.Employee employee;
            // if user have a team id get the employee according to team if
            if (unitType == UnitType.Team)
            {
                employee =
                    await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.TeamId == unitId && x.IsTeamManager == true);
            }
            else
            {
                employee =
                    await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.UnitId == unitId && x.IsManager == true);
            }

            var data = Mapper.Map<Entities.Entities.Hr.Employee, EmployeeDto>(employee);
            return new ResponseResult(data, HttpStatusCode.OK, null, "Data Retrieved Successfully");
        }
        /// <summary>
        /// Get By Id For View
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IResult> GetByIdForViewAsync(long id)
        {
            var entity =
                await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == id, include: src => src.Include(s => s.Unit).ThenInclude(p => p.Parent).ThenInclude(p => p.Parent).ThenInclude(p => p.Parent));
            var data = Mapper.Map<Entities.Entities.Hr.Employee, EmployeeDto>(entity);
            return new ResponseResult(data, HttpStatusCode.OK, null, "Data Retrieved Successfully");
        }
        /// <summary>
        /// Get Employee By National Id
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        public async Task<IResult> GetEmployeeInfoAsync(string nationalId)
        {

            var data = await _employeeRepository.GetEmployeeInfoAsync(nationalId);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());


        }
        /// <summary>
        /// Get Employee Info New View
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        public async Task<IResult> GetEmployeeInfoNewViewAsync(string nationalId)
        {

            var data = await _employeeRepository.GetEmployeeInfoFromNewViewAsync(nationalId);
            return ResponseResult.PostResult(data, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());


        }
        /// <summary>
        /// Get Employee Ids In Specific Unit
        /// </summary>
        /// <param name="unitId"></param>
        /// <returns></returns>
        public async Task<IResult> GetEmployeeIdsByUnitIdAsync(long unitId)
        {

            var ids = new List<long> { unitId };

            var unit = await _unitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == unitId,
                include: src => src.Include(sb => sb.SubUnits));
            await GetChildren(unit, ids);
            var employees = await UnitOfWork.Repository.FindAsync(x => ids.Contains(x.UnitId.Value));

            var employeeIds = employees.Select(x => x.Id).ToList();
            return ResponseResult.PostResult(employeeIds, status: HttpStatusCode.OK,
                message: HttpStatusCode.OK.ToString());

        }
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<DataPaging> GetAllPagedAsync(BaseParam<EmployeeFilter> filter)
        {

            int limit = filter.PageSize;
            int offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedWithOrderAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.Employee>, IEnumerable<EmployeeDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));

        }
        /// <summary>
        /// Get Drop Down
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter)
        {

            int limit = filter.PageSize;
            int offset = ((--filter.PageNumber) * filter.PageSize);
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

        public override async Task<IResult> UpdateAsync(AddEmployeeDto model)
        {

            var entityToUpdate = await UnitOfWork.Repository.GetAsync(model.Id);
            var oldUnitId = entityToUpdate.UnitId;
            var newEntity = Mapper.Map(model, entityToUpdate);
            newEntity.ModifiedById = long.Parse(ClaimData.UserId);
            newEntity.ModifiedDate = DateTime.Now;
            UnitOfWork.Repository.Update(entityToUpdate, newEntity);
            int affectedRows = await UnitOfWork.SaveChanges();
            if (affectedRows > 0)
            {
                if (oldUnitId != newEntity.UnitId)
                {
                    await SendEmployeeChangedEvent(newEntity);
                }

                RedisCacheHelper.Delete(newEntity.NationalId);
                Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                    message: "Data Updated Successfully");
            }

            return Result;
        }

        /// <summary>
        /// Update Employee Unit  ( Used In Stock Management )
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResult> UpdateEmployeeUnit(EmployeeUnitDto dto)
        {
            var entity = await UnitOfWork.Repository.GetAsync(dto.EmployeeNumber);
            entity.UnitId = dto.UnitId;
            entity.IsManager = dto.IsManager;
            entity.Retired = dto.Retired;
            entity.IsUpdated = true;
            await UnitOfWork.Repository.UpdateAsync(entity.Id, entity);
            var affectedRows = await UnitOfWork.SaveChanges();
            if (affectedRows >= 0)
            {
                Result = new ResponseResult(result: true, status: HttpStatusCode.OK,
                    message: "Updated");
            }
            else
            {
                Result = new ResponseResult(result: false, status: HttpStatusCode.BadRequest,
                    message: "ErrorUpdating");
            }
            return Result;

        }
        /// <summary>
        /// Update Employee Image For Card (Update FullEmployee Entity For Now)
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<IResult> UpdateEmployeeImageAsync(UpdateEmployeeImageDto dto)
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

        #endregion



        #region Private Methods
        /// <summary>
        /// Predicate For Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Hr.Employee, bool>> PredicateBuilderFunction(EmployeeFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.Employee>(true);

            if (filter.Id != 0 && filter.Id != null)
            {
                predicate = predicate.And(b => b.Id.ToString().Contains(filter.Id.ToString()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FullnameAr))
            {
                predicate = predicate.And(b => b.FullNameAr.ToLower().Contains(filter.FullnameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.FullnameEn))
            {
                predicate = predicate.And(b => b.FullNameEn.ToLower().Contains(filter.FullnameEn.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.NationalId))
            {
                predicate = predicate.And(b => b.NationalId.ToLower().Contains(filter.NationalId.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter.PhoneNumber))
            {
                predicate = predicate.And(b => b.PhoneNumber.Contains(filter.PhoneNumber));
            }
            return predicate;
        }
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
                predicate = predicate.Or(b => b.FullNameAr.ToLower().Contains(filter.SearchCriteria.ToLower()));
                predicate = predicate.Or(b => b.Id.ToString().Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.NationalId.Contains(filter.SearchCriteria));


            }
            return predicate;
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

            current = await _unitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == current.Id,
                include: src => src.Include(sb => sb.SubUnits));
            ids.AddRange(current.SubUnits.Select(x => x.Id));
            foreach (var child in current.SubUnits)
            {
                await GetChildren(child, ids);
            }

        }
        /// <summary>
        /// Check Existing National Id For Add Mode
        /// </summary>
        /// <param name="nationalId"></param>
        /// <returns></returns>
        async Task<IResult> CheckForNationalIdAtAddMode(string nationalId)
        {
            var data = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.NationalId == nationalId);
            if (data != null)
            {
                Result = ResponseResult.PostResult(true, status: HttpStatusCode.OK);
            }
            else
            {
                Result = ResponseResult.PostResult(false, status: HttpStatusCode.OK);
            }

            return Result;
        }/// <summary>
         /// Check Existing Email For Add Mode
         /// </summary>
         /// <param name="email"></param>
         /// <returns></returns>
        async Task<IResult> CheckForEmailAtAddMode(string email)
        {
            var data = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Email == email);
            if (data != null)
            {
                Result = ResponseResult.PostResult(true, status: HttpStatusCode.OK);
            }
            else
            {
                Result = ResponseResult.PostResult(false, status: HttpStatusCode.OK);
            }

            return Result;
        }
        /// <summary>
        /// Check Existing National Id For Edit Mode
        /// </summary>
        /// <param name="nationalId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        async Task<IResult> CheckForNationalIdAtEditMode(string nationalId, long employeeId)
        {
            var user = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == employeeId);
            var data = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.NationalId == nationalId);
            if (data != null)
            {
                // mean the same national id for the same employee so return false as the name is available for him
                if (data.Id == user.Id)
                {
                    Result = ResponseResult.PostResult(false, status: HttpStatusCode.OK,
                        message: HttpStatusCode.OK.ToString());
                }
                else
                {
                    // means this national already taken by another employee so return true as its not available for him
                    Result = ResponseResult.PostResult(true, status: HttpStatusCode.OK);
                }

            }
            else
            {
                Result = ResponseResult.PostResult(false, status: HttpStatusCode.OK);
            }

            return Result;
        }
        /// <summary>
        /// Check Existing Email For Edit Mode
        /// </summary>
        /// <param name="email"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        async Task<IResult> CheckForEmailAtEditMode(string email, long employeeId)
        {
            var user = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == employeeId);
            var data = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Email == email);
            if (data != null)
            {
                // mean the same user name for the same user so return false as the name is available for him
                if (data.Id == user.Id)
                {
                    Result = ResponseResult.PostResult(false, status: HttpStatusCode.OK);
                }
                else
                {
                    // means this username already taken by another user so return true as its not available for him
                    Result = ResponseResult.PostResult(true, status: HttpStatusCode.OK);
                }

            }
            else
            {
                Result = ResponseResult.PostResult(false, status: HttpStatusCode.OK);
            }

            return Result;
        }
        /// <summary>
        /// Send Event To Stock Queue When Employee Department Changes
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        async Task SendEmployeeChangedEvent(Entities.Entities.Hr.Employee entity)
        {
            var unit = await _unitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == entity.UnitId, include: src => src.Include(p => p.Parent));
            var employeeEvent = new EmployeeChangedEvent
            {
                EmployeeName = entity.FullNameAr,
                Id = entity.Id,
                UnitName = unit.NameAr + " " + unit.Parent?.NameAr
            };

            Publisher<EmployeeChangedEvent>.Publish(employeeEvent, QueuesNames.Employees);
        }
        #endregion











    }
}
