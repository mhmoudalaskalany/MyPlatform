namespace Common.DTO.Identity.User
{
    public class UploadProfileImageDto
    {
        public long UserId { get; set; }
        public string Base64 { get; set; }
    }
}
