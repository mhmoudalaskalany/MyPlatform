
using System.Threading.Tasks;
using Domain.Core;
using Domain.DTO.Hr.Card;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.Card.Reports;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    /// Cards Report Controller
    /// </summary>
    public class CardReportsController : BaseController
    {
        private readonly ICardReportService _cardService;
        /// <summary>
        /// Constructor
        /// </summary>
        public CardReportsController(ICardReportService cardService)
        {
            _cardService = cardService;
        }

        /// <summary>
        /// Get General Report
        /// /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("GetGeneralReport")]
        public async Task<IResult> GetGeneralReportAsync([FromBody] GeneralReportFilter filter)
        {
            var result = await _cardService.GetGeneralReportAsync(filter);
            return result;
        }

    }
}
