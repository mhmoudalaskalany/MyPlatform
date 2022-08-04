namespace Common.DTO.Identity.User.Parameters
{
    public class UserFilter
    { 
        public string UserName { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string Email { get; set; }
        public long AppId { get; set; }
    }
}
