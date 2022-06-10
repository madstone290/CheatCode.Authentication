using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CheatCode.Authentication.IdServer4.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestController : Controller
    {
        [Authorize]
        [HttpGet]
        public IActionResult Index()
        {
            var claims = User.Claims.Select(x => $"{x.Type}:{x.Value}");
            return Ok(new
            {
                Message = "Hello Mvc core api",
                Claims = claims.ToArray()
            });
        }

        [Authorize(Policy = "read")]
        [HttpGet("read")]
        public IActionResult Read()
        {
            var claims = User.Claims.Select(x => $"{x.Type}:{x.Value}");
            return Ok(new
            {
                Message = "You have read scope",
                Claims = claims.ToArray()
            });
        }

        [Authorize(Policy = "write")]
        [HttpGet("write")]
        public IActionResult Write()
        {
            var claims = User.Claims.Select(x => $"{x.Type}:{x.Value}");
            return Ok(new
            {
                Message = "You have write scope",
                Claims = claims.ToArray()
            });
        }



        [Route("No")]
        [HttpGet]
        public IActionResult NoAuth()
        {
            var claims = User.Claims.Select(x => $"{x.Type}:{x.Value}");
            return Ok(new
            {
                Message = "No Auth",
                Claims = claims.ToArray()
            });
        }
    }
}
