using System;
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Hr.Card;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.Card;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    /// Cards Controller
    /// </summary>
    public class CardsController : BaseController
    {
        private readonly ICardService _cardService;
        /// <summary>
        /// Constructor
        /// </summary>
        public CardsController(ICardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("Get/{id:long}")]
        public async Task<IFinalResult> GetAsync(long id)
        {
            var result = await _cardService.GetByIdAsync(id);
            return result;
        }

        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetByEmployeeId/{id:guid}")]
        public async Task<IFinalResult> GetByEmployeeIdAsync(Guid id)
        {
            var result = await _cardService.GetByEmployeeIdAsync(id);
            return result;
        }

        /// <summary>
        /// Add 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AddCard")]
        public async Task<IActionResult> AddCardAsync([FromBody] AddCardDto dto)
        {
            var result = await _cardService.AddCardAsync(dto);
            if (result == null)
            {
                return BadRequest();
            }
            var file = new FileContentResult(result , "application/pdf")
            {
                FileDownloadName = dto.Employee.NationalId
            };
            return file;
        }

        /// <summary>
        /// Soft Remove  by id
        /// </summary>
        /// <param name="id">PK</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeleteSoft/{id:long}")]
        public async Task<IFinalResult> DeleteSoftAsync(long id)
        {
            return await _cardService.DeleteSoftAsync(id);
        }

    }
}
