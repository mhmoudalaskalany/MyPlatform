using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Data.Context.Murasalat;
using Data.Context.Oracle;
using Domain.Abstraction.Repository.Attendance;
using Domain.DTO.Hr.Attendance;
using Entities.Entities.Murasalat.Hr;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace Data.Repository.Attendance
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private readonly OracleDbContext _dbContext;
        private readonly MurasalatDbContext _murasalatDbContext;
        private readonly IConfiguration _configuration;
        public AttendanceRepository(IConfiguration configuration, OracleDbContext dbContext, MurasalatDbContext murasalatDbContext)
        {
            _configuration = configuration;
            _dbContext = dbContext;
            _murasalatDbContext = murasalatDbContext;
            //OracleContext.OracleContext.ConfigureConnection();
        }

        #region Public Methods

        public async Task<List<LeaveInfoDto>> GetAttendanceAsync(string employeeNumber)
        {
            try
            {
                var conString = _configuration["ConnectionStrings:NewOracleConnection"];
                await using var connection = new OracleConnection(conString);
                var leaves = new List<LeaveInfoDto>();
                await using (var command = connection.CreateCommand())
                {
                    var query = $"select * from MAWRED_DM_EMP_LEAVES where EMPLOYEE_NUMBER = '{employeeNumber}'";
                    connection.Open();
                    command.BindByName = true;
                    command.CommandText = query;
                    OracleParameter id = new OracleParameter("employeeNumber", employeeNumber);
                    command.Parameters.Add(id);
                    OracleGlobalization info = connection.GetSessionInfo();
                    info.TimeZone = "CET";
                    connection.SetSessionInfo(info);
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            await using (reader)
                            {
                                var leave = new LeaveInfoDto();
                                MapLeaveObject(leave, reader);
                                leaves.Add(leave);
                            }

                        }
                        reader.Dispose();
                    }
                    else
                    {
                        leaves = null;
                    }

                }
                connection.Close();
                return leaves;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public async Task<List<LeaveInfoDto>> GetMawredAttendanceAsync()
        {
            try
            {
                var count = await GetCount();
                var batches = count / 1000f;
                var conString = _configuration["ConnectionStrings:NewOracleConnection"];
                await using var connection = new OracleConnection(conString);
                var leaves = new List<LeaveInfoDto>();
                await using (var command = connection.CreateCommand())
                {
                    var query = $"select * from MAWRED_DM_EMP_LEAVES";
                    connection.Open();
                    command.BindByName = true;
                    command.CommandText = query;
                    OracleGlobalization info = connection.GetSessionInfo();
                    info.TimeZone = "CET";
                    connection.SetSessionInfo(info);
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var leave = new LeaveInfoDto();
                            reader.Read();
                            MapLeaveObject(leave, reader);
                            leaves.Add(leave);
                        }
                        reader.Dispose();
                    }
                    else
                    {
                        leaves = null;
                    }

                }
                connection.Close();
                return leaves;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        public async Task<bool> InsertMawredAttendanceAsync()
        {

            var latestId = await GetLatestIdFromMurasalat();
            //var count = await GetCount();
            //var batches = count / 1000f;
            //if (batches <= 1 && batches > 0)
            //{
            //    await AddLeaveBatch(latestId);
            //}
            //else
            //{
            //    for (int i = 0; i < batches; i++)
            //    {
            //        latestId = await GetLatestIdFromMurasalat();
            //        await AddLeaveBatch(latestId);
            //    }
            //}

            await AddLeaveBatch(latestId);


            return true;


        }



        #endregion


        #region Private Methods

        private async Task AddLeaveBatch(long id)
        {
            try
            {
                var conString = _configuration["ConnectionStrings:NewOracleConnection"];
                await using var connection = new OracleConnection(conString);
                var leaves = new List<LeaveInfoDto>();
                await using (var command = connection.CreateCommand())
                {
                    var query = $"select * from mawred_dm_emp_leaves where  id > {id}";

                    connection.Open();
                    command.BindByName = true;
                    command.CommandText = query;
                    OracleGlobalization info = connection.GetSessionInfo();
                    info.TimeZone = "CET";
                    connection.SetSessionInfo(info);
                    OracleDataReader reader = command.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var leave = new LeaveInfoDto();
                            MapLeaveObject(leave, reader);
                            leaves.Add(leave);
                        }
                        reader.Dispose();

                    }
                    else
                    {
                        leaves = new List<LeaveInfoDto>();
                    }


                }
                connection.Close();
                await AddToMurasalat(leaves);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private async Task<long> GetCount()
        {
            var latestId = await GetLatestIdFromMurasalat();
            var conString = _configuration["ConnectionStrings:NewOracleConnection"];
            await using var connection = new OracleConnection(conString);
            var query = $"select count(*) from mawred_dm_emp_leaves where ID > {latestId}";
            await using var countCommand = connection.CreateCommand();
            connection.Open();
            countCommand.BindByName = true;
            countCommand.CommandText = query;
            OracleGlobalization countInfo = connection.GetSessionInfo();
            countInfo.TimeZone = "CET";
            connection.SetSessionInfo(countInfo);
            var countReader = await countCommand.ExecuteScalarAsync();
            connection.Close();
            return Convert.ToInt32(countReader);
        }

        private void MapLeaveObject(LeaveInfoDto leave, OracleDataReader reader)
        {
            leave.EmployeeNumber = !reader.IsDBNull(0) ? reader.GetString(0) : "";
            leave.LeaveTypeId = !reader.IsDBNull(1) ? reader.GetInt32(1) : 0;
            leave.LeaveNameAr = !reader.IsDBNull(2) ? reader.GetString(2) : "";
            leave.LeaveNameEn = !reader.IsDBNull(3) ? reader.GetString(3) : "";
            leave.StartDate = !reader.IsDBNull(4) ? reader.GetDateTime(4) : DateTime.Now;
            leave.EndDate = !reader.IsDBNull(5) ? reader.GetDateTime(5) : DateTime.Now;
            leave.LeavesCount = !reader.IsDBNull(6) ? reader.GetInt32(6) : 0;
            leave.Status = !reader.IsDBNull(7) ? reader.GetString(7) : "";
            leave.Id = !reader.IsDBNull(8) ? reader.GetInt64(8) : 0;

        }

        private async Task<long> GetLatestIdFromMurasalat()
        {
            var entity = await _murasalatDbContext.MawredLeaves.OrderByDescending(x => x.LeaveId).FirstOrDefaultAsync();
            return entity?.LeaveId ?? 0;
        }


        private async Task AddToMurasalat(List<LeaveInfoDto> leaves)
        {
            var entities = new List<MawredLeaves>();
            foreach (var leave in leaves)
            {
                var entity = new MawredLeaves
                {
                    Creationdate = DateTime.Now,
                    Id = Guid.NewGuid(),
                    Filenumber = leave.EmployeeNumber,
                    LeaveId = leave.Id,
                    Startdate = leave.StartDate,
                    Endate = leave.EndDate,
                    Leavetype = leave.LeaveNameAr,
                    NoOfDays = leave.LeavesCount,
                    Status = leave.Status,
                    LeaveTypeId = leave.LeaveTypeId
                };
                entities.Add(entity);
            }

            await _murasalatDbContext.MawredLeaves.AddRangeAsync(entities);
            var rows = await _murasalatDbContext.SaveChangesAsync();
        }

        #endregion


    }
}
