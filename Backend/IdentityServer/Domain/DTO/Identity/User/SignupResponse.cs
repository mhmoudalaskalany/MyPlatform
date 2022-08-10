namespace Domain.DTO.Identity.User
{
    public class SignUpResponse
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }

        public SignUpResponse() { }

        public SignUpResponse(Entities.Entities.Identity.User user, string role)
        {
            Id = user.Id.ToString();
            FullName = user.FullNameEn;
            Email = user.Email;
            Role = role;
        }
    }
}