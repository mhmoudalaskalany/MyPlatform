﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Identity.UserApp;
using Service.Services.Base;

namespace Service.Services.Identity.UserApp
{
    public interface IUserAppService : IBaseService<Entities.Entities.Identity.UserApp,AddUserAppDto, UserAppDto , long?>
    {
        Task<bool> AddAppListAsync(List<long> ids, long userId);
        Task<IResult> AddUsersToAppAsync(List<AddUserAppDto> userAppDtos);
        Task<IResult> GetUserApps(long userId);

    }
    
}