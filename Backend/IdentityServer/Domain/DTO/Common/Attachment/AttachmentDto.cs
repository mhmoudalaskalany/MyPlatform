using System;

namespace Domain.DTO.Common.Attachment
{
    public class AttachmentDto
    {
        public long? Id { get; set; }
        public Guid FileId { get; set; }
        public string Url { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string Size { get; set; }
        public bool IsPublic { get; set; }
        public string AttachmentDisplaySize { get; set; }
        public long? EmployeeId { get; set; }
    }
}
