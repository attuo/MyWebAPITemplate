using Microsoft.AspNetCore.Mvc;

namespace MyWebAPITemplate.Source.Web.Controllers
{
    /// <summary>
    /// Contains configs that each controller shared
    /// This base controller is inherited by every controller
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class BaseApiController : ControllerBase
    { }
}
