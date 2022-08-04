using System;
using Entities.Entities.Base;

namespace Entities.Entities.Identity
{
    public class LoginHistory : BaseEntity
    {
        public long? Id { get; set; }
        public long? UserId { get; set; }
        public long? AppId { get; set; }
        public string AppCode { get; set; }
        public DateTime LoginTime { get; set; }
    }
}
