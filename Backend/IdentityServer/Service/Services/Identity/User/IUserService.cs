using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.User;
using Domain.DTO.Identity.User.Parameters;
using Service.Services.Base;

namespace Service.Services.Identity.User
{
    public interface IUserService : IBaseService<Entities.Entities.Identity.User, AddUserDto, UserDto, Guid?>
    {
        Task<IFinalResult> GetUserCountAsync();
        Task<DataPaging> GetAllPagedAsync(BaseParam<UserFilter> filter);
        Task<IFinalResult> DeleteByUserAppId(Guid userId, Guid appId);
        Task<IFinalResult> GetByAppIdAsync(Guid appId);
        Task<DataPaging> GetByAppIdPagedAsync(BaseParam<UserSearchCriteriaFilter> filter);
        Task<IFinalResult> ChangePasswordAsync(ChangePasswordDto model);
        Task<IFinalResult> CheckNationalIdAsync(string username, Guid? userId);
        Task<IFinalResult> CheckEmailAsync(string email, Guid? userId);
        Task<IFinalResult> UploadProfileImageAsync(UploadProfileImageDto dto);
        Task<IFinalResult> GetUserProfileAsync(Guid id);

    }
}
