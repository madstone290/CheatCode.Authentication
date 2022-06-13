using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer4A.Server.Controllers.Account
{
    [AllowAnonymous]
    public class MyGoogleLoginController : ControllerBase
    {

        [HttpGet]
        public IActionResult Login([FromQuery] string returnUrl)
        {
            return Ok();
        }
    }
}
