﻿using System.Collections.Generic;
using Common.Core;
using Common.DTO.Identity.App;

namespace Common.DTO.Identity.User
{
    public class UserDto :IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserTypeId { get; set; }
        public string PersonId { get; set; }
        public string Position { get; set; }
        public string PhoneNumber { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string NationalId { get; set; }
        public long SessionDuration { get; set; }
        public string Base64 { get; set; }
        public ICollection<AppDto> Apps { get; set; }
    }
}
