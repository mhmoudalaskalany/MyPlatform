using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Common.Abstraction.UnitOfWork;
using Common.Core;
using Common.DTO.Common.Sms;
using Common.DTO.Identity.User;
using Common.Helper.SmsHelper;
using Entities.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Service.Services.Identity.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Entities.Entities.Identity.User> _userManager;
        private readonly IUnitOfWork<LoginHistory> _loginHistoryUnitOfWork;
        private readonly ISmsService _smsService;
        private readonly IConfiguration _configuration;
        public static IDictionary<string, string> OtpDictionary = new Dictionary<string, string>();
        public AccountService(UserManager<Entities.Entities.Identity.User> userManager, ISmsService smsService, IConfiguration configuration, IUnitOfWork<LoginHistory> loginHistoryUnitOfWork)
        {
            _userManager = userManager;
            _smsService = smsService;
            _configuration = configuration;
            _loginHistoryUnitOfWork = loginHistoryUnitOfWork;
        }

        public async Task<IFinalResult> LoginAsync(AuthenticationDto model)
        {
            return await Task.FromResult(new ResponseResult(true));
        }
        public async Task AddLoginHistory(long userId)
        {
            var record = new LoginHistory()
            {
                UserId = userId,
                LoginTime = DateTime.Now
            };
            _loginHistoryUnitOfWork.Repository.Add(record);
            await _loginHistoryUnitOfWork.SaveChanges();


        }

        public bool CheckUserFirstLogin(Entities.Entities.Identity.User user)
        {
            return user.PasswordChanged == true;
        }

        public async Task<bool> ChangePasswordAsync(ChangePasswordDto model)
        {

            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
            {
                return false;
            }
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                return false;
            }

            user.PasswordChanged = true;
            user.PasswordChangedDate = DateTime.Now;
            user.ModifiedDate = DateTime.Now;
            var updateResult = await _userManager.UpdateAsync(user);
            return updateResult.Succeeded;
        }

        public async Task<IFinalResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == model.Username);
            if (user == null)
            {
                return new ResponseResult(false, HttpStatusCode.BadGateway, null, "اسم المستخدم خطأ");
            }

            var message = _configuration["SmsMessages:ResetPassword"];
            SendOtp(user.PhoneNumber, message, user.UserName);

            return new ResponseResult(true, HttpStatusCode.OK, null, "تم ارسال رقم سرى متغير الى رقم هاتفك");
        }


        public async Task<IFinalResult> CompleteResetPassword(CompleteResetPasswordDto model)
        {
            var otp = OtpDictionary.FirstOrDefault(x => x.Value == model.Otp);
            if (otp.Value != model.Otp)
            {
                return new ResponseResult(result: null, status: HttpStatusCode.BadRequest,
                    message: "الرقم السرى المتغير خطأ");
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == otp.Key);
            user.ModifiedDate = DateTime.Now;
            user.PasswordChangedDate = DateTime.Now;
            user.PasswordChanged = true;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, model.NewPassword);
            OtpDictionary.Remove(otp.Key);
            var affectedRows = await _userManager.UpdateAsync(user);
            if (affectedRows.Succeeded)
            {
                return new ResponseResult(true, HttpStatusCode.OK, null, "تم تغيير الرقم السرى بنجاح");
            }
            return new ResponseResult(false, HttpStatusCode.BadRequest, null, "خطأ في تغيير كلمة المرور");
        }




        private async void SendOtp(string phone, string message, string username)
        {

            var otp = GenerateOtp();
            var existOtp = OtpDictionary.FirstOrDefault(x => x.Key == username);
            // remove otp for same user if request more than one time to avoid exception
            if (existOtp.Value != null)
            {
                OtpDictionary.Remove(username);
            }

            OtpDictionary.Add(username, otp);
            var smsDto = new SmsDto
            {
                Phone = phone,
                Message = $"{message} {otp}"
            };
            await _smsService.SendBulkSms(smsDto);

        }

        private string GenerateOtp()
        {
            var chars = "1234567890";
            var stringChars = new char[6];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var otp = new String(stringChars);
            return otp;
        }


    }
}
