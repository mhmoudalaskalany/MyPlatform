using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Base
{
    /// <summary>
    /// Base Controller
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public BaseController()
        {

        }
    }
}
