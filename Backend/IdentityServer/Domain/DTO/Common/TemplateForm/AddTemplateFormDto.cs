using Domain.Core;

namespace Domain.DTO.Common.TemplateForm
{
    public class AddTemplateFormDto : IEntityDto<long?>
    {
        public long? Id { get; set; }
    }
}
