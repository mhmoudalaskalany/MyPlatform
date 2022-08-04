using System;

namespace Domain.DTO.Identity.User
{
    public class ActiveDirectoryUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string LogonName { get; set; }
        public string Name { get; set; }
        public string Principal { get; set; }
        public string Email { get; set; }
        public string EmployeeId { get; set; }
        public string NameAr { get; set; }
        public string Mobile { get; set; }
        public string SecondMobile { get; set; }
        public string DisplayName { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
