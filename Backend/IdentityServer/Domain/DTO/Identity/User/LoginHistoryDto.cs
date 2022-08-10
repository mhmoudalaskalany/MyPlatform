using System;

namespace Domain.DTO.Identity.User
{
    public class LoginHistoryDto
    {
        public Guid? UserId { get; set; }
        public Guid? AppId { get; set; }
        public string AppCode { get; set; }
        public DateTime LoginTime { get; set; }
        public string IpAddress { get; set; }
    }
}
