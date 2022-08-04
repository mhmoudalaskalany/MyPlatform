using System;

namespace Integration.FileRepository.Dtos
{
    public class UploadResponseDto
    {
        public Guid FileId { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentExtension { get; set; }
        public string AttachmentSize { get; set; }
        public string AttachmentType { get; set; }
    }
}
