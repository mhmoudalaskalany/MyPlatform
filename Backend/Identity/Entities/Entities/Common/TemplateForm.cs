using Entities.Entities.Base;

namespace Entities.Entities.Common
{
    public class TemplateForm : BaseEntity
    {
        public long Id { get; set; }
        public string Form { get; set; }
        public string Language { get; set; }
        public long TemplateId { get; set; }
        public virtual Template Template { get; set; }
        
    }
}
