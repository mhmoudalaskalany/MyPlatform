using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Entities.Entities.Murasalat.Hr
{
    public partial class MawredLeaves
    {
        public Guid Id { get; set; }
        public string Filenumber { get; set; }
        public DateTime? Startdate { get; set; }
        public DateTime? Endate { get; set; }
        public string Leavetype { get; set; }
        public DateTime? Creationdate { get; set; }
        public bool? TransferDone { get; set; }
        public DateTime? Transferdate { get; set; }
        public string TransferStatus { get; set; }
        public long LeaveId { get; set; }
        public int? NoOfDays { get; set; }
        public string Status { get; set; }
        public int? LeaveTypeId { get; set; }
    }
}