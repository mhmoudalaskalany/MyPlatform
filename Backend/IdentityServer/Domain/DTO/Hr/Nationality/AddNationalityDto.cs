using System;
using Domain.Core;

namespace Domain.DTO.Hr.Nationality
{
    public class AddNationalityDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}
