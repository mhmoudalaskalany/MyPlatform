using System;

namespace Common.Mq.Events
{
    public class MurasalatEmployeeChangedEvent
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; }
        public string UnitName { get; set; }

        
    }
}
