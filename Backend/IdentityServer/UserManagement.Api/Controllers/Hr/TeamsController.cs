using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Team;
using Domain.DTO.Hr.Team.Parameters;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.Team;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    /// Teams Controller
    /// </summary>
    public class TeamsController : BaseController
    {
        private readonly ITeamService _teamService;
        /// <summary>
        /// Constructor
        /// </summary>
        public TeamsController(ITeamService teamService)
        {
            _teamService = teamService;
        }


        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IFinalResult> GetAsync(long id)
        {
            var result = await _teamService.GetByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get By Id For Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEdit/{id}")]
        public async Task<IFinalResult> GetEdit(long id)
        {
            return await _teamService.GetByIdForEditAsync(id);
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IFinalResult> GetAllAsync()
        {
            var result = await _teamService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetTeamsByUnitId/{unitId}")]
        public async Task<IFinalResult> GetTeamsByUnitIdAsync(string unitId)
        {
            var result = await _teamService.GetTeamsByUnitIdAsync(unitId);
            return result;
        }

        /// <summary>
        /// Get Team Users 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEmployeesByTeamId/{teamId}")]
        public async Task<IFinalResult> GetEmployeesByTeamIdAsync(long teamId)
        {
            var result = await _teamService.GetEmployeesByTeamIdAsync(teamId);
            return result;
        }

        /// <summary>
        /// GetAll Data paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPaged")]
        //[Authorize(policy: Permission.Permissions.View)]
        public async Task<DataPaging> GetPaged([FromBody] BaseParam<TeamFilter> filter)
        {
            return await _teamService.GetAllPagedAsync(filter);
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IFinalResult> PostAsync([FromBody] AddTeamDto dto)
        {
            var result = await _teamService.AddAsync(dto);
            return result;
        }


        /// <summary>
        /// Delete Employee Team 
        /// </summary>
        /// <param name="employeeId"></param>
        /// <param name="teamId"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("DeleteEmployeeTeam/{employeeId}/{teamId}")]
        public async Task<IFinalResult> DeleteEmployeeTeamAsync( Guid employeeId , long teamId)
        {
            var result = await _teamService.DeleteEmployeeTeamAsync(employeeId , teamId);
            return result;
        }

        /// <summary>
        /// Delete Employee Team 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddEmployeeTeam")]
        public async Task<IFinalResult> AddEmployeeTeamAsync([FromBody] TeamEmployeeDto dto)
        {
            var result = await _teamService.AddEmployeeTeamAsync(dto);
            return result;
        }


        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IFinalResult> Update(AddTeamDto model)
        {

            return await _teamService.UpdateAsync(model);
        }




        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("Delete/{id}")]
        public async Task<IFinalResult> Remove(long id)
        {
            return await _teamService.DeleteAsync(id);
        }

        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IFinalResult> DeleteSoftAsync(long id)
        {
            return await _teamService.DeleteSoftAsync(id);
        }

    }
}
