using System.Collections.Generic;
using Domain.Core;

namespace Domain.DTO.Common.Template
{
    public class AddTemplateDto : IEntityDto<long?>
    {
        public long? Id { get; set; }
        public Dictionary<string, dynamic> Parameters { get; set; }
        public string TemplateCode { get; set; }
    }
}
