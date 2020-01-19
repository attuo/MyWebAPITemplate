using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")] // Sets endpoints to produce json on default 
    [ApiConventionType(typeof(DefaultApiConventions))] // Sets default conventions for every controller endpoint
    public class BaseApiController : Controller
    { }
}
