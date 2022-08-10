using System;
using Domain.Core;

namespace Domain.DTO.Hr.Nationality
{
    public class NationalityDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Code { get; set; }
    }
}
