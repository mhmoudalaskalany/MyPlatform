using System;
using Domain.DTO.Base;

namespace Domain.DTO.Hr.Nationality.Parameters
{
    public class NationalityFilter : MainFilter
    {
        public Guid? Id { get; set; }
    }
}
