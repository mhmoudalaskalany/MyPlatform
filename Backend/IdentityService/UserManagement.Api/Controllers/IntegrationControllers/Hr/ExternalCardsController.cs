using System;
using System.Threading.Tasks;
using Domain.Core;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.Card;
using Service.Services.Hr.Card.Integration;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.IntegrationControllers.Hr
{
    /// <summary>
    /// Cards Controller
    /// </summary>
    public class ExternalCardsController : BaseController
    {
        private readonly IExternalCardService _cardService;
        /// <summary>
        /// Constructor
        /// </summary>
        public ExternalCardsController(IExternalCardService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Get By Id 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("GetCardDetailsByEmployeeId/{employeeId}")]
        public async Task<IResult> GetCardDetailsByEmployeeIdAsync(Guid employeeId)
        {
            var result = await _cardService.GetCardDetailsByEmployeeIdAsync(employeeId);
            return result;
        }

       


    }
}
