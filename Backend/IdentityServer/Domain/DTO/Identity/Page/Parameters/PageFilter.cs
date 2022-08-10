using System;
using Domain.DTO.Base;

namespace Domain.DTO.Identity.Page.Parameters
{
    public class PageFilter : MainFilter
    {
        public Guid? Id { get; set; }
        public string AppNameEn { get; set; }
        public string AppNameAr { get; set; }
        public string Url { get; set; }
    }
}