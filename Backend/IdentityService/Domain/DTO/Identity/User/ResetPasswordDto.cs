using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.Identity.User
{
    public class ResetPasswordDto
    {
        [Required(ErrorMessage = "ادخل اسم المستخدم")]
        public string Username { get; set; }
    }
}
