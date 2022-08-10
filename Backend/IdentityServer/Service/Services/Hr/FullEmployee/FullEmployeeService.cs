using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Common.Sms;
using Domain.DTO.Hr.FullEmployee;
using Domain.DTO.Hr.FullEmployee.Parameters;
using Domain.Extensions;
using Domain.Helper.HttpClient;
using Domain.Helper.SmsHelper;
using Entities.Entities.Hr;
using Entities.Enum;
using Integration.FileRepository;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using MoreLinq;
using Service.Services.Base;

namespace Service.Services.Hr.FullEmployee
{
    public class FullEmployeeService : BaseService<Entities.Entities.Hr.FullEmployee, AddFullEmployeeDto, FullEmployeeDto, Guid?>, IFullEmployeeService
    {
        private readonly ISmsService _smsService;
        private readonly IFileRepository _fileRepository;
        public static IDictionary<string, string> OtpDictionary = new Dictionary<string, string>();
        private readonly MicroServicesUrls _urls;
        public FullEmployeeService(IServiceBaseParameter<Entities.Entities.Hr.FullEmployee> parameters, ISmsService smsService, IFileRepository fileRepository, MicroServicesUrls urls) : base(parameters)
        {
            _smsService = smsService;
            _fileRepository = fileRepository;
            _urls = urls;
        }

        #region Public Method

        /// <summary>
        /// Get Counts For Cards
        /// </summary>
        /// <returns></returns>
        public async Task<IFinalResult> GetEmployeesStatusCountAsync()
        {

            var entities = (await UnitOfWork.GetRepository<Entities.Entities.Hr.FullEmployee>().FindAsync(x => x.IsVaccinated)).ToList();
            var vaccinatedIds = entities.Select(x => x.Id);

            var nonVaccinated =
                await UnitOfWork.Repository.Count(x => !vaccinatedIds.Contains(x.Id));
            var grouped = entities.GroupBy(x => x.DoseStatus).ToList();
            var firstDose = grouped.FirstOrDefault(x => x.Key == 1);
            var secondDose = grouped.FirstOrDefault(x => x.Key == 2);
            var dto = new EmployeeCountDto
            {
                FirstDoseVaccinated = firstDose?.Count() ?? 0,
                SecondDoseVaccinated = secondDose?.Count() ?? 0,
                Vaccinated = entities.Count,
                NonVaccinated = nonVaccinated
            };
            return ResponseResult.PostResult(dto, HttpStatusCode.OK);
        }
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>

