using Domain.Core;

namespace Domain.DTO.Common.TemplateForm
{
    public class AddTemplateFormDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
    }
}
