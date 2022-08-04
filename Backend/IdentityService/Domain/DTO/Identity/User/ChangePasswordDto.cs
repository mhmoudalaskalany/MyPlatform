using System.ComponentModel.DataAnnotations;
using Domain.Extensions;

namespace Domain.DTO.Identity.User
{
    public class ChangePasswordDto
    {
        public object UserId { get; set; }

        [Required(ErrorMessage = "ادخل كلمة المرور الحالية")]

        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Required(ErrorMessage = "ادخل كلمة المرور الجديدة")]
        [NotEqual("OldPassword")]

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
