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
        Task<IFinalResult> GetAppsCountAsync();
        Task<IFinalResult> GetByUserIdAsync(long userId);
        Task<IFinalResult> GetByUserAppIdAsync(long userId, long appId);
        Task<DataPaging> GetAllPagedAsync(BaseParam<AppFilter> filter);
        Task<IFinalResult> GetUserAppsWithNoRoles(UserAppRolesDto dto);
        Task<IFinalResult> GetUserAppsWithRoles(UserAppRolesDto dto);
        Task<IFinalResult> GetPublicAppsAsync();
    }

}
