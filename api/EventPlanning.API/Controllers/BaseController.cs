using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EventPlanning.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
    }
}
