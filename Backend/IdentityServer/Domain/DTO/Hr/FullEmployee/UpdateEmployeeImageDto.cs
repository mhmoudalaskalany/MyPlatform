using System;

namespace Domain.DTO.Hr.FullEmployee
{
    public class UpdateEmployeeImageDto
    {
        public Guid EmployeeId { get; set; }
        public Guid? OldPhotoId { get; set; }
        public Guid NewPhotoId { get; set; }
    }
}
