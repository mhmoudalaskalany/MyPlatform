using System;
using System.Threading.Tasks;
using Domain.Abstraction.UnitOfWork;
using Entities.Entities.Hr;
using Service.Services.Validators.Base;

namespace Service.Services.Validators.Employee
{
    public class EmployeeValidator : Validator<FullEmployee>, IEmployeeValidator
    {
        private readonly IUnitOfWork<FullEmployee> _uow;
        public EmployeeValidator(IUnitOfWork<FullEmployee> uow)
        {
            _uow = uow;
        }

        #region Public Methods

        public override async Task<(bool, string)> Validate(FullEmployee entity)
        {
            return await Task.FromResult((true, ""));
        }

        public async Task<bool> CheckNationalIdAsync(string nationalId, Guid employeeId)
        {
            bool result;
            if (employeeId != Guid.Empty && employeeId != Guid.NewGuid())
            {
                result = await CheckForNationalIdAtEditMode(nationalId, employeeId);
            }
            else
            {
                result = await CheckForNationalIdAtAddMode(nationalId);

            }
            return result;


        }

        public async Task<bool> CheckEmailAsync(string email, Guid employeeId)
        {
            bool result;
            if (employeeId != Guid.Empty && employeeId != Guid.NewGuid())
            {
                result = await CheckForEmailAtEditMode(email, employeeId);
            }
            else
            {
                result = await CheckForEmailAtAddMode(email);

            }
            return result;

        }

        public async Task<bool> CheckFileNumberAsync(string fileNumber, Guid employeeId)
        {
            bool result;
            if (employeeId != Guid.Empty && employeeId != Guid.NewGuid())
            {
                result = await CheckForFileNumberAtEditMode(fileNumber, employeeId);
            }
            else
            {
                result = await CheckForFileNumberAtAddMode(fileNumber);

            }
            return result;

        }

        #endregion

        #region Private Methods

     
        async Task<bool> CheckForNationalIdAtAddMode(string nationalId)
        {
            var data = await _uow.Repository.FirstOrDefaultAsync(x => x.CivilNumber == nationalId);
            if (data != null)
            {
                return true;
            }

            return false;

        }
       
        async Task<bool> CheckForEmailAtAddMode(string email)
        {
            var data = await _uow.Repository.FirstOrDefaultAsync(x => x.Email == email);
            if (data != null)
            {
                return true;
            }

            return false;

        }

      
        async Task<bool> CheckForFileNumberAtAddMode(string fileNumber)
        {
            var data = await _uow.Repository.FirstOrDefaultAsync(x => x.FileNumber == fileNumber);
            if (data != null)
            {
                return true;
            }

            return false;

        }
    
        async Task<bool> CheckForNationalIdAtEditMode(string nationalId, Guid employeeId)
        {
            var user = await _uow.Repository.FirstOrDefaultAsync(x => x.Id == employeeId);
            var data = await _uow.Repository.FirstOrDefaultAsync(x => x.CivilNumber == nationalId);
            if (data != null)
            {
                // mean the same national id for the same employee so return false as the name is available for him
                if (data.Id == user.Id)
                {
                    return false;
                }

                // means this national already taken by another employee so return true as its not available for him
                return true;

            }
            else
            {
                return false;
            }


        }
   
        async Task<bool> CheckForEmailAtEditMode(string email, Guid employeeId)
        {
            var user = await _uow.Repository.FirstOrDefaultAsync(x => x.Id == employeeId);
            var data = await _uow.Repository.FirstOrDefaultAsync(x => x.Email == email);
            if (data != null)
            {
                // mean the same user name for the same user so return false as the name is available for him
                if (data.Id == user.Id)
                {
                    return false;
                }

                // means this username already taken by another user so return true as its not available for him
                return true;

            }

            return false;

        }

        async Task<bool> CheckForFileNumberAtEditMode(string fileNumber, Guid employeeId)
        {
            var user = await _uow.Repository.FirstOrDefaultAsync(x => x.Id == employeeId);
            var data = await _uow.Repository.FirstOrDefaultAsync(x => x.FileNumber == fileNumber);
            if (data != null)
            {
                // mean the same user name for the same user so return false as the name is available for him
                if (data.Id == user.Id)
                {
                    return false;
                }

                // means this username already taken by another user so return true as its not available for him
                return true;

            }

            return false;

        }

        #endregion
    }
}
