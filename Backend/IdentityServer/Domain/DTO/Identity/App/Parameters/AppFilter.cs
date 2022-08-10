using System;
using Domain.DTO.Base;

namespace Domain.DTO.Identity.App.Parameters
{
    public class AppFilter : MainFilter
    {
        public Guid? Id { get; set; }
    }
}
