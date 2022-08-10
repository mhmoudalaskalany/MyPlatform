using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.FullEmployee;
using Domain.DTO.Hr.FullEmployee.Parameters;
using Entities.Enum;
using Service.Services.Base;

namespace Service.Services.Hr.FullEmployee
{
    public interface IFullEmployeeService : IBaseService<Entities.Entities.Hr.FullEmployee, AddFullEmployeeDto, FullEmployeeDto , Guid?>
    {
        
        /// <summary>
        /// Delete Certificate
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IFinalResult> DeleteCertificate(Guid id);
        /// <summary>
        /// Delete By Attachment Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<IFinalResult> DeleteCertificateByAttachmentIdAsync(Guid id);
        /// <summary>
        /// Get Employee Details By File Id
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        Task<IFinalResult> GetEmployeeDetailsByFileIdAsync(Guid fileId);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IFinalResult> AddException(AddFullEmployeeDto model);
        /// <summary>
        /// Get All Paged
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetAllPagedAsync(BaseParam<SearchCriteriaFilter> filter);

        /// <summary>
        /// Get Drop Down
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<DataPaging> GetDropDownAsync(BaseParam<SearchCriteriaFilter> filter);
        /// <summary>
        /// Get Status Count
        /// </summary>
        /// <returns></returns>
        Task<IFinalResult> GetEmployeesStatusCountAsync();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        Task<IFinalResult> GetEmployeeVaccinationReportAsync(EmployeeVaccinationReportFilter parameters);

        /// <summary>
        /// Confirm Phone
        /// </summary>
        /// <param name="nationalId"></param>
        /// <param name="phone"></param>
        /// <param name="doesStatus"></param>
        /// <returns></returns>
        Task<IFinalResult> ConfirmPhoneNumber(string nationalId, string phone , DoseStatus doesStatus);

        /// <summary>
        /// Confirm Phone
        /// </summary>
        /// <param name="otp"></param>
        /// <param name="phone"></param>
        /// <param name="nationalId"></param>
        /// <param name="doesStatus"></param>
        /// <returns></returns>
        Task<IFinalResult> ConfirmOtp(string otp, string phone , string nationalId , DoseStatus doesStatus);
    }
}
