﻿using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Identity.User;

namespace Service.Services.Identity.Account
{
    public interface IAccountService
    {
        /// <summary>
        /// Add Login History Record
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task AddLoginHistory(Guid userId);
        /// <summary>
        /// Check If User First Login
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        bool CheckUserFirstLogin(Entities.Entities.Identity.User user);
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<bool> ChangePasswordAsync(ChangePasswordDto dto);
        /// <summary>
        /// Send Otp To Reset Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IFinalResult> ResetPassword(ResetPasswordDto model);
        /// <summary>
        /// Complete Reset Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<IFinalResult> CompleteResetPassword(CompleteResetPasswordDto model);
        /// <summary>
        /// Cache User To Redis
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task CacheUserAsync(Entities.Entities.Identity.User user);
    }
}