using System;

namespace Domain.DTO.Identity.User
{
    public class AccountOptions
    {
        public static bool AllowLocalLogin = true;
        public static bool AllowRememberLogin = true;
        public static TimeSpan RememberMeLoginDuration = TimeSpan.FromMinutes(30);

        public static bool ShowLogoutPrompt = true;
        public static bool AutomaticRedirectAfterSignOut = true;

        // specify the Windows authentication scheme being used
        public static readonly string WindowsAuthenticationSchemeName = Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme;
        // if user uses windows auth, should we load the groups from windows
        public static bool IncludeWindowsGroups = false;

        public static string InvalidCredentialsErrorMessage = "اسم المستخدم او كلمة المرور غير صحيحة";
        public static string NotPartOfDomain = "You Are Not Part Of This Domain";
        public static string LoginFromPortal = "من فضلك قم بتسجيل الدخول من موقع البوابة الإلكترونية";
    }
}