using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.User;
using Domain.DTO.Identity.User.Parameters;
using Service.Services.Base;

namespace Service.Services.Identity.User
{
    public interface IUserService : IBaseService<Entities.Entities.Identity.User, AddUserDto, UserDto, long?>
    {
        Task<IResult> GetUserCountAsync();
        Task<DataPaging> GetAllPagedAsync(BaseParam<UserFilter> filter);
        Task<IResult> DeleteByUserAppId(long userId, long appId);
        Task<IResult> GetByAppIdAsync(long appId);
        Task<DataPaging> GetByAppIdPagedAsync(BaseParam<UserSearchCriteriaFilter> filter);
        Task<IResult> ChangePasswordAsync(ChangePasswordDto model);
        Task<IResult> CheckNationalIdAsync(string username, long userId);
        Task<IResult> CheckEmailAsync(string email, long userId);
        Task<IResult> UploadProfileImageAsync(UploadProfileImageDto dto);
        Task<IResult> GetUserProfileAsync(long id);

    }
}
