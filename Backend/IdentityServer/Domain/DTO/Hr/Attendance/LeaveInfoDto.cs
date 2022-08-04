using System;

namespace Domain.DTO.Hr.Attendance
{
    public class LeaveInfoDto
    {
        public long Id { get; set; }
        public string EmployeeNumber { get; set; }
        public int LeaveTypeId { get; set; }
        public string LeaveNameAr { get; set; }
        public string LeaveNameEn { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int LeavesCount { get; set; }
        public string Status { get; set; }
    }
}
