using System.ComponentModel.DataAnnotations;

namespace Common.DTO.Identity.User
{
    public class AuthenticationDto
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }
    }
}