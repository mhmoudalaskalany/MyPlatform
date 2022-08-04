using Common.Core;

namespace Common.DTO.Common.TemplateForm
{
    public class AddTemplateFormDto : IPrimaryKeyField<long?>
    {
        public long? Id { get; set; }
    }
}
