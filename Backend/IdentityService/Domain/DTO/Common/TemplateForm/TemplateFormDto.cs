using Domain.Core;

namespace Domain.DTO.Common.TemplateForm
{
    public class TemplateFormDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
    }
}
