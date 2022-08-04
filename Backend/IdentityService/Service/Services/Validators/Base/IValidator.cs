using System.Threading.Tasks;

namespace Service.Services.Validators.Base
{
    public interface IValidator<in T> where T : class
    {
        Task<(bool, string)> Validate(T entity);
    }
}
