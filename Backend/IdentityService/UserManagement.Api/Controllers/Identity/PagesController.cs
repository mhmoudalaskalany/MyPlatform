using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Base;
using Domain.DTO.Identity.Page;
using Domain.DTO.Identity.Page.Parameters;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Identity.Page;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Identity
{
    /// <summary>
    /// Pages Controller
    /// </summary>
    public class PagesController : BaseController
    {
        private readonly IPageService _pageService;
        /// <summary>
        /// Constructor
        /// </summary>
        public PagesController(IPageService pageService)
        {
            _pageService = pageService;
        }
        /// <summary>
        /// Get Pages COunt
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetPagesCount")]
        public async Task<IResult> GetPagesCountAsync()
        {
            var result = await _pageService.GetPagesCountAsync();
            return result;
        }
        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id}")]
        public async Task<IResult> GetAsync(long id)
        {
            var result = await _pageService.GetByIdAsync(id);
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
            return await _pageService.GetByIdForEditAsync(id);
        }

        /// <summary>
        /// Get By App Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByAppId/{appId}")]
        public async Task<IResult> GetByAppIdAsync(long appId)
        {
            var result = await _pageService.GetByAppId(appId);
            return result;
        }

        /// <summary>
        /// Get All 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetAll")]
        //[Authorize(policy: Permission.Pages.View)]
        public async Task<IResult> GetAllAsync()
        {
            var result = await _pageService.GetAllAsync();
            return result;
        }

        /// <summary>
        /// GetAll Data paged
        /// </summary>
        /// <param name="filter">Filter responsible for search and sort</param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetPaged")]
        //[Authorize(policy: Permission.Pages.View)]
        public async Task<DataPaging> GetPaged([FromBody] BaseParam<PageFilter> filter)
        {
            return await _pageService.GetAllPagedAsync(filter);
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("Add")]
        public async Task<IResult> PostAsync([FromBody] AddPageDto dto)
        {
            var result = await _pageService.AddAsync(dto);
            return result;
        }


        /// <summary>
        /// Update  
        /// </summary>
        /// <param name="model">Object content</param>
        /// <returns></returns>
        [HttpPut]
        [Route("Update")]
        public async Task<IResult> Update(AddPageDto model)
        {
            return await _pageService.UpdateAsync(model);
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
            return await _pageService.DeleteAsync(id);
        }

        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete("DeleteSoft/{id}")]
        public async Task<IResult> DeleteSoftAsync(long id)
        {
            return await _pageService.DeleteSoftAsync(id);
        }
    }
}
