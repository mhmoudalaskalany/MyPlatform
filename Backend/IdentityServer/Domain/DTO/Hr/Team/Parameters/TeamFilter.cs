using System;
using Domain.DTO.Base;

namespace Domain.DTO.Hr.Team.Parameters
{
    public class TeamFilter : MainFilter
    {
        public Guid? Id { get; set; }
    }
}
