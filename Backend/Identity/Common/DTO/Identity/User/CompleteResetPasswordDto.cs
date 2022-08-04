using System.ComponentModel.DataAnnotations;

namespace Common.DTO.Identity.User
{
    public class CompleteResetPasswordDto
    {
        [Required]
        public string Otp { get; set; }
        [DataType(DataType.Password)]

        [StringLength(100, ErrorMessage = "كلمة المرور الجديدة يجب ان تكون اكبر من 8 احرف", MinimumLength = 8)]

        [RegularExpression("^((?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])|(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[^a-zA-Z0-9])|(?=.*?[A-Z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])|(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^a-zA-Z0-9])).{8,}$", ErrorMessage = "كلمة المرور يجب ان لا تقل عن 8 احرف مكونة من احرف كبيرة وصغيرة وارقام ورموز لاتينية مثل (!@#$%^&*)")]
        public string NewPassword { get; set; }

        [Required]

        [DataType(DataType.Password)]

        [Compare("NewPassword", ErrorMessage = "كلمة المرور الجديدة غير متطابقة ")]
        public string ConfirmNewPassword { get; set; }
    }
}
