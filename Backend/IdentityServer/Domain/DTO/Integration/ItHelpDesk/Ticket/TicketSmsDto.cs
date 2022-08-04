using Entities.Enum;

namespace Domain.DTO.Integration.ItHelpDesk.Ticket
{
    public class TicketSmsDto
    {
        public long? EmployeeId { get; set; }
        public long? UnitId { get; set; }
        public string TicketNumber { get; set; }
        public string PhoneNumber { get; set; }
        public UnitType? UnitType { get; set; }
    }
}
