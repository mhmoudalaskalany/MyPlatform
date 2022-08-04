using System;
using System.Threading.Tasks;
using Domain.Abstraction.Repository.User;
using Domain.DTO.Hr.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace Data.Repository.User
{
    public class UserRepository : Repository<Entities.Entities.Identity.User>, IUserRepository
    {
        private readonly IConfiguration _configuration;
        public UserRepository(DbContext context, IConfiguration configuration) : base(context)
        {

            _configuration = configuration;
            //OracleContext.OracleContext.ConfigureConnection();
        }

        public async Task<EmployeeInfoDto> GetUserInfoAsync(string nationalId)
        {
            try
            {
                var conString = _configuration["ConnectionStrings:OracleConnection"];
                await using var connection = new OracleConnection(conString);
                var user = new EmployeeInfoDto();
                await using (var command = connection.CreateCommand())
                {
                    var query = $"select * from admin.all_emp_det_v where CS_NO = '{nationalId}' or SOCIAL_TITLE_ID = '{nationalId}'";
                    connection.Open();
                    command.BindByName = true;
                    command.CommandText = query;
                    OracleParameter id = new OracleParameter("nationalId", nationalId);
                    command.Parameters.Add(id);
                    OracleGlobalization info = connection.GetSessionInfo();
                    info.TimeZone = "CET";
                    connection.SetSessionInfo(info);
                    OracleDataReader reader = command.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        MapUserObject(user, reader);
                        reader.Dispose();
                    }
                    else
                    {
                        user = null;
                    }
                        
                }
                connection.Close();
                return user;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void MapUserObject(EmployeeInfoDto user, OracleDataReader reader)
        {
            user.EmployeeNumber = !reader.IsDBNull(0) ? reader.GetString(0) : "";
            user.NationalId = !reader.IsDBNull(1) ? reader.GetString(1) : "";
            user.EmployeeId = !reader.IsDBNull(2) ? reader.GetString(2) : "";
            user.FullName = !reader.IsDBNull(3) ? reader.GetString(3) : "";
            user.Gender = !reader.IsDBNull(4) ? reader.GetString(4) : "";
            user.DateOfBirth = reader.GetDateTime(5);
            user.MaritalStatus = !reader.IsDBNull(6) ? reader.GetString(6) : "";
            user.Nationality = !reader.IsDBNull(7) ? reader.GetString(7) : "";
            user.HireDate = !reader.IsDBNull(8) ? reader.GetDateTime(8) : DateTime.Now;
            user.Grade = !reader.IsDBNull(9) ? reader.GetString(9) : "" ;
            user.JobTitleId = !reader.IsDBNull(10) ? reader.GetInt32(10) : 0;
            user.JobTitle = !reader.IsDBNull(11) ? reader.GetString(11) : "";
            user.JobLevelId = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0;
            user.JobLevel = !reader.IsDBNull(13) ? reader.GetString(13) : "";
            user.Department = !reader.IsDBNull(14) ? reader.GetString(14) : "";
            user.Sector = !reader.IsDBNull(15) ? reader.GetString(15) : "";
            user.Section = !reader.IsDBNull(16) ? reader.GetString(16) : "";
        }
    }
}
