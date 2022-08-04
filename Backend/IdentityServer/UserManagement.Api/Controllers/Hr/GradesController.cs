using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Hr.Grade;
using Domain.DTO.Hr.Grade.Parameters;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.Grade;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    /// Grades Controller
    /// </summary>
    public class GradesController : BaseController
    {
        private readonly IGradeService _gradeService;
        /// <summary>
        /// Constructor
        /// </summary>
        public GradesController(IGradeService gradeService)
        {
            _gradeService = gradeService;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IResult> GetAsync(long id)
        {
            var result = await _gradeService.GetByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get By Id For Edit
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("GetEdit/{id}")]
        public async Task<IResult> GetEdit(long id)
        {
            return await _gradeService.GetByIdForEditAsync(id);
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IResult> GetAllAsync()
        {
            var result = await _gradeService.GetAllAsync();
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
        public async Task<DataPaging> GetPaged([FromBody] BaseParam<GradeFilter> filter)
        {
            return await _gradeService.GetAllPagedAsync(filter);
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IResult> PostAsync([FromBody] AddGradeDto dto)
        {
            var result = await _gradeService.AddAsync(dto);
            return result;
        }


        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IResult> Update(AddGradeDto model)
        {

            return await _gradeService.UpdateAsync(model);
        }
        /// <summary>
        /// Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete()]
        [Route("Delete/{id}")]
        public async Task<IResult> Remove(long id)
        {
            return await _gradeService.DeleteAsync(id);
        }

        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IResult> DeleteSoftAsync(long id)
        {
            return await _gradeService.DeleteSoftAsync(id);
        }


    }
}
