using System;
using Domain.Core;

namespace Domain.DTO.Identity.UserApp
{
    public class AddUserAppDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public Guid UserId { get; set; }
        public Guid AppId { get; set; }
    }
}
