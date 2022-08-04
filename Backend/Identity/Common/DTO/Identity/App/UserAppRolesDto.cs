using System.Collections.Generic;

namespace Common.DTO.Identity.App
{
    public class UserAppRolesDto
    {
        public long UserId { get; set; }
        public List<long> AppIds { get; set; }
    }
}
