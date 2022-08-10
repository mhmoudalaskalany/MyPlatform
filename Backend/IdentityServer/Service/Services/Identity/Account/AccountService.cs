using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Abstraction.UnitOfWork;
using Domain.Caching.Redis;
using Domain.Core;
using Domain.DTO.Common.Sms;
using Domain.DTO.Hr.Employee;
using Domain.DTO.Identity.Role;
using Domain.DTO.Identity.User;
using Domain.Helper.SmsHelper;
using Entities.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Service.Services.Identity.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<Entities.Entities.Identity.User> _userManager;
        private readonly RoleManager<Entities.Entities.Identity.Role> _roleManager;
        private readonly IUnitOfWork<Entities.Entities.Hr.Employee> _employeeUnitOfWork;
        private readonly IUnitOfWork<Entities.Entities.Identity.UserRole> _userRoleUnitOfWork;
        private readonly IUnitOfWork<LoginHistory> _loginHistoryUnitOfWork;
        private readonly IMapper _mapper;
        private readonly ISmsService _smsService;
        private readonly IConfiguration _configuration;
        public static IDictionary<string, string> OtpDictionary = new Dictionary<string, string>();
        public AccountService(UserManager<Entities.Entities.Identity.User> userManager, IMapper mapper, IUnitOfWork<Entities.Entities.Hr.Employee> employeeUnitOfWork, ISmsService smsService, IConfiguration configuration, RoleManager<Entities.Entities.Identity.Role> roleManager, IUnitOfWork<Entities.Entities.Identity.UserRole> userRoleUnitOfWork, IUnitOfWork<LoginHistory> loginHistoryUnitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
            _employeeUnitOfWork = employeeUnitOfWork;
            _smsService = smsService;
            _configuration = configuration;
            _roleManager = roleManager;
            _userRoleUnitOfWork = userRoleUnitOfWork;
            _loginHistoryUnitOfWork = loginHistoryUnitOfWork;
        }

        #region Public Methods
        /// <summary>
        /// Update Percentage
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Check if First Login Of User
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool CheckUserFirstLogin(Entities.Entities.Identity.User user)
        {

            if (user.PasswordChanged == true)
            {
                return true;
            }

            return false;

        }
        /// <summary>
        /// Change Password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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
            if (!updateResult.Succeeded)
            {
                return false;
            }

            return true;

        }
        /// <summary>
        /// Send  Reset Password Otp
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<IFinalResult> ResetPassword(ResetPasswordDto model)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName.ToLower() == model.Username);
            if (user == null)
            {
                return new ResponseResult(false, HttpStatusCode.BadGateway, null, "اسم المستخدم خطأ");
            }

           // var employee = await _employeeUnitOfWork.Repository.GetAsync(Guid.Parse(user.PersonId) );
            var message = _configuration["SmsMessages:ResetPassword"];
            SendOtp(user.PhoneNumber, message, user.UserName);

            return new ResponseResult(true, HttpStatusCode.OK, null, "تم ارسال رقم سرى متغير الى رقم هاتفك");
        }

        /// <summary>
        /// Complete  Reset Password Otp
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
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

        public async Task CacheUserAsync(Entities.Entities.Identity.User user)
        {
            var userRoles = (await _userRoleUnitOfWork.Repository.FindAsync(x => x.UserId == user.Id)).ToList();

            var roles = _roleManager.Roles.Where(x => userRoles.Select(ur => ur.RoleId).Contains(x.Id)).Include(a => a.App)
                .ToList();

            var employee = await _employeeUnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == Guid.Parse(user.PersonId), include: src => src.Include(u => u.Unit));

            var manager = await _employeeUnitOfWork.Repository.FirstOrDefaultAsync(x => x.Id == employee.ManagerId , include:src => src.Include( u => u.Unit));
            
            var employeeDto = _mapper.Map<Entities.Entities.Hr.Employee, EmployeeProfileDto>(employee);
            employeeDto.Manager = _mapper.Map<Entities.Entities.Hr.Employee, EmployeeProfileDto>(manager);
            employeeDto.Roles = _mapper.Map<List<Entities.Entities.Identity.Role>, List<RoleDto>>(roles);
            employeeDto.LastCacheUpdate = DateTime.Now;
            RedisCacheHelper.Set(employeeDto.NationalId, employeeDto);

        }
        #endregion

        #region Private Methods

       
        
        /// <summary>
        /// Send OTP
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="message"></param>
        /// <param name="username"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Generate OTP
        /// </summary>
        /// <returns></returns>
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

        #endregion

    }
}
