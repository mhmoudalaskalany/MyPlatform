using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuthServer.Controllers.Base
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
