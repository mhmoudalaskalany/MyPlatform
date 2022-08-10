using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Identity.App;
using Service.Services.Base;

namespace Service.Services.Identity.App.Integration
{
    public interface IExternalAppService : IBaseService<Entities.Entities.Identity.App, AddAppDto, AppDto, long?>
    {
        Task<IFinalResult> GetLoggedUserAppsAsync();
    }
}
