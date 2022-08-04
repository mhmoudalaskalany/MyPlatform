using Domain.DTO.Identity.User;

namespace Domain.Abstraction.ActiveDirectory
{
    public interface IActiveDirectoryRepository
    {
        ActiveDirectoryUserDto Login(LoginInputModel model);
    }
}
