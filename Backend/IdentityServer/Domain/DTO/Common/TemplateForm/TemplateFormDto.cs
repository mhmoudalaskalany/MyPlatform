using Domain.Core;

namespace Domain.DTO.Common.TemplateForm
{
    public class TemplateFormDto : IEntityDto<long?>
    {
        public long? Id { get; set; }
    }
}
