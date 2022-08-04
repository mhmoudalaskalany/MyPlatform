using System;
using System.Collections.Generic;
using Domain.Core;
using Domain.DTO.Common.Attachment;
using Entities.Enum;

namespace Domain.DTO.Hr.FullEmployee
{
    public class AddMurasalatEmployeeDto : IPrimaryKeyField<Guid?>
    {
        public Guid? Id { get; set; }
        public string FileNumber { get; set; }
        public Guid? ManagerId { get; set; }
        public string FullNameEn { get; set; }
        public string FullNameAr { get; set; }
        public string UnitId { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Position { get; set; }
        public string UnitName { get; set; }
        public bool IsVaccinated { get; set; }
        public string NationalId { get; set; }
        public string PhotoUrl { get; set; }
        public Guid? FileId { get; set; }
        public string Extension { get; set; }
        public Guid? PhotoId { get; set; }
        public DoseStatus? DoseStatus { get; set; }
        public DateTime? HiringDate { get; set; }
        public List<AttachmentDto> Attachments { get; set; } = new List<AttachmentDto>();
    }
}
