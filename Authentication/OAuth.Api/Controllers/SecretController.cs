using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace OAuth.Api.Controllers
{
    [Route("Secret")]
    [ApiController]
    public class SecretController : ControllerBase
    {
        [Route("Index")]
        [Authorize]
        public IActionResult Index()
        {
            return Ok("You are authenticated at secret index action");
        }
    }
}
