using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Entities.Entities.Base;

namespace Entities.Entities.Common
{
    public class Template : BaseEntity
    {
        public long Id { get; set; }
        public String TemplateCode { get; set; }
        public virtual ICollection<TemplateForm> TemplateForms { get; set; } = new Collection<TemplateForm>();
    }
}
