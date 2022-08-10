using System;
using Entities.Entities.Base;

namespace Entities.Entities.Identity
{
    public class LoginHistory : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid? UserId { get; set; }
        public Guid? AppId { get; set; }
        public string AppCode { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
