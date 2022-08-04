using System;

namespace Domain.Mq.Events
{
    public class MurasalatEmployeeChangedEvent
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; }
        public string UnitName { get; set; }

        
    }
}
