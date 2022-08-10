using System;
using Domain.Core;
using Domain.DTO.Identity.App;
using Domain.DTO.Identity.User;

namespace Domain.DTO.Identity.UserApp
{
    public class UserAppDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AppId { get; set; }
        public AppDto App { get; set; }
        public UserDto User { get; set; }
    }
}
