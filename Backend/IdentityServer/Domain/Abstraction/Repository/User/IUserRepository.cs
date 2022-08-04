using System.Threading.Tasks;
using Domain.DTO.Hr.Employee;

namespace Domain.Abstraction.Repository.User
{
    public interface IUserRepository : IRepository<Entities.Entities.Identity.User>
    {
        Task<EmployeeInfoDto> GetUserInfoAsync(string nationalId);
    }
}
