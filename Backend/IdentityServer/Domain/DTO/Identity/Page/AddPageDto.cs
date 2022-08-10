using System;
using Domain.Core;

namespace Domain.DTO.Identity.Page
{
    public class AddPageDto : IEntityDto<Guid?>
    {
        public Guid? Id { get; set; }
        public Guid AppId { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Url { get; set; }
        public string Icon { get; set; }
        public string Code { get; set; }
    }
}
