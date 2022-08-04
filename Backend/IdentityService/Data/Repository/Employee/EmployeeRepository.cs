using System;
using System.Threading.Tasks;
using Domain.Abstraction.Repository.Employee;
using Domain.DTO.Hr.Employee;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace Data.Repository.Employee
{
    public class EmployeeRepository : Repository<Entities.Entities.Hr.Employee>, IEmployeeRepository
    {
        private readonly IConfiguration _configuration;
        public EmployeeRepository(DbContext context, IConfiguration configuration) : base(context)
        {

            _configuration = configuration;
            //OracleContext.OracleContext.ConfigureConnection();
        }

        #region Public Methods

        public async Task<EmployeeInfoDto> GetEmployeeInfoAsync(string nationalId)
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

        public async Task<EmployeeInfoDto> GetEmployeeInfoFromNewViewAsync(string nationalId)
        {
            try
            {
                var conString = _configuration["ConnectionStrings:NewOracleConnection"];
                await using var connection = new OracleConnection(conString);
                var user = new EmployeeInfoDto();
                await using (var command = connection.CreateCommand())
                {
                    var query = $"select * from ALL_EMP_DETAILS where CS_NO = '{nationalId}' or SOCIAL_TITLE_ID = '{nationalId}'";
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

        #endregion


        #region Private Methods

        private void MapUserObject(EmployeeInfoDto employee, OracleDataReader reader)
        {
            employee.EmployeeNumber = !reader.IsDBNull(0) ? reader.GetString(0) : "";
            employee.NationalId = !reader.IsDBNull(1) ? reader.GetString(1) : "";
            employee.EmployeeId = !reader.IsDBNull(2) ? reader.GetString(2) : "";
            employee.FullName = !reader.IsDBNull(3) ? reader.GetString(3) : "";
            employee.Gender = !reader.IsDBNull(4) ? reader.GetString(4) : "";
            employee.DateOfBirth = reader.GetDateTime(5);
            employee.MaritalStatus = !reader.IsDBNull(6) ? reader.GetString(6) : "";
            employee.Nationality = !reader.IsDBNull(7) ? reader.GetString(7) : "";
            employee.HireDate = !reader.IsDBNull(8) ? reader.GetDateTime(8) : DateTime.Now;
            employee.Grade = !reader.IsDBNull(9) ? reader.GetString(9) : "";
            employee.JobTitleId = !reader.IsDBNull(10) ? reader.GetInt32(10) : 0;
            employee.JobTitle = !reader.IsDBNull(11) ? reader.GetString(11) : "";
            employee.JobLevelId = !reader.IsDBNull(12) ? reader.GetInt32(12) : 0;
            employee.JobLevel = !reader.IsDBNull(13) ? reader.GetString(13) : "";
            employee.Department = !reader.IsDBNull(14) ? reader.GetString(14) : "";
            employee.Sector = !reader.IsDBNull(15) ? reader.GetString(15) : "";
            employee.Section = !reader.IsDBNull(16) ? reader.GetString(16) : "";
        }

        #endregion


    }
}