        public async Task<DataPaging> GetAllPagedAsync(BaseParam<SearchCriteriaFilter> filter)
        {

            var limit = filter.PageSize;
            var offset = ((--filter.PageNumber) * filter.PageSize);
            var query = await UnitOfWork.Repository.FindPagedAsync(predicate: PredicateBuilderFunction(filter.Filter), skip: offset, take: limit, filter.OrderByValue);
            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.FullEmployee>, IEnumerable<MurasalatEmployeeDto>>(query.Item2);
            return new DataPaging(++filter.PageNumber, filter.PageSize, query.Item1, ResponseResult.PostResult(data, status: HttpStatusCode.OK, message: HttpStatusCode.OK.ToString()));
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetEmployeeVaccinationReportAsync(EmployeeVaccinationReportFilter parameters)
        {

            var predicate = await PredicateBuilderFunctionForVaccinationReport(parameters);

            var entities = await UnitOfWork.Repository.FindAsync(predicate, orderByCriteria: Utilities.GetOrderByList("DoseStatus", "Asc"), include: src => src.Include(a => a.Attachment));

            var data = Mapper.Map<IEnumerable<Entities.Entities.Hr.FullEmployee>, IEnumerable<MurasalatEmployeeDto>>(entities);

            return ResponseResult.PostResult(data, HttpStatusCode.OK, null, "Data Retrieved Successfully");


        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IFinalResult> AddException(AddFullEmployeeDto model)
        {
            var entityToUpdate = await UnitOfWork.Repository.GetAsync(model.Id);

            entityToUpdate.Notes = model.Notes;
            entityToUpdate.DoseStatus = 4;
            entityToUpdate.IsVaccinated = false;

            await UnitOfWork.Repository.UpdateAsync(entityToUpdate.Id, entityToUpdate);
            await UnitOfWork.SaveChanges();

            Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                message: "UpdateSuccess");

            return Result;
        }
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public override async Task<IFinalResult> AddAsync(AddFullEmployeeDto model)
        {
            var fullEmployee = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == model.Id, include: src => src.Include(a => a.Attachment), disableTracking: false);
            if (fullEmployee.DoseStatus == (int)model.DoseStatus)
            {
                return ResponseResult.PostResult(false, HttpStatusCode.BadRequest, null, "HasUploadDoseCertificate");
            }

            if (fullEmployee.DoseStatus == (int)DoseStatus.SecondDose && model.DoseStatus == DoseStatus.FirstDose)
            {
                return ResponseResult.PostResult(false, HttpStatusCode.BadRequest, null, "CannotUploadFirstDoseAfterSecond");
            }

            if (fullEmployee.AttachmentId != null)
            {
                await DeletePhysicalFile(fullEmployee.Attachment.FileId);
                UnitOfWork.GetRepository<Attachment>().Remove(fullEmployee.Attachment);
                await UnitOfWork.SaveChanges();
            }
            fullEmployee.Phone = model.PhoneNumber;
            fullEmployee.IsVaccinated = true;
            fullEmployee.DoseStatus = (int)model.DoseStatus;
            fullEmployee.Attachment = Mapper.Map<Attachment>(model.Attachments.LastOrDefault());
            fullEmployee.Attachment.FullEmployeeId = fullEmployee.Id;
            await UnitOfWork.Repository.UpdateAsync(fullEmployee.Id, fullEmployee);
            var affectedRows = await UnitOfWork.SaveChanges();
            if (affectedRows > 0)
            {
                return ResponseResult.PostResult(true, HttpStatusCode.Created, null, "AddSuccess");
            }

            return ResponseResult.PostResult(false, HttpStatusCode.BadRequest, null, "AddError");

        }
        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IFinalResult> DeleteCertificate(Guid id)
        {
            var entityToUpdate = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == id
                , include: src => src.Include(a => a.Attachment), disableTracking: false);
            entityToUpdate.DoseStatus = 5;
            entityToUpdate.IsVaccinated = false;
            var fileId = entityToUpdate.Attachment.FileId;
            await UnitOfWork.Repository.UpdateAsync(entityToUpdate.Id, entityToUpdate);
            UnitOfWork.GetRepository<Attachment>().Remove(entityToUpdate.Attachment);
            var affectedRows = await UnitOfWork.SaveChanges();

            if (affectedRows > 0)
            {

                await DeletePhysicalFile(fileId);
            }


            SendAlert(entityToUpdate.Phone, "تم الغاء تسجيلك بنظام شهادات التطعيم لعدم صحة الشهادة قم بمراجعة دائرة الموارد البشرية أو التسجيل مرة أخرى");
            Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                message: "UpdateSuccess");

