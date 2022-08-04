using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.App;
using Domain.DTO.Identity.App.Parameters;
using Service.Services.Base;

namespace Service.Services.Identity.App
{
    public interface IAppService : IBaseService<Entities.Entities.Identity.App, AddAppDto, AppDto, long?>
    {
        Task<IResult> GetAppsCountAsync();
        Task<IResult> GetByUserIdAsync(long userId);
        Task<IResult> GetByUserAppIdAsync(long userId, long appId);
        Task<DataPaging> GetAllPagedAsync(BaseParam<AppFilter> filter);
        Task<IResult> GetUserAppsWithNoRoles(UserAppRolesDto dto);
        Task<IResult> GetUserAppsWithRoles(UserAppRolesDto dto);
        Task<IResult> GetPublicAppsAsync();
    }

}
