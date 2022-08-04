using System;

namespace Common.DTO.Identity.User
{
    public class LoginHistoryDto
    {
        public long? UserId { get; set; }
        public long? AppId { get; set; }
        public string AppCode { get; set; }
        public DateTime LoginTime { get; set; }
        public string IpAddress { get; set; }
    }
}