            return Result;
        }

        /// <summary>
        /// Delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IFinalResult> DeleteCertificateByAttachmentIdAsync(Guid id)
        {
            var entityToUpdate = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.Attachment.FileId == id
                , include: src => src.Include(a => a.Attachment), disableTracking: false);

            entityToUpdate.DoseStatus = 5;
            entityToUpdate.IsVaccinated = false;
            var fileId = entityToUpdate.Attachment.FileId;
            await UnitOfWork.Repository.UpdateAsync(entityToUpdate.Id, entityToUpdate);
            UnitOfWork.GetRepository<Attachment>().Remove(entityToUpdate.Attachment);
            var affectedRows = await UnitOfWork.SaveChanges();

            if (affectedRows > 0)
            {

                await DeletePhysicalFile(fileId);
            }


            SendAlert(entityToUpdate.Phone, "تم الغاء تسجيلك بنظام شهادات التطعيم لعدم صحة الشهادة قم بمراجعة دائرة الموارد البشرية أو التسجيل مرة أخرى");
            Result = ResponseResult.PostResult(result: true, status: HttpStatusCode.Accepted,
                message: "UpdateSuccess");

            return Result;
        }

        /// <summary>
        /// Confirm Phone Number
        /// </summary>
        /// <param name="nationalId"></param>
        /// <param name="phone"></param>
        /// <param name="doesStatus"></param>
        /// <returns></returns>
        public async Task<IFinalResult> ConfirmPhoneNumber(string nationalId, string phone, DoseStatus doesStatus)
        {
            var entity = await UnitOfWork.Repository.FirstOrDefaultAsync(x => x.CivilNumber == nationalId);
            if (entity == null)
            {
                return ResponseResult.PostResult(false, HttpStatusCode.BadRequest, null, "NationalIdNotExist");
            }
            if (entity is { IsVaccinated: true } && entity.DoseStatus == (int)doesStatus)
            {
                return ResponseResult.PostResult(false, HttpStatusCode.BadRequest, null, "HasUploadDoseCertificate");
            }

            if (entity.DoseStatus == (int)DoseStatus.SecondDose && doesStatus == DoseStatus.FirstDose)
            {
                return ResponseResult.PostResult(false, HttpStatusCode.BadRequest, null, "CannotUploadFirstDoseAfterSecond");
            }



            var message = Configuration["SmsMessages:ConfirmPhone"];
            SendOtp(phone, message);
            return ResponseResult.PostResult(false, HttpStatusCode.OK, null, "OtpSent");

        }

        /// <summary>
        /// Confirm Otp And Return Data
        /// </summary>
        /// <param name="otp"></param>
        /// <param name="phone"></param>
        /// <param name="nationalId"></param>
        /// <param name="doesStatus"></param>
        /// <returns></returns>
        public async Task<IFinalResult> ConfirmOtp(string otp, string phone, string nationalId, DoseStatus doesStatus)
        {
            var otpRecord = OtpDictionary.FirstOrDefault(x => x.Key == phone);
            if (otpRecord.Value == null || otpRecord.Value != otp)
            {
                return ResponseResult.PostResult(false, HttpStatusCode.BadRequest, null, "InvalidOtp");
            }

            OtpDictionary.Remove(phone);
            return await GetByNationalIdAsync(nationalId, phone, doesStatus);
        }
        /// <summary>
        /// Get Employee Details By File Id
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        public async Task<IFinalResult> GetEmployeeDetailsByFileIdAsync(Guid fileId)
        {
            var entity = await UnitOfWork.Repository.FirstOrDefaultAsync(
                x => x.Attachment.FileId == fileId, include:
                src => src.Include(f => f.Attachment));
            var data = Mapper.Map<MurasalatEmployeeDto>(entity);
            return ResponseResult.PostResult(data, HttpStatusCode.OK);
        }

        #endregion

        #region Private Methods


        /// <summary>
        /// Predicate Builder For Employee Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Hr.FullEmployee, bool>> PredicateBuilderFunction(SearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.FullEmployee>(true);
            if (!string.IsNullOrWhiteSpace(filter.SearchCriteria))
            {
                predicate = predicate.Or(b => b.ArFullName.ToLower().Contains(filter.SearchCriteria.ToLower()));
                predicate = predicate.Or(b => b.Id.ToString().Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.CivilNumber.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.Phone.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.FileNumber.Contains(filter.SearchCriteria));
            }
            return predicate;
        }
        /// <summary>
        /// Predicate Builder For Employee Drop Down
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        static Expression<Func<Entities.Entities.Hr.FullEmployee, bool>> DropDownPredicateBuilderFunction(SearchCriteriaFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.FullEmployee>(true);
            if (!string.IsNullOrWhiteSpace(filter.SearchCriteria))
            {
                predicate = predicate.Or(b => b.ArFullName.ToLower().Contains(filter.SearchCriteria.ToLower()));
                predicate = predicate.Or(b => b.Id.ToString().Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.CivilNumber.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.Phone.Contains(filter.SearchCriteria));
                predicate = predicate.Or(b => b.FileNumber.ToString().Contains(filter.SearchCriteria));
            }
            return predicate;
        }
        /// <summary>
        /// Predicate Builder For Vaccination Report Page
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        async Task<Expression<Func<Entities.Entities.Hr.FullEmployee, bool>>> PredicateBuilderFunctionForVaccinationReport(EmployeeVaccinationReportFilter filter)
        {
            var predicate = PredicateBuilder.New<Entities.Entities.Hr.FullEmployee>(true);

            if (!string.IsNullOrWhiteSpace(filter?.NameAr))
            {
                predicate = predicate.And(b => b.ArFullName.ToLower().Contains(filter.NameAr.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter?.NameEn))
            {
                predicate = predicate.And(b => b.EnFullName.ToLower().Contains(filter.NameEn.ToLower()));
            }
            if (!string.IsNullOrWhiteSpace(filter?.NationalId))
            {
                predicate = predicate.And(b => b.CivilNumber.ToLower().Contains(filter.NationalId.ToLower()));
            }
            if (!string.IsNullOrEmpty(filter?.UnitId))
            {
                var localDatabase = (await UnitOfWork.GetRepository<Entities.Entities.Hr.FullUnit>().GetAllAsync()).ToList();

                var unit = localDatabase.First(x => x.Id.ToString() == filter.UnitId);
                var ids = new List<string> { filter.UnitId };
                await GetChildren(unit, ids, localDatabase);
                predicate = predicate.And(b => ids.Contains(b.DepartmentCode));
            }

            if (filter?.DoesStatus.Count > 0 && !filter.DoesStatus.Contains("-1"))
            {
                var listOfStatus = new List<int>();
                filter.DoesStatus.ForEach(e =>
                {
                    listOfStatus.Add(Convert.ToInt32(e));
                });
                predicate = predicate.And(b => listOfStatus.Contains((int)b.DoseStatus));
            }

            return predicate;
        }
        /// <summary>
        /// Get Unit Children
        /// </summary>
        /// <param name="current"></param>
        /// <param name="ids"></param>
        /// <param name="localDatabase"></param>
        /// <returns></returns>
        async Task GetChildren(Entities.Entities.Hr.FullUnit current, List<string> ids, List<Entities.Entities.Hr.FullUnit> localDatabase)
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
        /// Delete Attachment File From File Server
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        private async Task DeletePhysicalFile(Guid fileId)
        {
            await _fileRepository.DeleteFile(fileId);
        }

        /// <summary>
        /// Get By National Id When Search
        /// </summary>
        /// <param name="nationalId"></param>
        /// <param name="phone"></param>
        /// <param name="doesStatus"></param>
        /// <returns></returns>
        private async Task<IFinalResult> GetByNationalIdAsync(string nationalId, string phone, DoseStatus doesStatus)
        {
            var entity = await UnitOfWork.GetRepository<Entities.Entities.Hr.FullEmployee>().FirstOrDefaultAsync(x => x.CivilNumber == nationalId);
            if (entity is { IsVaccinated: true } && entity.DoseStatus == (int)doesStatus)
            {
                return ResponseResult.PostResult(false, HttpStatusCode.BadRequest, null, "HasUploadDoseCertificate");
            }

            if (entity.DoseStatus == (int)DoseStatus.SecondDose && doesStatus == DoseStatus.FirstDose)
            {
                return ResponseResult.PostResult(false, HttpStatusCode.BadRequest, null, "CannotUploadFirstDoseAfterSecond");
            }

            var data = Mapper.Map<MurasalatEmployeeDto>(entity);
            data.PhoneNumber = phone;
            return ResponseResult.PostResult(data, HttpStatusCode.OK, null, "PhoneConfirmed");
        }
        /// <summary>
        /// Send OTP
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async void SendOtp(string phone, string message)
        {

            var otp = GenerateOtp();
            var existOtp = OtpDictionary.FirstOrDefault(x => x.Key == phone);
            // remove otp for same user if request more than one time to avoid exception
            if (existOtp.Value != null)
            {
                OtpDictionary.Remove(phone);
            }

            OtpDictionary.Add(phone, otp);
            var smsDto = new SmsDto
            {
                Phone = phone,
                Message = $"{message} {otp}"
            };
            await _smsService.SendBulkSms(smsDto);

        }
        /// <summary>
        /// Send OTP
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        private async void SendAlert(string phone, string message)
        {


            var smsDto = new SmsDto
            {
                Phone = phone,
                Message = $"{message}"
            };
            await _smsService.SendBulkSms(smsDto);

        }
        /// <summary>
        /// Generate OTP
        /// </summary>
        /// <returns></returns>
        private string GenerateOtp()
        {
            var chars = "1234567890";
            var stringChars = new char[4];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var otp = new String(stringChars);
            return otp;
        }
        #endregion




    }
}
