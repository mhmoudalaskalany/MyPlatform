using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.Services.Hr.General;
using UserManagement.Api.Controllers.Base;

namespace UserManagement.Api.Controllers.Hr
{
    /// <summary>
    /// General Controller
    /// </summary>
    public class GeneralController : BaseController
    {
        private readonly IGeneralService _generalService;
        /// <summary>
        /// Constructor
        /// </summary>
        public GeneralController(IGeneralService generalService)
        {
            _generalService = generalService;
        }
        /// <summary>
        /// Send SMS From Excel
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [AllowAnonymous]
        [Route("SendSmsFromExcel")]
        public async Task<IActionResult> SendSmsFromExcelAsync()
        {
            await _generalService.SendSmsFromExcel();
            return Ok();
        }

      

    }
}
