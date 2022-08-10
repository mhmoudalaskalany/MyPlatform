using System;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Hr.Employee;
using Service.Services.Base;
using Service.Services.Validators.Employee;

namespace Service.Services.Validators.Services.Employee
{
    public class EmployeeValidationService : BaseService<Entities.Entities.Hr.Employee , AddEmployeeDto , EmployeeDto , Guid?> , IEmployeeValidationService
    {
        private readonly IEmployeeValidator _employeeValidator;
        public EmployeeValidationService(IServiceBaseParameter<Entities.Entities.Hr.Employee> parameters, IEmployeeValidator employeeValidator) : base(parameters)
        {
            _employeeValidator = employeeValidator;
        }

        #region Public Methods

        public async Task<IFinalResult> CheckNationalIdAsync(string nationalId, Guid employeeId)
        {
            var result = await _employeeValidator.CheckNationalIdAsync(nationalId, employeeId);
            return ResponseResult.PostResult(result , HttpStatusCode.OK);
        }

        public async Task<IFinalResult> CheckEmailAsync(string email, Guid employeeId)
        {
            var result = await _employeeValidator.CheckEmailAsync(email, employeeId);
            return ResponseResult.PostResult(result , HttpStatusCode.OK);
        }

        public async Task<IFinalResult> CheckFileNumberAsync(string fileNumber, Guid employeeId)
        {
            var result = await _employeeValidator.CheckFileNumberAsync(fileNumber, employeeId);
            return ResponseResult.PostResult(result, HttpStatusCode.OK);
        }

        #endregion



    }
}
