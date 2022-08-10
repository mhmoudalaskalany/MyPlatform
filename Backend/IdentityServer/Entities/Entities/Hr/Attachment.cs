using System;
using Entities.Entities.Base;

namespace Entities.Entities.Hr
{
    public class Attachment : BaseEntity
    {
        public Guid Id { get; set; }
        public Guid FileId { get; set; }
        public string FileName { get; set; }
        public string Extension { get; set; }
        public string Size { get; set; }
        public bool IsPublic { get; set; }
        public string AttachmentDisplaySize { get; set; }
        public Guid? FullEmployeeId { get; set; }
        public virtual FullEmployee FullEmployee { get; set; }
    }
}
