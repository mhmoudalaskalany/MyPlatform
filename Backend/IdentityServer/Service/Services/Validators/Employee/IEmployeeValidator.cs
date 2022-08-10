using System;
using System.Threading.Tasks;
using Service.Services.Validators.Base;

namespace Service.Services.Validators.Employee
{
    public interface IEmployeeValidator : IValidator<Entities.Entities.Hr.Employee>
    {
        Task<bool>  CheckNationalIdAsync(string nationalId, Guid employeeId);
        Task<bool> CheckEmailAsync(string email, Guid employeeId);
        Task<bool> CheckFileNumberAsync(string fileNumber, Guid employeeId);
    }
}
