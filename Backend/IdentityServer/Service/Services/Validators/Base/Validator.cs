using System;
using System.Threading.Tasks;

namespace Service.Services.Validators.Base
{
    public class Validator<T> :  IValidator<T> where T : class
    {
        public virtual async Task<(bool, string)> Validate(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
