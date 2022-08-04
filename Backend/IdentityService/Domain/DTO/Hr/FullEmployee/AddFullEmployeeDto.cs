using System;
using System.Collections.Generic;
using Domain.Core;
using Domain.DTO.Common.Attachment;
using Entities.Enum;

namespace Domain.DTO.Hr.FullEmployee
{
    public class AddFullEmployeeDto : IPrimaryKeyField<Guid?>
    {
        public Guid? Id { get; set; }
        public string PhoneNumber { get; set; }
        public DoseStatus DoseStatus { get; set; }
        public string Notes { get; set; }
        public List<AddAttachmentDto> Attachments { get; set; } = new List<AddAttachmentDto>();
    }
}
