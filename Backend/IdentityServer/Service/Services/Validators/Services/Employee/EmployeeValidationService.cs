using System;
using System.Net;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Hr.FullEmployee;
using Entities.Entities.Hr;
using Service.Services.Base;
using Service.Services.Validators.Employee;

namespace Service.Services.Validators.Services.Employee
{
    public class EmployeeValidationService : BaseService<FullEmployee , AddMurasalatEmployeeDto , MurasalatEmployeeDto , Guid?> , IEmployeeValidationService
    {
        private readonly IEmployeeValidator _employeeValidator;
        public EmployeeValidationService(IServiceBaseParameter<FullEmployee> parameters, IEmployeeValidator employeeValidator) : base(parameters)
        {
            _employeeValidator = employeeValidator;
        }

        #region Public Methods

        public async Task<IResult> CheckNationalIdAsync(string nationalId, Guid employeeId)
        {
            var result = await _employeeValidator.CheckNationalIdAsync(nationalId, employeeId);
            return ResponseResult.PostResult(result , HttpStatusCode.OK);
        }

        public async Task<IResult> CheckEmailAsync(string email, Guid employeeId)
        {
            var result = await _employeeValidator.CheckEmailAsync(email, employeeId);
            return ResponseResult.PostResult(result , HttpStatusCode.OK);
        }

        public async Task<IResult> CheckFileNumberAsync(string fileNumber, Guid employeeId)
        {
            var result = await _employeeValidator.CheckFileNumberAsync(fileNumber, employeeId);
            return ResponseResult.PostResult(result, HttpStatusCode.OK);
        }

        #endregion



    }
}
