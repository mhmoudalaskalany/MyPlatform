using System;

namespace Domain.Mq.Events
{
    public class EmployeeChangedEvent
    {
        public long Id { get; set; }
        public string EmployeeName { get; set; }
        public string UnitName { get; set; }

        
    }
}
