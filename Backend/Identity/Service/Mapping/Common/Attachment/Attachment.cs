using System.Net.Mail;
using Common.DTO.Common.Attachment;

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