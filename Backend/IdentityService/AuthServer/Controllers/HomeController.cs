using System.Threading.Tasks;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AuthServer.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IIdentityServerInteractionService _interaction;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="interaction"></param>
        /// <param name="logger"></param>
        public HomeController(IIdentityServerInteractionService interaction, ILogger<HomeController> logger)
        {
            _interaction = interaction;
            _logger = logger;
        }
        /// <summary>
        /// Index
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Error Page
        /// </summary>
        /// <param name="errorId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Error(string errorId)
        {
            var error = await _interaction.GetErrorContextAsync(errorId);
            _logger.LogError("Identity Server Error:  " + JsonConvert.SerializeObject(error));
            return View(error);
        }
    }
}
