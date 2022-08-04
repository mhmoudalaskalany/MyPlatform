using System.Collections.Generic;
using Domain.Core;

namespace Domain.DTO.Identity.User
{
    public class AddUserDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string UserTypeId { get; set; }
        public string PersonId { get; set; }
        public string Position { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string NationalId { get; set; }
        public long SessionDuration { get; set; }
        public List<long> Apps { get; set; }

    }
}
