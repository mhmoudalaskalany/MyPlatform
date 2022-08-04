using System.ComponentModel.DataAnnotations;

namespace Common.DTO.Identity.User
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "ادخل اسم المستخدم")]
        public string Username { get; set; }
    }
}
