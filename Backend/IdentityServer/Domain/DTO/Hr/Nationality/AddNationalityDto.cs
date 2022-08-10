using System;
using Domain.Core;

namespace Domain.DTO.Hr.Nationality
{
    public class AddNationalityDto : IPrimaryKeyField<Guid?>
    {
        public Guid? Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
    }
}
