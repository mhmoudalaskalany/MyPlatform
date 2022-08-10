using System;

namespace Domain.DTO.Identity.UserApp
{
    public class AppUsersCountDto
    {
        public Guid AppId { get; set; }
        public long Count { get; set; }
    }
}
