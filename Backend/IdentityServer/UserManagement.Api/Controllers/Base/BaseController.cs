using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Api.Controllers.Base
{
    /// <summary>
    /// Base Controller
    /// </summary>
    [ApiController]
    [Authorize(AuthenticationSchemes =
        JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
        
    }
}
