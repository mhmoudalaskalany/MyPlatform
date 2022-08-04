namespace Common.DTO.Identity.User
{
    public class SignupResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public SignupResponse() { }

        public SignupResponse(Entities.Entities.Identity.User user, string role)
        {
            Id = user.Id.ToString();
            FullName = user.FullNameEn;
            Email = user.Email;
            Role = role;
        }
    }
}