using System.Collections.Generic;
using Common.Core;

namespace Common.DTO.Common.Template
{
    public class TemplateDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
        public Dictionary<string, dynamic> Parameters { get; set; }
        public string TemplateCode { get; set; }
    }
}
