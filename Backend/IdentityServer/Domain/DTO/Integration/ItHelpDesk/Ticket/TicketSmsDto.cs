using System;
using Entities.Enum;

namespace Domain.DTO.Integration.ItHelpDesk.Ticket
{
    public class TicketSmsDto
    {
        public Guid? EmployeeId { get; set; }
        public Guid? UnitId { get; set; }
        public string TicketNumber { get; set; }
        public string PhoneNumber { get; set; }
        public UnitType? UnitType { get; set; }
    }
}
