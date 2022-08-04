using System;

namespace Entities.Entities.Base
{
    public class BaseEntity 
    {
        public long? CreatedById { get; set; }
        public DateTime? CreatedDate { get; set; } = DateTime.Now;
        public DateTime? ModifiedDate { get; set; } = DateTime.Now;
        public long? ModifiedById { get; set; }
        public bool? IsDeleted { get; set; } = false;
        public string CreatedByEmployeeEn { get; set; }
        public string CreatedByEmployeeAr { get; set; }
        public string ModifiedByEmployeeEn { get; set; }
        public string ModifiedByEmployeeAr { get; set; }
        public string CreatedByEmployeeId { get; set; }
        public string ModifiedByEmployeeId { get; set; }
        public string IpAddress { get; set; }
    }
}
