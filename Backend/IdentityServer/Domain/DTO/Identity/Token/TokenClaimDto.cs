namespace Domain.DTO.Identity.Token
{
    public class TokenClaimDto
    {
        public string Email { get; set; }
        public string UserId { get; set; }
        public string TeamId { get; set; }
        public string UnitId { get; set; }
        public string EmployeeEn { get; set; }
        public string EmployeeAr { get; set; }
        public string EmployeeId { get; set; }
        public string IpAddress { get; set; }
    }
}
