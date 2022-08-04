using System;
using Microsoft.Extensions.Configuration;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;
using Domain.Abstraction.ActiveDirectory;
using Domain.DTO.Identity.User;

namespace Data.Repository.ActiveDirectory
{
    public class ActiveDirectoryRepository : IActiveDirectoryRepository
    {
        private readonly IConfiguration _configuration;
        public ActiveDirectoryRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public ActiveDirectoryUserDto Login(LoginInputModel model)
        {
            try
            {
                var username = _configuration["ActiveDirectory:ADUserName"];
                var password = _configuration["ActiveDirectory:AdPassword"];
                var container = _configuration["ActiveDirectory:ADContainer"];
                var domain = _configuration["ActiveDirectory:ADDomain"];
                using var context = new PrincipalContext(ContextType.Domain, domain, container, username, password);
                if (context.ValidateCredentials(model.Username, model.Password))
                {
                    using var userPrincipal = new UserPrincipal(context)
                    {
                        SamAccountName = model.Username
                    };
                    using var principalSearcher = new PrincipalSearcher(userPrincipal);
                    var result = principalSearcher.FindOne();
                    if (result != null)
                    {
                        DirectoryEntry de = (DirectoryEntry)result.GetUnderlyingObject();
                        string name =
                            de.Properties["name"]?.Value != null
                                ? de.Properties["name"].Value.ToString()
                                : "";
                        string fName =
                            de.Properties["givenName"]?.Value != null
                                ? de.Properties["givenName"].Value.ToString()
                                : "";
                        string lName = de.Properties["sn"]?.Value != null
                            ? de.Properties["sn"].Value.ToString()
                            : "";

                        string uName =
                            de.Properties["samAccountName"]?.Value != null
                                ? de.Properties["samAccountName"].Value.ToString()
                                : "";

                        string principal =
                            de.Properties["userPrincipalName"]?.Value != null
                                ? de.Properties["userPrincipalName"].Value.ToString()
                                : "";
                        string employeeId =
                           de.Properties["employeeId"]?.Value != null
                               ? de.Properties["employeeId"].Value.ToString()
                               : "";
                        string email =
                            de.Properties["mail"]?.Value != null
                                ? de.Properties["mail"].Value.ToString()
                                : "";
                        string mobile =
                            de.Properties["mobile"]?.Value != null
                                ? de.Properties["mobile"].Value.ToString()
                                : "";
                        string displayName =
                            de.Properties["displayName"]?.Value != null
                                ? de.Properties["displayName"].Value.ToString()
                                : "";
                        string otherMobile = de.Properties["otherMobile"]?.Value != null
                            ? de.Properties["otherMobile"].Value.ToString()
                            : "";
                        var joinDate = de.Properties["whenCreated"]?.Value != null
                            ? de.Properties["whenCreated"].Value
                            : "";
                        var user = new ActiveDirectoryUserDto
                        {
                            FirstName = fName,
                            LastName = lName,
                            LogonName = uName,
                            EmployeeId = employeeId,
                            Principal = principal,
                            Email = email,
                            Mobile = mobile,
                            SecondMobile = otherMobile,
                            Name = name,
                            DisplayName = displayName,
                            JoinDate = (DateTime)joinDate
                        };
                        return user;
                    }

                }
                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}
