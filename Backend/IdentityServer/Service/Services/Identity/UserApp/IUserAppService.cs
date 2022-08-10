using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Identity.UserApp;
using Service.Services.Base;

namespace Service.Services.Identity.UserApp
{
    public interface IUserAppService : IBaseService<Entities.Entities.Identity.UserApp,AddUserAppDto, UserAppDto , Guid?>
    {
        Task<bool> AddAppListAsync(List<Guid> ids, Guid userId);
        Task<IFinalResult> AddUsersToAppAsync(List<AddUserAppDto> userAppDtos);
        Task<IFinalResult> GetUserApps(Guid userId);

    }
    
}
