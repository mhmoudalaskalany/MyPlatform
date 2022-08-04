using Domain.DTO.Common.Attachment;
using Entities.Entities.Hr;

// ReSharper disable once CheckNamespace
namespace Service.Mapping
{
    public partial class MappingService
    {
        public void MapAttachment()
        {
            CreateMap<Attachment, AttachmentDto>()
                .ReverseMap();

            CreateMap<Attachment, AddAttachmentDto>()
                .ReverseMap();
        }
    }
}